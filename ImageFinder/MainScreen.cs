using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClicker.ImageFinder
{
    public static class MainScreen
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);


        public static Color GetColorAt(Point location)
        {
            Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, location.X, location.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        public static Bitmap CaptureScreen(int monitor)
        {
            int xSize = Screen.PrimaryScreen.Bounds.Width;
            int topLeftX;


            switch (monitor)
            {
                case 1:
                    topLeftX = -xSize;
                    break;
                case 2:
                    topLeftX = 0;
                    break;
                case 3:
                    topLeftX = xSize;
                    break;
                default:
                    topLeftX = 0;
                    break;
            }
            var image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            using (Graphics gfx = Graphics.FromImage(image))
            {
                //gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                gfx.CopyFromScreen(topLeftX,0,0, 0,Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            }
            
            return image;
        }

        public static Bitmap LoadImageFile(string image)
        {
            return (Bitmap)Image.FromFile(image);
        }

        public static Point FindImage(Point topLeft, Point bottomRight, Bitmap image, decimal colorRange, int monitor)
        {
            var screenShot = CaptureScreen(monitor);
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

        public static Point FindImage(Bitmap screenShot, Point topLeft, Point bottomRight, Bitmap image, decimal colorRange, int monitor)
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
            return ModifyForMonitorPoint(imagePoint, monitor);
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

        public static Point FindColorScreenRange(Point topLeft, Point bottomRight, Color color, int pixelSkip, decimal colorRange, int monitor)
        {
            //1051,417
            var screenShot = MainScreen.CaptureScreen(monitor);
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

        public static Point FindColorScreenRange(Bitmap screenShot, Point topLeft, Point bottomRight, Color color, int pixelSkip, decimal colorRange, int monitor)
        {
            Point imagePoint = Point.Empty;

            var foundMatch = false;

            for (int r = topLeft.Y; r < bottomRight.Y; r += pixelSkip)
            {
                for (int c = topLeft.X; c < bottomRight.X; c += pixelSkip)
                {
                    //if (c > 986 && c < 996 && r > 598 && r < 608)
                    //    Console.WriteLine("TEST");
                    if (PixelCompare(screenShot.GetPixel(c, r), color, colorRange))
                    {
                        imagePoint = new Point(c, r);
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
            return ModifyForMonitorPoint(imagePoint, monitor);
        }

        public static Point FindColorScreen(Point topLeft, Point bottomRight, Color color, int pixelSkip, int monitor)
        {
            var screenShot = MainScreen.CaptureScreen(monitor);
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

        public static Point FindColorScreenCenterOut(Point topLeft, Point bottomRight, Color color, decimal colorRange, int pixelSkip, int monitor)
        {
            var screenShot = MainScreen.CaptureScreen(monitor);
            //var x = DateTime.Now.Millisecond;
            var imagePoint = new Point();

            //var foundMatch = false;


            // length of current segment
            int segment_length = 1;


            int segment_passed = 0;
            int NUMBER_OF_POINTS = Math.Abs( ((bottomRight.X - topLeft.X) * (bottomRight.Y - topLeft.Y)) / (pixelSkip * pixelSkip));
            // current position (i, j) and how much of current segment we passed
            int x = (bottomRight.X + topLeft.X) / 2;
            int y = (bottomRight.Y + topLeft.Y) / 2;
            // (di, dj) is a vector - direction in which we move right now
            int dx = pixelSkip;
            int dy = 0;
            for (int k = 0; k < NUMBER_OF_POINTS; k++)
            {
                if (PixelCompare(screenShot.GetPixel(x, y), color, colorRange))
                {
                    imagePoint.X = x;
                    imagePoint.Y = y;
                    //foundMatch = true;
                    break;
                }
                // make a step, add 'direction' vector (di, dj) to current position (i, j)
                x += dx;
                y += dy;
                segment_passed++;
                //Console.WriteLine(x + " " + y);

                if (segment_passed == segment_length)
                {
                    // done with current segment
                    segment_passed = 0;

                    // 'rotate' directions
                    int buffer = dx;
                    dx = -dy;
                    dy = buffer;

                    // increase segment length if necessary
                    if (dy == 0)
                    {
                        segment_length++;
                    }
                }
            }

            //for (int r = topLeft.Y; r < bottomRight.Y; r += pixelSkip)
            //{
            //    for (int c = topLeft.X; c < bottomRight.X; c += pixelSkip)
            //    {
            //        if (screenShot.GetPixel(c, r) == color)
            //        {
            //            imagePoint.X = c;
            //            imagePoint.Y = r;
            //            foundMatch = true;
            //            break;
            //        }
            //    }
            //    if (foundMatch)
            //        break;
            //}
            screenShot.Dispose();

            //var y = DateTime.Now.Millisecond;
            //Console.WriteLine(y + " - " + x + " = " + (y - x));
            return ModifyForMonitorPoint(imagePoint, monitor);
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

        public static Point ModifyForMonitorPoint(Point point, int monitor)
        {
            int xSize = Screen.PrimaryScreen.Bounds.Width;


            switch (monitor)
            {
                case 1:
                    point.X -= xSize;
                    break;
                case 3:
                    point.X += xSize;
                    break;
                default:
                    break;
            }

            return point;
        }

        public static Point ModifyFromMonitorPoint(Point point, int monitor)
        {
            int xSize = Screen.PrimaryScreen.Bounds.Width;


            switch (monitor)
            {
                case 1:
                    point.X += xSize;
                    break;
                case 3:
                    point.X -= xSize;
                    break;
                default:
                    break;
            }

            return point;
        }
    }
}
