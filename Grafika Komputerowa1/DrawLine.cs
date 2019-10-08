using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Komputerowa1
{
    public class DrawLine
    {
        Bitmap map;
        public DrawLine(Bitmap map)
        {
            this.map = map;
        }
        public Bitmap BrezenhamAlgorithm(int x, int y, int ex, int ey)
        {
            Pen pen = new Pen(Color.Black);
            using (Graphics g = Graphics.FromImage(map))
            {
                g.DrawLine(pen, x, y, ex, ey);
            }
            return map;
        }
    }
}
