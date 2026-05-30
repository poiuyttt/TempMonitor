using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using TempMonitor.Models;

namespace TempMonitor.Data
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            string dbDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(dbDir);
            _connectionString = $"Data Source={Path.Combine(dbDir, "TempMonitor.db")};Version=3;";
            InitializeDatabase();
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        private void InitializeDatabase()
        {
            string sql =
                @"
                CREATE TABLE IF NOT EXISTS SensorData (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Temperature REAL NOT NULL,
                    Humidity REAL NOT NULL,
                    RecordTime TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    Role TEXT NOT NULL DEFAULT 'Operator'
                );
                CREATE TABLE IF NOT EXISTS AlarmRecords (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    AlarmType TEXT NOT NULL,
                    AlarmValue REAL NOT NULL,
                    Level TEXT NOT NULL,
                    RecordTime TEXT NOT NULL,
                    Confirmed INTEGER NOT NULL DEFAULT 0
                );
                CREATE TABLE IF NOT EXISTS OperationLogs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL,
                    Action TEXT NOT NULL,
                    RecordTime TEXT NOT NULL
                )";
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                new SQLiteCommand(sql, conn).ExecuteNonQuery();

                // 首次使用创建默认管理员账号
                var cmd = new SQLiteCommand("SELECT COUNT(*) FROM Users", conn);
                if ((long)cmd.ExecuteScalar() == 0)
                {
                    new SQLiteCommand(
                        "INSERT INTO Users (Username, Password, Role) VALUES ('admin', 'admin', 'Admin')",
                        conn
                    ).ExecuteNonQuery();
                }
            }
        }

        /// <summary>插入一条温湿度记录</summary>
        public void InsertSensorData(double temp, double humid)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(
                    "INSERT INTO SensorData (Temperature, Humidity, RecordTime) VALUES (@temp, @humid, @time)",
                    conn
                );
                cmd.Parameters.AddWithValue("@temp", temp);
                cmd.Parameters.AddWithValue("@humid", humid);
                cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>按时间范围查询历史数据</summary>
        public List<SensorData> QuerySensorData(DateTime start, DateTime end)
        {
            var list = new List<SensorData>();
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(
                    "SELECT * FROM SensorData WHERE RecordTime BETWEEN @start AND @end",
                    conn
                );
                cmd.Parameters.AddWithValue("@start", start.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@end", end.ToString("yyyy-MM-dd HH:mm:ss"));

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(
                            new SensorData
                            {
                                Temperature = Convert.ToDouble(reader["Temperature"]),
                                Humidity = Convert.ToDouble(reader["Humidity"]),
                                RecordTime = DateTime.Parse(reader["RecordTime"].ToString()),
                            }
                        );
                    }
                }
            }
            return list;
        }

        /// <summary>清理指定天数前的旧数据</summary>
        public void CleanOldData(int retentionDays)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(
                    $"DELETE FROM SensorData WHERE RecordTime < @date",
                    conn
                );
                cmd.Parameters.AddWithValue(
                    "@date",
                    DateTime.Now.AddDays(-retentionDays).ToString("yyyy-MM-dd")
                );
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>验证用户名密码，成功返回用户信息</summary>
        public UserInfo ValidateUser(string username, string password)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(
                    "SELECT * FROM Users WHERE Username = @username AND Password = @password",
                    conn
                );
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new UserInfo
                        {
                            Username = reader["Username"].ToString(),
                            Role = reader["Role"].ToString(),
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>插入一条报警记录，返回新记录的 Id</summary>
        public int InsertAlarmRecord(AlarmRecord record)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(
                    "INSERT INTO AlarmRecords (AlarmType, AlarmValue, Level, RecordTime, Confirmed) VALUES (@type, @value, @level, @time, 0); SELECT last_insert_rowid();",
                    conn
                );
                cmd.Parameters.AddWithValue("@type", record.AlarmType);
                cmd.Parameters.AddWithValue("@value", record.AlarmValue);
                cmd.Parameters.AddWithValue("@level", record.Level);
                cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>确认报警记录</summary>
        public void ConfirmAlarm(int alarmId)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(
                    "UPDATE AlarmRecords SET Confirmed = 1 WHERE Id = @id",
                    conn
                );
                cmd.Parameters.AddWithValue("@id", alarmId);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>插入一条操作日志</summary>
        public void InsertOperationLog(string username, string action)
        {
            using (var conn = new SQLiteConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SQLiteCommand(
                    "INSERT INTO OperationLogs (Username, Action, RecordTime) VALUES (@username, @action, @time)",
                    conn
                );
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }
    }
}
