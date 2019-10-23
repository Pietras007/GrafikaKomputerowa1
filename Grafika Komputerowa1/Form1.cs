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
        public StylePainting stylePainting;
        public CollectionFigure collection;
        public Vertice clickedPoint;
        public Edge clickedEdge;
        public Figure clickedFigure;
        public Vertice clickedPointOnEdge;
        public Vertice clickedPointOnFigure;
        public bool isMoving;
        public static PictureBox pictureBOX;
        List<object> checkedList = new List<object>();
        List<object> checkedListStylePainting = new List<object>();

        public Form1()
        {
            stripChoice = ToolStripChoice.DrawFigure;
            stylePainting = StylePainting.Brezenham;
            collection = new CollectionFigure();
            isMoving = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBOX = pictureBox1;
            SampleFigure();
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

                if (stripChoice == ToolStripChoice.RemoveFigure)
                {
                    Figure figure = collection.GetFigureFromClickOnBorder(new Vertice(e.X, e.Y));
                    collection.figures.Remove(figure);
                    pictureBox1.Invalidate();
                }

                if (stripChoice == ToolStripChoice.AddRelation)
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

            if (stripChoice == ToolStripChoice.RemoveFigure)
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
            }

            if (stripChoice == ToolStripChoice.AddPoint)
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
                                    g.BrezenhamAlgorithm(edge.Start, edge.End, orangePen, orangeBrush, stylePainting);
                                }
                                else
                                {
                                    //g.BrezenhamAlgorithm(edge.Start, edge.End, blackBrush);
                                    g.BrezenhamAlgorithm(edge.Start, edge.End, blackPen, blackBrush, stylePainting);
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
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            UnCheckAll();
            Check(sender);
            collection.DeleteUnfinishedFigure();
            stripChoice = ToolStripChoice.RemoveFigure;
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void sampleFigureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleFigure();
        }

        private void SampleFigure()
        {
            collection.figures.Clear();
            pictureBox1.Invalidate();
            Figure figure = new Figure();
            figure.isFull = true;
            Vertice v1 = new Vertice(324, 265);
            Vertice v2 = new Vertice(476, 320);
            Vertice v3 = new Vertice(570, 145);
            Vertice v4 = new Vertice(740, 460);
            Vertice v5 = new Vertice(900, 300);
            Vertice v6 = new Vertice(1190, 600);
            Vertice v7 = new Vertice(950, 430);
            Vertice v8 = new Vertice(650, 530);
            Vertice v9 = new Vertice(250, 630);
            List<Vertice> vertices = new List<Vertice>();
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            vertices.Add(v4);
            vertices.Add(v5);
            vertices.Add(v6);
            vertices.Add(v7);
            vertices.Add(v8);
            vertices.Add(v9);

            Edge e1 = new Edge(v1, v2);
            e1.relation = RelationEnum.Perpendicular;
            e1.relationNumber = 1;
            Edge e2 = new Edge(v2, v3);
            Edge e3 = new Edge(v3, v4);
            e3.relation = RelationEnum.Perpendicular;
            e3.relationNumber = 1;
            Edge e4 = new Edge(v4, v5);
            Edge e5 = new Edge(v5, v6);
            e5.relation = RelationEnum.Equal;
            e5.relationNumber = 2;
            Edge e6 = new Edge(v6, v7);
            Edge e7 = new Edge(v7, v8);
            e7.relation = RelationEnum.Equal;
            e7.relationNumber = 2;
            Edge e8 = new Edge(v8, v9);
            Edge e9 = new Edge(v9, v1);
            List<Edge> edges = new List<Edge>();
            edges.Add(e1);
            edges.Add(e2);
            edges.Add(e3);
            edges.Add(e4);
            edges.Add(e5);
            edges.Add(e6);
            edges.Add(e7);
            edges.Add(e8);
            edges.Add(e9);

            Relation r1 = new Relation(e1, e3, RelationEnum.Perpendicular);
            Relation r2 = new Relation(e5, e7, RelationEnum.Equal);
            List<Relation> relations = new List<Relation>();
            relations.Add(r1);
            relations.Add(r2);

            figure.edges = edges;
            figure.points = vertices;
            figure.ps = relations;
            figure.relationNumber = 2;

            collection.figures.Add(figure);
            figure.KeepRelations(v1);
            pictureBox1.Invalidate();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            UnCheckAllStylePainting();
            CheckStylePainting(sender);
            stylePainting = StylePainting.Brezenham;
            pictureBox1.Invalidate();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            UnCheckAllStylePainting();
            CheckStylePainting(sender);
            stylePainting = StylePainting.DrawLine;
            pictureBox1.Invalidate();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            UnCheckAllStylePainting();
            CheckStylePainting(sender);
            stylePainting = StylePainting.WU;
            pictureBox1.Invalidate();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            UnCheckAllStylePainting();
            CheckStylePainting(sender);
            stylePainting = StylePainting.Symmetric;
            pictureBox1.Invalidate();
        }

        private void CheckStylePainting(object sender)
        {
            if (!checkedListStylePainting.Contains(sender))
            {
                checkedListStylePainting.Add(sender);
            }
            ToolStripButton butt = (ToolStripButton)sender;
            butt.Checked = true;
        }

        private void UnCheckAllStylePainting()
        {
            foreach (var sender in checkedListStylePainting)
            {
                ToolStripButton butt = (ToolStripButton)sender;
                butt.Checked = false;
            }
        }
    }
}
