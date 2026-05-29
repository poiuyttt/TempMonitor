using TempMonitor.Data;
using TempMonitor.Models;
using TempMonitor.Service.Interfaces;

namespace TempMonitor.Service
{
    public class UserService : IUserService
    {
        public UserInfo CurrentUser { get; private set; }

        private readonly DatabaseService _db;

        public UserService(DatabaseService db)
        {
            _db = db;
        }

        public bool Login(string username, string password)
        {
            var user = _db.ValidateUser(username, password);
            if (user != null)
            {
                CurrentUser = user;

                return true;
            }
            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
