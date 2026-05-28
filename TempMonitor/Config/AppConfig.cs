namespace TempMonitor.Config
{
    public class AppConfig
    {
        //串口参数
        public string PortName { get; set; } = "COM3";
        public int BaudRate { get; set; } = 9600;
        public int Interval { get; set; } = 1000;

        //报警阈值
        public double TempAlarmHigh { get; set; } = 30.0;
        public double TempAlarmLow { get; set; } = 5.0;
        public double HumidAlarmHigh { get; set; } = 90.0;
        public double HumidAlarmLow { get; set; } = 20.0;

        // 数据保留
        public int DataRetentionDays { get; set; } = 90;
    }
}
