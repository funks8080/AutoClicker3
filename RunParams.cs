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
        public Timeouts Timeouts { get; set; }
        public int RunLimit { get; set; }
        public List<Click> ClickList { get; set; }
        public int ClickOffset { get; set; }

        public int StartStep { get; set; }

    }
}
