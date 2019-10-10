using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Draw
{
    public static class Brezenham
    {
        public static void BrezenhamAlgorithm(this Graphics g, Models.Vertice start, Models.Vertice end, Pen pen)
        {
            g.DrawLine(pen, start.x, start.y, end.x, end.y);
        }
    }
}
