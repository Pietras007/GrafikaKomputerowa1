using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grafika_Komputerowa1
{
    public partial class Form1 : Form
    {
        List<(int, int)> points = new List<(int, int)>();
        Bitmap map;
        int x;
        int y;
        int setx;
        int sety;
        bool isOnPoint = false;
        public Form1()
        {
            InitializeComponent();
            map = new Bitmap(CONST.bitmapX, CONST.bitmapY);
            int width = map.Width;
            int height = map.Height;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    map.SetPixel(i, j, Color.White);
                }
            }
            pictureBox1.Image = map;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs eventargs)
        {
            MouseEventArgs e = (MouseEventArgs)eventargs;
            if (e.Button == MouseButtons.Left)
            {
                if (!e.IsPoint(points))
                {
                    SolidBrush brush = new SolidBrush(Color.Black);
                    using (Graphics g = Graphics.FromImage(map))
                    {
                        g.FillRectangle(brush, e.X- CONST.pointHalf, e.Y- CONST.pointHalf, CONST.pointSize, CONST.pointSize);
                        points.Add((e.X, e.Y));
                    }
                    pictureBox1.Image = map;
                }
                else
                {
                    if(e.IsFirstPoint(points))
                    {
                        points.Add((e.X, e.Y));
                    }
                    else
                    {
                        isOnPoint = true;
                        setx = e.X;
                        sety = e.Y;
                    }
                }
            }
            Paint();
        }

        private void Paint()
        {
            if (points.Count > 1)
            {
                for (int i = 0; i + 1 < points.Count; i++)
                {
                    DrawLine draw = new DrawLine(map);
                    pictureBox1.Image = draw.BrezenhamAlgorithm(points[i].Item1, points[i].Item2, points[i + 1].Item1, points[i + 1].Item2);
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            //if(e.Button == Mouse)
            if(isOnPoint)
            {
                points.MovePoint(setx, sety, x, y);
            }
            Paint();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isOnPoint = false;
        }
    }
}
