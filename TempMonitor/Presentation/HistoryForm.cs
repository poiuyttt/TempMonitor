using System;
using System.Windows.Forms;
using TempMonitor.Data;
using TempMonitor.Service.Interfaces;

namespace TempMonitor.Presentation
{
    public partial class HistoryForm : Form
    {
        private readonly DatabaseService _db;
        private readonly IExportService _export;

        public HistoryForm(DatabaseService db, IExportService export)
        {
            InitializeComponent();
            _db = db;
            _export = export;
            dtpStart.Value = DateTime.Now.AddDays(-1);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            var data = _db.QuerySensorData(dtpStart.Value.Date, dtpEnd.Value.Date.AddDays(1));
            dgvHistory.DataSource = data;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel文件(*.xlsx)|*.xlsx";
                dlg.FileName = $"历史数据_{DateTime.Now:yyyyMMdd}.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var data = _db.QuerySensorData(
                        dtpStart.Value.Date,
                        dtpEnd.Value.Date.AddDays(1)
                    );
                    _export.ExportToExcel(dlg.FileName, data);
                    MessageBox.Show($"已导出 {data.Count} 条");
                }
            }
        }
    }
}
