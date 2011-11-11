using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace APO
{
    public class FastBitmap : IDisposable, ICloneable
    {
        private Bitmap bitmap;
        private BitmapData bitmapData;
        public delegate void PixelChangedDelegate(Point position);
        public event PixelChangedDelegate PixelChanged;
        private int levels = 256;

        public Bitmap Bitmap
        {
            get { return bitmap; }
            set { bitmap = value; }
        }

        public int Width
        {
            get { return bitmap.Width; }
        }

        public int Height
        {
            get { return bitmap.Height; }
        }

        public Size Size
        {
            get { return new Size(Width, Height); }
        }

        public int Levels
        {
            get { return levels; }
        }

        public byte this[int x, int y]
        {
            get
            {
                while (x < 0)
                    x += Width;
                while (x >= Width)
                    x -= Width;
                while (y < 0)
                    y += Height;
                while (y >= Height)
                    y -= Height;

                unsafe
                {
                    byte* ptr = (byte*)((byte*)bitmapData.Scan0 + (y * bitmapData.Stride) + x);
                    return *ptr;
                }
            }
            set
            {
                while (x < 0)
                    x += Width;
                while (x >= Width)
                    x -= Width;
                while (y < 0)
                    y += Height;
                while (y >= Height)
                    y -= Height;

                unsafe
                {
                    byte* ptr = (byte*)((byte*)bitmapData.Scan0 + (y * bitmapData.Stride) + x);
                    *ptr = value;
                }

                if (PixelChanged != null)
                    PixelChanged(new Point(x, y));
            }
        }

        public ColorPalette Palette
        {
            get { return bitmap.Palette; }
            set { bitmap.Palette = value; }
        }

        public void Posterize(int levels, ColorPalette palette)
        {
            this.levels = levels;
            bitmap.Palette = palette;
        }

        public unsafe void Init(int width, int height, int levels, Bitmap bmp, ColorPalette palette)
        {
            bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            bitmap.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            this.levels = levels;

            if (palette == null)
            {
                ColorPalette pal = bitmap.Palette;
                for (int i = 0; i < pal.Entries.Length; i++)
                    pal.Entries[i] = Color.FromArgb(i, i, i);
                bitmap.Palette = pal;
            }
            else bitmap.Palette = palette;

            Lock();

            if (bmp != null)
            {
                Byte* pPixel = (Byte*)bitmapData.Scan0;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color clr = bmp.GetPixel(x, y);
                        Byte byPixel = (byte)((30 * clr.R + 59 * clr.G + 11 * clr.B) / 100);
                        pPixel[x] = byPixel;
                    }
                    pPixel += bitmapData.Stride;
                }
            }
        }

        public FastBitmap(int width, int height)
        {
            Init(width, height, 256, null, null);
        }

        public FastBitmap(int width, int height, int levels)
        {
            bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            bitmap.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            this.levels = levels;

            ColorPalette pal = bitmap.Palette;
            float param1 = (float)255 / (levels - 1);
            for (int i = 0; i < levels; i++)
            {
                byte color = (byte)(param1 * i);
                pal.Entries[i] = Color.FromArgb(color, color, color);
            }
            bitmap.Palette = pal;

            Lock();
        }

        public FastBitmap(Bitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException("bitmap");
            Init(bmp.Width, bmp.Height, 256, bmp, null);
        }

        public FastBitmap(FastBitmap bmp)
        {
            if (bmp == null)
                throw new ArgumentNullException("bitmap");
            bitmap = (Bitmap)bmp.Bitmap.Clone();
            bitmap.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            bitmap.Palette = bmp.Palette;
            levels = bmp.levels;
            Lock();
        }

        public FastBitmap(FastBitmap bmp, Rectangle src)
        {
            if (bmp == null)
                throw new ArgumentNullException("bitmap");

            bitmap = new Bitmap(src.Width, src.Height, PixelFormat.Format8bppIndexed);
            bitmap.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            levels = bmp.Levels;
            bitmap.Palette = bmp.Palette;

            Lock();

            unsafe
            {
                if (bmp != null)
                {
                    Byte* pPixel = (Byte*)bitmapData.Scan0;
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            pPixel[x] = bmp[x + src.X, y + src.Y];
                        }
                        pPixel += bitmapData.Stride;
                    }
                }
            }
        }

        public object Clone()
        {
            Unlock();
            FastBitmap bmp = new FastBitmap(this);
            Lock();
            return bmp;
        }

        public void Draw(Graphics graphics, int x, int y)
        {
            Unlock();
            graphics.DrawImage(bitmap, x, y);
            Lock();
        }

        public void Draw(Graphics graphics, int x, int y, int width, int height)
        {
            Unlock();
            graphics.DrawImage(bitmap, x, y, width, height);
            Lock();
        }

        public void Draw(Graphics graphics, int x, int y, Rectangle src)
        {
            Unlock();
            graphics.DrawImage(bitmap, x, y, src, GraphicsUnit.Pixel);
            Lock();
        }

        public void Save(Stream stream, ImageFormat format)
        {
            bitmap.Save(stream, format);
        }

        public void Lock()
        {
            if (bitmapData != null)
                return;

            bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
        }

        public Bitmap Unlock()
        {
            if (bitmapData == null)
                return bitmap;

            bitmap.UnlockBits(bitmapData);
            bitmapData = null;

            return bitmap;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Unlock();

                if (bitmap != null)
                    bitmap.Dispose();
            }

            bitmapData = null;
            bitmap = null;
        }

        public Graphics CreateGraphics()
        {
            return Graphics.FromImage(bitmap);
        }
    }
}
