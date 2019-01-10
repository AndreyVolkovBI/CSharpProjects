using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedule.Classes.RegisteredUser
{
    public class FavouriteStation
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Station Station { get; set; }
        public string Description { get; set; }
    }
}
