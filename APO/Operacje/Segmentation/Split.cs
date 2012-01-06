using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace APO.Operacje.Segmentation
{
    class Split : IFilter
    {
        private Bitmap image;
        private int tresholdRange;
        public void Convert()
        {
            createRegion(0, 0, image.Width - 1, image.Height - 1);
        }

        private void createRegion(int x0, int y0, int x1, int y1)
        {
            if (x0 > x1) x1 = x0;
            if (y0 > y1) y1 = y0;

            if (x0 == x1 && y0 == y1)
                return;

            int mean = 0;
            int width = x1 - x0 + 1;
            int height = y1 - y0 + 1;
            int xMax = x0 + width;
            int yMax = y0 + height;
            double deviation = countDeviation(x0, y0, x1, y1, out mean);
            if (tresholdRange > deviation)
                for (int i = x0; i < xMax; i++)
                    for (int j = y0; j < yMax; j++)
                        image.SetPixel(i, j, Color.FromArgb(mean, mean, mean));
            else
            {
                createRegion(x0, y0, x0 + width / 2 - 1, y0 + height / 2 - 1);
                createRegion(x0, y0 + height / 2, x0 + width / 2 - 1, y1);
                createRegion(x0 + width / 2, y0, x1, y0 + height / 2 - 1);
                createRegion(x0 + width / 2, y0 + height / 2, x1, y1);
            }
        }

        public bool hasDialog
        {
            get { return true; }
        }

        public void setImage(System.Drawing.Image image)
        {
            this.image = (Bitmap)image;
        }

        public bool showDialog()
        {
            myCustomDialog dialog = new myCustomDialog("Segmentacja przez dzielenie", "Podaj próg dzielenia:");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return false;

            tresholdRange = System.Convert.ToInt32(dialog.value);

            return true;
        }

        private double countDeviation(int x0, int y0, int x1, int y1, out int mean)
        {
            return countDeviationAmplitude(x0, y0, x1, y1, out mean);
            int width = x1 - x0 + 1;
            int height = y1 - y0 + 1;
            int xMax = x0 + width;
            int yMax = y0 + height;
            int N = width * height;
            mean = 0;
            double sum = 0;

            for (int i = x0; i < xMax; i++)
                for (int j = y0; j < yMax; j++)
                    mean += image.GetPixel(i, j).R;

            mean /= N;

            for (int i = x0; i < xMax; i++)
                for (int j = y0; j < yMax; j++)
                    sum += Math.Pow((image.GetPixel(i, j).R - mean), 2);

            return Math.Sqrt(sum / (N - 1));
        }

        private double countDeviationAmplitude(int x0, int y0, int x1, int y1, out int mean)
        {
            int width = x1 - x0 + 1;
            int height = y1 - y0 + 1;
            int xMax = x0 + width;
            int yMax = y0 + height;
            int N = width * height;
            int min = 255, max = 0;
            int color;
            mean = 0;
            double sum = 0;

            for (int i = x0; i < xMax; i++)
                for (int j = y0; j < yMax; j++)
                {
                    color = image.GetPixel(i, j).R;
                    mean += color;
                    min = (color < min) ? color : min;
                    max = (color > max) ? color : max;
                }

            mean /= N;

            return Math.Abs(max - min);
        }
    }
}
