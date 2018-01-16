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
        private bool recordClicks;
        private List<Point> Clicks;
        private int ClickCountPos;
        private Point CurrentPoint;
        private System.Windows.Forms.Timer timer1;
        private Random random;
        private int randomNumber;
        private int TotalCount;
	    private bool ctrl;
        private Label label1;
        private Label label2;
        private NumericUpDown numCount;
	    private bool InfiniteLoop;
	    private bool ResetClicks;
        List<int> Modifiers;

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern int SendInput(int nInputs, ref INPUT pInputs, int cbSize);

        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        const int INPUT_MOUSE = 0;
	    private int intervalTime;

        public MainForm()
        {
            ctrl = false;
            ClickCountPos = 0;
            intervalTime = 1500;
            Clicks = new List<Point>();
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numCount = new System.Windows.Forms.NumericUpDown();
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
            this.textBox.Location = new System.Drawing.Point(4, 106);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(322, 289);
            this.textBox.TabIndex = 3;
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMousePosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelMousePosition.Location = new System.Drawing.Point(4, 80);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(322, 23);
            this.labelMousePosition.TabIndex = 2;
            this.labelMousePosition.Text = "labelMousePosition";
            this.labelMousePosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonStart
            // 
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Location = new System.Drawing.Point(119, 3);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart);
            // 
            // buttonRecord
            // 
            this.buttonRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecord.Location = new System.Drawing.Point(4, 3);
            this.buttonRecord.Name = "buttonRecord";
            this.buttonRecord.Size = new System.Drawing.Size(107, 23);
            this.buttonRecord.TabIndex = 0;
            this.buttonRecord.Text = "Record Clicks";
            this.buttonRecord.Click += new System.EventHandler(this.ButtonRecord);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number of times to repeat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "(0 for infinite)";
            // 
            // numCount
            // 
            this.numCount.Location = new System.Drawing.Point(13, 45);
            this.numCount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(65, 20);
            this.numCount.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(328, 398);
            this.Controls.Add(this.numCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonRecord);
            this.Name = "MainForm";
            this.Text = "This application captures keystrokes";
            this.Load += new System.EventHandler(this.MainFormLoad);
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
        }
		
		public void MyKeyPress(object sender, KeyPressEventArgs e)
		{
		    //Nada
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
			    
		}

        private void timer1_Tick(object sender, EventArgs e)
        {
            //set cursor position to memorized location
            CurrentPoint = Clicks[ClickCountPos];
            if(Clicks.Count > 1)
            {
                ClickCountPos++;
                if (ClickCountPos > Clicks.Count - 1)
                    ClickCountPos = 0;
            }

            DoMouseClick(CurrentPoint);
        }

        private void LogWrite(string txt)
		{
			textBox.AppendText(txt + Environment.NewLine);
			textBox.SelectionStart = textBox.Text.Length;
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

            if(TotalCount <= 0 && !InfiniteLoop)
            {
                RunClicks();
                return;
            }
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);

            point.X += GetRandomOffset();
            point.Y += GetRandomOffset();
            //set cursor position to memorized location
            Cursor.Position = point;
            Thread.Sleep(400);
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
            TotalCount--;
            if (!InfiniteLoop)
                numCount.Value--;

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
            if (RunProgram && !timer1.Enabled)
            {
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
                timer1.Start();
                Thread.Sleep(3000);
            }
            else
            {
                buttonStart.Text = "Start";
                timer1.Stop();
                LogWrite("Stopping Auto Clicker");
            }
        }
       
    }			
}

