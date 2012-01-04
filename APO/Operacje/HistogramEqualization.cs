using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace APO.Operacje
{
    class HistogramEqualization : IFilter
    {
        public enum EqualizationMethod
        {
            AverageLevel,
            RandomLevel,
            AdjacencyLevel
        }

        private Bitmap picture;
        private bool m_hasDialog;
        private int[] histogram;
        private EqualizationMethod equalizationMethod;

        public bool hasDialog
        {
            get { return m_hasDialog; }
        }

        public HistogramEqualization()
        {
            m_hasDialog = false;
            equalizationMethod = EqualizationMethod.AverageLevel;
        }

        public HistogramEqualization(EqualizationMethod method)
        {
            m_hasDialog = false;
            equalizationMethod = method;
        }

        public void setImage(System.Drawing.Image image)
        {
            picture = (Bitmap)image;
        }

        public void Convert()
        {
            int[] H = new int[256];
            int[] New, left, right;

            histogram = getHistogram(picture);

            New = countRange(out left, out right);

            switch (equalizationMethod)
            {
                case EqualizationMethod.AverageLevel:
                    redrawImage(New, left, right);
                    break;
                case EqualizationMethod.RandomLevel:
                    redrawImageRandomLevel(New, left, right);
                    break;
                case EqualizationMethod.AdjacencyLevel:
                    redrawImageAdjacencyLevel(left, right);
                    break;
            }
        }

        private int[] getHistogram(Bitmap picture)
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

        private int getAverageHistogram()
        {
            int Havg = 0;

            for (int Z = 0; Z < 256; Z++)
            {
                Havg = Havg + histogram[Z];
            }

            return Havg / 256;
        }

        private int[] countRange(out int[] left, out int[] right)
        {
            int R    = 0, 
                Hint = 0, 
                Havg = getAverageHistogram();

            int[] New   = new int[256];
                  left  = new int[256];
                  right = new int[256];

            for (int Z = 0; Z < 256; Z++)
            {
                left[Z] = R;
                Hint = Hint + histogram[Z];

                while (Hint > Havg)
                {
                    Hint = Hint - Havg;
                    R++;
                }

                right[Z] = R;

                switch (equalizationMethod)
                {
                    case EqualizationMethod.AverageLevel:
                        New[Z] = ((left[Z] + right[Z]) / 2);
                        break;
                    case EqualizationMethod.RandomLevel:
                        New[Z] = (right[Z] - left[Z]);
                        break;
                    case EqualizationMethod.AdjacencyLevel:
                        break;
                }
            }

            return New;
        }

        private void redrawImage(int[] New, int[] left, int[] right)
        {
            for (int i = 0; i < picture.Height; i++)
            {
                for (int j = 0; j < picture.Width; j++)
                {
                    Color c = picture.GetPixel(j, i);

                    if (left[c.R] == right[c.R])
                    {
                        if (left[c.R] <= 255)
                        {
                            picture.SetPixel(j, i, Color.FromArgb(left[c.R], left[c.R], left[c.R]));
                        }
                        else
                        {
                            picture.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                        }

                    }
                    else
                    {
                        if (New[c.R] <= 255)
                        {
                            picture.SetPixel(j, i, Color.FromArgb(New[c.R], New[c.R], New[c.R]));
                        }
                        else
                        {
                            picture.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                        }
                    }
                }
            }
        }

        private void redrawImageRandomLevel(int[] New, int[] left, int[] right)
        {
            Random rand = new Random();
            for (int i = 0; i < picture.Height; i++)
            {
                for (int j = 0; j < picture.Width; j++)
                {
                    Color c = picture.GetPixel(j, i);

                    if (left[c.R] == right[c.R])
                    {
                        if (left[c.R] <= 255)
                        {
                            picture.SetPixel(j, i, Color.FromArgb(left[c.R], left[c.R], left[c.R]));
                        }
                        else
                        {
                            picture.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                        }

                    }
                    else
                    {
                        int x = rand.Next(0, New[c.R]);

                        if (left[c.R] + x <= 255)
                        {
                            picture.SetPixel(j, i, Color.FromArgb(left[c.R] + x, left[c.R] + x, left[c.R] + x));
                        }
                        else
                        {
                            picture.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                        }
                    }
                }
            }
        }

        private void redrawImageAdjacencyLevel(int[] left, int[] right)
        {
            for (int i = 0; i < picture.Height; i++)
            {
                for (int j = 0; j < picture.Width; j++)
                {
                    Color c = picture.GetPixel(j, i);

                    if (left[c.R] == right[c.R])
                    {
                        if (left[c.R] <= 255)
                        {
                            picture.SetPixel(j, i, Color.FromArgb(left[c.R], left[c.R], left[c.R]));
                        }
                        else
                        {
                            picture.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                        }

                    }
                    else
                    {
                        Color c1 = new Color();
                        int aj = j, ai = i, srednia = 0;

                        if (aj > 0 && aj + 1 < picture.Width && ai > 0 && ai + 1 < picture.Height)
                        {
                            c1 = picture.GetPixel(aj + 1, ai + 1);
                            srednia = srednia + c1.R;

                            c1 = picture.GetPixel(aj - 1, ai - 1);
                            srednia = srednia + c1.R;

                            c1 = picture.GetPixel(aj - 1, ai);
                            srednia = srednia + c1.R;

                            c1 = picture.GetPixel(aj + 1, ai);
                            srednia = srednia + c1.R;

                            c1 = picture.GetPixel(aj, ai - 1);
                            srednia = srednia + c1.R;

                            c1 = picture.GetPixel(aj, ai + 1);
                            srednia = srednia + c1.R;

                            c1 = picture.GetPixel(aj + 1, ai - 1);
                            srednia = srednia + c1.R;

                            c1 = picture.GetPixel(aj - 1, ai + 1);
                            srednia = srednia + c1.R;

                            srednia = srednia / 8;

                            if (srednia > right[c.R])
                            {
                                picture.SetPixel(j, i, Color.FromArgb(right[c.R], right[c.R], right[c.R]));
                            }
                            else
                            {
                                if (srednia < left[c.R])
                                {
                                    if (left[c.R] <= 255)
                                    {
                                        picture.SetPixel(j, i, Color.FromArgb(left[c.R], left[c.R], left[c.R]));
                                    }
                                    else
                                    {
                                        picture.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                                    }
                                }
                                else
                                {
                                    picture.SetPixel(j, i, Color.FromArgb(srednia, srednia, srednia));
                                }
                            }
                        }
                    }
                }
            }
        }

        #region IFilter Members


        public bool showDialog()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
