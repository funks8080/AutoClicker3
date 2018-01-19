using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using gma.System.Windows;

namespace GlobalHookDemo 
{
	class MainForm : System.Windows.Forms.Form
	{
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button buttonRecord;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Label labelMousePosition;
		private System.Windows.Forms.TextBox textBox;
        private bool runProgram;
        public bool RunProgram
        {
            get { return runProgram; }
            set
            {
                runProgram = value;
            }
        }
        private bool Dropping;
        private bool DropInverse;
        private bool LogInfo;
        private bool recordClicks;
        private List<Point> Clicks;
        private List<Point> Inventory;
        private int DropClickPos;
        private int ClickCountPos;
        private Point CurrentPoint;
        private System.Windows.Forms.Timer ClickTimer;
        private System.Windows.Forms.Timer DropTimer;
        private Random random;
        private int randomNumber;
        private int TotalCount;
	    private bool ctrl;
        private Label label1;
        private Label label2;
        private NumericUpDown numCount;
	    private bool InfiniteLoop;
	    private bool ResetClicks;
        private TrackBar trackTimer;
        private Label trackLabel1;
        private Label trackLabel;
        private CheckBox chkLog;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private CheckBox chkDropInverse;
        List<int> Modifiers;

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int SendInput(int nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        const int VK_SHIFT = 0x10; //up key
        const int VK_DOWN = 0x28;  //down key
        const int VK_LEFT = 0x25;
        const int VK_RIGHT = 0x27;
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        int press()
        {

            //Press the key
            keybd_event((byte)VK_SHIFT, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
            return 0;

        }
        int release()
        {
            //Release the key
            keybd_event((byte)VK_SHIFT, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
            return 0;
        }

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        const int INPUT_MOUSE = 0;

        public MainForm()
        {
            Dropping = false;
            DropInverse = false;
            LogInfo = true;
            ctrl = false;
            DropClickPos = 0;
            ClickCountPos = 0;
            Clicks = new List<Point>();
            Inventory = GetInventory();
            recordClicks = false;
            RunProgram = false;
            random = new Random();
            TotalCount = 0;
            Modifiers = new List<int> { -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5 };
            InfiniteLoop = false;
            ResetClicks = true;
            InitializeComponent();
		}
	
		// THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
		// DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
		void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.textBox = new System.Windows.Forms.TextBox();
            this.labelMousePosition = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonRecord = new System.Windows.Forms.Button();
            this.ClickTimer = new System.Windows.Forms.Timer(this.components);
            this.DropTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.trackTimer = new System.Windows.Forms.TrackBar();
            this.trackLabel1 = new System.Windows.Forms.Label();
            this.trackLabel = new System.Windows.Forms.Label();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkDropInverse = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTimer)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.textBox.Location = new System.Drawing.Point(4, 142);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(308, 253);
            this.textBox.TabIndex = 3;
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMousePosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelMousePosition.Location = new System.Drawing.Point(4, 116);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(309, 23);
            this.labelMousePosition.TabIndex = 2;
            this.labelMousePosition.Text = "labelMousePosition";
            this.labelMousePosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonStart
            // 
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Location = new System.Drawing.Point(445, 363);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart);
            // 
            // buttonRecord
            // 
            this.buttonRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecord.Location = new System.Drawing.Point(320, 363);
            this.buttonRecord.Name = "buttonRecord";
            this.buttonRecord.Size = new System.Drawing.Size(107, 23);
            this.buttonRecord.TabIndex = 0;
            this.buttonRecord.Text = "Record Clicks";
            this.buttonRecord.Click += new System.EventHandler(this.ButtonRecord);
            // 
            // ClickTimer
            // 
            this.ClickTimer.Interval = 1000;
            this.ClickTimer.Tick += new System.EventHandler(this.ClickTimer_Tick);
            // 
            // DropTimer
            // 
            this.DropTimer.Interval = 200;
            this.DropTimer.Tick += new System.EventHandler(this.DropTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number of times to repeat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "(0 for infinite)";
            // 
            // numCount
            // 
            this.numCount.Location = new System.Drawing.Point(10, 20);
            this.numCount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(65, 20);
            this.numCount.TabIndex = 7;
            // 
            // trackTimer
            // 
            this.trackTimer.Location = new System.Drawing.Point(6, 59);
            this.trackTimer.Maximum = 50;
            this.trackTimer.Minimum = 10;
            this.trackTimer.Name = "trackTimer";
            this.trackTimer.Size = new System.Drawing.Size(199, 45);
            this.trackTimer.TabIndex = 8;
            this.trackTimer.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackTimer.Value = 10;
            this.trackTimer.Scroll += new System.EventHandler(this.trackTimer_Scroll);
            // 
            // trackLabel1
            // 
            this.trackLabel1.AutoSize = true;
            this.trackLabel1.Location = new System.Drawing.Point(218, 91);
            this.trackLabel1.Name = "trackLabel1";
            this.trackLabel1.Size = new System.Drawing.Size(74, 13);
            this.trackLabel1.TabIndex = 9;
            this.trackLabel1.Text = "Second Delay";
            // 
            // trackLabel
            // 
            this.trackLabel.AutoSize = true;
            this.trackLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trackLabel.Location = new System.Drawing.Point(249, 68);
            this.trackLabel.Name = "trackLabel";
            this.trackLabel.Size = new System.Drawing.Size(15, 16);
            this.trackLabel.TabIndex = 10;
            this.trackLabel.Text = "1";
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Checked = true;
            this.chkLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLog.Location = new System.Drawing.Point(229, 28);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(79, 17);
            this.chkLog.TabIndex = 11;
            this.chkLog.Text = "Log Details";
            this.chkLog.UseVisualStyleBackColor = true;
            this.chkLog.CheckedChanged += new System.EventHandler(this.chkLog_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkLog);
            this.groupBox1.Controls.Add(this.numCount);
            this.groupBox1.Controls.Add(this.trackTimer);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 110);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkDropInverse);
            this.groupBox2.Location = new System.Drawing.Point(320, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 330);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Quick Buttons";
            // 
            // chkDropInverse
            // 
            this.chkDropInverse.AutoSize = true;
            this.chkDropInverse.Location = new System.Drawing.Point(7, 18);
            this.chkDropInverse.Name = "chkDropInverse";
            this.chkDropInverse.Size = new System.Drawing.Size(97, 17);
            this.chkDropInverse.TabIndex = 0;
            this.chkDropInverse.Text = "Drop From Top";
            this.chkDropInverse.UseVisualStyleBackColor = true;
            this.chkDropInverse.CheckedChanged += new System.EventHandler(this.chkDropInverse_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(560, 398);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.trackLabel);
            this.Controls.Add(this.trackLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonRecord);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "This application captures keystrokes";
            this.Load += new System.EventHandler(this.MainFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackTimer)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
			
		[STAThread]
		public static void Main(string[] args)
		{
			Application.Run(new MainForm());
		}

        void ButtonRecord(object sender, System.EventArgs e)
		{
            if (recordClicks)
            {
                buttonRecord.Text = "Record Clicks";
            }
            else
            {
                buttonRecord.Text = "Recording...";
                ClickCountPos = 0;
                Clicks.Clear();
            }

            recordClicks = !recordClicks;
		    ResetClicks = true;

		}
		
		void ButtonStart(object sender, System.EventArgs e)
		{
            RunClicks();
		}
		
		
		UserActivityHook actHook;
		void MainFormLoad(object sender, System.EventArgs e)
		{
            actHook = new UserActivityHook(); // crate an instance with global hooks
			// hang on events
			actHook.OnMouseActivity+=new MouseEventHandler(MouseMoved);
			actHook.KeyDown+=new KeyEventHandler(MyKeyDown);
			actHook.KeyPress+=new KeyPressEventHandler(MyKeyPress);
			actHook.KeyUp+=new KeyEventHandler(MyKeyUp);

            actHook.Start();
        }
		
		public void MouseMoved(object sender, MouseEventArgs e)
		{
			labelMousePosition.Text=String.Format("x={0}  y={1} wheel={2}", e.X, e.Y, e.Delta);
            if (e.Clicks > 0)
            {
                if (recordClicks)
                {
                    Clicks.Add(Cursor.Position);
                    LogWrite("Added Click");
                }
            }
		}
		
		public void MyKeyUp(object sender, KeyEventArgs e)
		{
		    if (e.KeyCode == Keys.LControlKey)
		        ctrl = false;
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                DropTimer.Stop();
                Dropping = false;
            }
        }
		
		public void MyKeyPress(object sender, KeyPressEventArgs e)
		{
            //LogWrite(e.KeyChar.GetType()..ToString());
        }
		
		public void MyKeyDown(object sender, KeyEventArgs e)
		{
		    if (e.KeyCode == Keys.LControlKey)
		        ctrl = true;
            if (e.KeyCode == Keys.D1 && ctrl)
            {
                RunClicks();
                ctrl = false;
            }
            if (e.KeyCode == Keys.L && ctrl)
            {
                chkLog.Checked = !chkLog.Checked;
            }
            if(e.KeyCode == Keys.Add)
            {
                trackTimer.Value++;
                UpdateTrack();
            }
            if (e.KeyCode == Keys.Subtract)
            {
                trackTimer.Value--;
                UpdateTrack();
            }
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                if (!Dropping)
                {
                    Dropping = true;
                    DropInventory();
                    //foreach(var point in Inventory)
                    //{
                    //    LogWrite("X:" + point.X + " Y:" + point.Y);
                    //}
                }
            }

        }

        private void ClickTimer_Tick(object sender, EventArgs e)
        {
            //set cursor position to memorized location
            CurrentPoint = Clicks[ClickCountPos];

            if (TotalCount <= 0 && !InfiniteLoop)
            {
                RunClicks();
                return;
            }

            if (Clicks.Count >= 1)
            {
                ClickCountPos++;
                if (ClickCountPos > Clicks.Count - 1)
                {
                    ClickCountPos = 0;
                    TotalCount--;
                    if (!InfiniteLoop)
                        numCount.Value = TotalCount;
                }
                    
            }

            CurrentPoint.X += GetRandomOffset();
            CurrentPoint.Y += GetRandomOffset();

            DoMouseClick(CurrentPoint);
        }

        private void DropTimer_Tick(object sender, EventArgs e)
        {
            if ((DropClickPos >= Inventory.Count  && !DropInverse ) || (DropClickPos <= 0 && DropInverse))
            {
                DropTimer.Stop();
                return;
            }

            //set cursor position to memorized location
            CurrentPoint = Inventory[DropClickPos];
            if (DropInverse)
                DropClickPos--;
            else
                DropClickPos++;

            DoMouseClick(CurrentPoint);
        }

        private void LogWrite(string txt)
		{
            if (LogInfo)
            {
                textBox.AppendText(txt + Environment.NewLine);
                textBox.SelectionStart = textBox.Text.Length;
            }
			
		}

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

        public void DoMouseClick(Point point)
        {
            
            //set cursor position to memorized location
            Cursor.Position = point;
            Thread.Sleep(100);
            //set up the INPUT struct and fill it for the mouse down
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

            LogWrite("Clicked at X:" + point.X + "  Y:" + point.Y);
        }

        public int GetRandomOffset()
        {
            randomNumber = random.Next(0, Modifiers.Count);
            return Modifiers[randomNumber];
        }

        public void RunClicks()
        {
            TotalCount = (int)numCount.Value;
            if (TotalCount == 0)
                InfiniteLoop = true;
            else
                InfiniteLoop = false;

            if (recordClicks)
            {
                buttonRecord.Text = "Record Clicks";
                recordClicks = !recordClicks;
            }

            RunProgram = !RunProgram;
            if (RunProgram && !ClickTimer.Enabled)
            {
                trackTimer.Enabled = false;
                numCount.Enabled = false;
                buttonRecord.Enabled = false;
                ClickCountPos = 0;

                if (Clicks.Count > 0 && ResetClicks)
                {
                    Clicks.RemoveAt(Clicks.Count - 1);
                    ResetClicks = false;
                }
                   
                if (Clicks.Count < 1)
                {
                    MessageBox.Show("Need to have at least one click");
                    RunProgram = !RunProgram;
                    Clicks.Clear();
                    return;
                }
                buttonStart.Text = "Stop";
                LogWrite("------------");
                foreach (var point in Clicks)
                {
                    LogWrite("X:" + point.X + "  Y:" + point.Y);
                }
                LogWrite("------------");
                LogWrite("Starting Auto Clicker");
                ClickTimer.Start();
                Thread.Sleep(3000);
            }
            else
            {
                ClickTimer.Stop();
                trackTimer.Enabled = true;
                numCount.Enabled = true;
                buttonRecord.Enabled = true;
                buttonStart.Text = "Start";
                LogWrite("Stopping Auto Clicker");
            }
        }

        private void trackTimer_Scroll(object sender, EventArgs e)
        {
            UpdateTrack();
        }

	    private int GetInterval()
	    {
	        return (int) ((trackTimer.Value / 10.0) * 1000);
	    }

        private void chkLog_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox.Checked)
                LogInfo = true;
            else
                LogInfo = false;
        }

        private void UpdateTrack()
        {
            var interval = GetInterval();
            trackLabel.Text = (interval / 1000.0).ToString();
            ClickTimer.Interval = interval;
        }

        private void chkDropInverse_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            if (checkBox.Checked)
                DropInverse = true;
            else
                DropInverse = false;
        }

        private List<Point> GetInventory()
        {
            var inventory = new List<Point>();
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;

            var xStartOffsetPercent = 0.02; //9.5%
            var yStartOffsetPercent = 0.095; //2%
            var xOffsetPercent = 0.0215; // 2.15%
            var yOffsetPercent = 0.035; // 3.5%
            var xOffsetValue = width * xOffsetPercent; // 2.15%
            var yOffsetValue = height * yOffsetPercent; // 3.5%
            var xStartValue = width * (1 - xStartOffsetPercent);
            var yStartValue = height * (1 - yStartOffsetPercent);

            for (int r = 0; r < 7; r++)
            {
                for(int c = 0; c < 4; c++)
                {
                    var x = (int)(xStartValue - xOffsetValue * c);
                    var y = (int)(yStartValue - yOffsetValue * r);
                    inventory.Add(new Point(x,y));
                }
            }
            return inventory;
        }

        private void DropInventory()
        {
            if (DropInverse)
                DropClickPos = 27;
            else
                DropClickPos = 0;
            DropTimer.Start();
        }
    }			
}

