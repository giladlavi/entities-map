using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creator
{
    public class Event
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Name { get; set; }

        public Event(string name, double x, double y)
        {
           X = x;
           Y = y;
           Name = name;
        }
    }
}
