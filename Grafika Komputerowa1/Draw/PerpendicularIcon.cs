using Grafika_Komputerowa1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Draw
{
    public static class PerpendicularIcon
    {
        public static void PaintPerpendicularIcon(this Graphics g, Edge edge, SolidBrush backgroundBrush, SolidBrush centreBrush, Font font)
        {
            int x = (edge.Start.x + edge.End.x) / 2 + CONST.distanceRelationIcon;
            int y = (edge.Start.y + edge.End.y) / 2;
            g.FillRectangle(backgroundBrush, x, y, CONST.sizeRelationIcon, CONST.sizeRelationIcon);
            g.FillRectangle(centreBrush, x + CONST.sizeRelationIcon / 2 - 1, y + 3, CONST.sizeRelationIcon / 2 - 4, CONST.sizeRelationIcon - 6);
            g.FillRectangle(centreBrush, x + 3, y + CONST.sizeRelationIcon / 2 + 2, CONST.sizeRelationIcon - 6, CONST.sizeRelationIcon / 2 - 4);
            g.DrawString(edge.relationNumber.ToString(), font, backgroundBrush, x + CONST.sizeRelationIcon, y + CONST.sizeRelationIcon);
        }
    }
}
