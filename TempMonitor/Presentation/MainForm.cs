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
        private readonly DatabaseService _db;
        private readonly IUserService _user;
        private readonly IOperationLogService _log;
        private readonly AppConfig _config;

        private Timer _statusTimer;
        private DateTime _startTime;
        private int _dataCount;

        public MainForm(
            IModbusService modbus,
            IAlarmService alarm,
            IExportService export,
            DatabaseService db,
            IUserService user,
            IOperationLogService log,
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
            _modbus.OnLogProduced += msg => AppendLog(msg);
            _alarm.OnAlarmTriggered += msg =>
                BeginInvoke(
                    new Action(() =>
                    {
                        AppendLog("报警：" + msg);
                        MessageBox.Show(msg, "报警", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            var area = new ChartArea();
            area.AxisY.Title = "温度 (℃)";
            area.AxisY2.Title = "湿度 (%)";
            area.AxisX.LabelStyle.Format = "HH:mm:ss";
            chartTemp.ChartAreas.Add(area);

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
            BeginInvoke(
                new Action(() =>
                {
                    lblTempValue.Text = $"{data.Temperature:F1}";
                    lblHumidValue.Text = $"{data.Humidity:F1}";
                    UpdateChart(data);
                    UpdateStatusColor(data);

                    _db.InsertSensorData(data.Temperature, data.Humidity);
                    _dataCount++;

                    _alarm.Check(
                        data.Temperature,
                        data.Humidity,
                        _config.TempAlarmHigh,
                        _config.TempAlarmLow,
                        _config.HumidAlarmHigh,
                        _config.HumidAlarmLow
                    );

                    AppendLog($"温度：{data.Temperature}℃  湿度：{data.Humidity}%");
                })
            );
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
                _modbus.Start(cmbPortName.Text, int.Parse(cmbBaudRate.Text));
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
                    var data = _db.QuerySensorData(DateTime.Now.AddDays(-1), DateTime.Now);
                    _export.ExportToExcel(dlg.FileName, data);
                    _log.Log(_user.CurrentUser?.Username, "导出Excel");
                    AppendLog($"已导出 {data.Count} 条数据");
                }
            }
        }

        private void btnSettings_Click(object sender, System.EventArgs e)
        {
            using (var form = new SettingsForm(_config, _log, _user.CurrentUser?.Username))
                form.ShowDialog();
        }

        private void AppendLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _modbus.Stop();
            base.OnFormClosing(e);
        }
    }
}
