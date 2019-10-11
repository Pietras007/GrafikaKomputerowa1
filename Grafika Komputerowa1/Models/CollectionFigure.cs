using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Models
{
    public class CollectionFigure
    {
        public List<Figure> figures { get; set; }
        public CollectionFigure()
        {
            figures = new List<Figure>();
        }

        public Vertice GetPoint(Vertice point)
        {
            foreach(var fig in figures)
            {
                foreach(var p in fig.points)
                {
                    if (point.x >= p.x - CONST.pointHalf && point.x <= p.x + CONST.pointHalf && point.y >= p.y - CONST.pointHalf && point.y <= p.y + CONST.pointHalf)
                    {
                        return p;
                    }
                }
            }
            return null;
        }

        public Edge GetEdgeFromPoint(Vertice point)
        {
            foreach(var fig in figures)
            {
                foreach(var e in fig.edges)
                {
                    if(e.ContainsPoint(point))
                    {
                        return e;
                    }
                }
            }
            return null;
        }

        public Figure GetExtendingFigure()
        {
            Figure fig = figures.FirstOrDefault(x => x.isFull == false);
            if(fig != null)
            {
                return fig;
            }
            return GetNewFigure();
        }

        public bool DeleteUnfinishedFigure()
        {
            foreach(var fig in figures)
            {
                if(!fig.isFull)
                {
                    figures.Remove(fig);
                    return true;
                }
            }
            return false;
        }

        public Figure GetNewFigure()
        {
            Figure figure = new Figure();
            figures.Add(figure);
            return figure;
        }

        public Figure GetFigureFromClickOnBorder(Vertice point)
        {
            var figurePoint = GetFigure(point);
            var edge = GetEdgeFromPoint(point);
            var figureEdge = GetFigure(edge);
            if(figurePoint != null)
            {
                return figurePoint;
            }
            else if(figureEdge != null)
            {
                return figureEdge;
            }
            return null;
        }

        public Figure GetFigure(Vertice point)
        {
            foreach(var fig in figures)
            {
                if(fig.points.Contains(point))
                {
                    return fig;
                }
            }
            return null;
        }

        public Figure GetFigure(Edge edge)
        {
            foreach (var fig in figures)
            {
                if(fig.edges.Contains(edge))
                {
                    return fig;
                }
            }
            return null;
        }
    }
}
