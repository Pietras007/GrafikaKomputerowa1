using Grafika_Komputerowa1.Constans;
using Grafika_Komputerowa1.Helpers;
using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.RelationLogic
{
    public static class RelationLogic
    {
        public static bool RepairRelation(Relation relation, (Edge, Edge) edgesPair, Vertice currentPoint)
        {
            Edge currentEdge = edgesPair.Item1;
            Edge nextEdge = edgesPair.Item2;
            RelationEnum currentRelation = relation.relation;
            Edge siblingEdge = null;
            if (relation.edge1.Equals(currentEdge))
                siblingEdge = relation.edge2;
            else
                siblingEdge = relation.edge1;

            if (currentRelation == RelationEnum.Equal)
            {
                if (nextEdge.relation == RelationEnum.None)
                {
                    if (DistanceHelpers.GetEdgeLength(siblingEdge) < DistanceHelpers.DistanceBetween(currentEdge.Start, nextEdge.End) - 20)
                    {
                        SetPointForEqual.SetOnLineBetween(DistanceHelpers.GetEdgeLength(siblingEdge), currentEdge, nextEdge);
                    }
                    else
                    {
                        SetPointForEqual.ShortenLineForEdge(DistanceHelpers.GetEdgeLength(siblingEdge), currentEdge, nextEdge);
                    }
                }
                else if (nextEdge.relation == RelationEnum.Equal)
                {
                    if (DistanceHelpers.GetEdgeLength(siblingEdge) + DistanceHelpers.GetEdgeLength(nextEdge) >= DistanceHelpers.DistanceBetween(currentEdge.Start, nextEdge.End))
                    {
                        SetPointForEqual.SetAsTriangleLength(DistanceHelpers.GetEdgeLength(siblingEdge), currentEdge, nextEdge);
                    }
                    else
                    {
                        SetPointForEqual.ShortenLineForEdge(DistanceHelpers.GetEdgeLength(siblingEdge), currentEdge, nextEdge);
                        //SetPointForEqual.SetOnLineBetween(DistanceHelpers.GetEdgeLength(siblingEdge), currentEdge, nextEdge);
                    }
                }
                else if (nextEdge.relation == RelationEnum.Perpendicular)
                {
                    Vertice start = currentEdge.Start;
                    Vertice middle = currentEdge.End;
                    Vertice end = nextEdge.End;
                    double d = DistanceHelpers.GetEdgeLength(siblingEdge);
                    (double, double) line = Line.GetStraightLine(middle, end);
                    double distance = Line.DistanceVerticeFromLine(line, start);
                    if (distance >= d + 1)
                    {
                        SetPointForEqual.SetAsTriangleEdge(DistanceHelpers.GetEdgeLength(siblingEdge), currentEdge, nextEdge);
                    }
                    else
                    {
                        SetPointForEqual.ShortenLineForEdge(DistanceHelpers.GetEdgeLength(siblingEdge), currentEdge, nextEdge);
                    }
                }
            }
            else if (currentRelation == RelationEnum.Perpendicular)
            {
                if (nextEdge.relation == RelationEnum.None)
                {
                    SetPointForPerpendicular.SetPerpednicular(siblingEdge, currentEdge, nextEdge);
                }
                else if (nextEdge.relation == RelationEnum.Equal)
                {
                    SetPointForPerpendicular.SetPerpednicularNextEqual(siblingEdge, currentEdge, nextEdge);
                }
                else if (nextEdge.relation == RelationEnum.Perpendicular)
                {
                    SetPointForPerpendicular.SetPerpednicularNextPerpendicular(siblingEdge, currentEdge, nextEdge);
                }
            }
            return true;
        }
    }
}
