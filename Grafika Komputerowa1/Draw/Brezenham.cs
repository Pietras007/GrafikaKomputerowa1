using Grafika_Komputerowa1.Constans;
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
        public static void BrezenhamAlgorithm(this Graphics g, Models.Vertice start, Models.Vertice end, Pen pen, Brush brush, StylePainting stylePainting)
        {
            if (stylePainting == StylePainting.DrawLine)
            {
                g.DrawLine(pen, start.x, start.y, end.x, end.y);
            }
            else if (stylePainting == StylePainting.Brezenham)
            {
                double a = (double)(end.y - start.y) / (end.x - start.x);
                if (a > 1)
                {
                    g.MidpointLineN_NE(brush, start.x, start.y, end.x, end.y);
                    g.MidpointLineN_NE(brush, end.x, end.y, start.x, start.y);
                }
                else if (a <= 1 && a > 0)
                {
                    g.MidpointLineE_NE(brush, start.x, start.y, end.x, end.y);
                    g.MidpointLineE_NE(brush, end.x, end.y, start.x, start.y);
                }
                else if (a <= 0 && a >= -1)
                {
                    g.MidpointLineE_SE(brush, start.x, start.y, end.x, end.y);
                    g.MidpointLineE_SE(brush, end.x, end.y, start.x, start.y);
                }
                else
                {
                    g.MidpointLineS_SE(brush, start.x, start.y, end.x, end.y);
                    g.MidpointLineS_SE(brush, end.x, end.y, start.x, start.y);
                }
            }
            else if (stylePainting == StylePainting.Symmetric)
            {
                double a = (double)(end.y - start.y) / (end.x - start.x);
                if (a > 1)
                {
                    g.SymmetricLineN_NE(brush, start.x, start.y, end.x, end.y);
                    g.SymmetricLineN_NE(brush, end.x, end.y, start.x, start.y);
                }
                else if (a <= 1 && a > 0)
                {
                    g.SymmetricLineE_NE(brush, start.x, start.y, end.x, end.y);
                    g.SymmetricLineE_NE(brush, end.x, end.y, start.x, start.y);
                }
                else if (a <= 0 && a >= -1)
                {
                    g.SymmetricLineE_SE(brush, start.x, start.y, end.x, end.y);
                    g.SymmetricLineE_SE(brush, end.x, end.y, start.x, start.y);
                }
                else
                {
                    g.SymmetricLineS_SE(brush, start.x, start.y, end.x, end.y);
                    g.SymmetricLineS_SE(brush, end.x, end.y, start.x, start.y);
                }
            }
            else if (stylePainting == StylePainting.WU)
            {
                //Rozwiazanie zaczerpniete z https://rosettacode.org/wiki/Xiaolin_Wu%27s_line_algorithm?fbclid=IwAR3NZV-yyZdm4uB8sM76pNIRu-bZZbcsdD7TMNKLknokcgbq8pjbtyu3OWA#C.23
                WU wu = new WU(start.x, start.y, end.x, end.y, Color.Black, g);
                g = wu.Draw();
            }

        }

        static void SymmetricLineN_NE(this Graphics g, Brush brush, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int incrE = 2 * dx;
            int incrNE = 2 * (dx - dy);
            int d = 2 * dx - dy;
            int xf = x1;
            int yf = y1;
            int xb = x2;
            int yb = y2;
            g.FillRectangle(brush, xf, yf, 1, 1);
            g.FillRectangle(brush, xb, yb, 1, 1);
            while (yf < yb)
            {
                yf++;
                yb--;
                if (d < 0) //Choose E and W
                    d += incrE;
                else //Choose NE and SW
                {
                    d += incrNE;
                    xf++;
                    xb--;
                }
                g.FillRectangle(brush, xf, yf, 1, 1);
                g.FillRectangle(brush, xb, yb, 1, 1);
            }
        }

        static void SymmetricLineE_NE(this Graphics g, Brush brush, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int incrE = 2 * dy;
            int incrNE = 2 * (dy - dx);
            int d = 2 * dy - dx;
            int xf = x1;
            int yf = y1;
            int xb = x2;
            int yb = y2;
            g.FillRectangle(brush, xf, yf, 1, 1);
            g.FillRectangle(brush, xb, yb, 1, 1);
            while (xf < xb)
            {
                xf++;
                xb--;
                if (d < 0) //Choose E and W
                {
                    d += incrE;
                }
                else //Choose NE and SW
                {
                    d += incrNE;
                    yf++;
                    yb--;
                }
                g.FillRectangle(brush, xf, yf, 1, 1);
                g.FillRectangle(brush, xb, yb, 1, 1);
            }
        }

        static void SymmetricLineE_SE(this Graphics g, Brush brush, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y1 - y2;
            int incrE = 2 * dy;
            int incrNE = 2 * (dy - dx);
            int d = 2 * dy - dx;
            int xf = x1;
            int yf = y1;
            int xb = x2;
            int yb = y2;
            g.FillRectangle(brush, xf, yf, 1, 1);
            g.FillRectangle(brush, xb, yb, 1, 1);
            while (xf < xb)
            {
                xf++;
                xb--;
                if (d < 0) //Choose E and W
                {
                    d += incrE;
                }
                else //Choose NE and SW
                {
                    d += incrNE;
                    yf--;
                    yb++;
                }
                g.FillRectangle(brush, xf, yf, 1, 1);
                g.FillRectangle(brush, xb, yb, 1, 1);
            }
        }

        static void SymmetricLineS_SE(this Graphics g, Brush brush, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y1 - y2;
            int incrE = 2 * dx;
            int incrNE = 2 * (dx - dy);
            int d = 2 * dx - dy;
            int xf = x1;
            int yf = y1;
            int xb = x2;
            int yb = y2;
            g.FillRectangle(brush, xf, yf, 1, 1);
            g.FillRectangle(brush, xb, yb, 1, 1);
            while (yb < yf)
            {
                yf--;
                yb++;
                if (d < 0) //Choose E and W
                {
                    d += incrE;
                }
                   
                else //Choose NE and SW
                {
                    d += incrNE;
                    xf++;
                    xb--;
                }
                g.FillRectangle(brush, xf, yf, 1, 1);
                g.FillRectangle(brush, xb, yb, 1, 1);
            }
        }


        static void MidpointLineN_NE(this Graphics g, Brush brush, int x1, int y1, int x2, int y2)
        {
            int dy = x2 - x1;
            int dx = y2 - y1;
            int d = 2 * dy - dx; //initial value of d 
            int incrN = 2 * dy; //increment used for move to E
            int incrNE = 2 * (dy - dx);//increment used for move to NE
            int x = x1;
            int y = y1;
            g.FillRectangle(brush, x, y, 1, 1);
            while (y < y2)
            {
                if (d < 0) //chooseE 
                {
                    d += incrN;
                    y++;
                }
                else//chooseNE
                {
                    d += incrNE;
                    x++;
                    y++;
                }
                g.FillRectangle(brush, x, y, 1, 1);
            }
        }

        static void MidpointLineE_NE(this Graphics g, Brush brush, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;
            int d = 2 * dy - dx; //initial value of d 
            int incrE = 2 * dy; //increment used for move to E
            int incrNE = 2 * (dy - dx);//increment used for move to NE
            int x = x1;
            int y = y1;
            g.FillRectangle(brush, x, y, 1, 1);
            while (x < x2)
            {
                if (d < 0) //chooseE 
                {
                    d += incrE;
                    x++;
                }
                else//chooseNE
                {
                    d += incrNE;
                    x++;
                    y++;
                }
                g.FillRectangle(brush, x, y, 1, 1);
            }
        }

        static void MidpointLineE_SE(this Graphics g, Brush brush, int x1, int y1, int x2, int y2)
        {
            int dx = x2 - x1;
            int dy = y1 - y2;
            int d = 2 * dy - dx; //initial value of d 
            int incrE = 2 * dy; //increment used for move to E
            int incrSE = 2 * (dy - dx);//increment used for move to NE
            int x = x1;
            int y = y1;
            g.FillRectangle(brush, x, y, 1, 1);
            while (x < x2)
            {
                if (d < 0) //chooseE 
                {
                    d += incrE;
                    x++;
                }
                else//chooseNE
                {
                    d += incrSE;
                    x++;
                    y--;
                }
                g.FillRectangle(brush, x, y, 1, 1);
            }
        }

        static void MidpointLineS_SE(this Graphics g, Brush brush, int x1, int y1, int x2, int y2)
        {
            int dy = x2 - x1;
            int dx = y1 - y2;
            int d = 2 * dy - dx; //initial value of d 
            int incrS = 2 * dy; //increment used for move to E
            int incrSE = 2 * (dy - dx);//increment used for move to NE
            int x = x1;
            int y = y1;
            g.FillRectangle(brush, x, y, 1, 1);
            while (y2 < y)
            {
                if (d < 0) //chooseE 
                {
                    d += incrS;
                    y--;
                }
                else//chooseNE
                {
                    d += incrSE;
                    x++;
                    y--;
                }
                g.FillRectangle(brush, x, y, 1, 1);
            }
        }
    }
}
