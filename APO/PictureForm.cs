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
    public partial class PictureForm : Form
    {
        private int[] histogram;
        public Bitmap bitmap;

        public PictureForm()
        {
            InitializeComponent();
        }

        public PictureForm(PictureForm picture)
        {
            InitializeComponent();

            bitmap = new Bitmap(picture.bitmap);
            pictureBox1.Image = bitmap;

            int height = bitmap.Size.Height;
            int width = bitmap.Size.Width;
            pictureBox1.Width = width * 420 / height;
        }

        public void loadImage(String path)
        {
            bitmap = CreateNonIndexedImage(new Bitmap(path));

            pictureBox1.Image = bitmap;

            int height = bitmap.Size.Height;
            int width = bitmap.Size.Width;
            pictureBox1.Width = width * 420 / height;
        }

        public Bitmap CreateNonIndexedImage(Image src)
        {
            Bitmap newBMP = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);
            Graphics gfx = Graphics.FromImage(newBMP);
            gfx.DrawImage(src, 0, 0);
            return newBMP;
        }

        public void refresh()
        {
            pictureBox1.Refresh();
            drawHistogram();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
            histogram = GetHistogram(bitmap);
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

        public void negacja()
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);

                    bitmap.SetPixel(j, i, Color.FromArgb(255 - c.R, 255 - c.R, 255 - c.R));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void progowanie(int prog)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);

                    int color = (c.R < prog ? 0 : 255);

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void redukcjaPoziomowSzarosci(int poziomy)
        {
            double prog = 256 / (poziomy-1);

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);

                    int color = 255;

                    for (int k = 0; k < poziomy-1; k++)
                    {
                        if (c.R < prog * (k + 0.5))
                        {
                            color = (int)prog * k;
                            break;
                        }
                    }

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void rozciaganie(int start, int koniec)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    int color = 0;

                    if (c.R >= start && c.R <= koniec)
                    {
                        color = 255 - (255 * (koniec - c.R) / (koniec - start));                        
                    }
                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void jasnosc(int procent)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);

                    int color = c.R + (255 * procent / 100);
                    if (color > 255) color = 255;
                    else if (color < 0) color = 0;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void kontrast(int procent)
        {
            if (procent == 0) return;
            double kontrast = Math.Pow(((100.0 + (double)procent) / 100.0), 2.0);
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    double col = c.R / 255.0;
                    col -= 0.5;
                    col *= kontrast;
                    col += 0.5;
                    col *= 255.0;

                    if (col > 255) col = 255.0;
                    else if (col < 0) col = 0.0;

                    int color = (int)col;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void gamma(double gamma)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);

                    double col = 255.0 * Math.Pow((c.R / 255.0), (1.0 / gamma));
                    int color = (int)col;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void add(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = (c.R + d.R) / 2;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void sub(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = Math.Abs(c.R - d.R);

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void and(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = c.R & d.R;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void or(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = c.R | d.R;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void xor(Bitmap bitmap2)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);
                    Color d = bitmap2.GetPixel(j, i);

                    int color = c.R ^ d.R;

                    bitmap.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
            pictureBox1.Refresh();
            drawHistogram();
        }
    }
}
