﻿using Grafika_Komputerowa1.Constans;
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
        public static bool RepairRelation(Relation relation, (Edge,Edge) edgesPair, Vertice currentPoint)
        {
            Edge currentEdge = edgesPair.Item1;
            Edge nextEdge = edgesPair.Item2;
            RelationEnum currentRelation = relation.relation;
            Edge siblingEdge = null;
            if(relation.edge1.Equals(currentEdge))
                siblingEdge = relation.edge2;
            else
                siblingEdge = relation.edge1;

            if (currentRelation == RelationEnum.Equal)
            {
                if(nextEdge.relation == RelationEnum.None)
                {
                    if (DistanceHelpers.GetEdgeLength(siblingEdge) < DistanceHelpers.DistanceBetween(currentEdge.Start, nextEdge.End))
                    {
                        SetPointForEqual.SetOnLineBetween();
                    }
                    else
                    {
                        SetPointForEqual.ShortenLineForEdge();
                    }
                }
                else if (nextEdge.relation == RelationEnum.Equal)
                {
                    if (DistanceHelpers.GetEdgeLength(siblingEdge) + DistanceHelpers.GetEdgeLength(nextEdge) >= DistanceHelpers.DistanceBetween(currentEdge.Start, nextEdge.End))
                    {
                        SetPointForEqual.SetAsTriangleLength();
                    }
                    else
                    {
                        SetPointForEqual.SetOnLineBetween();
                    }
                }
                else if(nextEdge.relation == RelationEnum.Perpendicular)
                {
                    if(true)//Odleglosd punktu od prostej jest <= od dlugosci odcinka
                    {
                        SetPointForEqual.SetAsTriangleEdge();
                    }
                    else
                    {
                        SetPointForEqual.ShortenLineForEdge();
                    }
                }
            }
            else if(currentRelation == RelationEnum.Perpendicular)
            {
                if (nextEdge.relation == RelationEnum.None)
                {
                    //No problem
                }
                else if (nextEdge.relation == RelationEnum.Equal)
                {
                    //No problem
                }
                else if (nextEdge.relation == RelationEnum.Perpendicular)
                {

                }
            }
            return true;
        }
    }
}
