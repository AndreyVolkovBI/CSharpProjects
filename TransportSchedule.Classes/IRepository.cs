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
        List<User> Users { get; set; } 

        List<FavouriteStation> StationsInListBox { get; set; }

        int Id { get; set; }
        int IndexId { get; set; }

        List<ScheduleItem> Result(string enter);
        void Restore(List<Route> routes, List<Station> stations);

        void FillInLists();
    }
}
