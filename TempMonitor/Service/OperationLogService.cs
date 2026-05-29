using TempMonitor.Data;
using TempMonitor.Service.Interfaces;

namespace TempMonitor.Service
{
    internal class OperationLogService : IOperationLogService
    {
        private readonly DatabaseService _db;

        public OperationLogService(DatabaseService db)
        {
            _db = db;
        }

        public void Log(string username, string action)
        {
            _db.InsertOperationLog(username, action);
        }
    }
}
