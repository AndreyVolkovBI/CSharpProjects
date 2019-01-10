using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes.RegisteredUser
{
    public class UserLogic : IUserLogic
    {
        IRepository _repo = new Repository();

        IJSON js = new JSON();

        public int IsUserExit(string email, string password)
        {
            foreach (var us in _repo.Users)
            {
                if (email == us.Email && GetHash(password) == us.Password)
                    return us.Id;
            }
            return 0;
        }

        public User IsUserExist(string email)
        {
            List<User> users = js.DownloadUsers();
            return users.FirstOrDefault(u => u.Email == email);
        }

        public string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(
            password));
            return Convert.ToBase64String(hash);
        }
        
        public User CreateNewUser(string fullName, string email, string password)
        {
            int count = js.CountUsers(); // how many users are there in the file
            count++;
            var user = new User
            {
                Id = count,
                FullName = fullName,
                Email = email,
                Password = GetHash(password)
            };
            js.WriteToFile(user, count); // Write new user
            return user;
        }
    }
}
