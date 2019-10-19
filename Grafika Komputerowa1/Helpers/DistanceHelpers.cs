using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Helpers
{
    public static class DistanceHelpers
    {
        public static double DistanceBetween(Vertice a, Vertice b)
        {
            return Math.Sqrt(Math.Pow(b.x - a.x, 2) + Math.Pow(b.y - a.y, 2));
        }

        public static double GetEdgeLength(Edge edge)
        {
            return Math.Sqrt(Math.Pow(edge.Start.x - edge.End.x, 2) + Math.Pow(edge.Start.y - edge.End.y, 2));
        }
    }
}
