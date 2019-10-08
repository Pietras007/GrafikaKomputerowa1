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
        public static bool IsPoint(this MouseEventArgs e, List<Point> points)
        {
            foreach(var p in points)
            {
                if (e.X >= p.x - CONST.pointHalf && e.X <= p.x + CONST.pointHalf && e.Y >= p.y - CONST.pointHalf && e.Y <= p.y + CONST.pointHalf)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsFirstPoint(this MouseEventArgs e, List<Point> points)
        {
            if (points != null && points.Count > 0)
            {
                var p = points.FirstOrDefault();
                if (e.X >= p.x - CONST.pointHalf && e.X <= p.x + CONST.pointHalf && e.Y >= p.y - CONST.pointHalf && e.Y <= p.y + CONST.pointHalf)
                {
                    return true;
                }
            }
            return false;
        }

        public static int GetPointIndex(this List<Point> list, Point point)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var p = list[i];
                if (point.x >= p.x - CONST.pointHalf && point.x <= p.x + CONST.pointHalf && point.y >= p.y - CONST.pointHalf && point.y <= p.y + CONST.pointHalf)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
