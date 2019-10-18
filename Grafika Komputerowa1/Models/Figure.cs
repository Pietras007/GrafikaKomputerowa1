using Grafika_Komputerowa1.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Models
{
    public class Figure
    {
        public bool isFull { get; set; }
        public List<Vertice> points { get; set; }
        public List<Edge> edges { get; set; }
        public List<(Edge, Edge, Relation)> ps { get; set; }
        public int relationNumber { get; set; }
        public Figure()
        {
            isFull = false;
            points = new List<Vertice>();
            edges = new List<Edge>();
            ps = new List<(Edge, Edge, Relation)>();
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
                    RemoveEdge(associatedEdges.Item1);
                }

                if (associatedEdges.Item2 != null)
                {
                    b = associatedEdges.Item2.End;
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
                MovePoint(edge.Start, new Vertice(edge.Start.x + X, edge.Start.y + Y));
                MovePoint(edge.End, new Vertice(edge.End.x + X, edge.End.y + Y));
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
        public bool SetEdgesEqual(Edge e1, Edge e2, Vertice v)
        {
            double length1 = Math.Sqrt(Math.Pow(e1.Start.x - e1.End.x, 2) + Math.Pow(e1.Start.y - e1.End.y, 2));
            double length2 = Math.Sqrt(Math.Pow(e2.Start.x - e2.End.x, 2) + Math.Pow(e2.Start.y - e2.End.y, 2));
            Edge firstStart = GetEdgesFromPoint(e1.Start).Item1;
            Edge firstEnd = GetEdgesFromPoint(e1.End).Item2;
            Edge secondStart = GetEdgesFromPoint(e2.Start).Item1;
            Edge secondEnd = GetEdgesFromPoint(e2.End).Item2;
            if(secondStart.DistanceFrom(e2.End) <= length1 && !v.Equals(e2.End))
            {
                SetVerticesOnEdges(e2.End, length1);
                return true;
            }
            else if(secondEnd.DistanceFrom(e2.Start) <= length1 && !v.Equals(e2.End))
            {
                SetVerticesOnEdges(e2.Start, length1);
                return true;
            }
            else if(firstStart.DistanceFrom(e1.End) <= length2 && !v.Equals(e2.End))
            {
                SetVerticesOnEdges(e1.End, length2);
                return true;
            }
            else if(firstEnd.DistanceFrom(e1.Start) <= length2 && !v.Equals(e2.End))
            {
                SetVerticesOnEdges(e1.Start, length2);
                return true;
            }
            return false;
        }

        public void SetVerticesOnEdges(Vertice v, double length)
        {
            (Edge, Edge) edges = GetEdgesFromPoint(v);
            Edge stateEdge = null;
            Edge moveEdge = null;
            if(edges.Item1.relation == Relation.Equal)
            {
                moveEdge = edges.Item1;
                stateEdge = edges.Item2;
            }
            else
            {
                moveEdge = edges.Item2;
                stateEdge = edges.Item1;
            }

        }

        //HELPERS
        public bool AddRelation(Edge a, Edge b, Relation relation)
        {
            if(a.relation == Relation.None && b.relation == Relation.None)
            {
                (Edge, Edge) e1 = GetEdgesFromPoint(a.Start);
                (Edge, Edge) e2 = GetEdgesFromPoint(a.End);
                (Edge, Edge) e3 = GetEdgesFromPoint(b.Start);
                (Edge, Edge) e4 = GetEdgesFromPoint(b.End);
                //if (e1.Item1.relation == Relation.Equal || e1.Item2.relation == Relation.Equal || e2.Item1.relation == Relation.Equal || e2.Item2.relation == Relation.Equal || e3.Item1.relation == Relation.Equal || e3.Item2.relation == Relation.Equal || e4.Item1.relation == Relation.Equal || e4.Item2.relation == Relation.Equal)
                //{
                //    //There is no possibility to have two equal 
                //    return false;
                //}
                if(true)
                {
                    relationNumber++;
                    ps.Add((a, b, relation));
                    a.relation = relation;
                    a.relationNumber = relationNumber;
                    b.relation = relation;
                    b.relationNumber = relationNumber;
                    if (relation == Relation.Equal)
                    {
                        if (!SetEdgesEqual(a, b, new Vertice(-1, -1)))
                        {
                            return false;
                        }
                    }
                    return true;
                }
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
                if(points.FirstOrDefault().Equals(po))
                {
                    return true;
                }
            }
            return false;
        }//Check if it is start point on figure
        public bool IsPoint(Vertice point)//Check if you click on point
        {
            foreach(var p in points)
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
            foreach(var p in points)
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
            foreach(var e in edges)
            {
                MoveEdge(e, X, Y);
            }
        }//Addes X and Y to all points
        public void AddPointOnEdge(Edge e)
        {
            Vertice start = e.Start;
            Vertice end = e.End;
            Vertice middle = new Vertice((start.x + end.x) / 2, (start.y + end.y) / 2);
            points.Add(middle);
            if(e.relation != Relation.None)
            {
                (Edge, Edge, Relation) ps1 = ps.FirstOrDefault(x => x.Item1.Equals(e) || x.Item2.Equals(e));
                ps1.Item1.relation = Relation.None;
                ps1.Item2.relation = Relation.None;
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
