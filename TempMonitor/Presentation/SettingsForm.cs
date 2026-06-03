using System;
using System.Data.SQLite;
using System.IO.Ports;
using System.Windows.Forms;
using TempMonitor.Config;
using TempMonitor.Data;
using TempMonitor.Service.Interfaces;

namespace TempMonitor.Presentation
{
    public partial class SettingsForm : Form
    {
        private readonly AppConfig _config;
        private readonly IOperationLogService _logService;
        private readonly string _currentUser;
        private readonly DatabaseService _db;

        public SettingsForm(
            AppConfig config,
            IOperationLogService logService,
            string currentUser,
            DatabaseService db
        )
        {
            InitializeComponent();
            _config = config;
            _logService = logService;
            _currentUser = currentUser;
            _db = db;

            LoadSettings();
            LoadPortNames();
            LoadUsers();
        }

        private void LoadSettings()
        {
            numTempHigh.Value = (decimal)_config.TempAlarmHigh;
            numTempLow.Value = (decimal)_config.TempAlarmLow;
            numHumidHigh.Value = (decimal)_config.HumidAlarmHigh;
            numHumidLow.Value = (decimal)_config.HumidAlarmLow;
            numInterval.Value = _config.Interval;
            cmbPortName.Text = _config.PortName;
        }

        private void LoadPortNames()
        {
            cmbPortName.Items.Clear();
            foreach (var port in SerialPort.GetPortNames())
                cmbPortName.Items.Add(port);
            if (!string.IsNullOrEmpty(_config.PortName))
                cmbPortName.Text = _config.PortName;
        }

        private void LoadUsers()
        {
            listUsers.Items.Clear();
            try
            {
                var users = _db.GetAllUsers();
                foreach (var user in users)
                    listUsers.Items.Add($"{user.Username} ({user.Role})");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"加载用户列表失败：{ex.Message}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string username = Microsoft.VisualBasic.Interaction.InputBox(
                "输入用户名：",
                "添加用户"
            );
            if (string.IsNullOrWhiteSpace(username))
                return;

            string password = Microsoft.VisualBasic.Interaction.InputBox("输入密码：", "添加用户");
            if (string.IsNullOrWhiteSpace(password))
                return;

            try
            {
                _db.AddUser(username, password);
                _logService.Log(_currentUser, $"添加用户：{username}");
                LoadUsers();
                MessageBox.Show("用户已添加", "提示");
            }
            catch (SQLiteException ex) when ((int)ex.ResultCode == 19) // SQLITE_CONSTRAINT
            {
                MessageBox.Show("用户名已存在", "错误");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"添加用户失败：{ex.Message}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _config.TempAlarmHigh = (double)numTempHigh.Value;
            _config.TempAlarmLow = (double)numTempLow.Value;
            _config.HumidAlarmHigh = (double)numHumidHigh.Value;
            _config.HumidAlarmLow = (double)numHumidLow.Value;
            _config.Interval = (int)numInterval.Value;
            _config.PortName = cmbPortName.Text;

            // 保存配置到 appsettings.json
            try
            {
                string configPath = System.IO.Path.Combine(
                    System.AppDomain.CurrentDomain.BaseDirectory,
                    "appsettings.json"
                );
                string json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(
                    _config
                );
                System.IO.File.WriteAllText(configPath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"保存配置文件失败：{ex.Message}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            _logService.Log(_currentUser, "修改配置");
            MessageBox.Show("设置已保存", "提示");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
