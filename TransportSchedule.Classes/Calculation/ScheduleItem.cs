using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes
{
    public class ScheduleItem
    {
        public string RouteName { get; set; }
        public Station Destination { get; set; }
        public int MinutesLeft { get; set; }
    }
}
