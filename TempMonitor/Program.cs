using System;
using System.Windows.Forms;
using TempMonitor.Config;
using TempMonitor.Data;
using TempMonitor.Presentation;
using TempMonitor.Service;

namespace TempMonitor
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //全员异常捕获
            Application.ThreadException += (s, e) =>
            {
                string logPath = Application.StartupPath + "\\error.log";
                System.IO.File.AppendAllText(
                    logPath,
                    $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] 异常：{e.Exception}{Environment.NewLine}"
                );
                MessageBox.Show(
                    $"程序发生错误：{e.Exception.Message}\n\n错误已记录到：{logPath}",
                    "错误",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            };

            // 组装依赖
            var config = new AppConfig();
            var db = new DatabaseService();
            var modbus = new ModbusService();
            var alarm = new AlarmService(db);
            var export = new ExportService();
            var user = new UserService(db);
            var log = new OperationLogService(db);

            var loginForm = new LoginForm(user, log);
            Application.Run(loginForm);

            if (loginForm.LoginSuccess)
            {
                // 进入主界面（依赖注入）
                Application.Run(new MainForm(modbus, alarm, export, db, user, log, config));
            }
        }
    }
}
