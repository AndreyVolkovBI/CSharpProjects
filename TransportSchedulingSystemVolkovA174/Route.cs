using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedulingSystemVolkovA174
{
    class Route
    {
        public int Id { get; set; }
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public int Interval { get; set; }
        public List<int> arrayStops { get; set; }
    }
}
