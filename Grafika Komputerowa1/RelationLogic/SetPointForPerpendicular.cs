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
    }
}
