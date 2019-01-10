using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportSchedule.Classes.RegisteredUser;

namespace TransportSchedule.Classes
{
    public class Repository : IRepository
    {
        public List<Route> Routes { get; set; }
        public List<Station> Stations { get; set; }
        public List<User> Users { get; set; } = new List<User>();

        public List<FavouriteStation> StationsInListBox { get; set; } = new List<FavouriteStation>();

        public int Id { get; set; } // unique id of the user
        public int IndexId { get; set; } // id's index in List<User> in case some users may delete their profiles

        public Repository()
        {
            Users = js.DownloadUsers();
            FillInLists();
        }

        JSON js = new JSON();
        
        public void FillInLists()
        {
            try
            {
                Routes = js.Read(Routes, "routes");
                Stations = js.Read(Stations, "stations");
                Restore(Routes, Stations);
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading data");
            }
        }

        public List<ScheduleItem> Result(string enter)
        {
            
            while (true)
            {
                DateTime currentDt = DateTime.Now;
                string name = enter;

                var station = Stations.FirstOrDefault(st => st.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

                List<ScheduleItem> result = new List<ScheduleItem>();
                if (station == null)
                {
                    return result;
                }

                else
                {

                    // Call to DateTime.Now only once to prevent different readings on different loop iterations
                    // Here manual time can also be set to test the algorithm

                    foreach (var route in Routes)
                    {

                        var routeStation = route.Stations
                            .FirstOrDefault(st => st.Station == station);

                        if (routeStation != null)
                        {

                            if (routeStation != route.Stations.Last())
                            {
                                int left = route.TimeToNextDepartureFromOrigin(routeStation, currentDt);
                                result.Add(new ScheduleItem
                                {
                                    RouteName = route.Name,
                                    Destination = route.Stations.Last().Station,
                                    MinutesLeft = left
                                });
                            }
                            if (routeStation != route.Stations.First())
                            {
                                int left = route.TimeToNextDepartureFromDest(routeStation, currentDt);
                                result.Add(new ScheduleItem
                                {
                                    RouteName = route.Name,
                                    Destination = route.Stations.First().Station,
                                    MinutesLeft = left
                                });
                            }
                        }
                    }
                    return result;
                }
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

    }
}
