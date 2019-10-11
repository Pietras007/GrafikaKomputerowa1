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

        public bool ContainsPoint(Vertice point)
        {
            int A = Start.y - End.y;
            int B = End.x - Start.x;
            int C = Start.y * (Start.x - End.x) + Start.x * (End.y - Start.y);
            double distance = (double)Math.Abs(A * point.x + B * point.y + C) / Math.Sqrt(A*A + B*B);
            if(distance < CONST.pointHalf)
            {
                return true;
            }

            return false;
        }
    }
}
