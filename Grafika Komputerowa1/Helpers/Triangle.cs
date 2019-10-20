﻿using Grafika_Komputerowa1.Models;
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
            double t = Pythagoras.GetCathetus(height, d2);

            double proportion = z / distance;
            double xDifference = pEnd.x - pStart.x;
            double yDifference = pEnd.y - pStart.y;
            int X = (int)(proportion * xDifference);
            int Y = (int)(proportion * yDifference);
            PointHelpers.SetPointXY(midPoint, pStart.x + X, pStart.y + Y);

            (double, double) line = Line.GetStraightLine(pStart, pEnd);
            (double, double) perpendicularLine = Line.GetPerpendicularThroughPoint(line, midPoint);
            return PointHelpers.GetPointFromLineDistanceAndPoint(perpendicularLine, height, midPoint);
        }
    }
}
