using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes
{
    public class RouteStation
    {
        public int StationId { get; set; }
        [JsonIgnore]
        public Station Station { get; set; }
        public int TimeFromOrigin { get; set; }
        public int TimeFromDest { get; set; }
    }
}
