using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Komputerowa1
{
    public static class DrawLine
    {
        public async static void BrezenhamAlgorithm(this Graphics g, Point start, Point end, Pen pen)
        {
            g.DrawLine(pen, start.x, start.y, end.x, end.y);
        }
    }
}
