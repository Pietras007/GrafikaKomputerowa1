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
        public static bool IsPoint(this MouseEventArgs e, List<(int,int)> points)
        {
            foreach(var p in points)
            {
                if (e.X >= p.Item1 - CONST.pointHalf && e.X <= p.Item1 + CONST.pointHalf && e.Y >= p.Item2 - CONST.pointHalf && e.Y <= p.Item2 + CONST.pointHalf)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsFirstPoint(this MouseEventArgs e, List<(int, int)> points)
        {
            if (points != null && points.Count > 0)
            {
                var p = points.FirstOrDefault();
                if (e.X >= p.Item1 - CONST.pointHalf && e.X <= p.Item1 + CONST.pointHalf && e.Y >= p.Item2 - CONST.pointHalf && e.Y <= p.Item2 + CONST.pointHalf)
                {
                    return true;
                }
            }
            return false;
        }

        public static void MovePoint(this List<(int,int)> list, int x, int y, int ex, int ey)
        {
            for(int i=0;i<list.Count;i++)
            {
                var p = list[i];
                if (x >= p.Item1 - CONST.pointHalf && x <= p.Item1 + CONST.pointHalf && y >= p.Item2 - CONST.pointHalf && y <= p.Item2 + CONST.pointHalf)
                {
                    p.Item1 = ex;
                    p.Item2 = ey;
                }
            }
        }
    }
}
