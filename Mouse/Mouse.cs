using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoClicker.Mouse
{
    public static class Mouse
    {
        [DllImport("User32.dll", SetLastError = true)]
        public static extern int SendInput(int nInputs, ref INPUT pInputs, int cbSize);

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int INPUT_MOUSE = 0;

        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public int dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        public struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        };

        public static void MoveTo(int x, int y)
        {
            Point moveToPoint = new Point(x, y);
            Cursor.Position = moveToPoint;
        }

        public static void MoveToX(int x)
        {
            Point moveToPoint = new Point(x, Cursor.Position.Y);
            Cursor.Position = moveToPoint;
        }

        public static void MoveToY(int x, int y)
        {
            Point moveToPoint = new Point(Cursor.Position.X, y);
            Cursor.Position = moveToPoint;
        }

        public static Point GetLocation()
        {
            return Cursor.Position;
        }

        public static int GetLocationX()
        {
            return Cursor.Position.X;
        }

        public static int GetLocationY()
        {
            return Cursor.Position.Y;
        }

        public static void RightClick()
        {
            INPUT i = new INPUT();
            i.type = INPUT_MOUSE;
            i.mi.dx = 0; //clickLocation.X;
            i.mi.dy = 0; // clickLocation.Y;
            i.mi.dwFlags = MOUSEEVENTF_RIGHTDOWN;
            i.mi.dwExtraInfo = IntPtr.Zero;
            i.mi.mouseData = 0;
            i.mi.time = 0;
            //send the input
            SendInput(1, ref i, Marshal.SizeOf(i));
            //set the INPUT for mouse up and send
            i.mi.dwFlags = MOUSEEVENTF_RIGHTUP;
            SendInput(1, ref i, Marshal.SizeOf(i));
        }

        public static void LeftClick()
        {
            INPUT i = new INPUT();
            i.type = INPUT_MOUSE;
            i.mi.dx = 0; //clickLocation.X;
            i.mi.dy = 0; // clickLocation.Y;
            i.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
            i.mi.dwExtraInfo = IntPtr.Zero;
            i.mi.mouseData = 0;
            i.mi.time = 0;
            //send the input
            SendInput(1, ref i, Marshal.SizeOf(i));
            //set the INPUT for mouse up and send
            i.mi.dwFlags = MOUSEEVENTF_LEFTUP;
            SendInput(1, ref i, Marshal.SizeOf(i));
        }
    }
}
