using OuiHop.Models;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuiHop.Common
{
   public class Database
    {
        string path;
        SQLiteConnection conn;

        public Database()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, 
                   "OuiDatabas.sqlite");
            conn = new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<User>();
        }

        public int Register(User user)
        {
            return conn.Insert(new User()
            {
                email = user.email,
                password = user.password
            });
        }
        public bool Login(string user, string password)
        {
            var query = conn.Table<User>().
                Where(i => i.email == user && i.password == password);
            if (query.Count() >= 0)
                return true;
            else
                return false;
        }


    }
}
