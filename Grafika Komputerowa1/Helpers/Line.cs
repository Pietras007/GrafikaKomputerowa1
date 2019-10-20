using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Helpers
{
    public static class Line
    {
        public static (double, double) GetStraightLine(Vertice pStart, Vertice pEnd)
        {
            int A = pStart.y - pEnd.y;
            int B = pEnd.x - pStart.x;
            int C = pStart.y * (pStart.x - pEnd.x) + pStart.x * (pEnd.y - pStart.y);

            double a = (double)A / B;
            double b = (double)C / B;
            return (a, b);
        }

        public static (double, double) GetPerpendicularThroughPoint((double, double) line, Vertice point)
        {
            double a = -1.0 / line.Item1;
            double b = point.y - a * point.x; 
            return (a, b);
        }

        public static double DistanceVerticeFromLine((double, double) line, Vertice point)
        {
            double A = line.Item1;
            double B = 1;
            double C = line.Item2;
            double distance = (double)Math.Abs(A * point.x + B * point.y + C) / Math.Sqrt(A * A + B * B);
            return distance;
        }

        public static Vertice IntersectionOfLines((double, double) line1, (double, double) line2)
        {
            double x = (line2.Item2 - line1.Item2) / (line1.Item1 - line2.Item1);
            double y = line1.Item1 * x + line1.Item2;
            return new Vertice((int)x, (int)y);
        }
       
    }
}
