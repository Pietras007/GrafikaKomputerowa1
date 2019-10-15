using Grafika_Komputerowa1.Constans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Models
{
    public class Edge
    {
        public Relation relation { get; set; }
        public Vertice Start { get; set; }
        public Vertice End { get; set; }
        public bool isSelected { get; set; }
        public int relationNumber { get; set; }
        public Edge(Vertice x, Vertice y)
        {
            Start = x;
            End = y;
            relation = Relation.None;
            isSelected = false;
            relationNumber = -1;
        }

        public bool ContainsPoint(Vertice point)
        {
            int A = Start.y - End.y;
            int B = End.x - Start.x;
            int C = Start.y * (Start.x - End.x) + Start.x * (End.y - Start.y);
            double distance = (double)Math.Abs(A * point.x + B * point.y + C) / Math.Sqrt(A*A + B*B);
            if(distance < CONST.pointHalf)
            {
                double xy = Math.Abs(Math.Sqrt(Math.Pow(End.x - Start.x, 2) + Math.Pow(End.y - Start.y, 2)));
                double xp = Math.Abs(Math.Sqrt(Math.Pow(End.x - point.x, 2) + Math.Pow(End.y - point.y, 2)));
                double py = Math.Abs(Math.Sqrt(Math.Pow(point.x - Start.x, 2) + Math.Pow(point.y - Start.y, 2)));
                double cosA = (Math.Pow(xy, 2) + Math.Pow(xp, 1) - Math.Pow(py, 2)) / (2 * xy * xp);
                double cosB = (Math.Pow(xy, 2) + Math.Pow(py, 1) - Math.Pow(xp, 2)) / (2 * xy * py);
                if (cosA > 0 && cosA < 1 && cosB > 0 && cosB < 1)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetSelected()
        {
            isSelected = true;
        }
    }
}
