using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafika_Komputerowa1.Draw
{
    public class WU
    {
        private double x0, y0, x1, y1;
        private Color foreColor;
        Graphics g;

        public WU(double x0, double y0, double x1, double y1, Color color, Graphics g)
        {
            this.g = g;
            this.x0 = x0;
            this.y0 = y0;
            this.y1 = y1;
            this.x1 = x1;
            this.foreColor = color;
        }

        private void PaintPixel(double x, double y, double c)
        {
            int alpha = (int)(c * 255);
            if (alpha > 255) alpha = 255;
            if (alpha < 0) alpha = 0;
            Color color = Color.FromArgb(alpha, foreColor);
            SolidBrush brush = new SolidBrush(color);
            g.FillRectangle(brush, (int)x, (int)y, 1, 1);
        }

        double Floor(double x)
        {
            if (x < 0) return (1 - (x - Math.Floor(x)));
            return (x - Math.Floor(x));
        }

        public Graphics Draw()
        {
            bool nextStep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            double temp;
            if (nextStep)
            {
                temp = x0;
                x0 = y0;
                y0 = temp;
                temp = x1;
                x1 = y1;
                y1 = temp;
            }
            if (x0 > x1)
            {
                temp = x0;
                x0 = x1;
                x1 = temp;
                temp = y0;
                y0 = y1;
                y1 = temp;
            }

            double dx = x1 - x0;
            double dy = y1 - y0;
            double gradient = dy / dx;
            double xEnd = (int)(x0);
            double yEnd = y0 + gradient * (xEnd - x0);
            double xGap = 1 - Floor(x0);
            double xPixel1 = xEnd;
            double yPixel1 = (int)(yEnd);

            if (nextStep)
            {
                PaintPixel(yPixel1, xPixel1, 1 - Floor(yEnd) * xGap);
                PaintPixel(yPixel1 + 1, xPixel1, Floor(yEnd) * xGap);
            }
            else
            {
                PaintPixel(xPixel1, yPixel1, 1 - Floor(yEnd) * xGap);
                PaintPixel(xPixel1, yPixel1 + 1, Floor(yEnd) * xGap);
            }

            double intery = yEnd + gradient;
            xEnd = (int)(x1);
            yEnd = y1 + gradient * (xEnd - x1);
            xGap = Floor(x1 + 0.5);
            double xPixel2 = xEnd;
            int yPixel2 = (int)(yEnd);
            if (nextStep)
            {
                PaintPixel(yPixel2, xPixel2, 1 - Floor(yEnd) * xGap);
                PaintPixel(yPixel2 + 1, xPixel2, Floor(yEnd) * xGap);
            }
            else
            {
                PaintPixel(xPixel2, yPixel2, 1 - Floor(yEnd) * xGap);
                PaintPixel(xPixel2, yPixel2 + 1, Floor(yEnd) * xGap);
            }

            if (nextStep)
            {
                for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                {
                    PaintPixel((int)(intery), x, 1 - Floor(intery));
                    PaintPixel((int)(intery) + 1, x, Floor(intery));
                    intery += gradient;
                }
            }
            else
            {
                for (int x = (int)(xPixel1 + 1); x <= xPixel2 - 1; x++)
                {
                    PaintPixel(x, (int)(intery), 1 - Floor(intery));
                    PaintPixel(x, (int)(intery) + 1, Floor(intery));
                    intery += gradient;
                }
            }
            return g;
        }
    }
}
