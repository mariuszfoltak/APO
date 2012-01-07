using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace APO.Operacje.Segmentation
{
    class RegionGrowing : IFilter
    {
        private int seedPoint;
        private int tresholdRange;
        private Bitmap image;

        public void Convert()
        {
            growPoints(findSeedPoints());
            progowanie(seedPoint);
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
            myCustomDialog dialog = new myCustomDialog("Segmentacja przez rozszerzanie", "Podaj wartość punktów startu:", "Podaj przedział segmentacji:");

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return false;

            seedPoint = System.Convert.ToInt32(dialog.value);
            tresholdRange = System.Convert.ToInt32(dialog.value2);

            return true;
        }

        private List<Point> findSeedPoints()
        {
            List<Point> points = new List<Point>();

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color c = image.GetPixel(j, i);

                    if (c.R == seedPoint)
                        points.Add(new Point(j, i));
                }
            }

            return points;
        }

        private void growPoints(List<Point> points)
        {
            int x,y,c,i=0;
            int min = (seedPoint - tresholdRange) < 0 ? 0 : (seedPoint - tresholdRange);
            int max = (seedPoint + tresholdRange) > 255 ? 255 : (seedPoint + tresholdRange);
            bool[,] pointsChecked = new bool[image.Width, image.Height];
            //List<Point> pointsChecked = new List<Point>();
            while (i < points.Count)
            {
                x = points[i].X == 0 ? 0 : points[i].X - 1;
                y = points[i].Y;
                c = image.GetPixel(x, y).R;
                if (!pointsChecked[x,y] && (c > min && c < max))
                {
                    points.Add(new Point(x, y));
                    image.SetPixel(x, y, Color.FromArgb(seedPoint, seedPoint, seedPoint));
                    pointsChecked[x, y] = true;
                }

                x = points[i].X;
                y = points[i].Y == 0 ? 0 : points[i].Y - 1;
                c = image.GetPixel(x, y).R;
                if (!pointsChecked[x, y] && (c > min && c < max))
                {
                    points.Add(new Point(x, y));
                    image.SetPixel(x, y, Color.FromArgb(seedPoint, seedPoint, seedPoint));
                    pointsChecked[x, y] = true;
                }

                x = points[i].X == image.Width - 1 ? image.Width - 1 : points[i].X + 1;
                y = points[i].Y;
                c = image.GetPixel(x, y).R;
                if (!pointsChecked[x, y] && (c > min && c < max))
                {
                    points.Add(new Point(x, y));
                    image.SetPixel(x, y, Color.FromArgb(seedPoint, seedPoint, seedPoint));
                    pointsChecked[x, y] = true;
                }

                x = points[i].X;
                y = points[i].Y == image.Height - 1 ? image.Height - 1 : points[i].Y + 1;
                c = image.GetPixel(x, y).R;
                if (!pointsChecked[x, y] && (c > min && c < max))
                {
                    points.Add(new Point(x, y));
                    image.SetPixel(x, y, Color.FromArgb(seedPoint, seedPoint, seedPoint));
                    pointsChecked[x, y] = true;
                }
                pointsChecked[points[i].X, points[i].Y] = true;
                i++;
            }
        }

        public void progowanie(int prog)
        {
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    Color c = image.GetPixel(j, i);

                    int color = (c.R == prog ? 0 : 255);

                    image.SetPixel(j, i, Color.FromArgb(color, color, color));
                }
            }
        }
    }
}
