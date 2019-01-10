using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes.RegisteredUser;

namespace TransportSchedule.Classes
{
    public class FileRepository : IRepository
    {
        public List<Route> Routes { get; set; }
        public List<Station> Stations { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public int Id { get; set; } // id of the current user
        
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

        public List<Route> GetRoutes()
        {
            return Routes;
        }

        public void FillRoutesAndStations()
        {
            try
            {
                Routes = Read(Routes, "routes");
                Stations = Read(Stations, "stations");
                Restore(Routes, Stations);
            }
            catch
            {
                throw new Exception("Error reading data");
            }
        }
        
        public void Restore(List<Route> routes, List<Station> stations) // restore data comparing ids in routes.IdStations and stations.id
        {
            foreach (var route in routes)
            {
                foreach (var st in route.Stations)
                {
                    st.Station = stations.FirstOrDefault(m => m.Id == st.StationId);
                }
            }
        }

        public void WriteToFile(User user, int id) // write user to JSON file
        {
            using (var writer = new StreamWriter($"../../../Users/{id}.json"))
            {
                writer.Write(JsonConvert.SerializeObject(user, Formatting.Indented));
            }
        }

        public List<Station> GetStations()
        {
            return Read(Stations, "stations");
        }

        public User ReadFile(int id) // read user from JSON file
        {
            try
            {
                using (var reader = new StreamReader($"../../../Users/{id}.json"))
                {
                    var user = JsonConvert.DeserializeObject<User>(reader.ReadToEnd());

                    foreach (var st in user.FavouriteStations)
                        st.Station = GetStations().FirstOrDefault(x => x.Id == st.Id);
                    return user;
                }
            }
            catch
            {
                throw new Exception("Error reading data");
            }
        }

        public List<User> GetUsers() // download all the users in Repository
        {
            if (CountUsers() != 0)
            {
                for (int i = 1; i <= CountUsers(); i++)
                    Users.Add(ReadFile(i));
            }
            return Users;
        }

        public User GetUser()
        {
            return Users.FirstOrDefault(x => x.Id == Id);
        }

        public int CountUsers() // the number of users in the file
        {
            string[] filePaths = Directory.GetFiles("../../../Users");
            return filePaths.Length;
        }

        public User IsUserExist(string email, string password)
        {
            var user = GetUsers().FirstOrDefault(x => x.Email == email && x.Password == GetHash(password));
            if (user != null)
                Id = user.Id; // remember user id
            return user;
        }

        public bool SingleEmail(string email) 
        {
            foreach (var us in GetUsers())
            {
                if (email == us.Email)
                    return false;
            }
            return true;
        }
        
        public string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public void CreateNewUser(string fullName, string email, string password)
        {
            int count = CountUsers();
            count++;
            var user = new User
            {
                Id = count,
                FullName = fullName,
                Email = email,
                Password = GetHash(password)
            };
            WriteToFile(user, count);
        }

        public void AddStation(User user, Station station, string description)
        {
            user.FavouriteStations.Add(new FavouriteStation { Id = station.Id, Station = Stations.FirstOrDefault(x => x.Id == station.Id), Description = description });
            WriteToFile(user, Id);
        }

        public void EditStation(User user, Station station, string description)
        {
            user.FavouriteStations.FirstOrDefault(k => k.Station.Id == station.Id).Description = description;
            WriteToFile(user, Id);
        }

        public void DeleteStation(FavouriteStation selectedStation)
        {
            User us = GetUsers().FirstOrDefault(x => x.Id == Id);
            us.FavouriteStations.Remove(selectedStation);
            WriteToFile(us, Id);
        }
    }
}
