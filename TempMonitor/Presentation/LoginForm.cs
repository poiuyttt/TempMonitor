using System;
using System.Windows.Forms;
using TempMonitor.Service.Interfaces;

namespace TempMonitor.Presentation
{
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;
        private readonly IOperationLogService _logService;
        public bool LoginSuccess { get; private set; }

        public LoginForm(IUserService userService, IOperationLogService logService)
        {
            InitializeComponent();
            _userService = userService;
            _logService = logService;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (
                string.IsNullOrWhiteSpace(txtUsername.Text)
                || string.IsNullOrWhiteSpace(txtPassword.Text)
            )
            {
                MessageBox.Show("请输入用户名和密码", "提示");
                return;
            }

            if (_userService.Login(txtUsername.Text.Trim(), txtPassword.Text.Trim()))
            {
                LoginSuccess = true;
                _logService.Log(_userService.CurrentUser.Username, "登陆系统");
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误", "登陆失败");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
