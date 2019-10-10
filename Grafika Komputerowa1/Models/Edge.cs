using Grafika_Komputerowa1.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Models
{
    public class Edge
    {
        public Relation relation { get; set; }
        public Vertice Start { get; set; }
        public Vertice End { get; set; }
        public Edge(Vertice x, Vertice y)
        {
            Start = x;
            End = y;
        }
    }
}
