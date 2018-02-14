﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoClicker.Keyboard
{
    public static class MyKeyboard
    {
        [DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);


        public const int KEYBDEVENTF_KEYDOWN = 0;
        public const int KEYBDEVENTF_KEYUP = 2;
        public const int VK_BACK = 0x08;
        public const int VK_TAB = 0x09;
        public const int VK_CLEAR = 0x0C;
        public const int VK_RETURN = 0x0D;
        public const int VK_SHIFT = 0x10;
        public const int VK_CONTROL = 0x11;
        public const int VK_MENU = 0x12;
        public const int VK_PAUSE = 0x13;
        public const int VK_CAPITAL = 0x14;
        public const int VK_ESCAPE = 0x1B;
        public const int VK_SPACE = 0x20;
        public const int VK_PAGE_UP = 0x21;
        public const int VK_PAGE_DOWN = 0x22;
        public const int VK_END = 0x23;
        public const int VK_HOME = 0x24;
        public const int VK_LEFT = 0x25;
        public const int VK_UP = 0x26;
        public const int VK_RIGHT = 0x27;
        public const int VK_DOWN = 0x28;
        public const int VK_SELECT = 0x29;
        public const int VK_PRINT = 0x2A;
        public const int VK_EXECUTE = 0x2B;
        public const int VK_SNAPSHOT = 0x2C;
        public const int VK_INSERT = 0x2D;
        public const int VK_DELETE = 0x2E;
        public const int VK_HELP = 0x2F;
        public const int VK_0 = 0x30;
        public const int VK_1 = 0x31;
        public const int VK_2 = 0x32;
        public const int VK_3 = 0x33;
        public const int VK_4 = 0x34;
        public const int VK_5 = 0x35;
        public const int VK_6 = 0x36;
        public const int VK_7 = 0x37;
        public const int VK_8 = 0x38;
        public const int VK_9 = 0x39;
        public const int VK_A = 0x41;
        public const int VK_B = 0x42;
        public const int VK_C = 0x43;
        public const int VK_D = 0x44;
        public const int VK_E = 0x45;
        public const int VK_F = 0x46;
        public const int VK_G = 0x47;
        public const int VK_H = 0x48;
        public const int VK_I = 0x49;
        public const int VK_J = 0x4A;
        public const int VK_K = 0x4B;
        public const int VK_L = 0x4C;
        public const int VK_M = 0x4D;
        public const int VK_N = 0x4E;
        public const int VK_O = 0x4F;
        public const int VK_P = 0x50;
        public const int VK_Q = 0x51;
        public const int VK_R = 0x52;
        public const int VK_S = 0x53;
        public const int VK_T = 0x54;
        public const int VK_U = 0x55;
        public const int VK_V = 0x56;
        public const int VK_W = 0x57;
        public const int VK_X = 0x58;
        public const int VK_Y = 0x59;
        public const int VK_Z = 0x5A;
        public const int VK_LWIN = 0x5B;
        public const int VK_RWIN = 0x5C;
        public const int VK_APPS = 0x5D;
        public const int VK_SLEEP = 0x5F;
        public const int VK_NUMPAD0 = 0x60;
        public const int VK_NUMPAD1 = 0x61;
        public const int VK_NUMPAD2 = 0x62;
        public const int VK_NUMPAD3 = 0x63;
        public const int VK_NUMPAD4 = 0x64;
        public const int VK_NUMPAD5 = 0x65;
        public const int VK_NUMPAD6 = 0x66;
        public const int VK_NUMPAD7 = 0x67;
        public const int VK_NUMPAD8 = 0x68;
        public const int VK_NUMPAD9 = 0x69;
        public const int VK_MULTIPLY = 0x6A;
        public const int VK_ADD = 0x6B;
        public const int VK_SEPARATOR = 0x6C;
        public const int VK_SUBTRACT = 0x6D;
        public const int VK_DECIMAL = 0x6E;
        public const int VK_DIVIDE = 0x6F;
        public const int VK_F1 = 0x70;
        public const int VK_F2 = 0x71;
        public const int VK_F3 = 0x72;
        public const int VK_F4 = 0x73;
        public const int VK_F5 = 0x74;
        public const int VK_F6 = 0x75;
        public const int VK_F7 = 0x76;
        public const int VK_F8 = 0x77;
        public const int VK_F9 = 0x78;
        public const int VK_F10 = 0x79;
        public const int VK_F11 = 0x7A;
        public const int VK_F12 = 0x7B;
        public const int VK_F13 = 0x7C;
        public const int VK_F14 = 0x7D;
        public const int VK_F15 = 0x7E;
        public const int VK_F16 = 0x7F;
        public const int VK_F17 = 0x80;
        public const int VK_F18 = 0x81;
        public const int VK_F19 = 0x82;
        public const int VK_F20 = 0x83;
        public const int VK_F21 = 0x84;
        public const int VK_F22 = 0x85;
        public const int VK_F23 = 0x86;
        public const int VK_F24 = 0x87;
        public const int VK_NUMLOCK = 0x90;
        public const int VK_SCROLL = 0x91;
        public const int VK_LSHIFT = 0xA0;
        public const int VK_RSHIFT = 0xA1;
        public const int VK_LCONTROL = 0xA2;
        public const int VK_RCONTROL = 0xA3;
        public const int VK_LMENU = 0xA4;
        public const int VK_RMENU = 0xA5;
        public const int VK_BROWSER_BACK = 0xA6;
        public const int VK_BROWSER_FORWARD = 0xA7;
        public const int VK_BROWSER_REFRESH = 0xA8;
        public const int VK_BROWSER_STOP = 0xA9;
        public const int VK_BROWSER_SEARCH = 0xAA;
        public const int VK_BROWSER_FAVORITES = 0xAB;
        public const int VK_BROWSER_HOME = 0xAC;
        public const int VK_VOLUME_MUTE = 0xAD;
        public const int VK_VOLUME_DOWN = 0xAE;
        public const int VK_VOLUME_UP = 0xAF;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        public const int VK_MEDIA_PREV_TRACK = 0xB1;
        public const int VK_MEDIA_STOP = 0xB2;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        public const int VK_LAUNCH_MAIL = 0xB4;
        public const int VK_LAUNCH_MEDIA_SELECT = 0xB5;
        public const int VK_LAUNCH_APP1 = 0xB6;
        public const int VK_LAUNCH_APP2 = 0xB7;
        public const int VK_COLON = 0xBA;
        public const int VK_PLUS = 0xBB;
        public const int VK_COMMA = 0xBC;
        public const int VK_MINUS = 0xBD;
        public const int VK_PERIOD = 0xBE;
        public const int VK_QUESTION = 0xBF;
        public const int VK_TILDA = 0xC0;
        public const int SCANKEY_ESC = 0X76;
        public const int SCANKEY_F1 = 0X05;
        public const int SCANKEY_F2 = 0X06;
        public const int SCANKEY_F3 = 0X04;
        public const int SCANKEY_F4 = 0X0C;
        public const int SCANKEY_F5 = 0X03;
        public const int SCANKEY_F6 = 0X0B;
        public const int SCANKEY_F7 = 0X83;
        public const int SCANKEY_F8 = 0X0A;
        public const int SCANKEY_F9 = 0X01;
        public const int SCANKEY_F10 = 0X09;
        public const int SCANKEY_F11 = 0X78;
        public const int SCANKEY_F12 = 0X07;
        //public const int SCANKEY_PRINT = 0XE012E07C;
        public const int SCANKEY_SCROLL = 0X7E;
        //public const int SCANKEY_PAUSE = 0XE11477E1F014E077;
        public const int SCANKEY_TILDA = 0X0E;
        public const int SCANKEY_1 = 0X16;
        public const int SCANKEY_2 = 0X1E;
        public const int SCANKEY_3 = 0X26;
        public const int SCANKEY_4 = 0X25;
        public const int SCANKEY_5 = 0X2E;
        public const int SCANKEY_6 = 0X36;
        public const int SCANKEY_7 = 0X3D;
        public const int SCANKEY_8 = 0X3E;
        public const int SCANKEY_9 = 0X46;
        public const int SCANKEY_0 = 0X45;
        public const int SCANKEY_DASH = 0X4E;
        public const int SCANKEY_EQUALS = 0X55;
        public const int SCANKEY_BACKSPACE = 0X66;
        public const int SCANKEY_TAB = 0X0D;
        public const int SCANKEY_Q = 0X15;
        public const int SCANKEY_W = 0X1D;
        public const int SCANKEY_E = 0X24;
        public const int SCANKEY_R = 0X2D;
        public const int SCANKEY_T = 0X2C;
        public const int SCANKEY_Y = 0X35;
        public const int SCANKEY_U = 0X3C;
        public const int SCANKEY_I = 0X43;
        public const int SCANKEY_O = 0X44;
        public const int SCANKEY_P = 0X4D;
        public const int SCANKEY_LBRACKET = 0X54;
        public const int SCANKEY_RBRACKET = 0X5B;
        public const int SCANKEY_BACK_SLASH = 0X5D;
        public const int SCANKEY_CAPSLOCK = 0X58;
        public const int SCANKEY_A = 0X1C;
        public const int SCANKEY_S = 0X1B;
        public const int SCANKEY_D = 0X23;
        public const int SCANKEY_F = 0X2B;
        public const int SCANKEY_G = 0X34;
        public const int SCANKEY_H = 0X33;
        public const int SCANKEY_J = 0X3B;
        public const int SCANKEY_K = 0X42;
        public const int SCANKEY_L = 0X4B;
        public const int SCANKEY_COLON = 0X4C;
        public const int SCANKEY_QUOTE = 0X52;
        public const int SCANKEY_RETURN = 0X5A;
        public const int SCANKEY_LSHIFT = 0X12;
        public const int SCANKEY_Z = 0X1A;
        public const int SCANKEY_X = 0X22;
        public const int SCANKEY_C = 0X21;
        public const int SCANKEY_V = 0X2A;
        public const int SCANKEY_B = 0X32;
        public const int SCANKEY_N = 0X31;
        public const int SCANKEY_M = 0X3A;
        public const int SCANKEY_COMMA = 0X41;
        public const int SCANKEY_PERIOD = 0X49;
        public const int SCANKEY_FORWARD_SLASH = 0X4A;
        public const int SCANKEY_RSHIFT = 0X59;
        public const int SCANKEY_LCONTROL = 0X14;
        public const int SCANKEY_LWIN = 0XE01F;
        public const int SCANKEY_LALT = 0X11;
        public const int SCANKEY_SPACEBAR = 0X29;
        public const int SCANKEY_RALT = 0XE011;
        public const int SCANKEY_RWIN = 0XE027;
        public const int SCANKEY_MENUS = 0XE02F;
        public const int SCANKEY_RCONTROL = 0XE014;
        public const int SCANKEY_INSERT = 0XE070;
        public const int SCANKEY_HOME = 0XE06C;
        public const int SCANKEY_PAGE_UP = 0XE07D;
        public const int SCANKEY_DELETE = 0XE071;
        public const int SCANKEY_END = 0XE069;
        public const int SCANKEY_PAGE_DOWN = 0XE07A;
        public const int SCANKEY_UP = 0XE075;
        public const int SCANKEY_LEFT = 0XE06B;
        public const int SCANKEY_DOWN = 0XE072;
        public const int SCANKEY_RIGHT = 0XE074;
        public const int SCANKEY_NUMLOCK = 0X77;
        public const int SCANKEY_DICIVDE = 0XE04A;
        public const int SCANKEY_MULTIPLY = 0X7C;
        public const int SCANKEY_SUBTRACT = 0X7B;
        public const int SCANKEY_NUMPAD7 = 0X6C;
        public const int SCANKEY_NUMPAD8 = 0X75;
        public const int SCANKEY_NUMPAD9 = 0X7D;
        public const int SCANKEY_ADD = 0X79;
        public const int SCANKEY_NUMPAD4 = 0X6B;
        public const int SCANKEY_NUMPAD5 = 0X73;
        public const int SCANKEY_NUMPAD6 = 0X74;
        public const int SCANKEY_NUMPAD1 = 0X69;
        public const int SCANKEY_NUMPAD2 = 0X72;
        public const int SCANKEY_NUMPAD3 = 0X7A;
        public const int SCANKEY_NUMPAD0 = 0X70;
        public const int SCANKEY_DECIMAL = 0X71;
        public const int SCANKEY_ENTER = 0XE05A;

        public static void PressKey(byte key, byte scanKey)
        {
            keybd_event(key, scanKey, KEYBDEVENTF_KEYDOWN, 0);
        }
        public static void ReleaseKey(byte key, byte scanKey)
        {
            keybd_event(key, scanKey, KEYBDEVENTF_KEYUP, 0);
        }
    }
}