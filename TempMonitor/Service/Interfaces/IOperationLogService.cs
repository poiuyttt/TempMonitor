namespace TempMonitor.Service.Interfaces
{
    public interface IOperationLogService
    {
        void Log(string username, string action);
    }
}
