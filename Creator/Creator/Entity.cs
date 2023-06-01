using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creator
{
    public class Entity
    {

        public double X { get; set; }
        public double Y { get; set; }
        public string Name { get; set; }

        public Entity(string name, double x, double y)
        {
           X = x;
           Y = y;
           Name = name;
        }
    }
}
