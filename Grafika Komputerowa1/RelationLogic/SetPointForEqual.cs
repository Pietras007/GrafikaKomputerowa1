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
            PointHelpers.SetPointXY(middle, missingPoint.x, missingPoint.y);
        }

        public static void SetAsTriangleEdge(double d, Edge currentEdge, Edge nextEdge)
        {
            //zachowaj kat nastepnego
        }

        //public static void ShortenLineForEdge(double d, Edge currentEdge, Edge nextEdge)
        //{
        //    Vertice start = currentEdge.Start;
        //    Vertice middle = currentEdge.End;
        //    Vertice end = nextEdge.End;
        //    (double, double) lineHelp = Line.GetStraightLine(middle, end);
        //    Vertice resultVertice = PointHelpers.GetPointInProportion(1/2, middle, end);
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
