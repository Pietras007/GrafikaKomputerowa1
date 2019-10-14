using Grafika_Komputerowa1.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Models
{
    public class Vertice
    {
        public int x { get; set; }
        public int y { get; set; }
        public Vertice(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
