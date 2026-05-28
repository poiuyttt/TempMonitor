namespace TempMonitor.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Admin / Operator / Viewer
    }
}
