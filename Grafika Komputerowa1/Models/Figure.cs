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
        public Figure()
        {
            isFull = false;
            points = new List<Vertice>();
            edges = new List<Edge>();
            ps = new List<(Edge, Edge, Relation)>();
        }

        public bool AddRelation(Edge a, Edge b, Relation relation)
        {
            if(a.relation == Relation.None && b.relation == Relation.None)
            {
                ps.Add((a, b, relation));
                a.relation = relation;
                b.relation = relation;
                return true;
            }
            return false;
        }

        public bool AddPoint(Vertice point)
        {
            if(!isFull)
            {
                if (points.Count > 0)
                {
                    if (IsStartPoint(point))
                    {
                        AddEdge(new Edge(points.LastOrDefault(), points.FirstOrDefault()));
                        isFull = true;
                    }
                    else if(IsPoint(point))
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
            if(points.Count > 3)
            {
                (Edge, Edge) associatedEdges = GetEdgesFromPoint(point);
                if(associatedEdges.Item1 != null)
                {
                    RemoveEdge(associatedEdges.Item1);
                }

                if (associatedEdges.Item2 != null)
                {
                    RemoveEdge(associatedEdges.Item2);
                }
                RemovePoint(point);
                return true;
            }
            return false;
        }

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

        public (Edge, Edge) GetEdgesFromPoint(Vertice point)
        {
            Edge a = edges.FirstOrDefault(e => e.End.Equals(point));
            Edge b = edges.FirstOrDefault(e => e.Start.Equals(point));
            return (a, b);
        }
        
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
        }

        public bool IsPoint(Vertice point)
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
        }

        public void MoveFigure(int X, int Y)
        {
            foreach(var e in edges)
            {
                MoveEdge(e, X, Y);
            }
        }
    }
}
