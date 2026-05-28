using System;

namespace TempMonitor.Models
{
    public class OperationLog
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
