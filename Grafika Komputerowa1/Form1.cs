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
        List<Point> points = new List<Point>();
        Bitmap map;
        Point clickedOn;
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
                clickedOn = new Point(e.X, e.Y);
                if (!e.IsPoint(points))
                {
                    points.Add(new Point(e.X, e.Y));
                }

            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point current = new Point(e.X, e.Y);
            if (e.IsPoint(points))
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
                if (e.Button == MouseButtons.Left)
                {
                    points.MovePoint(clickedOn, current);
                    clickedOn = current;
                }
            }
            else
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //isOnPoint = false;
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            SolidBrush whiteBrush = new SolidBrush(Color.White);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Pen pen = new Pen(Color.Black);
            g.FillRectangle(whiteBrush, 0, 0, CONST.bitmapX, CONST.bitmapY);

            foreach (var p in points)
            {
                g.FillRectangle(blackBrush, p.x - CONST.pointHalf, p.y - CONST.pointHalf, CONST.pointSize, CONST.pointSize);
            }

            if (points.Count > 1)
            {
                for (int i = 0; i + 1 < points.Count; i++)
                {
                    g.BrezenhamAlgorithm(points[i], points[i + 1], pen);
                }
            }

        }
    }
}
