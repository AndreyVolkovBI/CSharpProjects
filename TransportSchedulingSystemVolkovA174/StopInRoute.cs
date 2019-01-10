using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportSchedulingSystemVolkovA174
{
    class StopInRoute
    {
        public Route Routes { get; set; }
        public List<Stop> Stops { get; set; }
        public int[] IBS { get; set; }
    }
}
