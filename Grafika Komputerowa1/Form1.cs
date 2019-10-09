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
        List<(Point,Point)> points = new List<(Point, Point)>();
        bool isMoving = false;
        int movingIndex = -1;
        bool allowToAddPoint = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs eventargs)
        {
            MouseEventArgs e = (MouseEventArgs)eventargs;
            if (e.Button == MouseButtons.Left)
            {
                Point current = new Point(e.X, e.Y);
                if (!e.IsPoint(points) && allowToAddPoint)
                {
                    if(points.Count > 0)
                    {
                        points[points.Count - 1] = (points[points.Count - 1].Item1, current);
                    }
                    points.Add((current, null));
                }
                else if(e.IsFirstPoint(points))
                {
                    points[points.Count - 1] = (points[points.Count - 1].Item1, points.FirstOrDefault().Item1);
                    allowToAddPoint = false;
                }
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point current = new Point(e.X, e.Y);
            if (isMoving)
            {
                if (e.Button == MouseButtons.Left)
                {
                    points.MovePoint(movingIndex, current);
                }
            }

            if (e.IsPoint(points))
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
            }
            else
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }

            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Pen pen = new Pen(Color.Black);
            g.FillRectangle(whiteBrush, 0, 0, CONST.bitmapX, CONST.bitmapY);

            foreach (var _p in points)
            {
                var p = _p.Item1;
                g.FillRectangle(blackBrush, p.x - CONST.pointHalf, p.y - CONST.pointHalf, CONST.pointSize, CONST.pointSize);
            }

            if (points.Count > 1)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].Item2 != null)
                    {
                        g.BrezenhamAlgorithm(points[i].Item1, points[i].Item2, pen);
                    }
                }
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point current = new Point(e.X, e.Y);
                if (e.IsPoint(points))
                {
                    movingIndex = points.GetPointIndex(current);
                    isMoving = true;
                }
            }
        }
    }
}
