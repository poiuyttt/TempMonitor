using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TempMonitor.Config;
using TempMonitor.Data;
using TempMonitor.Models;
using TempMonitor.Presentation;
using TempMonitor.Service.Interfaces;

namespace TempMonitor
{
    public partial class MainForm : Form
    {
        private readonly IModbusService _modbus;
        private readonly IAlarmService _alarm;
        private readonly IExportService _export;
        private readonly IUserService _user;
        private readonly IOperationLogService _log;
        private readonly DatabaseService _db;
        private readonly AppConfig _config;

        private Timer _statusTimer;
        private DateTime _startTime;

        public MainForm(
            IModbusService modbus,
            IAlarmService alarm,
            IExportService export,
            IUserService user,
            IOperationLogService log,
            DatabaseService db,
            AppConfig config
        )
        {
            InitializeComponent();
            _modbus = modbus;
            _alarm = alarm;
            _export = export;
            _db = db;
            _user = user;
            _log = log;
            _config = config;

            _modbus.OnDataReceived += OnDataReceived;
            _modbus.OnStatusChanged += OnStatusChanged;
            _modbus.OnLogProduced += msg => BeginInvoke(new Action(() => AppendLog(msg)));
            _alarm.OnAlarmTriggered += record =>
                BeginInvoke(
                    new Action(() =>
                    {
                        string msg =
                            $"{record.AlarmType}！当前：{record.AlarmValue}，级别：{record.Level}";
                        AppendLog("报警：" + msg);
                        var result = MessageBox.Show(
                            msg + "\r\n\r\n点击「确定」确认此报警，点击「取消」稍后处理",
                            "报警",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Warning
                        );
                        if (result == DialogResult.OK)
                        {
                            _db.ConfirmAlarm(record.Id);
                            AppendLog($"报警已确认：ID={record.Id}");
                        }
                    })
                );

            cmbPortName.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            cmbPortName.Text = _config.PortName;
            cmbBaudRate.Text = "9600";

            _startTime = DateTime.Now;
            _statusTimer = new Timer();
            _statusTimer.Interval = 1000;
            _statusTimer.Tick += (s, e) =>
            {
                var elapsed = DateTime.Now - _startTime;
                lblTime.Text =
                    $"已运行 {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
            };
            _statusTimer.Start();

            InitChart();

            _db.CleanOldData(_config.DataRetentionDays);

            AppendLog("系统启动完成");
        }

        private void InitChart()
        {
            // 复用设计器已有的 ChartArea（避免重复添加）
            var area = chartTemp.ChartAreas[0];
            area.AxisY.Title = "温度 (℃)";
            area.AxisY2.Title = "湿度 (%)";
            area.AxisX.LabelStyle.Format = "HH:mm:ss";

            // 清除设计器生成的空默认 Series
            chartTemp.Series.Clear();

            var seriesTemp = new Series
            {
                Name = "温度",
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Red,
                YAxisType = AxisType.Primary,
            };
            var seriesHumid = new Series
            {
                Name = "湿度",
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = Color.Blue,
                YAxisType = AxisType.Secondary,
            };
            chartTemp.Series.Add(seriesTemp);
            chartTemp.Series.Add(seriesHumid);
        }

        private void OnDataReceived(SensorData data)
        {
            lblTempValue.Text = $"{data.Temperature:F1}";
            lblHumidValue.Text = $"{data.Humidity:F1}";
            UpdateChart(data);
            UpdateStatusColor(data);

            try
            {
                _db.InsertSensorData(data.Temperature, data.Humidity);
            }
            catch (Exception ex)
            {
                AppendLog($"数据库写入失败：{ex.Message}");
            }

            _alarm.Check(
                data.Temperature,
                data.Humidity,
                _config.TempAlarmHigh,
                _config.TempAlarmLow,
                _config.HumidAlarmHigh,
                _config.HumidAlarmLow
            );

            AppendLog($"温度：{data.Temperature}℃  湿度：{data.Humidity}%");
        }

        private void OnStatusChanged(string status)
        {
            BeginInvoke(
                new Action(() =>
                {
                    lblStatusValue.Text = status;
                    lblConnection.Text = status;
                    lblStatusValue.ForeColor = status == "已连接" ? Color.Green : Color.Red;
                })
            );
        }

        private void UpdateChart(SensorData data)
        {
            chartTemp.Series["温度"].Points.AddXY(data.RecordTime, data.Temperature);
            chartTemp.Series["湿度"].Points.AddXY(data.RecordTime, data.Humidity);

            while (chartTemp.Series["温度"].Points.Count > 120)
            {
                chartTemp.Series["温度"].Points.RemoveAt(0);
                chartTemp.Series["湿度"].Points.RemoveAt(0);
            }
        }

        private void UpdateStatusColor(SensorData data)
        {
            bool tempOk =
                data.Temperature <= _config.TempAlarmHigh
                && data.Temperature >= _config.TempAlarmLow;
            bool humidOk =
                data.Humidity <= _config.HumidAlarmHigh && data.Humidity >= _config.HumidAlarmLow;

            panelTemp.BackColor = tempOk ? Color.White : Color.LightPink;
            panelHumid.BackColor = humidOk ? Color.White : Color.LightPink;
        }

        private void btnStart_Click(object sender, System.EventArgs e)
        {
            try
            {
                _modbus.Start(cmbPortName.Text, int.Parse(cmbBaudRate.Text), _config.Interval);
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动失败：{ex.Message}");
            }
        }

        private void btnStop_Click(object sender, System.EventArgs e)
        {
            _modbus.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnExport_Click(object sender, System.EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel文件(*.xlsx)|*.xlsx";
                dlg.FileName = $"温湿度_{DateTime.Now:yyyyMMdd}.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var data = _db.QuerySensorData(DateTime.Now.AddDays(-1), DateTime.Now);
                        _export.ExportToExcel(dlg.FileName, data);
                        _log.Log(_user.CurrentUser?.Username ?? "", "导出Excel");
                        AppendLog($"已导出 {data.Count} 条数据");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"导出失败：{ex.Message}",
                            "错误",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            using (var form = new HistoryForm(_db, _export))
                form.ShowDialog();
        }

        private void btnSettings_Click(object sender, System.EventArgs e)
        {
            using (var form = new SettingsForm(_config, _log, _user.CurrentUser?.Username, _db))
                form.ShowDialog();
        }

        private void AppendLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                _modbus.Stop();
            }
            catch
            { /* 关闭时忽略清理异常 */
            }
            base.OnFormClosing(e);
        }
    }
}
