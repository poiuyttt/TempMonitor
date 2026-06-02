using System;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using TempMonitor.Models;
using TempMonitor.Service.Interfaces;
using Timer = System.Windows.Forms.Timer;

namespace TempMonitor.Service
{
    public class ModbusService : IModbusService, IDisposable
    {
        public event Action<SensorData> OnDataReceived;
        public event Action<string> OnStatusChanged;
        public event Action<string> OnLogProduced;

        private SerialPort _serialPort;
        private CancellationTokenSource _cts;
        private Task _connectTask;
        private ConcurrentQueue<SensorData> _dataQueue = new ConcurrentQueue<SensorData>();
        private Timer _consumeTimer;

        public bool IsConnceted => _serialPort != null && _serialPort.IsOpen;

        public void Start(string portName, int baudRate)
        {
            try
            {
                _dataQueue = new ConcurrentQueue<SensorData>();
                _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                _serialPort.ReadTimeout = 2000;
                _serialPort.WriteTimeout = 2000;
                _serialPort.Open();

                FireStatus("已连接");
                FireLog($"串口已打开：{portName}，{baudRate}");

                // 启动后台采集循环
                _cts = new CancellationTokenSource();
                _connectTask = Task.Run(() => CollectLoop(_cts.Token));

                _consumeTimer = new Timer();
                _consumeTimer.Interval = 500;
                _consumeTimer.Tick += (s, e) =>
                {
                    if (_dataQueue.TryDequeue(out SensorData data))
                        OnDataReceived?.Invoke(data);
                };
                _consumeTimer.Start();
            }
            catch (Exception ex)
            {
                FireStatus("连接失败");
                FireLog($"打开串口失败：{ex.Message}");
                throw;
            }
        }

        public void Stop()
        {
            _cts?.Cancel();

            if (_consumeTimer != null)
            {
                _consumeTimer.Stop();
                _consumeTimer.Dispose();
                _consumeTimer = null;
            }

            var port = _serialPort;
            _serialPort = null;
            if (port != null)
            {
                Task.Run(() =>
                {
                    try
                    {
                        if (port.IsOpen)
                            port.Close();
                        port.Dispose();
                    }
                    catch { }
                });
            }

            FireStatus("已停止");
            FireLog("串口已关闭");
        }

        /// <summary>
        /// 每秒发一次 Modbus 03 命令，读取温度+湿度
        /// Modbus命令：01   03   03 00         00 02      C4 4F
        ///            └地址 └功能码 └起始地址0x0300 └读2个寄存器 └CRC
        /// </summary>
        private async Task CollectLoop(CancellationToken token)
        {
            await Task.Run(
                () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        if (_serialPort == null || !_serialPort.IsOpen)
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        try
                        {
                            // 清空接收缓冲区，避免上一帧残留数据干扰
                            _serialPort.DiscardInBuffer();

                            byte[] cmd = { 0x01, 0x03, 0x03, 0x00, 0x00, 0x02, 0xC4, 0x4F };
                            _serialPort.Write(cmd, 0, cmd.Length);
                            Thread.Sleep(100);

                            // 等待响应，最多 500ms
                            int waitTime = 0;
                            while (_serialPort.BytesToRead < 9 && waitTime < 500)
                            {
                                Thread.Sleep(50);
                                waitTime += 50;
                            }

                            if (_serialPort.BytesToRead >= 9)
                            {
                                byte[] resp = new byte[9];
                                _serialPort.Read(resp, 0, 9);

                                // 丢弃缓冲区中可能残留的额外字节
                                if (_serialPort.BytesToRead > 0)
                                    _serialPort.DiscardInBuffer();

                                // 验证功能码（0x83 = 异常响应）
                                if (resp[1] != 0x03)
                                {
                                    FireLog($"Modbus 异常响应：功能码=0x{resp[1]:X2}");
                                    Thread.Sleep(1000);
                                    continue;
                                }

                                // CRC 校验
                                ushort calculatedCrc = CalculateCrc(resp, 0, 7);
                                ushort receivedCrc = (ushort)((resp[8] << 8) | resp[7]);

                                if (calculatedCrc != receivedCrc)
                                {
                                    FireLog("CRC 校验失败，数据已丢弃");
                                    Thread.Sleep(1000);
                                    continue;
                                }

                                // 解析温度（第3~4字节，大端，÷10）
                                int tempRaw = (resp[3] << 8) + resp[4];
                                double temperature = Math.Round(tempRaw / 10.0, 1);

                                // 解析湿度（第5~6字节，大端，÷10）
                                int humidRaw = (resp[5] << 8) + resp[6];
                                double humidity = Math.Round(humidRaw / 10.0, 1);

                                var data = new SensorData
                                {
                                    Temperature = temperature,
                                    Humidity = humidity,
                                    RecordTime = DateTime.Now,
                                };

                                _dataQueue.Enqueue(data);
                            }
                        }
                        catch (Exception ex)
                        {
                            FireLog($"采集异常：{ex.Message}");
                        }

                        Thread.Sleep(1000);
                    }
                },
                token
            );
        }

        private ushort CalculateCrc(byte[] data, int offset, int length)
        {
            ushort crc = 0xFFFF;
            for (int i = offset; i < offset + length; i++)
            {
                crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }

        private void FireStatus(string s) => OnStatusChanged?.Invoke(s);

        private void FireLog(string s) => OnLogProduced?.Invoke(s);

        public void Dispose()
        {
            Stop();
            _cts?.Dispose();
        }
    }
}
