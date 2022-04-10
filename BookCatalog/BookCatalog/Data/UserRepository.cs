using BookCatalog.Models;
using SQLite;
using System;
using System.Threading.Tasks;

namespace BookCatalog.Data
{
    public class UserRepository
    {
        SQLiteConnection db;
        public UserRepository(string dbPath)
        {
            db = new SQLiteConnection(dbPath);
            db.CreateTable<User>();
        }
        /// <summary>
        /// Add new user in DB
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> AddNewUserAsync(string login,string password)
        {
            int result = 0;
            
            if (string.IsNullOrWhiteSpace(login) == true || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Valid password required");
            }
            if (db.Table<User>().FirstOrDefault(u => u.Login == login) == null)
            {
                result = await Task.Run(() => db.Insert(new User() { Login = login, Password = password }));
            }
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public bool LoginUser(string login, string password)
        {
            if (db.Table<User>().FirstOrDefault(u=> u.Login == login && u.Password == password) != null)
            {
                return true;
            }
            return false;
        }
    }
}
