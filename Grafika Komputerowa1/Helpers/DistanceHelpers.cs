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

        public static Vertice GetCloserVerticeFromLine((double, double) line, (Vertice, Vertice) vertices)
        {
            double dist1 = Line.DistanceVerticeFromLine(line, vertices.Item1);
            double dist2 = Line.DistanceVerticeFromLine(line, vertices.Item2);
            if(dist1 <= dist2)
            {
                return vertices.Item1;
            }
            else
            {
                return vertices.Item2;
            }
        }
        public static Vertice GetCloserVerticeFromVertice((Vertice, Vertice) vertices, Vertice vertice)
        {
            double dist1 = DistanceBetween(vertices.Item1, vertice);
            double dist2 = DistanceBetween(vertices.Item2, vertice);
            if(dist1 <= dist2)
            {
                return vertices.Item1;
            }
            else
            {
                return vertices.Item2;
            }
        }
    }

    
}
