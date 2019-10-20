using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Helpers
{
    public static class PointHelpers
    {
        public static void MovePointXY(Vertice point, int X, int Y)
        {
            point.x += X;
            point.y += Y;
        }

        public static void SetPointXY(Vertice point, int X, int Y)
        {
            point.x = X;
            point.y = Y;
        }

        public static Vertice GetPointFromLineDistanceAndPoint((double, double) line, double distance, Vertice point)
        {
            Vertice resultVertice = new Vertice(-1, -1);
            double a = line.Item1;
            double x = Math.Sqrt(Math.Pow(distance,2) / (Math.Pow(a, 2) + 1));
            double y = a * x;
            int X = (int)x;
            int Y = (int)y;
            PointHelpers.SetPointXY(resultVertice, point.x + X, point.y + Y);
            return resultVertice;
        }
    }
}
