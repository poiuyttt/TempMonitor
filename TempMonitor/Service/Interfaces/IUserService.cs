using TempMonitor.Models;

namespace TempMonitor.Service.Interfaces
{
    public interface IUserService
    {
        UserInfo CurrentUser { get; }

        bool Login(string username, string password);

        void Logout();
    }
}
