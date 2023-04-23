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
            using (Graphics gfx = Graphics.FromImage(image))
            {
                //gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                gfx.CopyFromScreen(0,0,Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y,Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            }
            
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
            screenShot.Dispose();
            return imagePoint;
        }

        public static Point FindImage(Bitmap screenShot, Point topLeft, Point bottomRight, Bitmap image, decimal colorRange)
        {
            var corner = GetImageColors(image);
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = topLeft.Y; r < bottomRight.Y - image.Height; r++)
            {
                for (int c = topLeft.X; c < bottomRight.X - image.Width; c++)
                {
                    if (c == 1739 && r == 90)
                        Console.WriteLine("TEST");
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

        public static Point FindImageFromList(Bitmap screenShot, Point topLeft, Point bottomRight, List<Bitmap> imageList, decimal colorRange)
        {
            
            var imagePoint = new Point();

            var foundMatch = false;

            foreach(var image in imageList)
            {
                var corner = GetImageColors(image);
                for (int r = topLeft.Y; r < bottomRight.Y - image.Height; r++)
                {
                    for (int c = topLeft.X; c < bottomRight.X - image.Width; c++)
                    {
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
            }
            
            return imagePoint;
        }

        public static Point FindColorScreenRange(Point topLeft, Point bottomRight, Color color, int pixelSkip, decimal colorRange)
        {
            //1051,417
            var screenShot = MainScreen.CaptureScreen();
            //var x = DateTime.Now.Millisecond;
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = topLeft.Y; r < bottomRight.Y; r += pixelSkip)
            {
                for (int c = topLeft.X; c < bottomRight.X; c += pixelSkip)
                {
                    //if (c > 986 && c < 996 && r > 598 && r < 608)
                    //    Console.WriteLine("TEST");
                    if (PixelCompare(screenShot.GetPixel(c, r), color,colorRange))
                    {
                        imagePoint.X = c;
                        imagePoint.Y = r;
                        foundMatch = true;
                        break;
                    }
                }
                if (foundMatch)
                    break;
            }
            screenShot = null;

            //var y = DateTime.Now.Millisecond;
            //Console.WriteLine(y + " - " + x + " = " + (y - x));
            return imagePoint;
        }

        public static Point FindColorScreen(Point topLeft, Point bottomRight, Color color, int pixelSkip)
        {
            var screenShot = MainScreen.CaptureScreen();
            //var x = DateTime.Now.Millisecond;
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = topLeft.Y; r < bottomRight.Y; r += pixelSkip)
            {
                for (int c = topLeft.X; c < bottomRight.X; c += pixelSkip)
                {
                    if (screenShot.GetPixel(c, r) == color)
                    {
                        imagePoint.X = c;
                        imagePoint.Y = r;
                        foundMatch = true;
                        break;
                    }
                }
                if (foundMatch)
                    break;
            }
            screenShot.Dispose();

            //var y = DateTime.Now.Millisecond;
            //Console.WriteLine(y + " - " + x + " = " + (y - x));
            return imagePoint;
        }

        public static bool FindColorPointRange(Bitmap screenShot, Point point, Color color, decimal colorRange)
        {

            if (PixelCompare(screenShot.GetPixel(point.X, point.Y), color, colorRange))
                return true;

            return false;
        }

        public static Point FindColorRange(Bitmap screenShot, Point topLeft, Point bottomRight, Color color, decimal colorRange, int pixelSkip)
        {
            //var x = DateTime.Now.Millisecond;
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = topLeft.Y; r < bottomRight.Y; r += pixelSkip)
            {
                for (int c = topLeft.X; c < bottomRight.X; c += pixelSkip)
                {
                    if (screenShot.GetPixel(c, r) == color)
                    {
                        imagePoint.X = c;
                        imagePoint.Y = r;
                        foundMatch = true;
                        break;
                    }
                }
                if (foundMatch)
                    break;
            }
            screenShot = null;

            //var y = DateTime.Now.Millisecond;
            //Console.WriteLine(y + " - " + x + " = " + (y - x));
            return imagePoint;
        }

        public static Point FindColor(Bitmap screenShot, Point topLeft, Point bottomRight, Color color, int pixelSkip)
        {
            //var x = DateTime.Now.Millisecond;
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = topLeft.Y; r < bottomRight.Y; r+=pixelSkip)
            {
                for (int c = topLeft.X; c < bottomRight.X; c+=pixelSkip)
                {
                    if (screenShot.GetPixel(c,r) == color)
                    {
                        imagePoint.X = c;
                        imagePoint.Y = r;
                        foundMatch = true;
                        break;
                    }
                }
                if (foundMatch)
                    break;
            }
            screenShot = null;

            //var y = DateTime.Now.Millisecond;
            //Console.WriteLine(y + " - " + x + " = " + (y - x));
            return imagePoint;
        }

        public static Point FindColor2(Bitmap screenShot, Point topLeft, Point bottomRight, Color color, int pixelSkip)
        {
           // var x = DateTime.Now.Millisecond;
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = topLeft.Y; r < bottomRight.Y; r += pixelSkip)
            {
                for (int c = topLeft.X; c < bottomRight.X; c += pixelSkip)
                {
                    //Top
                    if( r + 5 < topLeft.Y)
                    {
                        if (screenShot.GetPixel(c, r+5) == color)
                        {
                            imagePoint.X = c;
                            imagePoint.Y = r+5;
                            foundMatch = true;
                            break;
                        }
                    }
                    //Left
                    if (c - 5 > topLeft.X)
                    {
                        if (screenShot.GetPixel(c, r + 5) == color)
                        {
                            imagePoint.X = c - 5;
                            imagePoint.Y = r;
                            foundMatch = true;
                            break;
                        }
                    }
                    //Right
                    if (c + 5 < topLeft.X)
                    {
                        if (screenShot.GetPixel(c, r + 5) == color)
                        {
                            imagePoint.X = c + 5;
                            imagePoint.Y = r;
                            foundMatch = true;
                            break;
                        }
                    }
                    //Bottom
                    if (r - 5 > topLeft.Y)
                    {
                        if (screenShot.GetPixel(c, r + 5) == color)
                        {
                            imagePoint.X = c;
                            imagePoint.Y = r - 5;
                            foundMatch = true;
                            break;
                        }
                    }
                }
                if (foundMatch)
                    break;
            }
            screenShot = null;
            //var y = DateTime.Now.Millisecond;
           // Console.WriteLine(y + " - " + x + " = " + (y-x));
            return imagePoint;
        }

        public static Point FindInInventory(Bitmap image, Bitmap screenshot, decimal colorRange)
        {
            var corner = GetImageColors(image);
            var imagePoint = new Point();

            var foundMatch = false;

            for (int r = 735; r < 1000 - image.Height; r++)
            {
                for (int c = 1720; c < 1915 - image.Width; c++)
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
                    if (corner[c, r].A == 0)
                        continue;
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
