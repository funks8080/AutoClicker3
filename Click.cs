using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker
{
    public class Click
    {
        private int clickSequence;
        public int ClickSequence
        {
            get { return clickSequence; }
            set { clickSequence = value; }
        }
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

        [IgnoreDataMember]
        public int ClickPointX { get { return clickPoint.X; } }
        [IgnoreDataMember]
        public int ClickPointY { get { return clickPoint.Y; } }

        private int clickOffset;
        public int ClickOffset
        {
            get { return clickOffset; }
            set { clickOffset = value; }
        }

        private Color clickColor;
        public Color ClickColor
        {
            get { return clickColor; }
            set { clickColor = value; }
        }

        [IgnoreDataMember]
        public string ClickColorText
        {
            get { return clickColor.ToString(); }
            set { clickColor = StringToColor(value); }
        }

        private Bitmap clickImage;
        [IgnoreDataMember]
        public Bitmap ClickImage
        {
            get { return clickImage; }
            set { clickImage = value; }
        }

        private string clickImagePath;
        [IgnoreDataMember]
        public string ClickImagePath
        {
            get { return clickImagePath; }
            set { clickImagePath = value; clickImage = GetBitmap(value); }
        }

        public Click()
        {

        }

        public Click(int sequence, Point point, int type, long delay, int offset)
        {
            ClickPoint = point;
            ClickType = type;
            DelayAfterClick = delay;
            ClickSequence = sequence;
            ClickOffset = offset;
        }

        Color StringToColor(string value)
        {
            if (value.ToUpper().Contains("EMPTY"))
                return Color.Empty;

            var colors = value.Split(',');
            Color returnValue;
            try
            {
                returnValue = Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]), int.Parse(colors[3]));
            }
            catch { returnValue = Color.Empty; }

            return returnValue;
        }

        Bitmap GetBitmap(string path)
        {
            if(string.IsNullOrEmpty(path)) return null;

            if(path.IndexOfAny(Path.GetInvalidPathChars()) != -1) return null;
            try
            {
                Bitmap bm = new Bitmap(path);
                return bm;
            }
            catch
            {
                return null;
            }

        }
    }
}
