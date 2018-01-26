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

	    private TrackBar ActiveSlider;
	    private Label ActiveLabel;
	    private Stopwatch ClickStopwatch;
	    private bool UseClickTime;
        private bool Dropping;
        private bool DropInverse;
        private bool LogInfo;
        private bool recordClicks;
        private List<Click> Clicks;
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
	    private bool InfiniteLoop;
	    private bool ResetClicks;
        private Label trackLabel1;
        private Label lblClickSeconds;
        private GroupBox groupBox2;
        private CheckBox chkDropInverse;
        private GroupBox groupBox3;
        private TrackBar sliderClicks;
        private NumericUpDown numCount;
        private CheckBox chkLog;
        private RadioButton radioCycles;
        private RadioButton radioClicks;
        private Label lblCycleSeconds;
        private TrackBar sliderCycles;
        private Label label5;
        private Label label6;
        private Label label3;
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
            ClickStopwatch = new Stopwatch();
            UseClickTime = false;
            Dropping = false;
            DropInverse = false;
            LogInfo = true;
            ctrl = false;
            DropClickPos = 0;
            ClickCountPos = 0;
            Clicks = new List<Click>();
            Inventory = GetInventory();
            recordClicks = false;
            RunProgram = false;
            random = new Random();
            TotalCount = 0;
            Modifiers = new List<int> { -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5 };
            InfiniteLoop = false;
            ResetClicks = true;
            InitializeComponent();
            sliderCycles.Enabled = false;
            ActiveLabel = lblClickSeconds;
            ActiveSlider = sliderClicks;
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
            this.trackLabel1 = new System.Windows.Forms.Label();
            this.lblClickSeconds = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioCycles = new System.Windows.Forms.RadioButton();
            this.radioClicks = new System.Windows.Forms.RadioButton();
            this.lblCycleSeconds = new System.Windows.Forms.Label();
            this.sliderCycles = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.sliderClicks = new System.Windows.Forms.TrackBar();
            this.chkDropInverse = new System.Windows.Forms.CheckBox();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderCycles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClicks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.textBox.Location = new System.Drawing.Point(4, 39);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(310, 534);
            this.textBox.TabIndex = 3;
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMousePosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelMousePosition.Location = new System.Drawing.Point(4, 13);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(310, 23);
            this.labelMousePosition.TabIndex = 2;
            this.labelMousePosition.Text = "labelMousePosition";
            this.labelMousePosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonStart
            // 
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Location = new System.Drawing.Point(546, 541);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart);
            // 
            // buttonRecord
            // 
            this.buttonRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecord.Location = new System.Drawing.Point(421, 541);
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
            this.label1.Location = new System.Drawing.Point(78, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number of times to repeat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "(0 for infinite)";
            // 
            // trackLabel1
            // 
            this.trackLabel1.AutoSize = true;
            this.trackLabel1.Location = new System.Drawing.Point(299, 69);
            this.trackLabel1.Name = "trackLabel1";
            this.trackLabel1.Size = new System.Drawing.Size(74, 13);
            this.trackLabel1.TabIndex = 9;
            this.trackLabel1.Text = "Second Delay";
            // 
            // lblClickSeconds
            // 
            this.lblClickSeconds.AutoSize = true;
            this.lblClickSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickSeconds.Location = new System.Drawing.Point(327, 46);
            this.lblClickSeconds.Name = "lblClickSeconds";
            this.lblClickSeconds.Size = new System.Drawing.Size(15, 16);
            this.lblClickSeconds.TabIndex = 10;
            this.lblClickSeconds.Text = "1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkLog);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.chkDropInverse);
            this.groupBox2.Controls.Add(this.numCount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(320, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 522);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.radioCycles);
            this.groupBox3.Controls.Add(this.radioClicks);
            this.groupBox3.Controls.Add(this.lblCycleSeconds);
            this.groupBox3.Controls.Add(this.sliderCycles);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lblClickSeconds);
            this.groupBox3.Controls.Add(this.sliderClicks);
            this.groupBox3.Controls.Add(this.trackLabel1);
            this.groupBox3.Location = new System.Drawing.Point(7, 87);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(382, 191);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Clicks";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(185, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "(User click time)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "(Static click time)";
            // 
            // radioCycles
            // 
            this.radioCycles.AutoSize = true;
            this.radioCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCycles.Location = new System.Drawing.Point(12, 95);
            this.radioCycles.Name = "radioCycles";
            this.radioCycles.Size = new System.Drawing.Size(172, 20);
            this.radioCycles.TabIndex = 17;
            this.radioCycles.TabStop = true;
            this.radioCycles.Text = "Time between cycles";
            this.radioCycles.UseVisualStyleBackColor = true;
            // 
            // radioClicks
            // 
            this.radioClicks.AutoSize = true;
            this.radioClicks.Checked = true;
            this.radioClicks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioClicks.Location = new System.Drawing.Point(12, 19);
            this.radioClicks.Name = "radioClicks";
            this.radioClicks.Size = new System.Drawing.Size(167, 20);
            this.radioClicks.TabIndex = 16;
            this.radioClicks.TabStop = true;
            this.radioClicks.Text = "Time between clicks";
            this.radioClicks.UseVisualStyleBackColor = true;
            this.radioClicks.CheckedChanged += new System.EventHandler(this.radioClicks_CheckedChanged);
            // 
            // lblCycleSeconds
            // 
            this.lblCycleSeconds.AutoSize = true;
            this.lblCycleSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCycleSeconds.Location = new System.Drawing.Point(329, 124);
            this.lblCycleSeconds.Name = "lblCycleSeconds";
            this.lblCycleSeconds.Size = new System.Drawing.Size(15, 16);
            this.lblCycleSeconds.TabIndex = 14;
            this.lblCycleSeconds.Text = "1";
            // 
            // sliderCycles
            // 
            this.sliderCycles.Location = new System.Drawing.Point(12, 121);
            this.sliderCycles.Maximum = 100;
            this.sliderCycles.Minimum = 1;
            this.sliderCycles.Name = "sliderCycles";
            this.sliderCycles.Size = new System.Drawing.Size(286, 45);
            this.sliderCycles.TabIndex = 12;
            this.sliderCycles.TickFrequency = 25;
            this.sliderCycles.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.sliderCycles.Value = 10;
            this.sliderCycles.Scroll += new System.EventHandler(this.sliderCycles_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(301, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Second Delay";
            // 
            // sliderClicks
            // 
            this.sliderClicks.Location = new System.Drawing.Point(12, 43);
            this.sliderClicks.Maximum = 100;
            this.sliderClicks.Minimum = 1;
            this.sliderClicks.Name = "sliderClicks";
            this.sliderClicks.Size = new System.Drawing.Size(284, 45);
            this.sliderClicks.TabIndex = 8;
            this.sliderClicks.TickFrequency = 25;
            this.sliderClicks.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.sliderClicks.Value = 10;
            this.sliderClicks.Scroll += new System.EventHandler(this.sliderClicks_Scroll);
            // 
            // chkDropInverse
            // 
            this.chkDropInverse.AutoSize = true;
            this.chkDropInverse.Location = new System.Drawing.Point(7, 19);
            this.chkDropInverse.Name = "chkDropInverse";
            this.chkDropInverse.Size = new System.Drawing.Size(97, 17);
            this.chkDropInverse.TabIndex = 0;
            this.chkDropInverse.Text = "Drop From Top";
            this.chkDropInverse.UseVisualStyleBackColor = true;
            this.chkDropInverse.CheckedChanged += new System.EventHandler(this.chkDropInverse_CheckedChanged);
            // 
            // numCount
            // 
            this.numCount.Location = new System.Drawing.Point(7, 49);
            this.numCount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(65, 20);
            this.numCount.TabIndex = 7;
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Checked = true;
            this.chkLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLog.Location = new System.Drawing.Point(112, 19);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(79, 17);
            this.chkLog.TabIndex = 11;
            this.chkLog.Text = "Log Details";
            this.chkLog.UseVisualStyleBackColor = true;
            this.chkLog.CheckedChanged += new System.EventHandler(this.chkLog_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(727, 576);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonRecord);
            this.Name = "MainForm";
            this.Text = "This application captures keystrokes";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderCycles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClicks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
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
                ClickStopwatch.Stop();
            }
            else
            {
                buttonRecord.Text = "Recording...";
                ClickCountPos = 0;
                Clicks.Clear();
                ClickStopwatch.Reset();
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
			labelMousePosition.Text = String.Format("x={0}  y={1} wheel={2}", e.X, e.Y, e.Delta);
            if (e.Clicks > 0)
            {
                if (recordClicks)
                {
                    ClickStopwatch.Stop();
                    if (Clicks.Count > 0)
                    {
                        
                        Clicks[Clicks.Count - 1].DelayAfterClick = ClickStopwatch.ElapsedMilliseconds;
                        LogWrite("Delay from click " + (Clicks.Count - 1) + ": "+ ClickStopwatch.ElapsedMilliseconds);
                    }

                    Clicks.Add(new Click(
                        Cursor.Position,
                        e.Button == MouseButtons.Left ? 0 : 1,
                        0));
                    LogWrite("Added Click");
                    ClickStopwatch.Reset();
                    ClickStopwatch.Start();

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
            //Num Pad +
            if(e.KeyCode == Keys.Add)
            {
                ActiveSlider.Value++;
                UpdateTrack();
            }
            //Num Pad -
            if (e.KeyCode == Keys.Subtract)
            {
                ActiveSlider.Value--;
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
            Click currentClick = Clicks[ClickCountPos];
            CurrentPoint = currentClick.ClickPoint;
            var mouseButton = currentClick.ClickType;
            if(radioCycles.Checked)
                ClickTimer.Interval = (int)currentClick.DelayAfterClick;

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
                    if (radioCycles.Checked)
                        ClickTimer.Interval = GetInterval();
                    ClickCountPos = 0;
                    TotalCount--;
                    if (!InfiniteLoop)
                        numCount.Value = TotalCount;
                }
                    
            }

            CurrentPoint.X += GetRandomOffset();
            CurrentPoint.Y += GetRandomOffset();

            DoMouseClick(mouseButton,CurrentPoint);
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

        public void DoMouseClick(int mouseButton, Point point)
        {
            
            //set cursor position to memorized location
            Cursor.Position = point;
            Thread.Sleep(100);
            //set up the INPUT struct and fill it for the mouse down
            INPUT i = new INPUT();
            i.type = INPUT_MOUSE;
            i.mi.dx = 0; //clickLocation.X;
            i.mi.dy = 0; // clickLocation.Y;
            i.mi.dwFlags = mouseButton == 0 ? MOUSEEVENTF_LEFTDOWN : MOUSEEVENTF_RIGHTDOWN;
            i.mi.dwExtraInfo = IntPtr.Zero;
            i.mi.mouseData = 0;
            i.mi.time = 0;
            //send the input
            SendInput(1, ref i, Marshal.SizeOf(i));
            //set the INPUT for mouse up and send
            i.mi.dwFlags = mouseButton == 0 ? MOUSEEVENTF_LEFTUP : MOUSEEVENTF_RIGHTUP;
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
                ClickStopwatch.Stop();
            }

            RunProgram = !RunProgram;
            if (RunProgram && !ClickTimer.Enabled)
            {
                sliderClicks.Enabled = false;
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
                foreach (var click in Clicks)
                {
                    LogWrite("X:" + click.ClickPoint.X + "  Y:" + click.ClickPoint.Y);
                }
                LogWrite("------------");
                LogWrite("Starting Auto Clicker");
                ClickTimer.Start();
                Thread.Sleep(3000);
            }
            else
            {
                ClickTimer.Stop();
                sliderClicks.Enabled = true;
                numCount.Enabled = true;
                buttonRecord.Enabled = true;
                buttonStart.Text = "Start";
                LogWrite("Stopping Auto Clicker");
            }
        }

        private void sliderClicks_Scroll(object sender, EventArgs e)
        {
            UpdateTrack();
        }

	    private int GetInterval()
	    {
	        return (int)((ActiveSlider.Value / 10.0) * 1000);
	    }

        private void UpdateTrack()
        {
            var interval = GetInterval();
            ActiveLabel.Text = (interval / 1000.0).ToString();
            if(radioClicks.Checked)
                ClickTimer.Interval = interval;
        }

        private void sliderCycles_Scroll(object sender, EventArgs e)
        {
            UpdateTrack();
        }

        private void radioClicks_CheckedChanged(object sender, EventArgs e)
        {
            var radio = (RadioButton)sender;
            if (radio.Checked)
            {
                sliderCycles.Enabled = false;
                sliderClicks.Enabled = true;
                ActiveSlider = sliderClicks;
                ActiveLabel = lblClickSeconds;
                ClickTimer.Interval = GetInterval();
            }
            else
            {
                sliderCycles.Enabled = true;
                sliderClicks.Enabled = false;
                ActiveSlider = sliderCycles;
                ActiveLabel = lblCycleSeconds;
            }

        }

	    private void chkLog_CheckedChanged(object sender, EventArgs e)
	    {
	        var checkBox = (CheckBox)sender;
	        if (checkBox.Checked)
	            LogInfo = true;
	        else
	            LogInfo = false;
	    }
	    private void LogWrite(string txt)
	    {
	        if (LogInfo)
	        {
	            textBox.AppendText(txt + Environment.NewLine);
	            textBox.SelectionStart = textBox.Text.Length;
	        }

	    }

        //-------------------------------------------------------------------------------------------------------
        private void chkDropInverse_CheckedChanged(object sender, EventArgs e)
	    {
	        var checkBox = (CheckBox)sender;
	        if (checkBox.Checked)
	            DropInverse = true;
	        else
	            DropInverse = false;
	    }

	    private void DropInventory()
	    {
	        if (DropInverse)
	            DropClickPos = 27;
	        else
	            DropClickPos = 0;
	        DropTimer.Start();
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
	            for (int c = 0; c < 4; c++)
	            {
	                var x = (int)(xStartValue - xOffsetValue * c);
	                var y = (int)(yStartValue - yOffsetValue * r);
	                inventory.Add(new Point(x, y));
	            }
	        }
	        return inventory;
	    }
	    private void DropTimer_Tick(object sender, EventArgs e)
	    {
	        if ((DropClickPos >= Inventory.Count && !DropInverse) || (DropClickPos <= 0 && DropInverse))
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

	        DoMouseClick(0, CurrentPoint);
	    }
    }			
}

