using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes.RegisteredUser;

namespace TransportSchedule.Classes
{
    public class JSON : IJSON
    {
        public void WriteToFile(User user, int id) // write user to JSON file
        {
            using (var writer = new StreamWriter($"../../../Users/{id}.json"))
            {
                writer.Write(JsonConvert.SerializeObject(user, Formatting.Indented));
            }
        }

        public User ReadFile(int id) // read user from JSON file
        {
            try
            {
                using (var reader = new StreamReader($"../../../Users/{id}.json"))
                {
                   return JsonConvert.DeserializeObject<User>(reader.ReadToEnd());
                }
            }
            catch
            {
                throw new Exception("Error reading data");
            }
        }

        public List<User> DownloadUsers() // download all the users in Repository
        {
            List<User> Users = new List<User>();

            for (int i = 1; i <= CountUsers(); i++)
                Users.Add(ReadFile(i));

            return Users;
        }

        public int CountUsers() // the number of users in the file
        {
            string[] filePaths = Directory.GetFiles("../../../Users");
            return filePaths.Length;
        }

        public List<T> Read<T>(List<T> fileName, string line) // read stations/rouets data from JSON file
        {
            try
            {
                using (var reader = new StreamReader($"../../../{line}.json"))
                {
                    return JsonConvert.DeserializeObject<List<T>>(reader.ReadToEnd());
                }

            }
            catch
            {
                throw new Exception("Error reading data");
            }
        }
    }
}

