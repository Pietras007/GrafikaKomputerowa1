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
            Vertice resultVertice = PointHelpers.GetPointInProportion(proportion, start, end);
            if (resultVertice.x < int.MinValue + 100 || resultVertice.y > int.MaxValue - 100 || resultVertice.y < int.MinValue + 100 || resultVertice.y > int.MaxValue - 100)
            {
                int xdddd = 2;
            }
            PointHelpers.SetPointXY(middle, resultVertice.x, resultVertice.y);
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
            if (missingPoint.x < int.MinValue + 100 || missingPoint.y > int.MaxValue - 100 || missingPoint.y < int.MinValue + 100 || missingPoint.y > int.MaxValue - 100)
            {
                int xdddd = 2;
            }
            PointHelpers.SetPointXY(middle, missingPoint.x, missingPoint.y);
        }

        public static void SetAsTriangleEdge(double d2, Edge currentEdge, Edge nextEdge)
        {
            Vertice start = currentEdge.Start;
            Vertice middle = currentEdge.End;
            Vertice end = nextEdge.End;
            (double, double) nextLine = Line.GetStraightLine(end, middle);
            (double, double) straightLine = Line.GetStraightLine(end, start);
            double cos = Angle.GetCosinusFromBetween(nextLine, straightLine);
            double d = DistanceHelpers.DistanceBetween(start, end);
            double sqrtDelta = Math.Sqrt(Math.Pow(2 * d * cos, 2) - 4 * (d * d - d2 * d2));
            double x1 = (2 * d * cos - sqrtDelta) / 2;
            double x2 = (2 * d * cos + sqrtDelta) / 2;
            double x = Math.Min(x1, x2);
            if (x < 0)
            {
                x = Math.Max(x1, x2);
            }

            (Vertice, Vertice) vertices = PointHelpers.GetPointFromLineDistanceAndPoint(nextLine, x, end);
            Vertice resultVertice = DistanceHelpers.GetCloserVerticeFromVertice(vertices, start);
            if(resultVertice.x < int.MinValue + 100 || resultVertice.y > int.MaxValue - 100 || resultVertice.y < int.MinValue + 100 || resultVertice.y > int.MaxValue - 100)
            {
                int xdddd = 2;
            }
            PointHelpers.SetPointXY(middle, resultVertice.x, resultVertice.y);
        }

        //public static void ShortenLineForEdge(double d, Edge currentEdge, Edge nextEdge)
        //{
        //    Vertice start = currentEdge.Start;
        //    Vertice middle = currentEdge.End;
        //    Vertice end = nextEdge.End;
        //    (double, double) lineHelp = Line.GetStraightLine(middle, end);
        //    Vertice resultVertice = PointHelpers.GetPointInProportion(1 / 2, middle, end);
        //    (double, double) line = Line.GetStraightLine(start, resultVertice);
        //    (Vertice, Vertice) midVertices = PointHelpers.GetPointFromLineDistanceAndPoint(line, d, currentEdge.Start);
        //    Vertice midVertice = DistanceHelpers.GetCloserVerticeFromLine(lineHelp, midVertices);
        //    PointHelpers.SetPointXY(middle, midVertice.x, midVertice.y);
        //}

        public static void ShortenLineForEdge(double d, Edge currentEdge, Edge nextEdge)
        {
            Vertice start = currentEdge.Start;
            Vertice middle = currentEdge.End;
            Vertice end = nextEdge.End;
            Vertice helper = new Vertice(end.x - CONST.ShortenLineForEdgeDistance, end.y - CONST.ShortenLineForEdgeDistance);
            (double, double) lineHelp = Line.GetStraightLine(helper, end);
            Vertice resultVertice = PointHelpers.GetPointInProportion(1 / 2, helper, end);
            (double, double) line = Line.GetStraightLine(start, resultVertice);
            (Vertice, Vertice) midVertices = PointHelpers.GetPointFromLineDistanceAndPoint(line, d, currentEdge.Start);
            Vertice midVertice = DistanceHelpers.GetCloserVerticeFromLine(lineHelp, midVertices);
            PointHelpers.SetPointXY(middle, midVertice.x, midVertice.y);
        }
    }
}
