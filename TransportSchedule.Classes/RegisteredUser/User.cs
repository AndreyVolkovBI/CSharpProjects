using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes;
using TransportSchedule.Classes.RegisteredUser;

namespace TransportSchedule.Classes
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<FavouriteStation> FavouriteStations { get; set; } = new List<FavouriteStation>();
    }
}
