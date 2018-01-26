using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalHookDemo
{
    public class Click
    {
        private long delayAfterClick;
        public long DelayAfterClick
        {
            get { return delayAfterClick; }
            set { delayAfterClick = value; }
        }

        private int clickType;
        public int ClickType
        {
            get { return clickType; }
            set { clickType = value; }
        }

        private Point clickPoint;
        public Point ClickPoint
        {
            get { return clickPoint; }
            set { clickPoint = value; }
        }

        public Click(Point point, int type, long delay)
        {
            ClickPoint = point;
            ClickType = type;
            DelayAfterClick = delay;
        }
    }
}
