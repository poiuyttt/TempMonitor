using System;

namespace TempMonitor.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
