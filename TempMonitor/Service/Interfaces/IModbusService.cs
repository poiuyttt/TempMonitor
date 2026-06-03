using System;
using TempMonitor.Models;

namespace TempMonitor.Service.Interfaces
{
    public interface IModbusService
    {
        //采集到新数据时触发
        event Action<SensorData> OnDataReceived;

        // 连接状态变化时触发
        event Action<string> OnStatusChanged;

        event Action<string> OnLogProduced;

        bool IsConnected { get; }

        void Start(string portName, int baudRate, int interval);

        void Stop();
    }
}
