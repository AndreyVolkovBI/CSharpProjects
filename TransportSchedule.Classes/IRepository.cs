using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes.RegisteredUser;

namespace TransportSchedule.Classes
{
    public interface IRepository
    {
        List<Route> Routes { get; set; }
        List<Station> Stations { get; set; }

        int Id { get; set; }

        User GetUser();
        List<Route> GetRoutes();
        void FillRoutesAndStations();
        string GetHash(string password);
        void CreateNewUser(string fullName, string email, string password);
        bool SingleEmail(string email);
        User IsUserExist(string email, string password);

        void AddStation(User user, Station station, string description);
        void EditStation(User user, Station station, string description);
        void DeleteStation(FavouriteStation selectedStation);
    }
}
