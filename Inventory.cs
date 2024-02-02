using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoClicker
{
    public class Inventory
    {
        public int TopLeftX { get; set; }
        public int TopLeftY { get; set; }
        public int BottomRightX { get; set; }
        public int InventoryWidth { get; set; }
        public int InventoryHeight { get; set; }
        public int BottomRightY { get; set; }
        [XmlIgnore]
        public List<Click> InventoryClicks { get; set; } = new List<Click>();
    }
}
