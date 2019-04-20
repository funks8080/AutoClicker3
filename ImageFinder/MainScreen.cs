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

        public static Point FindImage(Point topLeft, Point bottomRight, Bitmap image, decimal colorRange)
        {
            var screenShot = CaptureScreen();
            var corner = GetImageColors(image);
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = topLeft.Y; r < bottomRight.Y - image.Height; r++)
            {
                for (int c = topLeft.X; c < bottomRight.X - image.Width; c++)
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
            corner = null;
            screenShot = null;
            return imagePoint;
        }

        public static Point FindInInventory(Bitmap image, Bitmap screenshot, decimal colorRange)
        {
            var corner = GetImageColors(image);
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = 735; r < 990 - image.Width; r++)
            {
                for (int c = 1720; c < 1900 - image.Height; c++)
                {
                    foundMatch = CompareCorner(screenshot, corner, colorRange, screenShotX: c, screenShotY: r);
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
        public static Point FindTanner(Bitmap image, Bitmap screenshot, decimal colorRange)
        {
            var corner = GetImageColors(image);
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = 150; r < 600; r++)
            {
                for (int c = 725; c < 1150; c++)
                {
                    foundMatch = CompareCorner(screenshot, corner, colorRange, screenShotX: c, screenShotY: r);
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

        public static Color[,] GetImageColors(Bitmap image)
        {
            var x = image.Width;
            var y = image.Height;
            var corner = new Color[x, y];

            for (int r = 0; r < y; r++)
            {
                for (int c = 0; c < x; c++)
                {
                    corner[c, r] = image.GetPixel(c, r);
                }
            }

            return corner;
        }

        public static bool CompareCorner(Bitmap screenShot, Color[,] corner, decimal colorRange, int screenShotX, int screenShotY)
        {
            for (int r = 0; r < corner.GetLength(1) - 1; r++)
            {
                for (int c = 0; c < corner.GetLength(0) - 1; c++)
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
