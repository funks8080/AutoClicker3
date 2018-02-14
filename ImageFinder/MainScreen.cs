using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClicker.ImageFinder
{
    public static class MainScreen
    {
        public static Bitmap CaptureScreen()
        {
            var image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            var gfx = Graphics.FromImage(image);
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return image;
        }

        public static Bitmap LoadImageFile(string image)
        {
            return (Bitmap)Image.FromFile(image);
        }

        public static Point FindImage(Bitmap image, decimal colorRange)
        {
            var screenShot = CaptureScreen();
            var corner = GetCorner(10, image);
            var size = (int)Math.Sqrt(corner.Length);
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = 0; r < screenShot.Height - size; r++)
            {
                for (int c = 0; c < screenShot.Width - size; c++)
                {
                    if (r == 553 && c == 1281)
                    {
                        var test = 1;
                    }
                    foundMatch = CompareCorner(screenShot, corner, colorRange, screenShotX: c, screenShotY: r);
                    if (foundMatch)
                    {
                        imagePoint.X = c;
                        imagePoint.Y = r;
                        break;
                    }
                }
                if (foundMatch)
                    break;
            }

            return imagePoint;
        }

        public static Color[,] GetCorner(int size, Bitmap image)
        {
            var corner = new Color[size,size];

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    corner[c, r] = image.GetPixel(c, r);
                }
            }

            return corner;
        }

        public static bool CompareCorner(Bitmap screenShot, Color[,] corner, decimal colorRange, int screenShotX, int screenShotY)
        {
            var size = (int) Math.Sqrt(corner.Length);
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (PixelCompare(screenShot.GetPixel(screenShotX + c, screenShotY + r) , corner[c, r], colorRange ))
                        continue;
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool PixelCompare(Color screenPixel, Color imagePixel, decimal colorRange)
        {
            var range = ColorDiff(screenPixel, imagePixel);
            return range <= colorRange;
        }

        // distance in RGB space
        public static decimal ColorDiff(Color c1, Color c2)
        {
            return (decimal)Math.Sqrt((c1.R - c2.R) * (c1.R - c2.R)
                                  + (c1.G - c2.G) * (c1.G - c2.G)
                                  + (c1.B - c2.B) * (c1.B - c2.B));
        }
    }
}
