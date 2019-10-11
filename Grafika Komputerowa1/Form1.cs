using Grafika_Komputerowa1.Constans;
using Grafika_Komputerowa1.Draw;
using Grafika_Komputerowa1.Extentions;
using Grafika_Komputerowa1.Models;
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
        public ToolStripChoice stripChoice;
        public CollectionFigure collection;
        public Vertice clickedPoint;
        public bool isMoving;
        public Form1()
        {
            stripChoice = ToolStripChoice.DrawFigure;
            collection = new CollectionFigure();
            isMoving = false;
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
                if (stripChoice == ToolStripChoice.DrawFigure)
                {
                    Figure fig = collection.GetExtendingFigure();
                    fig.AddPoint(new Vertice(e.X, e.Y));
                }
                
            }
           // pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(stripChoice == ToolStripChoice.MoveVertice)
            {
                if(isMoving)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Figure fig = collection.GetFigure(clickedPoint);
                        fig.MovePoint(clickedPoint, new Vertice(e.X, e.Y));
                    }
                }

                if (e.IsPoint(collection))
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }
            }
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

            foreach (var fig in collection.figures)
            {
                foreach (var p in fig.points)
                {
                    g.FillRectangle(blackBrush, p.x - CONST.pointHalf, p.y - CONST.pointHalf, CONST.pointSize, CONST.pointSize);
                }
                foreach (var edge in fig.edges)
                {
                    g.BrezenhamAlgorithm(edge.Start, edge.End, pen);
                }
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (stripChoice == ToolStripChoice.MoveVertice)
            {
                Vertice p = collection.GetPoint(new Vertice(e.X, e.Y));
                if (p != null)
                {
                    clickedPoint = p;
                    isMoving = true;
                }
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            stripChoice = ToolStripChoice.DrawFigure;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            stripChoice = ToolStripChoice.DrawFigure;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.MoveVertice;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.MoveVertice;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.MoveEdge;
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.MoveEdge;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.MoveFigure;
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.MoveFigure;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.AddRelation;
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.AddRelation;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.AddPoint;
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.AddPoint;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.RemovePoint;
        }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.RemovePoint;
        }
    }
}
