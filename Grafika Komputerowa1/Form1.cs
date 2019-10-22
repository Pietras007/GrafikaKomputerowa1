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
        public Edge clickedEdge;
        public Figure clickedFigure;
        public Vertice clickedPointOnEdge;
        public Vertice clickedPointOnFigure;
        public bool isMoving;
        public static PictureBox pictureBOX;
        List<object> checkedList = new List<object>();

        public Form1()
        {
            stripChoice = ToolStripChoice.DrawFigure;
            collection = new CollectionFigure();
            isMoving = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBOX = pictureBox1;
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

                if (stripChoice == ToolStripChoice.AddPoint)
                {
                    Edge edge = collection.GetEdgeFromPoint(new Vertice(e.X, e.Y));
                    if (edge != null)
                    {
                        Figure fig = collection.GetFigure(edge);
                        fig.AddPointOnEdge(edge);
                    }
                }

                if (stripChoice == ToolStripChoice.RemovePoint)
                {
                    Vertice point = collection.GetPoint(new Vertice(e.X, e.Y));
                    if (point != null)
                    {
                        Figure fig = collection.GetFigure(point);
                        fig.RemovePoint(point);
                    }
                }

                if(stripChoice == ToolStripChoice.AddRelation)
                {
                    Edge edge = collection.GetEdgeFromPoint(new Vertice(e.X, e.Y));
                    if (edge != null)
                    {
                        Figure fig = collection.GetFigure(edge);
                        Edge edge1 = fig.GetSelectedEdge();
                        if (edge1 != null)
                        {
                            var formPopup = new RelationPopup();
                            formPopup.ShowDialog(this);
                            RelationEnum relation = formPopup.GetChoosenRelation();
                            if (relation != RelationEnum.None)
                            {
                                fig.AddRelation(edge, edge1, relation, this);
                                collection.RemoveSelection();
                            }
                        }
                        else
                        {
                            collection.RemoveSelection();
                            edge.SetSelected();
                        }
                    }
                }

                if (stripChoice == ToolStripChoice.RemoveRelation)
                {
                    Edge edge = collection.GetEdgeFromPoint(new Vertice(e.X, e.Y));
                    if (edge != null)
                    {
                        Figure fig = collection.GetFigure(edge);
                        fig.RemoveRelation(edge);
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Invalidate();
            if (stripChoice == ToolStripChoice.DrawFigure)
            {
                if (e.IsStartPoint(collection))
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }
            }

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

            if(stripChoice == ToolStripChoice.MoveEdge)
            {
                Edge edge = collection.GetEdgeFromPoint(new Vertice(e.X, e.Y));
                if (edge != null)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }

                if(isMoving && clickedEdge != null)
                {
                    Figure figure = collection.GetFigure(clickedEdge);
                    int X = e.X - clickedPointOnEdge.x;
                    int Y = e.Y - clickedPointOnEdge.y;
                    clickedPointOnEdge.x = e.X;
                    clickedPointOnEdge.y = e.Y;
                    figure.MoveEdge(clickedEdge, X, Y);
                }
            }

            if(stripChoice == ToolStripChoice.MoveFigure)
            {
                Figure figure = collection.GetFigureFromClickOnBorder(new Vertice(e.X, e.Y));
                if (figure != null)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Cross;
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }

                if (isMoving && clickedFigure != null)
                {
                    int X = e.X - clickedPointOnFigure.x;
                    int Y = e.Y - clickedPointOnFigure.y;
                    clickedPointOnFigure.x = e.X;
                    clickedPointOnFigure.y = e.Y;
                    clickedFigure.MoveFigure(X, Y);
                }
            }

            if(stripChoice == ToolStripChoice.AddPoint)
            {
                Edge edge = collection.GetEdgeFromPoint(new Vertice(e.X, e.Y));
                if (edge != null)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }
            }

            if(stripChoice == ToolStripChoice.RemovePoint)
            {
                if (e.IsPoint(collection))
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }
            }

            if(stripChoice == ToolStripChoice.AddRelation)
            {
                Edge edge = collection.GetEdgeFromPoint(new Vertice(e.X, e.Y));
                if (edge != null)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Hand;
                }
                else
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                }
            }

            if (stripChoice == ToolStripChoice.RemoveRelation)
            {
                Edge edge = collection.GetEdgeFromPoint(new Vertice(e.X, e.Y));
                if (edge != null)
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
            using (SolidBrush whiteBrush = new SolidBrush(Color.White), blackBrush = new SolidBrush(Color.Black), orangeBrush = new SolidBrush(Color.Orange))
            {
                using (Font font = new Font("Verdana", 8))
                {
                    using (Pen blackPen = new Pen(Color.Black), orangePen = new Pen(Color.Orange))
                    {
                        g.FillRectangle(whiteBrush, 0, 0, CONST.bitmapX, CONST.bitmapY);
                        foreach (var fig in collection.figures)
                        {
                            foreach (var edge in fig.edges)
                            {
                                if (edge.relation == RelationEnum.Equal)
                                {
                                    g.PaintEqualIcon(edge, blackBrush, orangeBrush, font);
                                }
                                else if (edge.relation == RelationEnum.Perpendicular)
                                {
                                    g.PaintPerpendicularIcon(edge, blackBrush, orangeBrush, font);
                                }

                                if (edge.isSelected)
                                {
                                    //g.BrezenhamAlgorithm(edge.Start, edge.End, orangeBrush);
                                    g.BrezenhamAlgorithm(edge.Start, edge.End, orangePen, orangeBrush);
                                }
                                else
                                {
                                    //g.BrezenhamAlgorithm(edge.Start, edge.End, blackBrush);
                                    g.BrezenhamAlgorithm(edge.Start, edge.End, blackPen, blackBrush);
                                }
                            }

                            foreach (var p in fig.points)
                            {
                                g.FillRectangle(blackBrush, p.x - CONST.pointHalf, p.y - CONST.pointHalf, CONST.pointSize, CONST.pointSize);
                            }
                        }
                    }
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Invalidate();
            if (stripChoice == ToolStripChoice.MoveVertice)
            {
                Vertice p = collection.GetPoint(new Vertice(e.X, e.Y));
                if (p != null)
                {
                    clickedPoint = p;
                    isMoving = true;
                }
            }

            if (stripChoice == ToolStripChoice.MoveEdge)
            {
                Edge edge = collection.GetEdgeFromPoint(new Vertice(e.X, e.Y));
                if (edge != null)
                {
                    clickedEdge = edge;
                    clickedPointOnEdge = new Vertice(e.X, e.Y);
                    isMoving = true;
                }
            }

            if (stripChoice == ToolStripChoice.MoveFigure)
            {
                Figure figure = collection.GetFigureFromClickOnBorder(new Vertice(e.X, e.Y));
                if(figure != null)
                {
                    clickedFigure = figure;
                    clickedPointOnFigure = new Vertice(e.X, e.Y);
                    isMoving = true;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            Check(sender);
            stripChoice = ToolStripChoice.DrawFigure;
            collection.RemoveSelection();
            pictureBox1.Invalidate();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            Check(sender);
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.MoveVertice;
            collection.RemoveSelection();
            pictureBox1.Invalidate();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            Check(sender);
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.MoveEdge;
            collection.RemoveSelection();
            pictureBox1.Invalidate();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            Check(sender);
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.MoveFigure;
            collection.RemoveSelection();
            pictureBox1.Invalidate();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            Check(sender);
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.AddRelation;
            pictureBox1.Invalidate();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            Check(sender);
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.RemoveRelation;
            pictureBox1.Invalidate();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            Check(sender);
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.AddPoint;
            collection.RemoveSelection();
            pictureBox1.Invalidate();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            Check(sender);
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.RemovePoint;
            collection.RemoveSelection();
            pictureBox1.Invalidate();
        }

        private void Check(object sender)
        {
            if(!checkedList.Contains(sender))
            {
                checkedList.Add(sender);
            }
            ToolStripButton butt = (ToolStripButton)sender;
            butt.Checked = true;
        }

        private void UnCheckAll()
        {
            foreach(var sender in checkedList)
            {
                ToolStripButton butt = (ToolStripButton)sender;
                butt.Checked = false;
            }
        }

        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            collection.figures.Clear();
            pictureBox1.Invalidate();
        }
    }
}
