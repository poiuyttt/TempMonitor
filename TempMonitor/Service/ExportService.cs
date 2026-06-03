using System.Collections.Generic;
using System.IO;
using System.Text;
using TempMonitor.Models;
using TempMonitor.Service.Interfaces;

namespace TempMonitor.Service
{
    public class ExportService : IExportService
    {
        public void ExportToCsv(string filePath, List<SensorData> data)
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("时间,温度(℃),湿度(%)");
                foreach (var item in data)
                    writer.WriteLine(
                        $"{item.RecordTime:yyyy-MM-dd HH:mm:ss},{item.Temperature},{item.Humidity}"
                    );
            }
        }

        public void ExportToExcel(string filePath, List<SensorData> data)
        {
            var workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
            var sheet = workbook.CreateSheet("温湿度数据");

            var header = sheet.CreateRow(0);
            header.CreateCell(0).SetCellValue("时间");
            header.CreateCell(1).SetCellValue("温度(℃)");
            header.CreateCell(2).SetCellValue("湿度(%)");

            for (int i = 0; i < data.Count; i++)
            {
                var row = sheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(data[i].RecordTime.ToString("yyyy-MM-dd HH:mm:ss"));
                row.CreateCell(1).SetCellValue(data[i].Temperature);
                row.CreateCell(2).SetCellValue(data[i].Humidity);
            }

            for (int i = 0; i < 3; i++)
            {
                sheet.AutoSizeColumn(i);
            }

            using (var fs = new FileStream(filePath, FileMode.Create))
                workbook.Write(fs);

            workbook.Close();
            workbook = null;
        }
    }
}
