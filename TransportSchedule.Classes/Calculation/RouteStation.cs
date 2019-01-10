using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes
{
    public class RouteStation
    {
        [Required]
        public int Id { get; set; }
        public int StationId { get; set; }
        public Route Route { get; set; }
        [JsonIgnore]
        public Station Station { get; set; }
        public int TimeFromOrigin { get; set; }
        public int TimeFromDest { get; set; }
    }
}
