using System;
using TempMonitor.Models;

namespace TempMonitor.Service.Interfaces
{
    public interface IAlarmService
    {
        event Action<AlarmRecord> OnAlarmTriggered;

        void Check(
            double temperature,
            double humidity,
            double tempHigh,
            double tempLow,
            double humidHigh,
            double humidLow
        );
    }
}
