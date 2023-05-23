using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker
{
    public class ScreenshotInfo
    {
        public int SelectedMonitor { get; set; }
        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }
        public int PixelSkip { get; set; }
        public decimal ColorRange { get; set; }
    }
}
