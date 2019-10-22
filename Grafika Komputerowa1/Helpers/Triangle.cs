using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Helpers
{
    public static class Triangle
    {
        public static double TriangleArea(double a, double b, double c)
        {
            double p = (a + b + c) / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        public static Vertice GetPointFromTriangleArea(double area, Vertice pStart, Vertice pEnd, double d, double d2)
        {
            Vertice midPoint = new Vertice(-1, -1);
            double distance = DistanceHelpers.DistanceBetween(pStart, pEnd);
            double height = (2 * area) / distance;
            double z = Pythagoras.GetCathetus(height, d);

            double proportion = z / distance;
            Vertice resultVertice = PointHelpers.GetPointInProportion(proportion, pStart, pEnd);
            PointHelpers.SetPointXY(midPoint, resultVertice.x, resultVertice.y);

            (double, double) line = Line.GetStraightLine(pStart, pEnd);
            (double, double) perpendicularLine = Line.GetPerpendicularThroughPoint(line, midPoint);
            if(line.Item1 == 0)
            {
                return new Vertice(midPoint.x, midPoint.y - (int)height);
            }
            return PointHelpers.GetPointFromLineDistanceAndPoint(perpendicularLine, height, midPoint).Item2;
        }
    }
}
