using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker
{
    public class RunParams<T>
    {
        public IProgress<T> ReportProgress { get; set; }
        public bool Timeouts { get; set; }
        public int RunLimit { get; set; }
        public int TimeoutLow { get; set; }
        public int TimeoutHigh { get; set; }

    }
}
