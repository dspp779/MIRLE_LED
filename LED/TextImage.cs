using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LED
{
    /* TextImage is a image class used for
     * displaying Big5 or other character code in CP5200
     * by sending image to CP5200 dynamically.
     * 
     * which have featrues as below:
     * 1. dynamically create temp text-content image file for sending to CP5200,
     *    and after then temp image file would be removed.
     * 2. create text image according to the pixel-sized font style setting in LEDConfig.
     * 3. you can set text color and background color.
     * */
    public class TextImage
    {
        // used for tracking all the created image that 
        private static List<TextImage> TextImagePool = new List<TextImage>();

        // counter for temp text image
        private static int counter = 0;
        // interlock for the counter
        private static int counterlock = 0;

        private string _path;
        private Image _img;

        // path property
        public string path
        {
            get { return _path; }
        }
        // image width property
        public int Width
        {
            get { return _img.Width; }
        }
        // image width property        
        public int Height
        {
            get { return _img.Height; }
        }

        // constructor
        public TextImage(String text, Font font, Color textColor, Color backColor)
        {
            // get available path
            _path = getTempPath();
            // draw text image
            _img = DrawText(text, font, textColor, backColor);
            // add to text image pool
            TextImagePool.Add(this);
            // save the file
            _img.Save(_path, ImageFormat.Png);
            // uncomment the below statement if you want to set temp file hidden
            //File.SetAttributes(_path, FileAttributes.Hidden);
        }
        // remove the temp image file
        public void release()
        {
            // delete temp file
            File.Delete(_path);
            // remove from text image pool
            TextImagePool.Remove(this);
        }


        // release all the image in text image pool
        public static void releaseAll()
        {
            foreach (TextImage img in TextImagePool)
            {
                File.Delete(img.path);
            }
        }

        /* get an available path for temp images.
         * synchronization between different instances needed.
         * */
        private static string getTempPath()
        {
            try
            {
                while (true)
                {
                    /* there should be at most one thread running belowing part
                     * 0 indicates that the method is not in use. 
                     * */
                    if (0 == Interlocked.Exchange(ref counterlock, 1))
                    {
                        String filePath = string.Format("./~tempimg{0}.png", counter++);
                        //Release the lock
                        Interlocked.Exchange(ref counterlock, 0);
                        // check if the path available
                        if (!File.Exists(filePath))
                            return filePath;
                    }
                    else
                    {
                        // wait
                        SpinWait.SpinUntil(() => false, 200);
                    }

                }
            }
            catch(Exception)
            {
                return null;
            }
        }

        // draw text on image object
        private static Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            
            //measure the string to see how big the image needs to be
            Size textSize = TextRenderer.MeasureText(text, font);

            // remove front and after padding
            textSize.Width -= 8;
            // set height
            textSize.Height = 16;

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

            // drawing setting
            drawing.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            /* draw text on the image
             * Do not use Graphic.DrawString!
             * it does not draw accurate pixel.
             * use TextRenderer instead.
             * */
            TextRenderer.DrawText(drawing, text, font, new Point(-2, 0), textColor);

            // save the operation
            drawing.Save();

            // dispose resource
            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}
