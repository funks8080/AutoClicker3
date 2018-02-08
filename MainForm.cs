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
using AutoClicker.ImageFinder;
using gma.System.Windows;
using AutoClicker.Mouse;

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
		private int IterationCount;
		private TrackBar ActiveSlider;
		private Stopwatch ClickStopwatch;
		private List<Click> Clicks;
		private List<Point> Inventory;
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


		const int VK_SHIFT = 0x10; //up key
		const int VK_DOWN = 0x28;  //down key
		const int VK_LEFT = 0x25;
		const int VK_RIGHT = 0x27;
		const uint KEYEVENTF_KEYUP = 0x0002;
        private Button button1;
        private TabControl WoodCut;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel1;
        private RadioButton radioOakTree;
        private RadioButton radioRegTree;
        private RadioButton radioWillowTree;
        private RadioButton radioYewTree;
        private RadioButton radioMapleTree;
        private RadioButton radioMagicTree;
        private PictureBox pictureBox8;
        private PictureBox pictureBox9;
        private PictureBox pictureBox10;
        private PictureBox pictureBox11;
        private PictureBox pictureBox12;
        private PictureBox pictureBox13;
        private Button btnWoodcut;
        private Button btnMining;
        private TableLayoutPanel tableLayoutPanel2;
        private RadioButton radioRune;
        private RadioButton radioMithril;
        private RadioButton radioCoal;
        private RadioButton radioSilver;
        private RadioButton radioClay;
        private RadioButton radioCopper;
        private RadioButton radioIron;
        private RadioButton radioTin;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private RadioButton radioGems;
        private RadioButton radioGold;
        private RadioButton radioAdamantite;
        private PictureBox pictureBox7;
        private PictureBox pictureBox14;
        private PictureBox pictureBox15;
        private PictureBox pictureBox16;
        private PictureBox pictureBox17;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;


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
			IterationCount = 0;
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
            this.button1 = new System.Windows.Forms.Button();
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
            this.WoodCut = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnWoodcut = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radioMagicTree = new System.Windows.Forms.RadioButton();
            this.radioYewTree = new System.Windows.Forms.RadioButton();
            this.radioRegTree = new System.Windows.Forms.RadioButton();
            this.radioWillowTree = new System.Windows.Forms.RadioButton();
            this.radioMapleTree = new System.Windows.Forms.RadioButton();
            this.radioOakTree = new System.Windows.Forms.RadioButton();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnMining = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.radioCoal = new System.Windows.Forms.RadioButton();
            this.radioSilver = new System.Windows.Forms.RadioButton();
            this.radioClay = new System.Windows.Forms.RadioButton();
            this.radioCopper = new System.Windows.Forms.RadioButton();
            this.radioIron = new System.Windows.Forms.RadioButton();
            this.radioTin = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.radioGems = new System.Windows.Forms.RadioButton();
            this.radioGold = new System.Windows.Forms.RadioButton();
            this.radioMithril = new System.Windows.Forms.RadioButton();
            this.radioAdamantite = new System.Windows.Forms.RadioButton();
            this.radioRune = new System.Windows.Forms.RadioButton();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClickOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderCycles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClicks)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInventoryCount)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.WoodCut.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
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
            this.groupBox2.Controls.Add(this.button1);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 469);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Find Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.menuStrip1.Size = new System.Drawing.Size(1162, 24);
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
            // WoodCut
            // 
            this.WoodCut.Controls.Add(this.tabPage1);
            this.WoodCut.Controls.Add(this.tabPage2);
            this.WoodCut.Location = new System.Drawing.Point(721, 28);
            this.WoodCut.Name = "WoodCut";
            this.WoodCut.SelectedIndex = 0;
            this.WoodCut.Size = new System.Drawing.Size(413, 507);
            this.WoodCut.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnWoodcut);
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(405, 481);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Wood Cutting";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnWoodcut
            // 
            this.btnWoodcut.Location = new System.Drawing.Point(136, 438);
            this.btnWoodcut.Name = "btnWoodcut";
            this.btnWoodcut.Size = new System.Drawing.Size(114, 23);
            this.btnWoodcut.TabIndex = 1;
            this.btnWoodcut.Text = "Start Woodcutting";
            this.btnWoodcut.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.radioMagicTree, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.radioYewTree, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.radioRegTree, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radioWillowTree, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.radioMapleTree, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.radioOakTree, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox8, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox9, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox10, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox11, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox12, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox13, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel1.MaximumSize = new System.Drawing.Size(405, 469);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(393, 418);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // radioMagicTree
            // 
            this.radioMagicTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioMagicTree.AutoSize = true;
            this.radioMagicTree.Location = new System.Drawing.Point(5, 765);
            this.radioMagicTree.Name = "radioMagicTree";
            this.radioMagicTree.Size = new System.Drawing.Size(79, 144);
            this.radioMagicTree.TabIndex = 10;
            this.radioMagicTree.TabStop = true;
            this.radioMagicTree.Text = "Magic Tree";
            this.radioMagicTree.UseVisualStyleBackColor = true;
            // 
            // radioYewTree
            // 
            this.radioYewTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioYewTree.AutoSize = true;
            this.radioYewTree.Location = new System.Drawing.Point(5, 613);
            this.radioYewTree.Name = "radioYewTree";
            this.radioYewTree.Size = new System.Drawing.Size(71, 144);
            this.radioYewTree.TabIndex = 5;
            this.radioYewTree.TabStop = true;
            this.radioYewTree.Text = "Yew Tree";
            this.radioYewTree.UseVisualStyleBackColor = true;
            // 
            // radioRegTree
            // 
            this.radioRegTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioRegTree.AutoSize = true;
            this.radioRegTree.Location = new System.Drawing.Point(5, 5);
            this.radioRegTree.Name = "radioRegTree";
            this.radioRegTree.Size = new System.Drawing.Size(175, 144);
            this.radioRegTree.TabIndex = 0;
            this.radioRegTree.TabStop = true;
            this.radioRegTree.Text = "Regular Tree";
            this.radioRegTree.UseVisualStyleBackColor = true;
            // 
            // radioWillowTree
            // 
            this.radioWillowTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioWillowTree.AutoSize = true;
            this.radioWillowTree.Location = new System.Drawing.Point(5, 309);
            this.radioWillowTree.Name = "radioWillowTree";
            this.radioWillowTree.Size = new System.Drawing.Size(81, 144);
            this.radioWillowTree.TabIndex = 3;
            this.radioWillowTree.TabStop = true;
            this.radioWillowTree.Text = "Willow Tree";
            this.radioWillowTree.UseVisualStyleBackColor = true;
            // 
            // radioMapleTree
            // 
            this.radioMapleTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioMapleTree.AutoSize = true;
            this.radioMapleTree.Location = new System.Drawing.Point(5, 461);
            this.radioMapleTree.Name = "radioMapleTree";
            this.radioMapleTree.Size = new System.Drawing.Size(79, 144);
            this.radioMapleTree.TabIndex = 4;
            this.radioMapleTree.TabStop = true;
            this.radioMapleTree.Text = "Maple Tree";
            this.radioMapleTree.UseVisualStyleBackColor = true;
            // 
            // radioOakTree
            // 
            this.radioOakTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioOakTree.AutoSize = true;
            this.radioOakTree.Location = new System.Drawing.Point(5, 157);
            this.radioOakTree.Name = "radioOakTree";
            this.radioOakTree.Size = new System.Drawing.Size(70, 144);
            this.radioOakTree.TabIndex = 2;
            this.radioOakTree.TabStop = true;
            this.radioOakTree.Text = "Oak Tree";
            this.radioOakTree.UseVisualStyleBackColor = true;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(188, 5);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(176, 144);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 12;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(188, 157);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(176, 144);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 13;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.Location = new System.Drawing.Point(188, 309);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(176, 144);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabIndex = 14;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(188, 461);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(176, 144);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 15;
            this.pictureBox11.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(188, 613);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(176, 144);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 16;
            this.pictureBox12.TabStop = false;
            // 
            // pictureBox13
            // 
            this.pictureBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox13.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox13.Image")));
            this.pictureBox13.Location = new System.Drawing.Point(188, 765);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(176, 144);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox13.TabIndex = 17;
            this.pictureBox13.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnMining);
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(405, 481);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mining";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnMining
            // 
            this.btnMining.Location = new System.Drawing.Point(136, 438);
            this.btnMining.Name = "btnMining";
            this.btnMining.Size = new System.Drawing.Size(114, 23);
            this.btnMining.TabIndex = 3;
            this.btnMining.Text = "Start Mining";
            this.btnMining.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoScroll = true;
            this.tableLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.radioRune, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.radioMithril, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.radioCoal, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.radioSilver, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.radioClay, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.radioCopper, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.radioIron, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.radioTin, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox4, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox5, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox6, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.radioGems, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.radioGold, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.radioAdamantite, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox7, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox14, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox15, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox16, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox17, 1, 6);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 6);
            this.tableLayoutPanel2.MaximumSize = new System.Drawing.Size(405, 469);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(393, 418);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // radioCoal
            // 
            this.radioCoal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioCoal.AutoSize = true;
            this.radioCoal.Location = new System.Drawing.Point(5, 765);
            this.radioCoal.Name = "radioCoal";
            this.radioCoal.Size = new System.Drawing.Size(46, 144);
            this.radioCoal.TabIndex = 10;
            this.radioCoal.TabStop = true;
            this.radioCoal.Text = "Coal";
            this.radioCoal.UseVisualStyleBackColor = true;
            // 
            // radioSilver
            // 
            this.radioSilver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioSilver.AutoSize = true;
            this.radioSilver.Location = new System.Drawing.Point(5, 613);
            this.radioSilver.Name = "radioSilver";
            this.radioSilver.Size = new System.Drawing.Size(51, 144);
            this.radioSilver.TabIndex = 5;
            this.radioSilver.TabStop = true;
            this.radioSilver.Text = "Silver";
            this.radioSilver.UseVisualStyleBackColor = true;
            // 
            // radioClay
            // 
            this.radioClay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioClay.AutoSize = true;
            this.radioClay.Location = new System.Drawing.Point(5, 5);
            this.radioClay.Name = "radioClay";
            this.radioClay.Size = new System.Drawing.Size(175, 144);
            this.radioClay.TabIndex = 0;
            this.radioClay.TabStop = true;
            this.radioClay.Text = "Clay";
            this.radioClay.UseVisualStyleBackColor = true;
            // 
            // radioCopper
            // 
            this.radioCopper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioCopper.AutoSize = true;
            this.radioCopper.Location = new System.Drawing.Point(5, 309);
            this.radioCopper.Name = "radioCopper";
            this.radioCopper.Size = new System.Drawing.Size(59, 144);
            this.radioCopper.TabIndex = 3;
            this.radioCopper.TabStop = true;
            this.radioCopper.Text = "Copper";
            this.radioCopper.UseVisualStyleBackColor = true;
            // 
            // radioIron
            // 
            this.radioIron.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioIron.AutoSize = true;
            this.radioIron.Location = new System.Drawing.Point(5, 461);
            this.radioIron.Name = "radioIron";
            this.radioIron.Size = new System.Drawing.Size(43, 144);
            this.radioIron.TabIndex = 4;
            this.radioIron.TabStop = true;
            this.radioIron.Text = "Iron";
            this.radioIron.UseVisualStyleBackColor = true;
            // 
            // radioTin
            // 
            this.radioTin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioTin.AutoSize = true;
            this.radioTin.Location = new System.Drawing.Point(5, 157);
            this.radioTin.Name = "radioTin";
            this.radioTin.Size = new System.Drawing.Size(40, 144);
            this.radioTin.TabIndex = 2;
            this.radioTin.TabStop = true;
            this.radioTin.Text = "Tin";
            this.radioTin.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::AutoClicker.Properties.Resources.Clay_rock;
            this.pictureBox1.Location = new System.Drawing.Point(188, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(188, 157);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(176, 144);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(188, 309);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(176, 144);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(188, 461);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(176, 144);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 15;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(188, 613);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(176, 144);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 16;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(188, 765);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(176, 144);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 17;
            this.pictureBox6.TabStop = false;
            // 
            // radioGems
            // 
            this.radioGems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioGems.AutoSize = true;
            this.radioGems.Location = new System.Drawing.Point(5, 917);
            this.radioGems.Name = "radioGems";
            this.radioGems.Size = new System.Drawing.Size(52, 144);
            this.radioGems.TabIndex = 18;
            this.radioGems.TabStop = true;
            this.radioGems.Text = "Gems";
            this.radioGems.UseVisualStyleBackColor = true;
            // 
            // radioGold
            // 
            this.radioGold.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioGold.AutoSize = true;
            this.radioGold.Location = new System.Drawing.Point(5, 1069);
            this.radioGold.Name = "radioGold";
            this.radioGold.Size = new System.Drawing.Size(47, 144);
            this.radioGold.TabIndex = 19;
            this.radioGold.TabStop = true;
            this.radioGold.Text = "Gold";
            this.radioGold.UseVisualStyleBackColor = true;
            // 
            // radioMithril
            // 
            this.radioMithril.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioMithril.AutoSize = true;
            this.radioMithril.Location = new System.Drawing.Point(5, 1221);
            this.radioMithril.Name = "radioMithril";
            this.radioMithril.Size = new System.Drawing.Size(52, 144);
            this.radioMithril.TabIndex = 20;
            this.radioMithril.TabStop = true;
            this.radioMithril.Text = "Mithril";
            this.radioMithril.UseVisualStyleBackColor = true;
            // 
            // radioAdamantite
            // 
            this.radioAdamantite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioAdamantite.AutoSize = true;
            this.radioAdamantite.Location = new System.Drawing.Point(5, 1373);
            this.radioAdamantite.Name = "radioAdamantite";
            this.radioAdamantite.Size = new System.Drawing.Size(78, 144);
            this.radioAdamantite.TabIndex = 21;
            this.radioAdamantite.TabStop = true;
            this.radioAdamantite.Text = "Adamantite";
            this.radioAdamantite.UseVisualStyleBackColor = true;
            // 
            // radioRune
            // 
            this.radioRune.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioRune.AutoSize = true;
            this.radioRune.Location = new System.Drawing.Point(5, 1525);
            this.radioRune.Name = "radioRune";
            this.radioRune.Size = new System.Drawing.Size(51, 144);
            this.radioRune.TabIndex = 22;
            this.radioRune.TabStop = true;
            this.radioRune.Text = "Rune";
            this.radioRune.UseVisualStyleBackColor = true;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(188, 1373);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(176, 144);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 23;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox14
            // 
            this.pictureBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox14.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox14.Image")));
            this.pictureBox14.Location = new System.Drawing.Point(188, 1221);
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.Size = new System.Drawing.Size(176, 144);
            this.pictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox14.TabIndex = 24;
            this.pictureBox14.TabStop = false;
            // 
            // pictureBox15
            // 
            this.pictureBox15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(188, 1069);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(176, 144);
            this.pictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox15.TabIndex = 25;
            this.pictureBox15.TabStop = false;
            // 
            // pictureBox16
            // 
            this.pictureBox16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox16.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox16.Image")));
            this.pictureBox16.Location = new System.Drawing.Point(188, 1525);
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.Size = new System.Drawing.Size(176, 144);
            this.pictureBox16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox16.TabIndex = 26;
            this.pictureBox16.TabStop = false;
            // 
            // pictureBox17
            // 
            this.pictureBox17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox17.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox17.Image")));
            this.pictureBox17.Location = new System.Drawing.Point(188, 917);
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.Size = new System.Drawing.Size(176, 144);
            this.pictureBox17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox17.TabIndex = 27;
            this.pictureBox17.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1162, 576);
            this.Controls.Add(this.WoodCut);
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
            this.WoodCut.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
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
				Clicks.RemoveAt(Clicks.Count - 1);
			}
			else
			{
				buttonRecord.Text = "Recording...";
				ClickCountPos = 0;
				Clicks.Clear();
				ClickStopwatch.Reset();
			}

			RecordClicks = !RecordClicks;
		}
		
		void ButtonStart(object sender, System.EventArgs e)
		{
			StartAutoClicker();
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
			if (Ctrl)
			{
				switch (e.KeyCode)
				{
					case Keys.D1:
						StartAutoClicker();
						Ctrl = false;
						break;
					case Keys.L:
						chkLog.Checked = !chkLog.Checked;
						break;
					case Keys.LShiftKey:
					case Keys.RShiftKey:
						Dropping = true;
						DropInventory();
						break;
				}
			}
			else
			{
				switch (e.KeyCode)
				{
					case Keys.LControlKey:
					case Keys.RControlKey:
						Ctrl = true;
						break;
					case Keys.Add:
						ActiveSlider.Value++;
						UpdateTrack();
						break;
					case Keys.Subtract:
						ActiveSlider.Value--;
						UpdateTrack();
						break;
				}
			}

		}

		private void ClickTimer_Tick(object sender, EventArgs e)
		{
			if (IterationCount <= 0 && !InfiniteLoop)
			{
				StopAutoClicker();
				return;
			}

			//set cursor position to memorized location
			Click currentClick = Clicks[ClickCountPos];
			var clickPoint = currentClick.ClickPoint;
			var mouseButton = currentClick.ClickType;

			if(radioCycles.Checked)
				ClickTimer.Interval = (int)currentClick.DelayAfterClick;
			else if (TimedOut)
			{
				TimedOut = !TimedOut;
				ClickTimer.Interval = GetInterval();
			}
			
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
						TimedOut = true;
					}
				}

				ClickCountPos = 0;
				if (!InfiniteLoop)
				{
					numCount.Value = IterationCount;
					IterationCount--;
				}
						
			}

			clickPoint.X += GetRandomOffset();
			clickPoint.Y += GetRandomOffset();

			DoMouseClick(mouseButton, clickPoint);

		}

		public void DoMouseClick(int mouseButton, Point point)
		{
			LogWrite("Clicked at X:" + point.X + "  Y:" + point.Y);
			Mouse.Mouse.MoveTo(point.X, point.Y);
			switch (mouseButton)
			{
				case 0:
					Mouse.Mouse.LeftClick();
					break;
				case 1:
					Mouse.Mouse.RightClick();
					break;
			}
		}
		
		public int GetRandomOffset()
		{
			return RandomGenerate.Next(0, ClickOffset * 2) - ClickOffset;
		}

		public void StartAutoClicker()
		{
			if (RunProgram)
			{
				StopAutoClicker();
				return;
			}

			IterationCount = (int)numCount.Value;
			if (IterationCount == 0)
				InfiniteLoop = true;
			else
				InfiniteLoop = false;

			StopRecordingClicks();

			if (Clicks.Count < 1)
			{
				MessageBox.Show("Need to have at least one click recored");
				return;
			}

			sliderClicks.Enabled = false;
			numCount.Enabled = false;
			buttonRecord.Enabled = false;
			ClickCountPos = 0;
			buttonStart.Text = "Stop";
			RunProgram = true;

			ClickTimer.Start();
			LogWrite("Starting Auto Clicker");
			Thread.Sleep(3000);
		}

		public void StopAutoClicker()
		{
			ClickTimer.Stop();
			sliderClicks.Enabled = true;
			numCount.Enabled = true;
			buttonRecord.Enabled = true;
			RunProgram = false;

			buttonStart.Text = "Start";
			LogWrite("Stopping Auto Clicker");
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
			var currentPoint = Inventory[DropClickPos];
			if (DropInverse)
				DropClickPos--;
			else
				DropClickPos++;

			DoMouseClick(0, currentPoint);
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
			return RandomGenerate.Next(2, 3);
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

			StartAutoClicker();
		}

		private void stopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			stopToolStripMenuItem.Enabled = false;
			startToolStripMenuItem.Enabled = true;
			StartAutoClicker();
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

		private void StopRecordingClicks()
		{
			if (RecordClicks)
			{
				buttonRecord.Text = "Record Clicks";
				RecordClicks = !RecordClicks;
				ClickStopwatch.Stop();
				Clicks.RemoveAt(Clicks.Count - 1);
			}
		}

        private void button1_Click(object sender, EventArgs e)
        {
            var image = MainScreen.LoadImageFile("c:\\AppData\\AutoClicker\\Images\\Capture2.png");
            var offsetX = image.Width / 2;
            var offsetY = image.Height / 2;
            var point = MainScreen.FindImage(image);

            LogWrite(String.Format("X:{0} Y:{1}", point.X, point.Y));

            Mouse.Mouse.MoveTo(point.X + offsetX,  point.Y + offsetY);
        }
    }			
}

