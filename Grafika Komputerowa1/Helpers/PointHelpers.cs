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

        public static (Vertice, Vertice) GetPointFromLineDistanceAndPoint((double, double) line, double distance, Vertice point)
        {
            Vertice resultVertice1 = new Vertice(-1, -1);
            Vertice resultVertice2 = new Vertice(-1, -1);
            double a = line.Item1;
            double x = Math.Sqrt(Math.Pow(distance,2) / (Math.Pow(a, 2) + 1));
            double y = a * x;
            int X = (int)x;
            int Y = (int)y;
            PointHelpers.SetPointXY(resultVertice1, point.x + X, point.y - Y);
            PointHelpers.SetPointXY(resultVertice2, point.x - X, point.y + Y);
            return (resultVertice1, resultVertice2);
        }

        public static Vertice GetPointInProportion(double proportion, Vertice start, Vertice end)
        {
            Vertice middle = new Vertice(-1,-1);
            double xDifference = end.x - start.x;
            double yDifference = end.y - start.y;
            int X = (int)(proportion * xDifference);
            int Y = (int)(proportion * yDifference);
            SetPointXY(middle, start.x + X, start.y + Y);
            return middle;
        }
    }
}
