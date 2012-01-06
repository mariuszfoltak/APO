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

        public void progowanie(int progDown, int progUp)
        {
            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color c = bitmap.GetPixel(j, i);

                    int color = ((c.R < progDown || c.R > progUp) ? 255 : 0);

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

        public void ApplyMask(int[,] mask, int divisor)
        {
            FastBitmap bmp = new FastBitmap(bitmap);
            FastBitmap bitmap2 = new FastBitmap(bitmap);

            if (divisor == 0) divisor = 1;

            int size = (int)Math.Sqrt(mask.Length);
            Point[,] temp = new Point[size, size];

            int index = size - size / 2 - 1;
            for (int i = -index; i <= index; i++)
                for (int j = -index; j <= index; j++)
                    temp[i + index, j + index] = new Point(i, j);

            for (int x = index; x < bmp.Width - index; x++)
            {
                for (int y = index; y < bmp.Height - index; y++)
                {
                    int newColor = 0;
                    for (int k = 0; k < size; k++)
                    {
                        for (int l = 0; l < size; l++)
                        {
                            byte oldColor = bmp[x + temp[k, l].X, y + temp[k, l].Y];
                            newColor += mask[k, l] * oldColor;
                        }
                    }
                    newColor /= divisor;

                    // Skalowanie: Metoda 3
                    if (newColor > bitmap2.Levels - 1) newColor = bitmap2.Levels - 1;
                    else if (newColor < 0) newColor = 0;

                    bitmap2[x, y] = (byte)newColor;
                }
            }

            bitmap2.Unlock();
            bitmap = bitmap2.Bitmap;
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void FiltracjaMedianowa(int value)
        {
            FastBitmap bmp = new FastBitmap(bitmap);

            int filterSize = value;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    byte[] neighbours = new byte[filterSize * filterSize];
                    int a = 0;
                    for (int k = -filterSize / 2; k <= filterSize / 2; ++k)
                        for (int l = -filterSize / 2; l <= filterSize / 2; ++l)
                            neighbours[a++] = bmp[x + k, y + l];
                                        
                    Array.Sort(neighbours);
                    if (neighbours.Length % 2 == 1)
                        bmp[x, y] = neighbours[neighbours.Length / 2];
                    else
                        bmp[x, y] = (byte)Math.Min((neighbours[neighbours.Length / 2] + neighbours[(neighbours.Length / 2) + 1]) / 2, bmp.Levels - 1);
                }
            }
            bmp.Unlock();
            bitmap = bmp.Bitmap;
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            drawHistogram();

        }
        public void Szkieletyzacja()
        {
            FastBitmap bmp = new FastBitmap(bitmap);

            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { 1, 1, 0, -1, -1, -1, 0, 1 };

            bool[,] img = new bool[bmp.Width, bmp.Height];
            int W = bmp.Width;
            int H = bmp.Height;
            for (int i = 0; i < bmp.Width; ++i)
            {
                for (int j = 0; j < bmp.Height; ++j)
                {
                    img[i, j] = bmp[i, j] < 128;
                }
            }


            bool pass = false;
            LinkedList<Point> list;
            do
            {
                pass = !pass;
                list = new LinkedList<Point>();

                for (int x = 1; x < W - 1; ++x)
                {
                    for (int y = 1; y < H - 1; ++y)
                    {
                        if (img[x, y])
                        {
                            int cnt = 0;
                            int hm = 0;
                            bool prev = img[x - 1, y + 1];
                            for (int i = 0; i < 8; ++i)
                            {
                                bool cur = img[x + dx[i], y + dy[i]];
                                hm += cur ? 1 : 0;
                                if (prev && !cur) ++cnt;
                                prev = cur;
                            }
                            if (hm > 1 && hm < 7 && cnt == 1)
                            {
                                if (pass && (!img[x + 1, y] || !img[x, y + 1] || !img[x, y - 1] && !img[x - 1, y]))
                                {
                                    list.AddLast(new Point(x, y));
                                }
                                if (!pass && (!img[x - 1, y] || !img[x, y - 1] || !img[x, y + 1] && !img[x + 1, y]))
                                {
                                    list.AddLast(new Point(x, y));
                                }
                            }
                        }

                    }
                }
                foreach (Point p in list)
                {
                    img[p.X, p.Y] = false;
                }
            } while (list.Count != 0);

            for (int x = 0; x < W; ++x)
            {
                for (int y = 0; y < H; ++y)
                {
                    bmp[x, y] = (byte)(img[x, y] ? 0 : bmp.Levels - 1);
                }
            }

            bmp.Unlock();
            bitmap = bmp.Bitmap;
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            drawHistogram();
        }
        public void Erozja(int spojnosc)
        {
            FastBitmap bmp = new FastBitmap(bitmap);

            int i, j, pam;
            int[,] erode = new int[bmp.Width, bmp.Height];
            int[,] tab = new int[bmp.Width, bmp.Height];

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    tab[x, y] = bmp[x, y];

            for (i = 1; i < bmp.Height - 1; i++)
            {
                for (j = 1; j < bmp.Width - 1; j++)
                {
                    pam = tab[j, i];

                    if (spojnosc == 4)
                    {
                        if (pam > tab[j + 1, i]) pam = tab[j + 1, i];
                        if (pam > tab[j + 1, i + 1]) pam = tab[j + 1, i + 1];
                        if (pam > tab[j, i + 1]) pam = tab[j, i + 1];
                        if (pam > tab[j - 1, i + 1]) pam = tab[j - 1, i + 1];
                        if (pam > tab[j - 1, i]) pam = tab[j - 1, i];
                        if (pam > tab[j - 1, i - 1]) pam = tab[j - 1, i - 1];
                        if (pam > tab[j, i - 1]) pam = tab[j, i - 1];
                        if (pam > tab[j + 1, i - 1]) pam = tab[j + 1, i - 1];
                    }
                    else if (spojnosc == 8)
                    {
                        if (pam > tab[j + 1, i]) pam = tab[j + 1, i];
                        if (pam > tab[j, i + 1]) pam = tab[j, i + 1];
                        if (pam > tab[j - 1, i]) pam = tab[j - 1, i];
                        if (pam > tab[j, i - 1]) pam = tab[j, i - 1];
                    }

                    erode[j, i] = pam;
                }
            }

            for (i = 0; i < bmp.Height; i++)
                for (j = 0; j < bmp.Width; j++)
                    bmp[j, i] = (byte)erode[j, i];

            bmp.Unlock();
            bitmap = bmp.Bitmap;
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void Dylatacja(int spojnosc)
        {
            FastBitmap bmp = new FastBitmap(bitmap);

            int i, j, pam;
            int[,] dilate = new int[bmp.Width, bmp.Height];
            int[,] tab = new int[bmp.Width, bmp.Height];

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                    tab[x, y] = bmp[x, y];

            for (i = 1; i < bmp.Height - 1; i++)
            {
                for (j = 1; j < bmp.Width - 1; j++)
                {
                    pam = tab[j, i];

                    if (spojnosc == 4)
                    {
                        if (pam <= tab[j + 1, i]) pam = tab[j + 1, i];
                        if (pam <= tab[j + 1, i + 1]) pam = tab[j + 1, i + 1];
                        if (pam <= tab[j, i + 1]) pam = tab[j, i + 1];
                        if (pam <= tab[j - 1, i + 1]) pam = tab[j - 1, i + 1];
                        if (pam <= tab[j - 1, i]) pam = tab[j - 1, i];
                        if (pam <= tab[j - 1, i - 1]) pam = tab[j - 1, i - 1];
                        if (pam <= tab[j, i - 1]) pam = tab[j, i - 1];
                        if (pam <= tab[j + 1, i - 1]) pam = tab[j + 1, i - 1];
                    }
                    else if (spojnosc == 8)
                    {
                        if (pam <= tab[j + 1, i]) pam = tab[j + 1, i];
                        if (pam <= tab[j, i + 1]) pam = tab[j, i + 1];
                        if (pam <= tab[j - 1, i]) pam = tab[j - 1, i];
                        if (pam <= tab[j, i - 1]) pam = tab[j, i - 1];
                    }

                    dilate[j, i] = pam;
                }
            }

            for (i = 0; i < bmp.Height; i++)
                for (j = 0; j < bmp.Width; j++)
                    bmp[j, i] = (byte)dilate[j, i];

            bmp.Unlock();
            bitmap = bmp.Bitmap;
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            drawHistogram();
        }

        public void zolw()
        {
            FastBitmap bmp = new FastBitmap(bitmap);

            int[,] rtab = new int[bmp.Width, bmp.Height];
            int[,] gtab = new int[bmp.Width, bmp.Height];
            int[,] btab = new int[bmp.Width, bmp.Height];

            int i, j;
            for (i = 1; i < bmp.Width - 1; i++)
            {
                for (j = 1; j < bmp.Height - 1; j++)
                {
                    //rtab[i, j] = bmp[i, j].R;
                    //gtab[i, j] = bmp[i, j].G;
                    //btab[i, j] = bmp[i, j].B;
                    rtab[i, j] = bmp[i, j];
                    gtab[i, j] = bmp[i, j];
                    btab[i, j] = bmp[i, j];
                }
            }
            int d = 0;
            int pami = 0, pamj = 0, ja = 0, ia = 0;
            int x, y;
            int[] wynik = new int[bmp.Width * bmp.Height];
            int[] droga = new int[bmp.Width * bmp.Height];
            for (i = 1; i < bmp.Height - 1; i++)
            {
                for (j = 1; j < bmp.Width - 1; j++)
                {
                    if (rtab[j, i] != 0 || gtab[j, i] != 0 || btab[j, i] != 0)
                    {
                        ja = j;
                        ia = i;
                        pamj = j;
                        pami = i;
                        //wynik[bmp.Width * i + j] = 255;
                        wynik[bmp.Width * i + j] = bmp.Levels - 1;
                        goto cont;
                    }
                }
            }
        cont:
            j = pamj;
            i = pami - 1;
            //wynik[bmp.Width * i + j] = 255;
            wynik[bmp.Width * i + j] = bmp.Levels - 1;
            droga[d] = 1;
            do
            {
                x = j - pamj;
                y = i - pami;
                pamj = j;
                pami = i;
                d++;
                if (rtab[j, i] != 0 || gtab[j, i] != 0 || btab[j, i] != 0)
                {
                    if (x == 0 && y == (-1))
                    {
                        j--;
                        droga[d] = 2;
                    }
                    if (x == 1 && y == 0)
                    {
                        i--;
                        droga[d] = 1;
                    }
                    if (x == 0 && y == 1)
                    {
                        j++;
                        droga[d] = 0;
                    }
                    if (x == (-1) && y == 0)
                    {
                        i++;
                        droga[d] = 3;
                    }
                }
                else
                {
                    if (x == 0 && y == (-1))
                    {
                        j++;
                        droga[d] = 0;
                    }
                    if (x == 1 && y == 0)
                    {
                        i++;
                        droga[d] = 3;
                    }
                    if (x == 0 && y == 1)
                    {
                        j--;
                        droga[d] = 2;
                    }
                    if (x == (-1) && y == 0)
                    {
                        i--;
                        droga[d] = 1;
                    }
                }
                //wynik[bmp.Width * i + j] = 255;
                wynik[bmp.Width * i + j] = bmp.Levels - 1;
            }
            while (j != ja || i != ia);
            for (i = 0; i < bmp.Height; i++)
            {
                for (j = 0; j < bmp.Width; j++)
                {
                    //if (wynik[bmp.Width * i + j] == 255)
                    if (wynik[bmp.Width * i + j] == bmp.Levels - 1)
                    {
                        //rtab[j, i] = 255;
                        rtab[j, i] = bmp.Levels / 2;
                        gtab[j, i] = 0;
                        btab[j, i] = 0;
                    }
                }
            }

            for (i = 0; i < bmp.Width; i++)
            {
                for (j = 0; j < bmp.Height; j++)
                {
                    //bmp[i, j] = Color.FromArgb(rtab[i, j], gtab[i, j], btab[i, j]);
                    bmp[i, j] = (byte)rtab[i, j];
                }
            }

            bmp.Unlock();
            bitmap = bmp.Bitmap;
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            drawHistogram();
        }
        public void segmProba (int max_reg, int prog)
	    {         
		    int [] l_region = new int [max_reg];
		    int [] srednia = new int [max_reg];
		    int [] wynik = new int [bitmap.Height*bitmap.Width];

		    long [] suma_region = new long [max_reg];
    		
		    float [] temp = new float [max_reg];

		    float suma=0,max_p;
		    int i, j, a, x, down_min,min;
		    int lacz=0, count_region=1;


		    for(i=0;i<bitmap.Height;i++)
		    {
			    for(j=0;j<bitmap.Width;j++)
			    {
				    wynik[bitmap.Width*i+j]=0;
			    }
		    }

		    for(i=0;i<max_reg;i++)
		    {
			    l_region[i]=0;
			    srednia[i]=0;
			    suma_region[i]=0;
			    temp[i]=0;
		    }


		    for(i=1;i<bitmap.Height-1;i++)
		    {
			    for(j=1;j<bitmap.Width-1;j++)
			    {
				    if(i==1 && j==1)
				    {
					    wynik[bitmap.Width*i+j] = count_region;
					    l_region[count_region-1] = 1;
					    suma_region[count_region-1] = bitmap.GetPixel(j,i).R;
					    srednia[count_region-1] = bitmap.GetPixel(j,i).R;
				    }
				    else
				    {
					    for(x=1;x<=count_region;x++)
					    {
					    down_min =Math.Abs( bitmap.GetPixel(j,i).R - srednia[x-1] );
    						
						    if( down_min == 0 )
						    {
							    temp[x-1] = (float)10/9;
						    }
						    else
						    {
							    temp[x-1] = (float)(1/down_min);
						    }

						    suma = suma + temp[x-1];
					    }

					    min = 1;
					    max_p = temp[0]/suma;

					    for(x=2;x<=count_region;x++)
					    {
						    if( max_p < temp[x-1]/suma )
						    {
							    max_p = temp[x-1]/suma;
							    min = x;
						    }
					    }

					    if( Math.Abs( bitmap.GetPixel(j,i).R - srednia[min-1] ) < prog)
					    {
						    lacz = 1;
					    }

					    if(lacz == 1)
					    {
						    a = min;					

						    wynik[bitmap.Width*i+j] = a;
						    l_region[a-1]++; 
						    suma_region[a-1] = suma_region[a-1] + bitmap.GetPixel(j,i).R;
						    srednia[a-1] = (int) suma_region[a-1]/l_region[a-1];
					    }

					    if( lacz == 0 && count_region < max_reg )
					    {
						    count_region++;						
						    wynik[bitmap.Width*i+j] = count_region;
						    l_region[count_region-1] = 1;
						    suma_region[count_region-1] = bitmap.GetPixel(j,i).R;
						    srednia[count_region-1] = bitmap.GetPixel(j,i).R;
					    }
					    else
					    {
						    lacz = 0;
					    }
    		

					    suma = 0;
				    }
			    }
		    }

    	
		    for(i=0;i<bitmap.Height;i++)
		    {
			    for(j=0;j<bitmap.Width;j++)
			    {
				    for(x=1;x<=count_region;x++)
				    {
					    if( x == wynik[bitmap.Width*i+j] )
					    {					
						     wynik[bitmap.Width*i+j] = srednia[x-1];
					    }
				    }
			    }
		    }

		    for(i=0;i<bitmap.Height;i++)	
		    {
			    for(j=0;j<bitmap.Width;j++)
			    {
				    bitmap.SetPixel(j, i, Color.FromArgb(wynik[bitmap.Width*i+j], wynik[bitmap.Width*i+j],wynik[bitmap.Width*i+j]));
			    }
		    }
    		
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            drawHistogram();
	    }
        public void Voronoi(int n, int r)
        {
            int i, j;
            int[,] V = new int[bitmap.Width, bitmap.Height];

            for (i = 0; i < bitmap.Height; i++)
            {
                for (j = 0; j < bitmap.Width; j++)
                {
                    V[j, i] = 0;
                }
            }
            Color c = new Color();
            Color c2 = new Color(); ;

            Random rand = new Random();

            int[] x = new int[n];
            int[] y = new int[n];

            int[] x_new = new int[n];
            int[] y_new = new int[n];

            int[] sr = new int[n];
            int[] licz = new int[n];

            double odl;

            for (i = 0; i < n; i++)
            {
                x[i] = rand.Next(0, bitmap.Width);
                y[i] = rand.Next(0, bitmap.Height);
                x_new[i] = -1;
                y_new[i] = -1;
                licz[i] = 0;
                sr[i] = 0;
            }

            int a, min = 0;
            double test = 0;

            for (i = 0; i < bitmap.Height; i++)
            {
                for (j = 0; j < bitmap.Width; j++)
                {

                    for (a = 0; a < n; a++)
                    {
                        odl = ((j - x[a]) * (j - x[a])) + ((i - y[a]) * (i - y[a]));
                        odl = (double)Math.Sqrt(odl);

                        if (a == 0)
                        {
                            min = 1;
                            test = odl;
                        }
                        else
                        {
                            if (odl < test)
                            {
                                min = a + 1;
                                test = odl;
                            }
                        }
                    }

                    V[j, i] = min;

                    // znajdowanie nowego miejsca centralnego
                    c = bitmap.GetPixel(j, i);

                    if (x_new[min - 1] == -1 && y_new[min - 1] == -1)
                    {
                        c2 = bitmap.GetPixel(x[min - 1], y[min - 1]);
                    }
                    else
                    {
                        c2 = bitmap.GetPixel(x_new[min - 1], y_new[min - 1]);
                    }

                    // min || max
                    if ((r == 1 && c.R < c2.R) || (r == 2 && c.R > c2.R))
                    {
                        x_new[min - 1] = j;
                        y_new[min - 1] = i;
                    }

                }
            }

            for (a = 0; a < n; a++)
            {
                if (x_new[a] != -1 && y_new[a] != -1)
                {
                    x[a] = x_new[a];
                    y[a] = y_new[a];

                    x_new[a] = -1;
                    y_new[a] = -1;
                }
            }

            // srednia obszaru
            for (i = 0; i < bitmap.Height; i++)
            {
                for (j = 0; j < bitmap.Width; j++)
                {
                    c = bitmap.GetPixel(j, i);

                    sr[(V[j, i] - 1)] += c.R;
                    licz[(V[j, i] - 1)]++;
                }
            }

            for (i = 0; i < n; i++)
            {
                if (licz[i] != 0)
                {
                    sr[i] = sr[i] / licz[i];
                }
            }

            for (i = 0; i < bitmap.Height; i++)
            {
                for (j = 0; j < bitmap.Width; j++)
                {
                    bitmap.SetPixel(j, i, Color.FromArgb(sr[(V[j, i] - 1)], sr[(V[j, i] - 1)], sr[(V[j, i] - 1)]));
                }
            }
            pictureBox1.Image = bitmap;
            pictureBox1.Refresh();
            drawHistogram();
        }
    }
}
