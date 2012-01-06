using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace APO
{
    public partial class HistDiffForm : Form
    {
        private int[] histogram;
        public Bitmap bitmap;
        FastBitmap bmp;

        public HistDiffForm()
        {
            InitializeComponent();
        }

        public HistDiffForm(PictureForm picture)
        {
            InitializeComponent();

            bitmap = new Bitmap(picture.bitmap);
            pictureBox1.Image = bitmap;

            int height = bitmap.Size.Height;
            int width = bitmap.Size.Width;
            pictureBox1.Width = width * 420 / height;

            bmp = new FastBitmap(bitmap);
            numericUpDownX.Maximum = bmp.Width - 1;
            numericUpDownY.Maximum = bmp.Height - 1;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void refresh()
        {
            pictureBox1.Refresh();
            drawHistogram();
        }

        private int[] GetHistogram(Bitmap picture)
        {
            int[] myHistogram = new int[256];

            BitmapData bmd = picture.LockBits(new Rectangle(0, 0, picture.Size.Width, picture.Size.Height),
                            System.Drawing.Imaging.ImageLockMode.ReadOnly,
                            picture.PixelFormat);

            int PixelSize = 0;
            switch (picture.PixelFormat)
            {
                case PixelFormat.Format32bppArgb:
                    PixelSize = 4;
                    break;
                case PixelFormat.Format24bppRgb:
                    PixelSize = 3;
                    break;
            }

            unsafe
            {
                for (int y = 0; y < bmd.Height; y++)
                {
                    byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);

                    for (int x = 0; x < bmd.Width; x++)
                    {
                        byte color = (byte)((row[x * PixelSize] + row[x * PixelSize + 1] + row[x * PixelSize + 2]) / 3);
                        myHistogram[color]++;
                    }
                }
            }

            picture.UnlockBits(bmd);

            return myHistogram;
        }

        private void drawHistogram()
        {
            int dx = (int)numericUpDownX.Value;
            int dy = (int)numericUpDownY.Value;
            histogram = GrayLevelDiff(bmp, dx, dy, new Point(0, 0), new Point(bmp.Width - 1, bmp.Height - 1));
            Graphics graphicsObj = panel3.CreateGraphics();
            Pen myPen = new Pen(System.Drawing.Color.Black, 1);

            long max = histogram.Max();
            graphicsObj.Clear(panel3.BackColor);
            for (int i = 0; i < 256; i++)
            {
                graphicsObj.DrawLine(myPen, i, 150, i, 150 - histogram[i] * 150 / max); 
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            drawHistogram();
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Text = e.X.ToString();
            label4.Text = histogram[e.X].ToString();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            drawHistogram();
        }

        public static int[] GrayLevelDiff(FastBitmap bmp, int dx, int dy, Point begin, Point end)
        {
            int xbegin, ybegin, xend, yend, difference;
            int[] lh = new int[bmp.Levels];
            int[] hv = new int[bmp.Levels];

            if (dx < 0) xbegin = begin.X - dx;
            else xbegin = begin.X;
            if (dx > 0) xend = end.X - dx;
            else xend = end.X;
            if (dy < 0) ybegin = begin.Y - dy;
            else ybegin = begin.Y;
            if (dy > 0) yend = end.Y - dy;
            else yend = end.Y;

            for (int y = ybegin; y < yend; y++)
            {
                for (int x = xbegin; x < xend; x++)
                {
                    int color1 = bmp[x, y];
                    int color2 = bmp[x + dx, y + dy];
                    difference = Math.Abs(color1 - color2);
                    lh[difference]++;
                }
            }

            for (int i = 0; i < bmp.Levels; i++)
                hv[i] = (int)(lh[i] / ((float)(end.X - begin.X - Math.Abs(dx)) * (float)(end.Y - begin.Y - Math.Abs(dy))) * 1000000);

            return hv;
        }
    }
}
