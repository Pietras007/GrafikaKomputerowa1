using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.RelationLogic
{
    public static class Perpendicularity
    {
        public static Vertice CalculateVertice(Vertice p, Vertice endPoint, Edge e)
        {
            double c = Math.Sqrt((endPoint.x - p.x) * (endPoint.x - p.x) + ((endPoint.y - p.y) * (endPoint.y - p.y)));
            double tmpA = 1;
            if(Math.Abs((e.Start.x - e.End.x)) >= 1 && (e.Start.y - e.End.y) != 0)
            {
                tmpA = (double)(e.Start.y - e.End.y) / (double)(e.Start.x - e.End.x);
            }
            else if(e.Start.x - e.End.x < 1)
            {
                return new Vertice((int)(p.x + c), p.y);
            }
            else
            {
                return new Vertice(p.x, (int)(p.y + c));
            }

            double a = -1 / tmpA;
            double b = p.y - a * p.x;

            double deltaA = a * a + 1;
            double deltaB = (2 * a * b - 2 * p.x - 2 * a * p.y);
            double deltaC = p.x * p.x + p.y * p.y - c * c - 2 * p.y * b + b * b;
            double delta = deltaB * deltaB - 4 * deltaA * deltaC;
            int sign = p.x > endPoint.x ? -1 : 1;
            double x = Math.Max(1, (-deltaB + sign * Math.Sqrt(delta)) / (2 * deltaA));
            double y = a * x + b;
            return new Vertice((int)x, (int)y);
        }
    }
}
