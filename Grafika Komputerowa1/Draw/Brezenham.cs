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
            //g.DrawLine(pen, start.x, start.y, end.x, end.y);
            SolidBrush brush = new SolidBrush(Color.Black);
            //g.SymmetricLine(brush, start.x, start.y, end.x, end.y);

            //if (Math.Abs(end.x - start.x) <= 1)
            //{
            //    int yStart = Math.Min(start.y, end.y);
            //    int yEnd = Math.Max(start.y, end.y);
            //    int x = end.x;
            //    for(int y = yStart; y <= yEnd; y++ )
            //    {
            //        g.FillRectangle(brush, x, y, 1, 1);
            //    }
            //}
            //else
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
                    //g.MidpointLineS_SE(brush, start.x, start.y, end.x, end.y);
                    //g.MidpointLineS_SE(brush, end.x, end.y, start.x, start.y);
                }
            }

        }
        
        static void MidpointLineN_NE(this Graphics g, Brush brush, int x1, int y1, int x2, int y2)
        {
            int dy = x2 - x1;
            int dx = y2 - y1;
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
            int incrNE = 2 * (dy-dx);//increment used for move to NE
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
                    y--;
                }
                else//chooseNE
                {
                    d += incrNE;
                    x++;
                    y--;
                }
                g.FillRectangle(brush, x, y, 1, 1);
            }
        }
    }
}
