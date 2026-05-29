using System;

namespace TempMonitor.Service.Interfaces
{
    public interface IAlarmService
    {
        event Action<string> OnAlarmTriggered;

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
