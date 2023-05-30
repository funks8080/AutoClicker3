using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClicker
{
    public enum Result
    {
        EMPTY = 0,
        SUCCESS = 1,
        FAIL = 2
    }
    public enum Action
    {
        EMPTY = 0,
        GOTO = 1,
        STOP = 2,
        REPEAT = 3,
        PRESS_KEY = 4,
    }
    public class UserScript
    {
        public Result ClickResult { get; set; }
        public Action ResultAction { get; set; }
        public int GoToSequence { get; set; }
        public Keys PressKey { get; set; }
        public ClickOptions ClickOptions { get; set; }
        public bool CheckOnlyNoClick { get; set; }
    }

    public class ClickOptions
    {
        public Point SearchAreaTopLeft { get; set; }
        public Point SearchAreaBottomRight { get; set; }
    }
}
