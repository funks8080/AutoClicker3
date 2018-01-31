using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using gma.System.Windows;

namespace AutoClicker 
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

	    private int TotalInventoryClickCount;
	    private int CurrentInventoryClickCount;
        private static string FilePath = "c:\\AppData\\AutoClicker\\Inventory.txt";
		private bool InfiniteLoop;
		private bool ResetClicks;
		private bool TimedOut;
		private bool HideButtons;
		private bool Ctrl;
		private bool SetupInventory;
		private bool UseRandomTimeouts;
		private bool Dropping;
		private bool DropInverse;
		private bool LogInfo;
		private bool RecordClicks;
		private int RandomTimeoutCount;
		private int ClickOffset;
		private int DropClickPos;
		private int ClickCountPos;
		private int TotalCount;
		private TrackBar ActiveSlider;
		private Stopwatch ClickStopwatch;
		private List<Click> Clicks;
		private List<Point> Inventory;
		private Point CurrentPoint;
		private System.Windows.Forms.Timer ClickTimer;
		private System.Windows.Forms.Timer DropTimer;
		private Random RandomGenerate;

		private Label ActiveLabel;
		private Label label1;
		private Label label2;
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
		private Button btnSetupInventory;
		private Button btnSingleClickInv;
		private Label lblClickOffsetNumber;
		private Label label7;
		private Label lblClickOffset;
		private TrackBar sliderClickOffset;
		private CheckBox chkTimeOut;
		private Button btnHide;
		private ContextMenuStrip contextMenuStrip1;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem startToolStripMenuItem;
		private ToolStripMenuItem stopToolStripMenuItem;
		private ToolStripMenuItem saveInventoryToolStripMenuItem;
		private ToolStripMenuItem loadInventoryToolStripMenuItem;
        private ToolStripMenuItem toggleLoggingToolStripMenuItem;
        private Label lblTotalInventory;
        private Label label8;
        private Label label4;
        private NumericUpDown numInventoryCount;
        private Button btnDropInventory;
        private GroupBox groupBox1;

		[DllImport("User32.dll", SetLastError = true)]
		public static extern int SendInput(int nInputs, ref INPUT pInputs, int cbSize);

		const int VK_SHIFT = 0x10; //up key
		const int VK_DOWN = 0x28;  //down key
		const int VK_LEFT = 0x25;
		const int VK_RIGHT = 0x27;
		const uint KEYEVENTF_KEYUP = 0x0002;
		const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

		//Mouse actions
		private const int MOUSEEVENTF_LEFTDOWN = 0x02;
		private const int MOUSEEVENTF_LEFTUP = 0x04;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
		private const int MOUSEEVENTF_RIGHTUP = 0x10;
		const int INPUT_MOUSE = 0;

		public MainForm()
		{
		    TotalInventoryClickCount = 0;
		    CurrentInventoryClickCount = 0;
            TimedOut = false;
			HideButtons = false;
			RandomTimeoutCount = 0;
			UseRandomTimeouts = false;
			ClickOffset = 3;
			SetupInventory = false;
			ClickStopwatch = new Stopwatch();
			Dropping = false;
			DropInverse = false;
			LogInfo = true;
			Ctrl = false;
			DropClickPos = 0;
			ClickCountPos = 0;
			Clicks = new List<Click>();
			Inventory = new List<Point>();
			RecordClicks = false;
			RunProgram = false;
			RandomGenerate = new Random();
			TotalCount = 0;
			InfiniteLoop = false;
			ResetClicks = true;
			InitializeComponent();
			sliderCycles.Enabled = false;
			ActiveLabel = lblClickSeconds;
			ActiveSlider = sliderClicks;
		    stopToolStripMenuItem.Enabled = false;
		    LoadInventory();

		}
	
		// THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
		// DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
		void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.btnHide = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblClickOffsetNumber = new System.Windows.Forms.Label();
            this.chkTimeOut = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblClickOffset = new System.Windows.Forms.Label();
            this.sliderClickOffset = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.radioCycles = new System.Windows.Forms.RadioButton();
            this.radioClicks = new System.Windows.Forms.RadioButton();
            this.lblCycleSeconds = new System.Windows.Forms.Label();
            this.sliderCycles = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.sliderClicks = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalInventory = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numInventoryCount = new System.Windows.Forms.NumericUpDown();
            this.btnDropInventory = new System.Windows.Forms.Button();
            this.chkDropInverse = new System.Windows.Forms.CheckBox();
            this.btnSetupInventory = new System.Windows.Forms.Button();
            this.btnSingleClickInv = new System.Windows.Forms.Button();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClickOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderCycles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClicks)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInventoryCount)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.textBox.Location = new System.Drawing.Point(4, 50);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(310, 523);
            this.textBox.TabIndex = 3;
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMousePosition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelMousePosition.Location = new System.Drawing.Point(4, 24);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(205, 23);
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
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number of times to repeat";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "(0 for infinite)";
            // 
            // trackLabel1
            // 
            this.trackLabel1.AutoSize = true;
            this.trackLabel1.Location = new System.Drawing.Point(299, 114);
            this.trackLabel1.Name = "trackLabel1";
            this.trackLabel1.Size = new System.Drawing.Size(74, 13);
            this.trackLabel1.TabIndex = 9;
            this.trackLabel1.Text = "Second Delay";
            // 
            // lblClickSeconds
            // 
            this.lblClickSeconds.AutoSize = true;
            this.lblClickSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickSeconds.Location = new System.Drawing.Point(327, 91);
            this.lblClickSeconds.Name = "lblClickSeconds";
            this.lblClickSeconds.Size = new System.Drawing.Size(15, 16);
            this.lblClickSeconds.TabIndex = 10;
            this.lblClickSeconds.Text = "1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnHide);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(320, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 511);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(253, 469);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(136, 23);
            this.btnHide.TabIndex = 15;
            this.btnHide.Text = "Hide Buttons";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblClickOffsetNumber);
            this.groupBox3.Controls.Add(this.chkTimeOut);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblClickOffset);
            this.groupBox3.Controls.Add(this.sliderClickOffset);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.numCount);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.radioCycles);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.radioClicks);
            this.groupBox3.Controls.Add(this.lblCycleSeconds);
            this.groupBox3.Controls.Add(this.sliderCycles);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.lblClickSeconds);
            this.groupBox3.Controls.Add(this.sliderClicks);
            this.groupBox3.Controls.Add(this.trackLabel1);
            this.groupBox3.Location = new System.Drawing.Point(6, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(383, 330);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Clicks";
            // 
            // lblClickOffsetNumber
            // 
            this.lblClickOffsetNumber.AutoSize = true;
            this.lblClickOffsetNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickOffsetNumber.Location = new System.Drawing.Point(327, 233);
            this.lblClickOffsetNumber.Name = "lblClickOffsetNumber";
            this.lblClickOffsetNumber.Size = new System.Drawing.Size(15, 16);
            this.lblClickOffsetNumber.TabIndex = 23;
            this.lblClickOffsetNumber.Text = "3";
            // 
            // chkTimeOut
            // 
            this.chkTimeOut.AutoSize = true;
            this.chkTimeOut.Location = new System.Drawing.Point(12, 300);
            this.chkTimeOut.Name = "chkTimeOut";
            this.chkTimeOut.Size = new System.Drawing.Size(125, 17);
            this.chkTimeOut.TabIndex = 14;
            this.chkTimeOut.Text = "Use random timeouts";
            this.chkTimeOut.UseVisualStyleBackColor = true;
            this.chkTimeOut.CheckedChanged += new System.EventHandler(this.chkTimeOut_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(319, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Pixels";
            // 
            // lblClickOffset
            // 
            this.lblClickOffset.AutoSize = true;
            this.lblClickOffset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickOffset.Location = new System.Drawing.Point(26, 214);
            this.lblClickOffset.Name = "lblClickOffset";
            this.lblClickOffset.Size = new System.Drawing.Size(86, 16);
            this.lblClickOffset.TabIndex = 21;
            this.lblClickOffset.Text = "Click Offset";
            // 
            // sliderClickOffset
            // 
            this.sliderClickOffset.LargeChange = 1;
            this.sliderClickOffset.Location = new System.Drawing.Point(12, 233);
            this.sliderClickOffset.Minimum = 1;
            this.sliderClickOffset.Name = "sliderClickOffset";
            this.sliderClickOffset.Size = new System.Drawing.Size(286, 45);
            this.sliderClickOffset.TabIndex = 20;
            this.sliderClickOffset.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.sliderClickOffset.Value = 3;
            this.sliderClickOffset.Scroll += new System.EventHandler(this.sliderClickOffset_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(185, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "(User click time)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "(Static click time)";
            // 
            // numCount
            // 
            this.numCount.Location = new System.Drawing.Point(12, 38);
            this.numCount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(65, 20);
            this.numCount.TabIndex = 7;
            // 
            // radioCycles
            // 
            this.radioCycles.AutoSize = true;
            this.radioCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCycles.Location = new System.Drawing.Point(12, 140);
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
            this.radioClicks.Location = new System.Drawing.Point(12, 64);
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
            this.lblCycleSeconds.Location = new System.Drawing.Point(329, 169);
            this.lblCycleSeconds.Name = "lblCycleSeconds";
            this.lblCycleSeconds.Size = new System.Drawing.Size(15, 16);
            this.lblCycleSeconds.TabIndex = 14;
            this.lblCycleSeconds.Text = "1";
            // 
            // sliderCycles
            // 
            this.sliderCycles.Location = new System.Drawing.Point(12, 166);
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
            this.label5.Location = new System.Drawing.Point(301, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Second Delay";
            // 
            // sliderClicks
            // 
            this.sliderClicks.Location = new System.Drawing.Point(12, 88);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalInventory);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numInventoryCount);
            this.groupBox1.Controls.Add(this.btnDropInventory);
            this.groupBox1.Controls.Add(this.chkDropInverse);
            this.groupBox1.Controls.Add(this.btnSetupInventory);
            this.groupBox1.Controls.Add(this.btnSingleClickInv);
            this.groupBox1.Location = new System.Drawing.Point(7, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 101);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inventory";
            // 
            // lblTotalInventory
            // 
            this.lblTotalInventory.AutoSize = true;
            this.lblTotalInventory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalInventory.Location = new System.Drawing.Point(96, 64);
            this.lblTotalInventory.Name = "lblTotalInventory";
            this.lblTotalInventory.Size = new System.Drawing.Size(17, 18);
            this.lblTotalInventory.TabIndex = 26;
            this.lblTotalInventory.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(82, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 20);
            this.label8.TabIndex = 25;
            this.label8.Text = "/";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "Number of inv to click";
            // 
            // numInventoryCount
            // 
            this.numInventoryCount.Location = new System.Drawing.Point(12, 64);
            this.numInventoryCount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numInventoryCount.Name = "numInventoryCount";
            this.numInventoryCount.Size = new System.Drawing.Size(64, 20);
            this.numInventoryCount.TabIndex = 24;
            this.numInventoryCount.ValueChanged += new System.EventHandler(this.numInventoryCount_ValueChanged);
            // 
            // btnDropInventory
            // 
            this.btnDropInventory.Location = new System.Drawing.Point(236, 72);
            this.btnDropInventory.Name = "btnDropInventory";
            this.btnDropInventory.Size = new System.Drawing.Size(137, 23);
            this.btnDropInventory.TabIndex = 14;
            this.btnDropInventory.Text = "Drop Inventory";
            this.btnDropInventory.UseVisualStyleBackColor = true;
            this.btnDropInventory.Click += new System.EventHandler(this.btnDropInventory_Click);
            // 
            // chkDropInverse
            // 
            this.chkDropInverse.AutoSize = true;
            this.chkDropInverse.Location = new System.Drawing.Point(12, 19);
            this.chkDropInverse.Name = "chkDropInverse";
            this.chkDropInverse.Size = new System.Drawing.Size(97, 17);
            this.chkDropInverse.TabIndex = 0;
            this.chkDropInverse.Text = "Drop From Top";
            this.chkDropInverse.UseVisualStyleBackColor = true;
            this.chkDropInverse.CheckedChanged += new System.EventHandler(this.chkDropInverse_CheckedChanged);
            // 
            // btnSetupInventory
            // 
            this.btnSetupInventory.Location = new System.Drawing.Point(236, 13);
            this.btnSetupInventory.Name = "btnSetupInventory";
            this.btnSetupInventory.Size = new System.Drawing.Size(137, 23);
            this.btnSetupInventory.TabIndex = 12;
            this.btnSetupInventory.Text = "Setup Inventory";
            this.btnSetupInventory.UseVisualStyleBackColor = true;
            this.btnSetupInventory.Click += new System.EventHandler(this.btnSetupInventory_Click);
            // 
            // btnSingleClickInv
            // 
            this.btnSingleClickInv.Location = new System.Drawing.Point(236, 42);
            this.btnSingleClickInv.Name = "btnSingleClickInv";
            this.btnSingleClickInv.Size = new System.Drawing.Size(137, 23);
            this.btnSingleClickInv.TabIndex = 13;
            this.btnSingleClickInv.Text = "Single Click Inv";
            this.btnSingleClickInv.UseVisualStyleBackColor = true;
            this.btnSingleClickInv.Click += new System.EventHandler(this.btnSingleClickInv_Click);
            // 
            // chkLog
            // 
            this.chkLog.AutoSize = true;
            this.chkLog.Checked = true;
            this.chkLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLog.Location = new System.Drawing.Point(215, 28);
            this.chkLog.Name = "chkLog";
            this.chkLog.Size = new System.Drawing.Size(79, 17);
            this.chkLog.TabIndex = 11;
            this.chkLog.Text = "Log Details";
            this.chkLog.UseVisualStyleBackColor = true;
            this.chkLog.CheckedChanged += new System.EventHandler(this.chkLog_CheckedChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(727, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.saveInventoryToolStripMenuItem,
            this.loadInventoryToolStripMenuItem,
            this.toggleLoggingToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.AutoToolTip = true;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + 1";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.ToolTipText = "Ctrl + 1";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + 1";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.ToolTipText = "Ctrl + 1";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // saveInventoryToolStripMenuItem
            // 
            this.saveInventoryToolStripMenuItem.Name = "saveInventoryToolStripMenuItem";
            this.saveInventoryToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.saveInventoryToolStripMenuItem.Text = "Save Inventory";
            this.saveInventoryToolStripMenuItem.Click += new System.EventHandler(this.saveInventoryToolStripMenuItem_Click);
            // 
            // loadInventoryToolStripMenuItem
            // 
            this.loadInventoryToolStripMenuItem.Name = "loadInventoryToolStripMenuItem";
            this.loadInventoryToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.loadInventoryToolStripMenuItem.Text = "Load Inventory";
            this.loadInventoryToolStripMenuItem.Click += new System.EventHandler(this.loadInventoryToolStripMenuItem_Click);
            // 
            // toggleLoggingToolStripMenuItem
            // 
            this.toggleLoggingToolStripMenuItem.Name = "toggleLoggingToolStripMenuItem";
            this.toggleLoggingToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + L";
            this.toggleLoggingToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.toggleLoggingToolStripMenuItem.Text = "Toggle Logging";
            this.toggleLoggingToolStripMenuItem.ToolTipText = "Ctrl + L";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(727, 576);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.chkLog);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonRecord);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Auto Clicker";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClickOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderCycles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClicks)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInventoryCount)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
			if (RecordClicks)
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

			RecordClicks = !RecordClicks;
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
				if (RecordClicks)
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

				if (SetupInventory)
				{
					Inventory.Add(Cursor.Position);
				}
			}
		}
		
		public void MyKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.LControlKey)
				Ctrl = false;
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
				Ctrl = true;
			if (e.KeyCode == Keys.D1 && Ctrl)
			{
				RunClicks();
				Ctrl = false;
			}
			if (e.KeyCode == Keys.L && Ctrl)
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
			if (Ctrl && (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey))
			{
				if (!Dropping)
				{
					Dropping = true;
					DropInventory();
				}
			}

		}

		private void ClickTimer_Tick(object sender, EventArgs e)
		{
			LogWrite("RandomTimeoutCount: " + RandomTimeoutCount);
			LogWrite("Current ClickTimerInterval: " + ClickTimer.Interval);
			//set cursor position to memorized location
			Click currentClick = Clicks[ClickCountPos];
			CurrentPoint = currentClick.ClickPoint;
			var mouseButton = currentClick.ClickType;
			if(radioCycles.Checked)
				ClickTimer.Interval = (int)currentClick.DelayAfterClick;
			else if (TimedOut)
			{
				TimedOut = !TimedOut;
				ClickTimer.Interval = GetInterval();

			}

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
					if (UseRandomTimeouts)
					{
						RandomTimeoutCount--;
						if(RandomTimeoutCount <=0)
						{
							RandomTimeoutCount = GetRandomTimeoutCount();
							ClickTimer.Interval = GetRandomTimeout();
							LogWrite("SettingRandomTimeoutCount: " + RandomTimeoutCount);
							LogWrite("Setting ClickTimerInterval Long Delay: " + ClickTimer.Interval);
							TimedOut = true;
						}
					}

					ClickCountPos = 0;
					if (!InfiniteLoop)
					{
						numCount.Value = TotalCount;
						TotalCount--;
					}
						
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

			//LogWrite("Clicked at X:" + point.X + "  Y:" + point.Y);
		}

		public int GetRandomOffset()
		{
			return RandomGenerate.Next(0, ClickOffset * 2) - ClickOffset;
		}

		public void RunClicks()
		{
			TotalCount = (int)numCount.Value;
			if (TotalCount == 0)
				InfiniteLoop = true;
			else
				InfiniteLoop = false;

			if (RecordClicks)
			{
				buttonRecord.Text = "Record Clicks";
				RecordClicks = !RecordClicks;
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
			if (Inventory.Count < 1)
			{
				MessageBox.Show("Need to setup inventory first.");
				return;
			}

		    TotalInventoryClickCount = (int)numInventoryCount.Value;


            if (DropInverse)
				DropClickPos = Inventory.Count - 1;
			else
				DropClickPos = 0;
			DropTimer.Start();
		}

		private void DropTimer_Tick(object sender, EventArgs e)
		{
			if (DropClickPos >= Inventory.Count || DropClickPos < 0 || CurrentInventoryClickCount >= TotalInventoryClickCount)
			{
				DropTimer.Stop();
			    CurrentInventoryClickCount = 0;
                return;
			}

		    CurrentInventoryClickCount++;
            //set cursor position to memorized location
            CurrentPoint = Inventory[DropClickPos];
			if (DropInverse)
				DropClickPos--;
			else
				DropClickPos++;

			DoMouseClick(0, CurrentPoint);
		}

		private void btnSetupInventory_Click(object sender, EventArgs e)
		{
			if (SetupInventory)
			{
				btnSetupInventory.Text = "Setup Inventory";
				Inventory.RemoveAt(Inventory.Count - 1);
			    lblTotalInventory.Text = Inventory.Count.ToString();
			    numInventoryCount.Value = Inventory.Count;
			}
			else
			{
				btnSetupInventory.Text = "Recording Inventory...";
				Inventory.Clear();
			}

			SetupInventory = !SetupInventory;
		}

		private void DisableAll()
		{
			buttonStart.Enabled = false;
			//buttonRecord.Enabled = false;
			chkLog.Enabled = false;
			chkDropInverse.Enabled = false;
			radioClicks.Enabled = false;
			radioCycles.Enabled = false;
			btnSetupInventory.Enabled = false;
			btnSingleClickInv.Enabled = false;
		}

		private void EnableAll()
		{
			buttonStart.Enabled = true;
			//buttonRecord.Enabled = true;
			chkLog.Enabled = true;
			chkDropInverse.Enabled = true;
			radioClicks.Enabled = true;
			radioCycles.Enabled = true;
			btnSetupInventory.Enabled = true;
			btnSingleClickInv.Enabled = true;
		}

		private void btnSingleClickInv_Click(object sender, EventArgs e)
		{
			DropInventory();
		}

		private void sliderClickOffset_Scroll(object sender, EventArgs e)
		{
			TrackBar slider = (TrackBar)sender;
			lblClickOffsetNumber.Text = slider.Value.ToString();
			ClickOffset = slider.Value;
		}

		private void chkTimeOut_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox chkBox = (CheckBox)sender;
			if (chkBox.Checked)
			{
				UseRandomTimeouts = true;
				RandomTimeoutCount = GetRandomTimeoutCount();
			}
				
			else
				UseRandomTimeouts = false;
		}

		private int GetRandomTimeoutCount()
		{
			return RandomGenerate.Next(75, 125);
		}
		private int GetRandomTimeout()
		{
			return RandomGenerate.Next(10000, 20000);
		}

		private void btnHide_Click(object sender, EventArgs e)
		{
			HideButtons = !HideButtons;
			if (HideButtons)
			{
				btnHide.Text = "Hiding...";
				DisableAll();
			}
			else
			{
				btnHide.Text = "Hide Buttons";
				EnableAll();
			}
		}

		private void startToolStripMenuItem_Click(object sender, EventArgs e)
		{
		    startToolStripMenuItem.Enabled = false;
		    stopToolStripMenuItem.Enabled = true;

            RunClicks();
		}

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = true;
            RunClicks();
        }

        private void saveInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(FilePath, false))
            {
                foreach (var point in Inventory)
                {
                    file.WriteLine(String.Format("{0}:{1}", point.X, point.Y));
                }
                
            }
        }

        private void loadInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }

	    private void LoadInventory()
	    {
	        if (File.Exists(FilePath))
	        {
	            Inventory.Clear();

	            string line;
	            List<int> points;

	            using (System.IO.StreamReader file =
	                new System.IO.StreamReader(FilePath, false))
	            {
	                while ((line = file.ReadLine()) != null)
	                {
	                    points = line.Split(':').Select(int.Parse).ToList();
	                    if (points.Count != 2)
	                        continue;
	                    Inventory.Add(new Point(points[0], points[1]));
	                }
	            }

	            lblTotalInventory.Text = Inventory.Count.ToString();
	            numInventoryCount.Value = Inventory.Count;
            }
	        else
	        {
	            (new FileInfo(FilePath)).Directory.Create();

                using (System.IO.StreamWriter file =
	                new System.IO.StreamWriter(FilePath, false))
	            {
	                file.Write("");
	            }
	        }
	    }
        
        private void btnDropInventory_Click(object sender, EventArgs e)
        {
            DropInventory();
        }

        private void numInventoryCount_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown counter = (NumericUpDown) sender;
            if ((int) counter.Value > Inventory.Count)
            {
                counter.Value = Inventory.Count;
                return;
            }
        }
    }			
}

