using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Komputerowa1
{
    public static class Helpers
    {
        public static bool IsPoint(this MouseEventArgs e, List<(Vertice, Vertice)> points)
        {
            foreach(var _p in points)
            {
                var p = _p.Item1;
                if (e.X >= p.x - CONST.pointHalf && e.X <= p.x + CONST.pointHalf && e.Y >= p.y - CONST.pointHalf && e.Y <= p.y + CONST.pointHalf)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsFirstPoint(this MouseEventArgs e, List<(Vertice, Vertice)> points)
        {
            if (points != null && points.Count > 0)
            {
                var p = points.FirstOrDefault().Item1;
                if (e.X >= p.x - CONST.pointHalf && e.X <= p.x + CONST.pointHalf && e.Y >= p.y - CONST.pointHalf && e.Y <= p.y + CONST.pointHalf)
                {
                    return true;
                }
            }
            return false;
        }

        public static int GetPointIndex(this List<(Vertice, Vertice)> list, Vertice point)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var p = list[i].Item1;
                if (point.x >= p.x - CONST.pointHalf && point.x <= p.x + CONST.pointHalf && point.y >= p.y - CONST.pointHalf && point.y <= p.y + CONST.pointHalf)
                {
                    return i;
                }
            }
            return -1;
        }

        public static void MovePoint(this List<(Vertice, Vertice)> points, int movingIndex, Vertice current)
        {
            points[movingIndex] = (current, points[movingIndex].Item2);
            if (movingIndex == 0)
            {
                points[points.Count - 1] = (points[points.Count - 1].Item1, current);
            }
            else
            {
                points[movingIndex - 1] = (points[movingIndex - 1].Item1, current);
            }
        }
    }
}
