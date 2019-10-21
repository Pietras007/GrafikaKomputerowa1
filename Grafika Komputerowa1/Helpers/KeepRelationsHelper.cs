using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Helpers
{
    public static class KeepRelationsHelper
    {
        public static (Edge, Edge) GetEdgesFromPointListEdge(Vertice point, List<Edge> edges)
        {
            Edge a = edges.FirstOrDefault(e => e.End.Equals(point));
            Edge b = edges.FirstOrDefault(e => e.Start.Equals(point));
            return (a, b);
        }

        public static List<Edge> GetReversedEdgesList(List<Edge> edges)
        {
            List<Edge> reverseEdges = new List<Edge>();
            List<Vertice> vertices = new List<Vertice>();
            foreach(var e in edges)
            {
                vertices.Add(e.Start);
            }
            vertices.Reverse();

            for (int i=0; i<vertices.Count; i++)
            {
                if(i == 0)
                {
                    Edge firstEdge = new Edge(vertices[i], null);
                    reverseEdges.Add(firstEdge);
                }
                else if(i == vertices.Count - 1)
                {
                    Edge lastEdge = reverseEdges.LastOrDefault();
                    lastEdge.End = vertices[i];
                    Edge currentEdge = new Edge(vertices[i], vertices[0]);
                    reverseEdges.Add(currentEdge);
                }
                else
                {
                    Edge lastEdge = reverseEdges.LastOrDefault();
                    lastEdge.End = vertices[i];
                    Edge currentEdge = new Edge(vertices[i], null);
                    reverseEdges.Add(currentEdge);
                }
            }
            return reverseEdges;
        }

        public static Relation GetRelationFromEdgeListRelation(Edge e, List<Relation> ps)
        {
            return ps.FirstOrDefault(x => x.edge1.Equals(e) || x.edge2.Equals(e));
        }
    }
}
