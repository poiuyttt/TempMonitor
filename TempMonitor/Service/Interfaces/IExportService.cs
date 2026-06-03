using System.Collections.Generic;
using TempMonitor.Models;

namespace TempMonitor.Service.Interfaces
{
    public interface IExportService
    {
        void ExportToExcel(string filePath, List<SensorData> data);
    }
}
