using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LED
{
    public class TextImage
    {
        private static int counter = 0;

        private string _path;
        private Image _img;

        public string path
        {
            get { return _path; }
        }

        public TextImage(String text, Font font, Color textColor, Color backColor)
        {
            _path = getTempPath();
            _img = DrawText(text, font, textColor, backColor);
            _img.Save(_path, ImageFormat.Jpeg);
            File.SetAttributes(_path, FileAttributes.Hidden);
        }

        public void release()
        {
            File.Delete(_path);
        }


        private static string getTempPath()
        {
            while (true)
            {
                //0 indicates that the method is not in use. 
                if (0 == Interlocked.Exchange(ref counter, 1))
                {
                    String filePath = string.Format("~tempimg{0}.jpg", counter++);
                    //Release the lock
                    Interlocked.Exchange(ref counter, 0);
                    if (!File.Exists(filePath))
                        return filePath;
                }
                else
                {
                    SpinWait.SpinUntil(() => false, 200);
                }

            }
        }

        private static Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap(Convert.ToInt32(textSize.Width), Convert.ToInt32(textSize.Height));

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}
