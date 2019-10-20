using Grafika_Komputerowa1.Helpers;
using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.RelationLogic
{
    public static class SetPointForPerpendicular
    {
        public static void SetPerpednicular(Edge siblingEdge, Edge currentEdge, Edge nextEdge)
        {
            Vertice start = currentEdge.Start;
            Vertice middle = currentEdge.End;
            Vertice end = nextEdge.End;
            double d = DistanceHelpers.GetEdgeLength(siblingEdge);
            (double, double) line = Line.GetStraightLine(siblingEdge.Start, siblingEdge.End);
            (double, double) perpendicularLine = Line.GetPerpendicularThroughPoint(line, start);
            (Vertice, Vertice) vertices = PointHelpers.GetPointFromLineDistanceAndPoint(perpendicularLine, d, start);
            Vertice resultVertice = DistanceHelpers.GetCloserVerticeFromVertice(vertices, middle);
            PointHelpers.SetPointXY(middle, resultVertice.x, resultVertice.y);
        }

        public static void SetPerpednicularNextEqual(Edge siblingEdge, Edge currentEdge, Edge nextEdge)
        {
            Vertice start = currentEdge.Start;
            Vertice middle = currentEdge.End;
            Vertice end = nextEdge.End;
            double d = DistanceHelpers.GetEdgeLength(siblingEdge);
            double d2 = DistanceHelpers.GetEdgeLength(nextEdge);
            (double, double) line = Line.GetStraightLine(siblingEdge.Start, siblingEdge.End);
            (double, double) perpendicularLine = Line.GetPerpendicularThroughPoint(line, start);
            if (d2 < Line.DistanceVerticeFromLine(perpendicularLine, end))
            {
                d2 = Line.DistanceVerticeFromLine(perpendicularLine, end) + 1;
            }
            (double, double) distanceLine = Line.GetStraightLine(start, end);
            double cos = Angle.GetCosinusFromBetween(perpendicularLine, distanceLine);
            double sqrtDelta = Math.Sqrt(Math.Pow(2*d*cos, 2) - 4*(d*d - d2*d2));
            double x1 = (2*d*cos - sqrtDelta) / 2;
            double x2 = (2*d*cos + sqrtDelta) / 2;
            double x = Math.Min(x1, x2);
            if(x < 0)
            {
                x = Math.Max(x1, x2);
            }
            (Vertice, Vertice) vertices = PointHelpers.GetPointFromLineDistanceAndPoint(perpendicularLine, x, start);
            Vertice resultVertice = DistanceHelpers.GetCloserVerticeFromVertice(vertices, end);
            PointHelpers.SetPointXY(middle, resultVertice.x, resultVertice.y);
        }

        public static void SetPerpednicularNextPerpendicular(Edge siblingEdge, Edge currentEdge, Edge nextEdge)
        {
            Vertice start = currentEdge.Start;
            Vertice middle = currentEdge.End;
            Vertice end = nextEdge.End;
            (double, double) line = Line.GetStraightLine(siblingEdge.Start, siblingEdge.End);
            (double, double) perpendicularLine = Line.GetPerpendicularThroughPoint(line, start);
            (double, double) nextLine = Line.GetStraightLine(middle, end);
            Vertice resultVertice = Line.IntersectionOfLines(perpendicularLine, nextLine);
            PointHelpers.SetPointXY(middle, resultVertice.x, resultVertice.y);
        }
    }
}
