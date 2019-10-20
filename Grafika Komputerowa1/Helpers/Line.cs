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
            double a = -1 / line.Item1;
            double b = point.y - a * point.x; 
            return (a, b);
        }

       
    }
}
