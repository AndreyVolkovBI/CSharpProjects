using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes.Calculation
{
    public class Main
    {
        public List<ScheduleItem> Result(string name, List<Route> routes, List<Station> stations)
        {
            DateTime currentDt = DateTime.Now;

            var station = stations.FirstOrDefault(st => st.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            List<ScheduleItem> result = new List<ScheduleItem>();

            foreach (var route in routes)
            {
                var routeStation = route.Stations
                    .FirstOrDefault(st => st.Station.Id == station.Id);

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
