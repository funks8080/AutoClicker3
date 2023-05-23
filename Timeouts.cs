using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker
{
    public class Timeouts
    {
        //public int ShortTimeoutMin { get; set; }
        //public int ShortTimeoutMax { get; set; }
        //public int LongTimeoutMin { get; set; }
        //public int LongTimeoutMax { get; set; }
        public int TimeoutLengthMin { get; set; }
        public int TimeoutLengthMax { get; set; }
        public bool EndTimeoutsOnly { get; set; }
        public bool Active { get; set; }
        public int TimeoutCountMin { get; set; }
        public int TimeoutCountMax { get; set; }
        public bool UseLongTimeouts { get; set; }
    }
}
