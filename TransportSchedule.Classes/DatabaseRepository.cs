using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TransportSchedule.Classes.RegisteredUser;
using System.Security.Cryptography;

namespace TransportSchedule.Classes
{
    public class DatabaseRepository : IRepository
    {
        public List<Route> Routes { get; set; } = new List<Route>();
        public List<Station> Stations { get; set; } = new List<Station>();

        public int Id { get; set; } // id of the current user

        public User GetUser() // get all the users on 'LogIn Page'
        {
            using (var context = new Context())
            {
                return context.Users.Include("FavouriteStations.Station").SingleOrDefault(x => x.Id == Id);
            }
        }

        public List<Route> GetRoutes() // get all routes on 'Schedule Page'
        {
            using (var context = new Context())
            {
                return context.Routes.Include("Stations.Station").ToList();
            }
        }

        public void FillRoutesAndStations() // fill all the routes and stations on 'Schedule Page'
        {
            using (var context = new Context())
            {
                foreach (var route in GetRoutes())
                    Routes.Add(route);

                foreach (var station in context.Stations.ToList())
                    Stations.Add(station);
            }
        }

        public User IsUserExist(string email, string password)
        {
            using (var context = new Context())
            {
                string hash = GetHash(password);
                var user = context.Users.SingleOrDefault(x => x.Email == email && x.Password == hash);
                if (user != null)
                    Id = user.Id; // remember user id
                return user;
            }
        }

        public bool SingleEmail(string email)
        {
            using (var context = new Context())
            {
                foreach (var us in context.Users.ToList())
                {
                    if (email == us.Email)
                        return false;
                }
                return true;
            }
        }

        public string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public void CreateNewUser(string fullName, string email, string password)
        {
            var user = new User
            {
                FullName = fullName,
                Email = email,
                Password = GetHash(password)
            };

            using (var context = new Context())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void AddStation(User user, Station station, string description)
        {
            using (var context = new Context())
            {
                context.Users.FirstOrDefault(u => u.Id == user.Id).FavouriteStations.Add(new FavouriteStation
                { Station = context.Stations.FirstOrDefault(x => x.Id == station.Id), Description = description });
                context.SaveChanges();
            }
        }

        public void EditStation(User user, Station station, string description)
        {
            using (var context = new Context())
            {
                context.Users.Include("FavouriteStations.Station").FirstOrDefault(u => u.Email == user.Email).FavouriteStations.FirstOrDefault(k => k.Station.Id == station.Id).Description = description;
                context.SaveChanges();
            }
        }

        public void DeleteStation(FavouriteStation selectedStation)
        {
            using (var context = new Context())
            {
                context.FavouriteStations.Remove(context.FavouriteStations.FirstOrDefault(s => s.Id == selectedStation.Id));
                context.SaveChanges();
            }
        }
    }
}
