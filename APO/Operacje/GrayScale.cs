using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace APO.Operacje
{
    class GrayScale : APO.Operacje.IFilter
    {
        private bool m_hasDialog;

        public bool hasDialog
        {
            get
            {
                return m_hasDialog;
            }
        }

        private Image image;

        public GrayScale()
        {
            m_hasDialog = false;
        }

        public void setImage(Image image)
        {
            this.image = image;
        }

        public void Convert()
        {
            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(image);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
                  {
                     new float[] {.3f, .3f, .3f, 0, 0},
                     new float[] {.59f, .59f, .59f, 0, 0},
                     new float[] {.11f, .11f, .11f, 0, 0},
                     new float[] {0, 0, 0, 1, 0},
                     new float[] {0, 0, 0, 0, 1}
                  });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
               0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
        }
    }
}
