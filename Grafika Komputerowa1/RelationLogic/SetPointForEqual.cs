using Grafika_Komputerowa1.Helpers;
using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.RelationLogic
{
    public static class SetPointForEqual
    {
        public static void SetOnLineBetween(double d, Edge currentEdge, Edge nextEdge)
        {
            Vertice start = currentEdge.Start;
            Vertice middle = currentEdge.End;
            Vertice end = nextEdge.End;
            double distance = DistanceHelpers.DistanceBetween(start, end);
            double proportion = d / distance;
            double xDifference = end.x - start.x;
            double yDifference = end.y - start.y;
            int X = (int)(proportion * xDifference);
            int Y = (int)(proportion * yDifference);
            PointHelpers.SetPointXY(middle, start.x + X, start.y + Y);
        }

        public static void SetAsTriangleLength(double d, Edge currentEdge, Edge nextEdge)
        {
            Vertice start = currentEdge.Start;
            Vertice middle = currentEdge.End;
            Vertice end = nextEdge.End;
            double d2 = DistanceHelpers.GetEdgeLength(nextEdge);
            double distance = DistanceHelpers.DistanceBetween(start, end);
            double triangleArea = Triangle.TriangleArea(d, d2, distance);
            Vertice missingPoint = Triangle.GetPointFromTriangleArea(triangleArea, start, end, d, d2);
            PointHelpers.SetPointXY(middle, missingPoint.x, missingPoint.y);
        }

        public static void SetAsTriangleEdge()
        {
            //zachowaj kat nastepnego
        }

        public static void ShortenLineForEdge()
        {
            //prosta prostopadla do prostej ktora mamy w nastepnym przechodzaza przez punkt start.start i na tej prostej dlugosc odpowiednia
        }
    }
}
