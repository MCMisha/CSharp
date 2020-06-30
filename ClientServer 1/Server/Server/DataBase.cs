using System;
using System.IO;
using System.Data.SQLite;
namespace Server
{
    class DataBase
    {
        private readonly SQLiteConnection m_dbConn = new SQLiteConnection();
        private readonly SQLiteCommand m_sqlCmd = new SQLiteCommand();
        private readonly string dbPath = @"F:\Michael\Programming\C#\DB\databaseOfServer.db";
        public DataBase()
        {
            m_dbConn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
            m_dbConn.Open();
            m_sqlCmd.Connection = m_dbConn;
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
            }
            CreateTableOfUsers();
        }
        private void CreateTableOfUsers()
        {
            m_sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Users (id INTEGER PRIMARY KEY AUTOINCREMENT, user TEXT UNIQUE, password TEXT)";
            m_sqlCmd.ExecuteNonQuery();
        }
        public void InsertDataInUsers(string userName, string password)
        {
            m_sqlCmd.CommandText = "INSERT OR IGNORE INTO Users ('user', 'password') values ('" +
                userName + "' , '" +
                password + "')";
            if (m_sqlCmd.ExecuteNonQuery() != 0)
            {
                Console.WriteLine("Record was created.");
            }
        }
        public bool FindData(string userName)
        {
            string request = "SELECT * FROM Users WHERE user = '" + userName + "'";
            var cmd = new SQLiteCommand(request, m_dbConn);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr.GetString(1) == userName)
                {
                    //Console.WriteLine("Запись найдена.");
                    return true;
                }
            }
            //Console.WriteLine("Запись не найдена.");
            return false;
        }
        public bool FindData(string userName, string password)
        {
            string request = "SELECT * FROM Users WHERE user = '" + userName + "'";
            var cmd = new SQLiteCommand(request, m_dbConn);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr.GetString(1) == userName && rdr.GetString(2) == password)
                {
                    //Console.WriteLine("Запись найдена.");
                    return true;
                }
            }
            return false;
        }
    }
}