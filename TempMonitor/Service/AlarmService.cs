using System;
using System.Collections.Generic;
using TempMonitor.Data;
using TempMonitor.Models;
using TempMonitor.Service.Interfaces;

namespace TempMonitor.Service
{
    public class AlarmService : IAlarmService
    {
        public event Action<AlarmRecord> OnAlarmTriggered;

        private readonly DatabaseService _db;
        private readonly Dictionary<string, DateTime> _lastAlarmTime =
            new Dictionary<string, DateTime>();

        public AlarmService(DatabaseService db)
        {
            _db = db;
        }

        public void Check(
            double temperature,
            double humidity,
            double tempHigh,
            double tempLow,
            double humidHigh,
            double humidLow
        )
        {
            CheckOne("温度", temperature, tempHigh, tempLow);
            CheckOne("湿度", humidity, humidHigh, humidLow);
        }

        private void CheckOne(string type, double value, double high, double low)
        {
            if (value <= high && value >= low)
            {
                return;
            }

            // 防抖动：30 秒内不重复弹窗
            string key = type;
            if (
                _lastAlarmTime.TryGetValue(key, out DateTime last)
                && (DateTime.Now - last).TotalSeconds < 30
            )
                return;

            _lastAlarmTime[key] = DateTime.Now;

            string level = (value > high * 1.2 || value < low * 0.8) ? "Critical" : "Alarm";

            var record = new AlarmRecord
            {
                AlarmType = $"{type}超限",
                AlarmValue = value,
                Level = level,
                RecordTime = DateTime.Now,
            };

            record.Id = _db.InsertAlarmRecord(record);

            OnAlarmTriggered?.Invoke(record);
        }
    }
}
