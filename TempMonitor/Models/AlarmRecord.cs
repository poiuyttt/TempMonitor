using System;

namespace TempMonitor.Models
{
    public class AlarmRecord
    {
        public int Id { get; set; }
        public string AlarmType { get; set; }
        public double AlarmValue { get; set; }
        public string Level { get; set; } // Alarm / Critical
        public DateTime RecordTime { get; set; }
        public bool Confirmed { get; set; } //是否已被操作员确认
    }
}
