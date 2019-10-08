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
                if (e.X >= p.Item1 - 1 && e.X <= p.Item1 + 1 && e.Y >= p.Item2 - 1 && e.Y <= p.Item2 + 1)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsLastPoint(this MouseEventArgs e, List<(int, int)> points)
        {
            if (points != null && points.Count > 0)
            {
                var p = points.LastOrDefault();
                if (e.X >= p.Item1 - 1 && e.X <= p.Item1 + 1 && e.Y >= p.Item2 - 1 && e.Y <= p.Item2 + 1)
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
                if (x >= p.Item1 - 1 && x <= p.Item1 + 1 && y >= p.Item2 - 1 && y <= p.Item2 + 1)
                {
                    p.Item1 = ex;
                    p.Item2 = ey;
                }
            }
        }
    }
}
