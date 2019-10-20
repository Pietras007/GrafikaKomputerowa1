using Grafika_Komputerowa1.Constans;
using Grafika_Komputerowa1.Extentions;
using Grafika_Komputerowa1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Komputerowa1.Models
{
    public class Figure
    {
        public bool isFull { get; set; }
        public List<Vertice> points { get; set; }
        public List<Edge> edges { get; set; }
        public List<Relation> ps { get; set; }
        public int relationNumber { get; set; }
        public Figure()
        {
            isFull = false;
            points = new List<Vertice>();
            edges = new List<Edge>();
            ps = new List<Relation>();
            relationNumber = 0;
        }

        //POINTS
        public bool AddPoint(Vertice point)
        {
            if (!isFull)
            {
                if (points.Count > 0)
                {
                    if (IsStartPoint(point))
                    {
                        AddEdge(new Edge(points.LastOrDefault(), points.FirstOrDefault()));
                        isFull = true;
                    }
                    else if (IsPoint(point))
                    {
                        return false;
                    }
                    else
                    {
                        AddEdge(new Edge(points.LastOrDefault(), point));
                        points.Add(point);
                    }
                }
                else
                {
                    points.Add(point);
                }

                return true;
            }
            return false;
        }
        public bool MovePoint(Vertice start, Vertice end)
        {
            if (start != null && end != null)
            {
                (Edge, Edge) e = GetEdgesFromPoint(start);
                if (!KeepRelations(e.Item2.End))
                {
                    MoveFigure(end.x - start.x, end.y - start.y);
                    return false;
                }
                else
                {
                    if (e.Item1 != null)
                    {
                        e.Item1.End.x = end.x;
                        e.Item1.End.y = end.y;
                    }
                    if (e.Item2 != null)
                    {
                        e.Item2.Start.x = end.x;
                        e.Item2.Start.y = end.y;
                    }
                    start.x = end.x;
                    start.y = end.y;
                }
                return true;
            }
            return false;
        }
        public bool RemovePoint(Vertice point)
        {
            if (points.Count > 3)
            {
                Vertice a = null;
                Vertice b = null;
                (Edge, Edge) associatedEdges = GetEdgesFromPoint(point);
                if (associatedEdges.Item1 != null)
                {
                    a = associatedEdges.Item1.Start;
                    Edge e = associatedEdges.Item1;
                    if (e.relation != RelationEnum.None)
                    {
                        Relation ps1 = ps.FirstOrDefault(x => x.edge1.Equals(e) || x.edge2.Equals(e));
                        ps1.edge1.relation = RelationEnum.None;
                        ps1.edge2.relation = RelationEnum.None;
                        ps.Remove(ps1);
                    }
                    RemoveEdge(associatedEdges.Item1);
                }

                if (associatedEdges.Item2 != null)
                {
                    b = associatedEdges.Item2.End;
                    Edge e = associatedEdges.Item2;
                    if (e.relation != RelationEnum.None)
                    {
                        Relation ps2 = ps.FirstOrDefault(x => x.edge1.Equals(e) || x.edge2.Equals(e));
                        ps2.edge1.relation = RelationEnum.None;
                        ps2.edge2.relation = RelationEnum.None;
                        ps.Remove(ps2);
                    }
                    RemoveEdge(associatedEdges.Item2);
                }
                points.Remove(point);
                AddEdge(new Edge(a, b));
                return true;
            }
            return false;
        }

        //EDGES
        public bool AddEdge(Edge edge)
        {
            if (edge != null)
            {
                edges.Add(edge);
                return true;
            }
            return false;
        }
        public bool MoveEdge(Edge edge, int X, int Y)
        {
            if (edge != null)
            {
                if (MovePoint(edge.Start, new Vertice(edge.Start.x + X, edge.Start.y + Y)))
                {
                    MovePoint(edge.End, new Vertice(edge.End.x + X, edge.End.y + Y));
                }

                return true;
            }
            return false;

        }
        public bool RemoveEdge(Edge edge)
        {
            if (edge != null)
            {
                edges.Remove(edge);
                return true;
            }
            return false;
        }


        //HELPERS
        public bool KeepRelations(Vertice v)
        {
            List<Vertice> oldVertices = points.Clone();
            int indexer = 0;
            int maxEdges = edges.Count * 10;
            Vertice startPoint = GetEdgesFromPoint(v).Item1.Start;
            Vertice currentPoint = v;
            while(true)
            {
                (Edge, Edge) edgesFromPoint = GetEdgesFromPoint(currentPoint);
                if (AllRelationsOk())
                {
                    return true;
                }
                if (!currentPoint.Equals(startPoint))
                {
                    Relation rel = GetRelationFromEdge(edgesFromPoint.Item1);
                    if (rel != null)
                    {
                        if (!IsRelationOk(rel))
                        {
                            RelationLogic.RelationLogic.RepairRelation(rel, edgesFromPoint, currentPoint);
                        }
                    }
                }
                if (indexer > maxEdges)
                {
                    for (int i = 0; i < oldVertices.Count; i++)
                    {
                        points[i].x = oldVertices[i].x;
                        points[i].y = oldVertices[i].y;
                    }
                    return false;
                }

                currentPoint = edgesFromPoint.Item2.End;
                indexer++;
            }
        }

        public Relation GetWrongRelation()
        {
            foreach (var rel in ps)
            {
                if (rel.relation == RelationEnum.Equal)
                {
                    if (!IsEqualRelation(rel.edge1, rel.edge2))
                    {
                        return rel;
                    }
                }

                if (rel.relation == RelationEnum.Perpendicular)
                {
                    if (!IsPerpendicularRelation(rel.edge1, rel.edge2))
                    {
                        return rel;
                    }
                }
                
            }
            return new Relation(new Edge(new Vertice(1,1), new Vertice(1,1)), new Edge(new Vertice(1, 1), new Vertice(1, 1)), RelationEnum.None);
        }
        public Relation GetRelationFromEdge(Edge e)
        {
            return ps.FirstOrDefault(x => x.edge1.Equals(e) || x.edge2.Equals(e));
        }
        public bool AllRelationsOk()
        {
            foreach(var rel in ps)
            {
                if(rel.relation == RelationEnum.Equal)
                {
                    if(!IsEqualRelation(rel.edge1, rel.edge2))
                    {
                        return false;
                    }
                }

                if(rel.relation == RelationEnum.Perpendicular)
                {
                    if(!IsPerpendicularRelation(rel.edge1, rel.edge2))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public bool IsRelationOk(Relation relation)
        {
            if(relation.relation == RelationEnum.Equal)
            {
                return IsEqualRelation(relation.edge1, relation.edge2);
            }
            else if (relation.relation == RelationEnum.Perpendicular)
            {
                return IsPerpendicularRelation(relation.edge1, relation.edge2);
            }
            else
            {
                return true;
            }
        }
        public bool IsEqualRelation(Edge e1, Edge e2)
        {
            //Because of unencountered exceptions sometimes xD
            if((int)DistanceHelpers.GetEdgeLength(e1) - (int)DistanceHelpers.GetEdgeLength(e2) == int.MinValue)
            {
                return false;
            }

            int dist = Math.Abs((int)DistanceHelpers.GetEdgeLength(e1) - (int)DistanceHelpers.GetEdgeLength(e2));
            if (dist < CONST.differenceToMakeEdgesEqual)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsPerpendicularRelation(Edge e1, Edge e2)
        {
            double a1 = (double)(e1.End.y - e1.Start.y) / (e1.End.x - e1.Start.x);
            double a2 = (double)(e2.End.y - e2.Start.y) / (e2.End.x - e2.Start.x);
            int multiplication100 = (int)(a1 * a2 * 100);
            if (multiplication100 >=95 && multiplication100 <= 105)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddRelation(Edge a, Edge b, RelationEnum relation, IWin32Window window)
        {
            if (a.relation == RelationEnum.None && b.relation == RelationEnum.None)
            {
                relationNumber++;
                Relation addingRelation = new Relation(a, b, relation);
                ps.Add(addingRelation);
                a.relation = relation;
                a.relationNumber = relationNumber;
                b.relation = relation;
                b.relationNumber = relationNumber;
                if (!KeepRelations(a.End))
                {
                    relationNumber--;
                    ps.Remove(addingRelation);
                    a.relation = RelationEnum.None;
                    a.relationNumber = -1;
                    b.relation = RelationEnum.None;
                    b.relationNumber = -1;
                    System.Windows.Forms.MessageBox.Show(window, "No possibility to add relation", "Relation Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            return false;
        }
        public (Edge, Edge) GetEdgesFromPoint(Vertice point)
        {
            Edge a = edges.FirstOrDefault(e => e.End.Equals(point));
            Edge b = edges.FirstOrDefault(e => e.Start.Equals(point));
            return (a, b);
        } //If you click edge end
        public bool IsStartPoint(Vertice point)
        {
            Vertice po = GetPoint(point);
            if (po != null)
            {
                if (points.FirstOrDefault().Equals(po))
                {
                    return true;
                }
            }
            return false;
        }//Check if it is start point on figure
        public bool IsPoint(Vertice point)//Check if you click on point
        {
            foreach (var p in points)
            {
                if (point.x >= p.x - CONST.pointHalf && point.x <= p.x + CONST.pointHalf && point.y >= p.y - CONST.pointHalf && point.y <= p.y + CONST.pointHalf)
                {
                    return true;
                }
            }
            return false;
        }
        public Vertice GetPoint(Vertice point)
        {
            foreach (var p in points)
            {
                if (point.x >= p.x - CONST.pointHalf && point.x <= p.x + CONST.pointHalf && point.y >= p.y - CONST.pointHalf && point.y <= p.y + CONST.pointHalf)
                {
                    return p;
                }
            }
            return null;
        }//If you click on point gives reference to point
        public void MoveFigure(int X, int Y)
        {
            foreach(var p in points)
            {
                PointHelpers.MovePointXY(p, X, Y);
            }
        }//Addes X and Y to all points
        public void AddPointOnEdge(Edge e)
        {
            Vertice start = e.Start;
            Vertice end = e.End;
            Vertice middle = new Vertice((start.x + end.x) / 2, (start.y + end.y) / 2);
            points.Add(middle);
            if (e.relation != RelationEnum.None)
            {
                Relation ps1 = ps.FirstOrDefault(x => x.edge1.Equals(e) || x.edge2.Equals(e));
                ps1.edge1.relation = RelationEnum.None;
                ps1.edge2.relation = RelationEnum.None;
                ps.Remove(ps1);
            }
            edges.Remove(e);
            edges.Add(new Edge(start, middle));
            edges.Add(new Edge(middle, end));
        }
        public Edge GetSelectedEdge()
        {
            return edges.FirstOrDefault(e => e.isSelected == true);
        }
    }
}
