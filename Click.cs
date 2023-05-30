using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        private string clickName;
        public string ClickName
        {
            get { return clickName; }
            set { clickName = value; }
        }

        private long delayAfterClick;
        public long DelayAfterClick
        {
            get { return delayAfterClick; }
            set { delayAfterClick = value < 200 ? 200 : value; }
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

        [XmlIgnore]
        public int ClickPointX { get { return clickPoint.X; } set { clickPoint.X = value; } }
        [XmlIgnore]
        public int ClickPointY { get { return clickPoint.Y; } set { clickPoint.Y = value; } }

        private int clickOffset;
        public int ClickOffset
        {
            get { return clickOffset; }
            set { clickOffset = value; }
        }

        private bool clickEmptyPoint;
        public bool ClickEmptyPoint
        {
            get { return clickEmptyPoint; }
            set { clickEmptyPoint = value; }
        }

        private Color clickColor;
        [XmlIgnore]
        public Color ClickColor
        {
            get { return clickColor; }
            set { clickColor = value; }
        }

        public string ClickColorText
        {
            get { return EasyColorFormat(clickColor.ToString()); }
            set { clickColor = StringToColor(value); }
        }

        private Color clickColor2;
        [XmlIgnore]
        public Color ClickColor2
        {
            get { return clickColor2; }
            set { clickColor2 = value; }
        }
        
        public string ClickColor2Text
        {
            get { return EasyColorFormat(clickColor2.ToString()); }
            set { clickColor2 = StringToColor(value); }
        }

        private Bitmap clickImage;
        [XmlIgnore]
        public Bitmap ClickImage
        {
            get { return clickImage; }
            set { clickImage = value; }
        }

        private string clickImagePath;
        public string ClickImagePath
        {
            get { return clickImagePath; }
            set { clickImagePath = value; clickImage = GetBitmap(value); }
        }

        private UserScript clickScript;
        public UserScript ClickScript
        {
            get { return clickScript; }
            set { clickScript = value; }
        }
        [XmlIgnore]
        public string ClickScriptText
        {
            get { return ToXML(clickScript); }
            set { clickScript = FromXML<UserScript>(value); }
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
            if (string.IsNullOrEmpty(value) || value.ToUpper().Contains("EMPTY"))
                return Color.Empty;

            value = EasyColorFormat(value);
            var colors = value.Split(',');
            Color returnValue;
            try
            {
                switch (colors.Count())
                {
                    case 3:
                        returnValue = Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
                        break;
                    case 4:
                        returnValue = Color.FromArgb(int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]), int.Parse(colors[3]));
                        break;
                    default:
                        returnValue = Color.Empty;
                        break;
                            
                }
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

        private string EasyColorFormat(string color)
        {
            if (string.IsNullOrEmpty(color) || color.ToUpper().Contains("EMPTY"))
                return "";

           return( Regex.Replace(color, "[^0-9,]", ""));
        }

        private static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        private string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }
    }
}
