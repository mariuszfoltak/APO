using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace APO
{
    public partial class UOPDialog : Form
    {
        private List<Point> points = new List<Point>();

        class PointComparer : IComparer<Point>
        {
            public int Compare(Point x, Point y)
            {
                if (x.X != y.X)
                {
                    return x.X - y.X;
                }
                else
                {
                    return x.Y - y.Y;
                }
            }
        }

        public UOPDialog()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphicsObj = panel1.CreateGraphics();
            Pen myPen = new Pen(System.Drawing.Color.Black, 1);

            graphicsObj.Clear(panel1.BackColor);

            graphicsObj.DrawLine(myPen, 0, 250, 250, 0);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Right)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].X - 3 < me.X && points[i].X + 3 > me.X)
                        if (points[i].Y - 3 < me.Y && points[i].Y + 3 > me.Y)
                        {
                            points.Remove(points[i]);
                            break;
                        }
                }
            }
            else
            {
                points.Add(new Point(((MouseEventArgs)e).X, ((MouseEventArgs)e).Y));
                points.Sort(new PointComparer());
            }
            Point a = new Point(0,250);

            Graphics graphicsObj = panel1.CreateGraphics();
            Pen myPen = new Pen(System.Drawing.Color.Black, 1);
            
            graphicsObj.Clear(panel1.BackColor);

            for (int i = 0; i < points.Count; i++)
            {
                graphicsObj.DrawLine(myPen, a, points[i]);
                a = points[i];
                //graphicsObj.DrawRectangle(myPen, new Rectangle(a, new Size(2, 2)));
                graphicsObj.FillRectangle(Brushes.Black, new Rectangle(a.X - 2, a.Y - 2, 5, 5));
            }
            graphicsObj.DrawLine(myPen, a, new Point(250,0));
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X - 3 < e.X && points[i].X + 3 > e.X)
                    if (points[i].Y - 3 < e.Y && points[i].Y + 3 > e.Y)
                    {
                        points.Remove(points[i]);
                        label1.Text = "Udało się!";
                        return;
                    }
            }
        }
    }
}
