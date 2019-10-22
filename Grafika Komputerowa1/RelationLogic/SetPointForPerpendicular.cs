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
            (Vertice, Vertice) vertices;
            if (line.Item1 == 0)
            {
                vertices = (new Vertice(start.x, start.y - (int)d), new Vertice(start.x, start.y + (int)d));
            }
            else if (line.Item1 > int.MaxValue - 100 || line.Item1 < int.MinValue + 100)
            {
                vertices = (new Vertice(start.x - (int)d, start.y), new Vertice(start.x + (int)d, start.y));
            }
            else
            {
                (double, double) perpendicularLine = Line.GetPerpendicularThroughPoint(line, start);
                vertices = PointHelpers.GetPointFromLineDistanceAndPoint(perpendicularLine, d, start);
            }
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
            (double, double) distanceLine = Line.GetStraightLine(start, end);
            (Vertice, Vertice) vertices = (null, null);
            if (line.Item1 != 0 && line.Item1 < int.MaxValue - 100 && line.Item1 > int.MinValue + 100)
            {
                (double, double) perpendicularLine = Line.GetPerpendicularThroughPoint(line, start);
                if (d2 < Line.DistanceVerticeFromLine(perpendicularLine, end))
                {
                    d2 = Line.DistanceVerticeFromLine(perpendicularLine, end) + 1;
                }
                double cos = Angle.GetCosinusFromBetween(perpendicularLine, distanceLine);
                double sqrtDelta = Math.Sqrt(Math.Pow(2 * d * cos, 2) - 4 * (d * d - d2 * d2));
                double x1 = (2 * d * cos - sqrtDelta) / 2;
                double x2 = (2 * d * cos + sqrtDelta) / 2;
                double x = Math.Min(x1, x2);
                if (x < 0)
                {
                    x = Math.Max(x1, x2);
                }
                vertices = PointHelpers.GetPointFromLineDistanceAndPoint(perpendicularLine, x, start);
                Vertice resultVertice = DistanceHelpers.GetCloserVerticeFromVertice(vertices, end);
                PointHelpers.SetPointXY(middle, resultVertice.x, resultVertice.y);
            }
            else
            {
                SetPerpednicularNextPerpendicular(siblingEdge, currentEdge, nextEdge);
            }
            
        }

        public static void SetPerpednicularNextPerpendicular(Edge siblingEdge, Edge currentEdge, Edge nextEdge)
        {
            //Vertice start = currentEdge.Start;
            //Vertice middle = currentEdge.End;
            //Vertice end = nextEdge.End;
            //Vertice resultVertice = null;
            //(double, double) line = Line.GetStraightLine(siblingEdge.Start, siblingEdge.End);
            //(double, double) nextLine = Line.GetStraightLine(middle, end);
            //if (line.Item1 == 0)
            //{
            //    //int diff = end.y - start.y;
            //    //resultVertice = new Vertice(start.x, start.y + diff);
            //    int myY = (int)(nextLine.Item1 * start.x + nextLine.Item2);
            //    resultVertice = new Vertice(start.x, myY);
            //}
            //else if(line.Item1 > int.MaxValue - 100 || line.Item1 < int.MinValue + 100)
            //{
            //    //int diff = end.x - start.x;
            //    //resultVertice = new Vertice(start.x + diff, start.y);
            //    int myX = (int)((start.y - nextLine.Item2)/nextLine.Item1);
            //    resultVertice = new Vertice(myX, start.y);
            //}
            //else
            //{
            //    (double, double) perpendicularLine = Line.GetPerpendicularThroughPoint(line, start);
            //    resultVertice = Line.IntersectionOfLines(perpendicularLine, nextLine);
            //}
            //PointHelpers.SetPointXY(middle, resultVertice.x, resultVertice.y);
        }
    }
}
