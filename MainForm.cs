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
using tessnet2;
using System.ComponentModel;

namespace AutoClicker 
{
    class MainForm : System.Windows.Forms.Form, IProgress<string>
    {
        private int delayTime = 1000;
        //private Tesseract ocr = new Tesseract();
        private System.ComponentModel.IContainer components = null;
        private Button buttonRecord;
        private Button buttonStart;
        private Label labelMousePosition;
        private TextBox textBox;
        private bool RunProgram;
        private int EatFailCount;
        private int EatFailCountMax = 10;
        private bool logClicks = true;

        private int TotalInventoryClickCount;
        private int CurrentInventoryClickCount;
        private static string FilePath = "c:\\AppData\\AutoClicker\\Inventory.txt";
        private bool agilOn = false;
        private bool FishOn = false;
        private bool AutoShootOn = false;
        private bool WoodCutOn = false;
        private bool RuneCraft = false;
        private bool Dropping;
        private bool InfiniteLoop;
        private bool TimedOut;
        private bool HideButtons;
        private bool Ctrl;
        private bool SetupInventory;
        private bool UseRandomTimeouts;
        private bool DropInverse;
        private bool LogInfo;
        private bool RecordClicks;
        private bool NMZEnabled;
        private bool PickPocket;
        private bool Woodcutting;
        private bool SettingAlchPoint;
        private int RandomTimeoutCount;
        private int ClickOffset;
        private int DropClickPos;
        private int ClickCountPos;
        private int IterationCount;
        private TrackBar ActiveSlider;
        private Stopwatch ClickStopwatch;
        private List<Click> Clicks;
        private List<Point> Inventory;
        private System.Windows.Forms.Timer DrinkTimer;
        private System.Windows.Forms.Timer PrayerTimer;
        private System.Windows.Forms.Timer OverloadTimer;
        private System.Windows.Forms.Timer LogoutTimer;
        private System.Windows.Forms.Timer ClickTimer;
        private System.Windows.Forms.Timer DropTimer;
        private System.Windows.Forms.Timer EatTimer;
        private System.Windows.Forms.Timer PickPocketTimer;
        private System.Windows.Forms.Timer WoodcutTimer;
        private System.Windows.Forms.Timer InvFullTimer;
        private System.Windows.Forms.Timer AgilityTimer;
        private System.Windows.Forms.Timer BarbFishTimer;
        private System.Windows.Forms.Timer AutoShootTimer;
        private System.Windows.Forms.Timer WoodCutTimer2;
        private System.Windows.Forms.Timer RuneCraftTimer;
        private System.Windows.Forms.Timer RapidClickTimer;
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
        private Button button1;
        private TabControl TabController;
        private TabPage WoodCut;
        private TabPage Mining;
        private TableLayoutPanel tableWoodCut;
        private RadioButton radioOakTree;
        private RadioButton radioRegularTree;
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
        private Label lblColorRange;
        private Button btnNightmare;
        private Button btnTan;
        private TabPage Buttons;
        private Button btnPickPocket;
        private Button btnWoodcut2;
        private Button btnSeersAgil;
        private System.ComponentModel.BackgroundWorker workerSeersAgility;
        private System.ComponentModel.BackgroundWorker workerCanifisAgility;
        private Button btnBarbFish;
        private BackgroundWorker workerBarbFish;
        private Button btnRC;
        private Button btnWC2;
        private Button btnCanAgi;
        private Label label9;
        private NumericUpDown maxRandom;
        private NumericUpDown minRandom;
        private CheckBox chkClicks;
        private Button btnAutoShoot;
        private BackgroundWorker workerRC;
        private Button btnMine;
        private BackgroundWorker workerMining;
        private Button button2;
        private Label label10;
        private Button btnStartAlch;
        private Button btnSetAlch;
        private TrackBar sliderColorRange;
        private BackgroundWorker workerAlch;
        private Point AlchPoint;


        public MainForm()
        {
            try
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
                InitializeComponent();
                sliderCycles.Enabled = false;
                ActiveLabel = lblClickSeconds;
                ActiveSlider = sliderClicks;
                stopToolStripMenuItem.Enabled = false;
                Dropping = false;
                NMZEnabled = false;
                PickPocket = false;
                Woodcutting = false;
                AlchPoint = new Point();
                SettingAlchPoint = false;
                //ocr.Init(@"E:\AutoClicker\AutoClicker3\tessdata", "eng", false);
                LoadInventory();
            }
            catch
            {

            }
            

        }

        // THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
        // DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBox = new System.Windows.Forms.TextBox();
            this.labelMousePosition = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonRecord = new System.Windows.Forms.Button();
            this.LogoutTimer = new System.Windows.Forms.Timer(this.components);
            this.ClickTimer = new System.Windows.Forms.Timer(this.components);
            this.RapidClickTimer = new System.Windows.Forms.Timer(this.components);
            this.DropTimer = new System.Windows.Forms.Timer(this.components);
            this.DrinkTimer = new System.Windows.Forms.Timer(this.components);
            this.PrayerTimer = new System.Windows.Forms.Timer(this.components);
            this.OverloadTimer = new System.Windows.Forms.Timer(this.components);
            this.EatTimer = new System.Windows.Forms.Timer(this.components);
            this.PickPocketTimer = new System.Windows.Forms.Timer(this.components);
            this.WoodcutTimer = new System.Windows.Forms.Timer(this.components);
            this.InvFullTimer = new System.Windows.Forms.Timer(this.components);
            this.AgilityTimer = new System.Windows.Forms.Timer(this.components);
            this.BarbFishTimer = new System.Windows.Forms.Timer(this.components);
            this.AutoShootTimer = new System.Windows.Forms.Timer(this.components);
            this.WoodCutTimer2 = new System.Windows.Forms.Timer(this.components);
            this.RuneCraftTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackLabel1 = new System.Windows.Forms.Label();
            this.lblClickSeconds = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblColorRange = new System.Windows.Forms.Label();
            this.sliderColorRange = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.maxRandom = new System.Windows.Forms.NumericUpDown();
            this.minRandom = new System.Windows.Forms.NumericUpDown();
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
            this.btnHide = new System.Windows.Forms.Button();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabController = new System.Windows.Forms.TabControl();
            this.Buttons = new System.Windows.Forms.TabPage();
            this.btnStartAlch = new System.Windows.Forms.Button();
            this.btnSetAlch = new System.Windows.Forms.Button();
            this.btnMine = new System.Windows.Forms.Button();
            this.btnAutoShoot = new System.Windows.Forms.Button();
            this.btnCanAgi = new System.Windows.Forms.Button();
            this.btnWC2 = new System.Windows.Forms.Button();
            this.btnRC = new System.Windows.Forms.Button();
            this.btnBarbFish = new System.Windows.Forms.Button();
            this.btnSeersAgil = new System.Windows.Forms.Button();
            this.btnWoodcut2 = new System.Windows.Forms.Button();
            this.btnPickPocket = new System.Windows.Forms.Button();
            this.btnNightmare = new System.Windows.Forms.Button();
            this.btnTan = new System.Windows.Forms.Button();
            this.WoodCut = new System.Windows.Forms.TabPage();
            this.btnWoodcut = new System.Windows.Forms.Button();
            this.tableWoodCut = new System.Windows.Forms.TableLayoutPanel();
            this.radioMagicTree = new System.Windows.Forms.RadioButton();
            this.radioYewTree = new System.Windows.Forms.RadioButton();
            this.radioRegularTree = new System.Windows.Forms.RadioButton();
            this.radioWillowTree = new System.Windows.Forms.RadioButton();
            this.radioMapleTree = new System.Windows.Forms.RadioButton();
            this.radioOakTree = new System.Windows.Forms.RadioButton();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.Mining = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnMining = new System.Windows.Forms.Button();
            this.workerSeersAgility = new System.ComponentModel.BackgroundWorker();
            this.workerCanifisAgility = new System.ComponentModel.BackgroundWorker();
            this.workerBarbFish = new System.ComponentModel.BackgroundWorker();
            this.chkClicks = new System.Windows.Forms.CheckBox();
            this.workerRC = new System.ComponentModel.BackgroundWorker();
            this.workerMining = new System.ComponentModel.BackgroundWorker();
            this.workerAlch = new System.ComponentModel.BackgroundWorker();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderColorRange)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxRandom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minRandom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClickOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderCycles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClicks)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInventoryCount)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.TabController.SuspendLayout();
            this.Buttons.SuspendLayout();
            this.WoodCut.SuspendLayout();
            this.tableWoodCut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.Mining.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.buttonStart.Location = new System.Drawing.Point(446, 541);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.Click += new System.EventHandler(this.ButtonStart);
            // 
            // buttonRecord
            // 
            this.buttonRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecord.Location = new System.Drawing.Point(320, 541);
            this.buttonRecord.Name = "buttonRecord";
            this.buttonRecord.Size = new System.Drawing.Size(107, 23);
            this.buttonRecord.TabIndex = 0;
            this.buttonRecord.Text = "Record Clicks";
            this.buttonRecord.Click += new System.EventHandler(this.ButtonRecord);
            // 
            // LogoutTimer
            // 
            this.LogoutTimer.Interval = 19800000;
            this.LogoutTimer.Tick += new System.EventHandler(this.LogoutTimer_Tick);
            // 
            // ClickTimer
            // 
            this.ClickTimer.Tick += new System.EventHandler(this.ClickTimer_Tick);
            // 
            // RapidClickTimer
            // 
            this.RapidClickTimer.Interval = 1000;
            this.RapidClickTimer.Tick += new System.EventHandler(this.RapidClickTimer_Tick);
            // 
            // DropTimer
            // 
            this.DropTimer.Tick += new System.EventHandler(this.DropTimer_Tick);
            // 
            // DrinkTimer
            // 
            this.DrinkTimer.Interval = 43000;
            this.DrinkTimer.Tick += new System.EventHandler(this.NMZDrinkTimer_Tick);
            // 
            // PrayerTimer
            // 
            this.PrayerTimer.Interval = 43000;
            this.PrayerTimer.Tick += new System.EventHandler(this.NMZPrayerTimer_Tick);
            // 
            // OverloadTimer
            // 
            this.OverloadTimer.Interval = 302000;
            this.OverloadTimer.Tick += new System.EventHandler(this.NMZOverloadTimer_Tick);
            // 
            // EatTimer
            // 
            this.EatTimer.Interval = 30000;
            this.EatTimer.Tick += new System.EventHandler(this.EatTimer_Tick);
            // 
            // PickPocketTimer
            // 
            this.PickPocketTimer.Interval = 15000;
            this.PickPocketTimer.Tick += new System.EventHandler(this.PickPocketTimer_Tick);
            // 
            // WoodcutTimer
            // 
            this.WoodcutTimer.Interval = 15000;
            this.WoodcutTimer.Tick += new System.EventHandler(this.WoodcutTimer_Tick);
            // 
            // InvFullTimer
            // 
            this.InvFullTimer.Interval = 30000;
            this.InvFullTimer.Tick += new System.EventHandler(this.InvFullTimer_Tick);
            // 
            // AgilityTimer
            // 
            this.AgilityTimer.Interval = 46000;
            this.AgilityTimer.Tick += new System.EventHandler(this.AgilityTimer_Tick);
            // 
            // BarbFishTimer
            // 
            this.BarbFishTimer.Interval = 15000;
            this.BarbFishTimer.Tick += new System.EventHandler(this.BarbFishTimer_Tick);
            // 
            // AutoShootTimer
            // 
            this.AutoShootTimer.Interval = 4000;
            this.AutoShootTimer.Tick += new System.EventHandler(this.AutoShootTimer_Tick);
            // 
            // WoodCutTimer2
            // 
            this.WoodCutTimer2.Interval = 15000;
            this.WoodCutTimer2.Tick += new System.EventHandler(this.WoodCutTimer2_Tick);
            // 
            // RuneCraftTimer
            // 
            this.RuneCraftTimer.Interval = 79000;
            this.RuneCraftTimer.Tick += new System.EventHandler(this.RuneCraftTimer_Tick);
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
            this.groupBox2.Controls.Add(this.lblColorRange);
            this.groupBox2.Controls.Add(this.sliderColorRange);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(320, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 511);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // lblColorRange
            // 
            this.lblColorRange.AutoSize = true;
            this.lblColorRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorRange.Location = new System.Drawing.Point(324, 469);
            this.lblColorRange.Name = "lblColorRange";
            this.lblColorRange.Size = new System.Drawing.Size(54, 20);
            this.lblColorRange.TabIndex = 19;
            this.lblColorRange.Text = "100%";
            // 
            // sliderColorRange
            // 
            this.sliderColorRange.Location = new System.Drawing.Point(88, 460);
            this.sliderColorRange.Maximum = 50;
            this.sliderColorRange.Name = "sliderColorRange";
            this.sliderColorRange.Size = new System.Drawing.Size(237, 45);
            this.sliderColorRange.SmallChange = 5;
            this.sliderColorRange.TabIndex = 18;
            this.sliderColorRange.TickFrequency = 5;
            this.sliderColorRange.Scroll += new System.EventHandler(this.sliderColorRange_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 469);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Find Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.maxRandom);
            this.groupBox3.Controls.Add(this.minRandom);
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(226, 301);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "TO";
            // 
            // maxRandom
            // 
            this.maxRandom.Location = new System.Drawing.Point(267, 299);
            this.maxRandom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxRandom.Name = "maxRandom";
            this.maxRandom.Size = new System.Drawing.Size(63, 20);
            this.maxRandom.TabIndex = 25;
            // 
            // minRandom
            // 
            this.minRandom.Location = new System.Drawing.Point(143, 299);
            this.minRandom.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.minRandom.Name = "minRandom";
            this.minRandom.Size = new System.Drawing.Size(69, 20);
            this.minRandom.TabIndex = 24;
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
            this.sliderCycles.Maximum = 200;
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
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(543, 541);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(136, 23);
            this.btnHide.TabIndex = 15;
            this.btnHide.Text = "Hide Buttons";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
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
            this.startToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.ToolTipText = "Ctrl + 1";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + 1";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.ToolTipText = "Ctrl + 1";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // saveInventoryToolStripMenuItem
            // 
            this.saveInventoryToolStripMenuItem.Name = "saveInventoryToolStripMenuItem";
            this.saveInventoryToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveInventoryToolStripMenuItem.Text = "Save Inventory";
            this.saveInventoryToolStripMenuItem.Click += new System.EventHandler(this.saveInventoryToolStripMenuItem_Click);
            // 
            // loadInventoryToolStripMenuItem
            // 
            this.loadInventoryToolStripMenuItem.Name = "loadInventoryToolStripMenuItem";
            this.loadInventoryToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.loadInventoryToolStripMenuItem.Text = "Load Inventory";
            this.loadInventoryToolStripMenuItem.Click += new System.EventHandler(this.loadInventoryToolStripMenuItem_Click);
            // 
            // toggleLoggingToolStripMenuItem
            // 
            this.toggleLoggingToolStripMenuItem.Name = "toggleLoggingToolStripMenuItem";
            this.toggleLoggingToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + L";
            this.toggleLoggingToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.toggleLoggingToolStripMenuItem.Text = "Toggle Logging";
            this.toggleLoggingToolStripMenuItem.ToolTipText = "Ctrl + L";
            // 
            // TabController
            // 
            this.TabController.Controls.Add(this.Buttons);
            this.TabController.Controls.Add(this.WoodCut);
            this.TabController.Controls.Add(this.Mining);
            this.TabController.Location = new System.Drawing.Point(721, 28);
            this.TabController.Name = "TabController";
            this.TabController.SelectedIndex = 0;
            this.TabController.Size = new System.Drawing.Size(413, 507);
            this.TabController.TabIndex = 16;
            // 
            // Buttons
            // 
            this.Buttons.Controls.Add(this.btnStartAlch);
            this.Buttons.Controls.Add(this.btnSetAlch);
            this.Buttons.Controls.Add(this.btnMine);
            this.Buttons.Controls.Add(this.btnAutoShoot);
            this.Buttons.Controls.Add(this.btnCanAgi);
            this.Buttons.Controls.Add(this.btnWC2);
            this.Buttons.Controls.Add(this.btnRC);
            this.Buttons.Controls.Add(this.btnBarbFish);
            this.Buttons.Controls.Add(this.btnSeersAgil);
            this.Buttons.Controls.Add(this.btnWoodcut2);
            this.Buttons.Controls.Add(this.btnPickPocket);
            this.Buttons.Controls.Add(this.btnNightmare);
            this.Buttons.Controls.Add(this.btnTan);
            this.Buttons.Location = new System.Drawing.Point(4, 22);
            this.Buttons.Name = "Buttons";
            this.Buttons.Size = new System.Drawing.Size(405, 481);
            this.Buttons.TabIndex = 2;
            this.Buttons.Text = "Buttons";
            this.Buttons.UseVisualStyleBackColor = true;
            // 
            // btnStartAlch
            // 
            this.btnStartAlch.Location = new System.Drawing.Point(16, 349);
            this.btnStartAlch.Name = "btnStartAlch";
            this.btnStartAlch.Size = new System.Drawing.Size(102, 23);
            this.btnStartAlch.TabIndex = 29;
            this.btnStartAlch.Text = "Start Alching";
            this.btnStartAlch.UseVisualStyleBackColor = true;
            this.btnStartAlch.Click += new System.EventHandler(this.btnStartAlch_Click);
            // 
            // btnSetAlch
            // 
            this.btnSetAlch.Location = new System.Drawing.Point(16, 320);
            this.btnSetAlch.Name = "btnSetAlch";
            this.btnSetAlch.Size = new System.Drawing.Size(102, 23);
            this.btnSetAlch.TabIndex = 28;
            this.btnSetAlch.Text = "Set Alch Point";
            this.btnSetAlch.UseVisualStyleBackColor = true;
            this.btnSetAlch.Click += new System.EventHandler(this.btnSetAlch_Click);
            // 
            // btnMine
            // 
            this.btnMine.Location = new System.Drawing.Point(16, 238);
            this.btnMine.Name = "btnMine";
            this.btnMine.Size = new System.Drawing.Size(75, 23);
            this.btnMine.TabIndex = 27;
            this.btnMine.Text = "Mining";
            this.btnMine.UseVisualStyleBackColor = true;
            this.btnMine.Click += new System.EventHandler(this.btnMine_Click);
            // 
            // btnAutoShoot
            // 
            this.btnAutoShoot.Location = new System.Drawing.Point(134, 89);
            this.btnAutoShoot.Name = "btnAutoShoot";
            this.btnAutoShoot.Size = new System.Drawing.Size(75, 23);
            this.btnAutoShoot.TabIndex = 26;
            this.btnAutoShoot.Text = "AutoShoot";
            this.btnAutoShoot.UseVisualStyleBackColor = true;
            this.btnAutoShoot.Click += new System.EventHandler(this.btnAutoShoot_Click);
            // 
            // btnCanAgi
            // 
            this.btnCanAgi.Location = new System.Drawing.Point(134, 178);
            this.btnCanAgi.Name = "btnCanAgi";
            this.btnCanAgi.Size = new System.Drawing.Size(75, 23);
            this.btnCanAgi.TabIndex = 25;
            this.btnCanAgi.Text = "Canifis Agi";
            this.btnCanAgi.UseVisualStyleBackColor = true;
            this.btnCanAgi.Click += new System.EventHandler(this.btnCanAgi_Click);
            // 
            // btnWC2
            // 
            this.btnWC2.Location = new System.Drawing.Point(134, 51);
            this.btnWC2.Name = "btnWC2";
            this.btnWC2.Size = new System.Drawing.Size(75, 23);
            this.btnWC2.TabIndex = 24;
            this.btnWC2.Text = "Woodcut";
            this.btnWC2.UseVisualStyleBackColor = true;
            this.btnWC2.Click += new System.EventHandler(this.btnWC2_Click);
            // 
            // btnRC
            // 
            this.btnRC.Location = new System.Drawing.Point(16, 208);
            this.btnRC.Name = "btnRC";
            this.btnRC.Size = new System.Drawing.Size(75, 23);
            this.btnRC.TabIndex = 23;
            this.btnRC.Text = "RC";
            this.btnRC.UseVisualStyleBackColor = true;
            this.btnRC.Click += new System.EventHandler(this.btnRC_Click);
            // 
            // btnBarbFish
            // 
            this.btnBarbFish.Location = new System.Drawing.Point(134, 15);
            this.btnBarbFish.Name = "btnBarbFish";
            this.btnBarbFish.Size = new System.Drawing.Size(75, 23);
            this.btnBarbFish.TabIndex = 22;
            this.btnBarbFish.Text = "Barb Fish";
            this.btnBarbFish.UseVisualStyleBackColor = true;
            this.btnBarbFish.Click += new System.EventHandler(this.btnBarbFish_Click);
            // 
            // btnSeersAgil
            // 
            this.btnSeersAgil.Location = new System.Drawing.Point(16, 178);
            this.btnSeersAgil.Name = "btnSeersAgil";
            this.btnSeersAgil.Size = new System.Drawing.Size(75, 23);
            this.btnSeersAgil.TabIndex = 21;
            this.btnSeersAgil.Text = "Seers Agility";
            this.btnSeersAgil.UseVisualStyleBackColor = true;
            this.btnSeersAgil.Click += new System.EventHandler(this.btnSeersAgil_Click);
            // 
            // btnWoodcut2
            // 
            this.btnWoodcut2.Location = new System.Drawing.Point(16, 129);
            this.btnWoodcut2.Name = "btnWoodcut2";
            this.btnWoodcut2.Size = new System.Drawing.Size(75, 23);
            this.btnWoodcut2.TabIndex = 20;
            this.btnWoodcut2.Text = "Woodcut";
            this.btnWoodcut2.UseVisualStyleBackColor = true;
            this.btnWoodcut2.Click += new System.EventHandler(this.btnWoodcut2_Click);
            // 
            // btnPickPocket
            // 
            this.btnPickPocket.Location = new System.Drawing.Point(16, 89);
            this.btnPickPocket.Name = "btnPickPocket";
            this.btnPickPocket.Size = new System.Drawing.Size(75, 23);
            this.btnPickPocket.TabIndex = 19;
            this.btnPickPocket.Text = "Pick Pocket";
            this.btnPickPocket.UseVisualStyleBackColor = true;
            this.btnPickPocket.Click += new System.EventHandler(this.btnPickPocket_Click);
            // 
            // btnNightmare
            // 
            this.btnNightmare.Location = new System.Drawing.Point(16, 15);
            this.btnNightmare.Name = "btnNightmare";
            this.btnNightmare.Size = new System.Drawing.Size(75, 23);
            this.btnNightmare.TabIndex = 17;
            this.btnNightmare.Text = "Nightmare";
            this.btnNightmare.UseVisualStyleBackColor = true;
            this.btnNightmare.Click += new System.EventHandler(this.btnNightmare_Click);
            // 
            // btnTan
            // 
            this.btnTan.Location = new System.Drawing.Point(16, 51);
            this.btnTan.Name = "btnTan";
            this.btnTan.Size = new System.Drawing.Size(75, 23);
            this.btnTan.TabIndex = 18;
            this.btnTan.Text = "Tan Hides";
            this.btnTan.UseVisualStyleBackColor = true;
            this.btnTan.Click += new System.EventHandler(this.btnTan_Click);
            // 
            // WoodCut
            // 
            this.WoodCut.Controls.Add(this.btnWoodcut);
            this.WoodCut.Controls.Add(this.tableWoodCut);
            this.WoodCut.Location = new System.Drawing.Point(4, 22);
            this.WoodCut.Name = "WoodCut";
            this.WoodCut.Padding = new System.Windows.Forms.Padding(3);
            this.WoodCut.Size = new System.Drawing.Size(405, 481);
            this.WoodCut.TabIndex = 0;
            this.WoodCut.Text = "Wood Cutting";
            this.WoodCut.UseVisualStyleBackColor = true;
            // 
            // btnWoodcut
            // 
            this.btnWoodcut.Location = new System.Drawing.Point(136, 438);
            this.btnWoodcut.Name = "btnWoodcut";
            this.btnWoodcut.Size = new System.Drawing.Size(114, 23);
            this.btnWoodcut.TabIndex = 1;
            this.btnWoodcut.Text = "Start Woodcutting";
            this.btnWoodcut.UseVisualStyleBackColor = true;
            this.btnWoodcut.Click += new System.EventHandler(this.btnWoodcut_Click);
            // 
            // tableWoodCut
            // 
            this.tableWoodCut.AutoScroll = true;
            this.tableWoodCut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tableWoodCut.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.tableWoodCut.ColumnCount = 2;
            this.tableWoodCut.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableWoodCut.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableWoodCut.Controls.Add(this.radioMagicTree, 0, 5);
            this.tableWoodCut.Controls.Add(this.radioYewTree, 0, 4);
            this.tableWoodCut.Controls.Add(this.radioRegularTree, 0, 0);
            this.tableWoodCut.Controls.Add(this.radioWillowTree, 0, 2);
            this.tableWoodCut.Controls.Add(this.radioMapleTree, 0, 3);
            this.tableWoodCut.Controls.Add(this.radioOakTree, 0, 1);
            this.tableWoodCut.Controls.Add(this.pictureBox8, 1, 0);
            this.tableWoodCut.Controls.Add(this.pictureBox9, 1, 1);
            this.tableWoodCut.Controls.Add(this.pictureBox10, 1, 2);
            this.tableWoodCut.Controls.Add(this.pictureBox11, 1, 3);
            this.tableWoodCut.Controls.Add(this.pictureBox12, 1, 4);
            this.tableWoodCut.Controls.Add(this.pictureBox13, 1, 5);
            this.tableWoodCut.Location = new System.Drawing.Point(6, 6);
            this.tableWoodCut.MaximumSize = new System.Drawing.Size(405, 469);
            this.tableWoodCut.Name = "tableWoodCut";
            this.tableWoodCut.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tableWoodCut.RowCount = 6;
            this.tableWoodCut.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableWoodCut.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableWoodCut.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableWoodCut.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableWoodCut.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableWoodCut.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableWoodCut.Size = new System.Drawing.Size(393, 418);
            this.tableWoodCut.TabIndex = 0;
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
            // radioRegularTree
            // 
            this.radioRegularTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioRegularTree.AutoSize = true;
            this.radioRegularTree.Checked = true;
            this.radioRegularTree.Location = new System.Drawing.Point(5, 5);
            this.radioRegularTree.Name = "radioRegularTree";
            this.radioRegularTree.Size = new System.Drawing.Size(179, 144);
            this.radioRegularTree.TabIndex = 0;
            this.radioRegularTree.TabStop = true;
            this.radioRegularTree.Text = "Regular Tree";
            this.radioRegularTree.UseVisualStyleBackColor = true;
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
            this.pictureBox8.Location = new System.Drawing.Point(192, 5);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(179, 144);
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
            this.pictureBox9.Location = new System.Drawing.Point(192, 157);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(179, 144);
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
            this.pictureBox10.Location = new System.Drawing.Point(192, 309);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(179, 144);
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
            this.pictureBox11.Location = new System.Drawing.Point(192, 461);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(179, 144);
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
            this.pictureBox12.Location = new System.Drawing.Point(192, 613);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(179, 144);
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
            this.pictureBox13.Location = new System.Drawing.Point(192, 765);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(179, 144);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox13.TabIndex = 17;
            this.pictureBox13.TabStop = false;
            // 
            // Mining
            // 
            this.Mining.Controls.Add(this.label10);
            this.Mining.Controls.Add(this.button2);
            this.Mining.Controls.Add(this.btnMining);
            this.Mining.Location = new System.Drawing.Point(4, 22);
            this.Mining.Name = "Mining";
            this.Mining.Padding = new System.Windows.Forms.Padding(3);
            this.Mining.Size = new System.Drawing.Size(405, 481);
            this.Mining.TabIndex = 1;
            this.Mining.Text = "Mining";
            this.Mining.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(200, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "lblInvCheck";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(26, 25);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Set Inv Check For Drop";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnMining
            // 
            this.btnMining.Location = new System.Drawing.Point(141, 256);
            this.btnMining.Name = "btnMining";
            this.btnMining.Size = new System.Drawing.Size(114, 23);
            this.btnMining.TabIndex = 3;
            this.btnMining.Text = "Start Mining";
            this.btnMining.UseVisualStyleBackColor = true;
            // 
            // workerSeersAgility
            // 
            this.workerSeersAgility.WorkerReportsProgress = true;
            this.workerSeersAgility.WorkerSupportsCancellation = true;
            this.workerSeersAgility.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerSeersAgility_DoWork);
            this.workerSeersAgility.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerSeersAgility_RunWorkerCompleted);
            // 
            // workerCanifisAgility
            // 
            this.workerCanifisAgility.WorkerReportsProgress = true;
            this.workerCanifisAgility.WorkerSupportsCancellation = true;
            this.workerCanifisAgility.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerCanifisAgility_DoWork);
            this.workerCanifisAgility.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerCanifisAgility_RunWorkerCompleted);
            // 
            // workerBarbFish
            // 
            this.workerBarbFish.WorkerReportsProgress = true;
            this.workerBarbFish.WorkerSupportsCancellation = true;
            this.workerBarbFish.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerBarbFish_DoWork);
            this.workerBarbFish.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.workerBarbFish_RunWorkerCompleted);
            // 
            // chkClicks
            // 
            this.chkClicks.AutoSize = true;
            this.chkClicks.Checked = true;
            this.chkClicks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClicks.Location = new System.Drawing.Point(130, 28);
            this.chkClicks.Name = "chkClicks";
            this.chkClicks.Size = new System.Drawing.Size(75, 17);
            this.chkClicks.TabIndex = 17;
            this.chkClicks.Text = "Log Clicks";
            this.chkClicks.UseVisualStyleBackColor = true;
            this.chkClicks.CheckedChanged += new System.EventHandler(this.chkClicks_CheckedChanged);
            // 
            // workerRC
            // 
            this.workerRC.WorkerReportsProgress = true;
            this.workerRC.WorkerSupportsCancellation = true;
            this.workerRC.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerRC_DoWork);
            // 
            // workerMining
            // 
            this.workerMining.WorkerReportsProgress = true;
            this.workerMining.WorkerSupportsCancellation = true;
            this.workerMining.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerMining_DoWork);
            // 
            // workerAlch
            // 
            this.workerAlch.WorkerReportsProgress = true;
            this.workerAlch.WorkerSupportsCancellation = true;
            this.workerAlch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerAlch_DoWork);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1162, 576);
            this.Controls.Add(this.chkClicks);
            this.Controls.Add(this.TabController);
            this.Controls.Add(this.btnHide);
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
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderColorRange)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxRandom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minRandom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClickOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderCycles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClicks)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInventoryCount)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.TabController.ResumeLayout(false);
            this.Buttons.ResumeLayout(false);
            this.WoodCut.ResumeLayout(false);
            this.tableWoodCut.ResumeLayout(false);
            this.tableWoodCut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.Mining.ResumeLayout(false);
            this.Mining.PerformLayout();
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
            //LogWrite(Inventory[1].ToString())
;            StartAutoClicker();
        }


        UserActivityHook actHook;
        void MainFormLoad(object sender, System.EventArgs e)
        {
            actHook = new UserActivityHook(); // crate an instance with global hooks
                                              // hang on events
            actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);
            actHook.KeyDown += new KeyEventHandler(MyKeyDown);
            actHook.KeyPress += new KeyPressEventHandler(MyKeyPress);
            actHook.KeyUp += new KeyEventHandler(MyKeyUp);

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
                        LogWrite("Delay from click " + (Clicks.Count - 1) + ": " + ClickStopwatch.ElapsedMilliseconds);
                    }

                    Clicks.Add(new Click(
                        Cursor.Position,
                        e.Button == MouseButtons.Left ? 0 : 1,
                        0));
                    LogWrite("Added Click");
                    ClickStopwatch.Reset();
                    ClickStopwatch.Start();

                }

                if (SettingAlchPoint)
                {
                    AlchPoint = Cursor.Position;
                    SettingAlchPoint = false;
                    btnSetAlch.BackColor = Color.Transparent;
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
                    case Keys.D2:
                        if (RapidClickTimer.Enabled)
                        {
                            LogWrite("End RapidClick");
                            RapidClickTimer.Stop();
                        }
                        else
                        {
                            LogWrite("Start RapidClick");
                            RapidClickTimer.Start();
                        }
                        Ctrl = false;
                        break;
                    case Keys.D3:
                        if (PickPocketTimer.Enabled)
                        {
                            LogWrite("End PickPocket");
                            StopAutoClicker();
                        }
                        else
                        {
                            LogWrite("Start PickPocket");
                            btnPickPocket.PerformClick();
                        }
                        Ctrl = false;
                        break;
                    case Keys.L:
                        chkLog.Checked = !chkLog.Checked;
                        break;
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                        //ClickInventory(true);
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

        private void LogoutTimer_Tick(object sender, EventArgs e)
        {
            StopAutoClicker();
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

            if (radioCycles.Checked)
                ClickTimer.Interval = (int)currentClick.DelayAfterClick + RandomGenerate.Next(-200,200);
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
                    if (RandomTimeoutCount <= 0)
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

            DoMouseClick(mouseButton, clickPoint);

        }

        private void RapidClickTimer_Tick(object sender, EventArgs e)
        {
            var max = (int)(sliderClicks.Value * 1.2) * 100;
            var min = (int)(sliderClicks.Value * .8) * 100;
            if(RandomGenerate.Next(1,10) == 1)
                RapidClickTimer.Interval =  RandomGenerate.Next(20, 100);
            else
                RapidClickTimer.Interval = RandomGenerate.Next(min, max);
            DoMouseClick(0, Point.Empty);
        }

        private void NMZPrayerTimer_Tick(object sender, EventArgs e)
        {
            var delay = RandomGenerate.Next(1200, 2000);
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var secondClick = false;
            DoMouseClick(0, new Point(1746, 133));
            while (!secondClick)
            {
                if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds >= delay)
                {
                    secondClick = true;
                    DoMouseClick(0, new Point(1746, 133));
                }

            }
            PrayerTimer.Interval = RandomGenerate.Next(45000, 55000);
            LogWrite("Next prayer tick: " + PrayerTimer.Interval);
        }

        private void EatTimer_Tick(object sender, EventArgs e)
        {
            //Rectangle cloneRect = new Rectangle(1710, 94, 20, 14);
            //var screeshot = MainScreen.CaptureScreen();
            ////var health = screeshot.
            //System.Drawing.Imaging.PixelFormat format = screeshot.PixelFormat;
            //Bitmap image = screeshot.Clone(cloneRect, format);
            //Bitmap imageClone = new Bitmap(image);
            //var time = DateTime.Now.ToString("h.mm.ss");

            //Convert(imageClone, false);
            //var image2 = new Bitmap(imageClone, imageClone.Width * 3, imageClone.Height * 3);
            //Convert2(image2, false);
            
            ////ocr.SetVariable("tessedit_char_whitelist", "0123456789");
            //var result = ocr.DoOCR(image2, Rectangle.Empty);
            //int hp = 0;
            //var success = int.TryParse(result[0].Text, out hp);
            //if (!success)
            //{
            //    Bitmap imageClone2 = new Bitmap(image);
            //    Convert(imageClone, true);
            //    image2 = new Bitmap(imageClone, imageClone.Width * 3, imageClone.Height * 3);
            //    Convert2(image2, false);
            //    result = ocr.DoOCR(image2, Rectangle.Empty);
            //    success = int.TryParse(result[0].Text, out hp);
            //    if (!success)
            //    if (!success)
            //    {
            //        EatFailCount++;

            //        LogWrite("EAT FAIL: " + EatFailCount);
            //        if (EatFailCount >= EatFailCountMax)
            //            StopAutoClicker();
            //        return;
            //    }
            //}
            //EatFailCount = 0;
            //if (hp <= 30)
            //{
            //    if (CurrentInventoryClickCount >= TotalInventoryClickCount || hp < 10)
            //    {
            //        StopAutoClicker();
            //        return;
            //    }
            //    PickPocketTimer.Stop();
            //    long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //    while(DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds < 1500)
            //    {

            //    }
                
            //    var point = Inventory[CurrentInventoryClickCount];
            //    DoMouseClick(0, point);
            //    CurrentInventoryClickCount++;
            //    LogWrite("Eating");
            //    PickPocketTimer.Start();
                    
            //}
            //else
            //    LogWrite("HP Safe");

            //screeshot = null;
            //image2 = null;
            //image = null;
            //imageClone = null;

        }

        private void PickPocketTimer_Tick(object sender, EventArgs e)
        {
            //450,330

            if (UseRandomTimeouts)
            {
                if (TimedOut)
                {
                    PickPocketTimer.Interval = 15000;
                    TimedOut = false;
                }  
                else
                {
                    RandomTimeoutCount--;
                    if (RandomTimeoutCount <= 0)
                    {
                        RapidClickTimer.Stop();
                        RandomTimeoutCount = GetRandomTimeoutCount();
                        PickPocketTimer.Interval = GetRandomTimeout();
                        LogWrite("Hit Timeout for : " + PickPocketTimer.Interval);
                        LogWrite("New count : " + RandomTimeoutCount);

                        TimedOut = true;
                        return;
                    }
                }
            }
            
            var origMousePoint = Mouse.Mouse.GetLocation();
            var heartImage = Properties.Resources.HealthHeart2;
            var imageList = new List<Bitmap>() { Properties.Resources.Gold20, Properties.Resources.Gold21,
                                                 Properties.Resources.Gold22, Properties.Resources.Gold23,
                                                 Properties.Resources.Gold24, Properties.Resources.Gold25,
                                                 Properties.Resources.Gold26, Properties.Resources.Gold27, Properties.Resources.Gold28};
            var colorRange = 360m * (20 / 100m); //sliderColorRange.Value

            var healthBar = Color.FromArgb(225, 35, 0);

            LogWrite("Checking for coin bag");
            var screenShot = MainScreen.CaptureScreen();
            var lowerX = screenShot.Width / 4;
            var lowerY = screenShot.Height / 4;
            Point point = MainScreen.FindImageFromList(screenShot, new Point(screenShot.Width - lowerX, screenShot.Height / 2), new Point(screenShot.Width, screenShot.Height), imageList, colorRange);
            if (!point.IsEmpty)
            {
                RapidClickTimer.Stop();
                point.X += 10;
                point.Y += 20;
                LogWrite("FOUND");
                DoMouseClick(0, point);

                Thread.Sleep(RandomGenerate.Next(500, 2000));

                Mouse.Mouse.MoveTo(origMousePoint.X + RandomGenerate.Next(-5, 5), origMousePoint.Y + RandomGenerate.Next(-5, 5));
            }

            if(!MainScreen.FindColorPointRange(screenShot, AlchPoint, healthBar, colorRange))
            {
                EatFailCount++;
                RapidClickTimer.Stop();
                if(CurrentInventoryClickCount >= TotalInventoryClickCount && EatFailCount < 9)
                {
                    EatFailCount = 9;
                }
                else
                {
                    point = Inventory[CurrentInventoryClickCount];
                    CurrentInventoryClickCount++;
                    point.X += 10;
                    point.Y += 10;
                    LogWrite("FOUND");
                    DoMouseClick(0, point);
                }
                

                Thread.Sleep(RandomGenerate.Next(500, 2000));

                Mouse.Mouse.MoveTo(origMousePoint.X + RandomGenerate.Next(-5, 5), origMousePoint.Y + RandomGenerate.Next(-5, 5));
            }
            else
            {
                EatFailCount = 0;
            }

            if(EatFailCount >= EatFailCountMax)
            {
                StopAutoClicker();
                return;
            }


            if (!RapidClickTimer.Enabled)
                RapidClickTimer.Start();

            

            //LogWrite("Checking for health");
            //Point point = MainScreen.FindImage(screenShot, new Point(screenShot.Width - lowerX, 0), new Point(screenShot.Width, lowerY), heartImage, colorRange);
            //if (!point.IsEmpty)
            //{
            //    LogWrite("FOUND");
            //    DoMouseClick(0, point);
            //}

            //long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //var colorRange = 360m * (sliderColorRange.Value / 100m);
            //var image = Properties.Resources.Pick_Pocket;
            //DoMouseClick(1, new Point(x, y));
            //while (DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds < 1500)
            //{

            //}
            //var point = MainScreen.FindImage(new Point(200, 170), new Point(700, 550), image, colorRange);

            //if (!point.IsEmpty)
            //{
            //    point.X += image.Width / 2;
            //    point.Y += image.Height / 2;

            //    DoMouseClick(0, point);
            //}
        }

        private void WoodcutTimer_Tick(object sender, EventArgs e)
        {
            //950, 420
            var y = 420;
            var x = 950;
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var image = Properties.Resources.Woodcut;
            DoMouseClick(1, new Point(x, y));
            while (DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds < 1500)
            {

            }
            var point = MainScreen.FindImage(new Point(750, 300), new Point(delayTime, 500), image, colorRange);
            
            point.X += image.Width / 2;
            point.Y += image.Height / 2;

            DoMouseClick(0, point);
        }

        private void InvFullTimer_Tick(object sender, EventArgs e)
        {
            //1835,925
            //1900,990
            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var image = Properties.Resources.Empty_Inv_Space;
            var point = MainScreen.FindImage(new Point(1835, 925), new Point(1900, 990), image, colorRange);
            if (point.IsEmpty)
            {
                WoodcutTimer.Stop();
                ClickInventory(true);
                WoodcutTimer.Start();
            }

        }
        private void NMZDrinkTimer_Tick(object sender, EventArgs e)
        {
            var screenshot = MainScreen.CaptureScreen();
            FindAbsorbPotion(screenshot);
            //long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //var secondClick = false;
            //while (!secondClick)
            //{
            //    if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds >= delayTime)
            //    {
            //        secondClick = true;
            //        FindOverloadPotion(screenshot);
            //    }

            //}
            DrinkTimer.Interval = RandomGenerate.Next(60000, 240000);
            LogWrite("Next drink tick: " + DrinkTimer.Interval);
            System.GC.Collect();
            
            //LogWrite(String.Format("X:{0} Y:{1}", point.X, point.Y));
        }
        private void FindOverloadPotion(Bitmap screenshot)
        {
            LogWrite("Checking for overload");

            var image = Properties.Resources.Over_1;
            var offsetX = image.Width / 2;
            var offsetY = image.Height / 2;

            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var point = MainScreen.FindInInventory(Properties.Resources.Over_1, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 1 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Over_2, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 2 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Over_3, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 3 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Over_4, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 4 dose");
                DoMouseClick(0, point);
                return;
            }
        }

        private void FindAbsorbPotion(Bitmap screenshot)
        {
            LogWrite("Checking for absorb");

            var image = Properties.Resources.Absorb_1;
            var offsetX = image.Width / 2;
            var offsetY = image.Height / 2;

            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var point = MainScreen.FindInInventory(Properties.Resources.Absorb_1, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 1 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Absorb_2, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 2 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Absorb_3, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 3 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Absorb_4, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 4 dose");
                DoMouseClick(0, point);
                return;
            }
        }

        private void NMZOverloadTimer_Tick(object sender, EventArgs e)
        {
            LogWrite("Checking for overload");
            var screenshot = MainScreen.CaptureScreen();

            var image = Properties.Resources.Over_1;
            var offsetX = image.Width / 2;
            var offsetY = image.Height / 2;

            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var point = MainScreen.FindInInventory(Properties.Resources.Over_1, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 1 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Over_2, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 2 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Over_3, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 3 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Over_4, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += offsetX;
                point.Y += offsetY;
                LogWrite("found 4 dose");
                DoMouseClick(0, point);
                return;
            }
            //LogWrite(String.Format("X:{0} Y:{1}", point.X, point.Y));
        }

        public void DoMouseClick(int mouseButton, Point point)
        {
            if(point != Point.Empty)
            {
                point.X += GetRandomOffset();
                point.Y += GetRandomOffset();
                if (logClicks)
                    LogWrite("Clicked at X:" + point.X + "  Y:" + point.Y);
                Mouse.Mouse.MoveTo(point.X, point.Y);
            }
            
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            while(DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds <= 200)
            {

            }
            //Thread.Sleep(200);
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

        public void DoMouseClickNew(int mouseButton, Point point)
        {
            if (point != Point.Empty)
            {
                point.X += GetRandomOffset();
                point.Y += GetRandomOffset();
                if (logClicks)
                    LogWrite("Clicked at X:" + point.X + "  Y:" + point.Y);
                Mouse.Mouse.MoveTo(point.X, point.Y);
            }

            //Thread.Sleep(200);
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

        public void DoMouseClickAsync(IProgress<string> report, int mouseButton, Point point)
        {
            if (point != Point.Empty)
            {
                point.X += GetRandomOffset();
                point.Y += GetRandomOffset();
                report.Report("Clicked at X:" + point.X + "  Y:" + point.Y);
                Mouse.Mouse.MoveTo(point.X, point.Y);
            }

            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            while (DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds <= 200)
            {

            }
            //Thread.Sleep(200);
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
            if (RunProgram || NMZEnabled || PickPocket || Woodcutting)
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
            ClickTimer.Interval = GetInterval();
            ClickTimer.Start();
            LogWrite("Starting Auto Clicker");
        }

        public void StopAutoClicker()
        {

            RapidClickTimer.Stop();
            BarbFishTimer.Stop();
            AutoShootTimer.Stop();
            LogoutTimer.Stop();
            ClickTimer.Stop();
            PrayerTimer.Stop();
            OverloadTimer.Stop();
            DrinkTimer.Stop();
            EatTimer.Stop();
            PickPocketTimer.Stop();
            WoodcutTimer.Stop();
            InvFullTimer.Stop();
            sliderClicks.Enabled = true;
            numCount.Enabled = true;
            buttonRecord.Enabled = true;
            RunProgram = false;
            NMZEnabled = false;
            PickPocket = false;
            Woodcutting = false;
            workerAlch.CancelAsync();

            buttonStart.Text = "Start";
            LogWrite("Stopping Auto Clicker");
        }

        private void sliderClicks_Scroll(object sender, EventArgs e)
        {
            UpdateTrack();
        }

        private int GetInterval()
        {
            var max = (int)(ActiveSlider.Value * 1.1 * 100);
            var min = (int)(ActiveSlider.Value * 0.9 * 100);
            return RandomGenerate.Next(min, max); //(int)((ActiveSlider.Value / 10.0) * delayTime);
        }

        private void UpdateTrack()
        {
            var interval = GetInterval();
            ActiveLabel.Text = (ActiveSlider.Value / 10.0).ToString();
            if (radioClicks.Checked)
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

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox.Text = text;
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void LogWrite(string txt)
        {
            if (LogInfo)
            {
                //SetText(txt + Environment.NewLine);
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

        private void ClickInventory(bool dropping)
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

            if (dropping)
            {
                Dropping = true;
                DoMouseClick(0, Inventory[DropClickPos]);
                Thread.Sleep(100);
                DoMouseClick(0, Inventory[DropClickPos]);
                Thread.Sleep(100);
                //Keyboard.Keyboard.HoldKey(Keys.ShiftKey);
                Keyboard.MyKeyboard.PressKey(Keyboard.MyKeyboard.VK_LSHIFT, Keyboard.MyKeyboard.SCANKEY_LSHIFT);
            }

            DropTimer.Start();
        }

        private void DropTimer_Tick(object sender, EventArgs e)
        {
            if (DropClickPos >= Inventory.Count || DropClickPos < 0 || CurrentInventoryClickCount >= TotalInventoryClickCount)
            {
                DropTimer.Stop();
                //Keyboard.Keyboard.ReleaseKey(Keys.ShiftKey);
                if (Dropping)
                {
                    Thread.Sleep(100);
                    Keyboard.MyKeyboard.ReleaseKey(Keyboard.MyKeyboard.VK_LSHIFT, Keyboard.MyKeyboard.SCANKEY_LSHIFT);
                    Dropping = false;
                }
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

            currentPoint.X += RandomGenerate.Next(-5, 5);
            currentPoint.Y += RandomGenerate.Next(-5, 5);
            DoMouseClick(0, currentPoint);
            DropTimer.Interval = RandomGenerate.Next(50,200);
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
            btnDropInventory.Enabled = false;
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
            btnDropInventory.Enabled = true;
        }

        private void btnSingleClickInv_Click(object sender, EventArgs e)
        {
            ClickInventory(false);
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
            return RandomGenerate.Next((int)minRandom.Value, (int)maxRandom.Value);
        }
        private int GetRandomTimeout()
        {
            return RandomGenerate.Next(30000, 240000);
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
            ClickInventory(true);
        }

        private void numInventoryCount_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown counter = (NumericUpDown)sender;
            if ((int)counter.Value > Inventory.Count)
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
            var image = Properties.Resources.tanner_head; //. //MainScreen.LoadImageFile("c:\\AppData\\AutoClicker\\Images\\TreeSwatch.png");
            var checkedImage = TabController.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var offsetX = image.Width / 2;
            var offsetY = image.Height / 2;
            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var point = MainScreen.FindImage(new Point(5, 35), new Point(1910, 990), image, colorRange);

            LogWrite(String.Format("X:{0} Y:{1}", point.X, point.Y));

            //Mouse.Mouse.MoveTo(point.X + offsetX, point.Y + offsetY);
            DoMouseClick(1, new Point(point.X + offsetX, point.Y + offsetY));
        }

        private void sliderColorRange_Scroll(object sender, EventArgs e)
        {
            lblColorRange.Text = (100 - sliderColorRange.Value).ToString() + "%";
        }

        private void btnWoodcut_Click(object sender, EventArgs e)
        {

            var checkedButton = tableWoodCut.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var checkedImage = PictureDictionary.Dictionary[checkedButton.Name];
            var offsetX = checkedImage.Width / 2;
            var offsetY = checkedImage.Height / 2;
            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var point = MainScreen.FindImage(new Point(5,35), new Point(1910, 990), checkedImage, colorRange);
            LogWrite(String.Format("X:{0} Y:{1}", point.X, point.Y));

            Mouse.Mouse.MoveTo(point.X + offsetX, point.Y + offsetY);
        }

        private void btnNightmare_Click(object sender, EventArgs e)
        {
            if (NMZEnabled)
            {
                LogWrite("Stopping Nightmare Zone!");
                StopAutoClicker();
                buttonStart.Enabled = true;
                return;
            }

            buttonStart.Enabled = false;
            LogWrite("Starting Nightmare Zone!");

            PrayerTimer.Start();
            LogoutTimer.Start();
            //OverloadTimer.Start();
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var secondStart = false;
            while (!secondStart)
            {
                if (DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds >= 6000)
                {
                    secondStart = true;

                    DrinkTimer.Start();
                }
            }
            NMZEnabled = true;
        }

        private void btnTan_Click(object sender, EventArgs e)
        {
            var interval = delayTime;
            Point point = new Point(); ;
            List<Bitmap> imageList = new List<Bitmap>()
            {
                Properties.Resources.Tanner_1,
                Properties.Resources.Tanner_2,
                Properties.Resources.Tanner_3,
                Properties.Resources.Tanner_4,
                Properties.Resources.Tanner_Head1
            };
            for(int i = 0; i < imageList.Count; i++)
            {
                point = FindTanner(imageList[i]);
                if (point.IsEmpty)
                    continue;
            }
            if (point.IsEmpty)
                return;

            LogWrite("tanner");
            DoMouseClick(1, point);
        }

        private Point FindTanner(Bitmap image) {
            var point = new Point();
            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var screenshot = MainScreen.CaptureScreen();
            point = MainScreen.FindTanner(image, screenshot, colorRange);
            return point;
        }

        private void btnPickPocket_Click(object sender, EventArgs e)
        {
            
            //var colorRange = 360m * (sliderColorRange.Value / 100m);
            //LogWrite(colorRange.ToString());
            if (Inventory.Count < 1)
            {
                MessageBox.Show("Need to setup inventory first.");
                return;
            }

            if (PickPocket)
            {
                StopAutoClicker();
                return;
            }

            LogWrite("Starting Pickpocket!");
            EatFailCount = 0;
            CurrentInventoryClickCount = 2;
            TotalInventoryClickCount = (int)numInventoryCount.Value;
            //EatTimer.Start();
            PickPocketTimer.Interval = 15000;
            PickPocketTimer.Start();
            RapidClickTimer.Start();
            PickPocket = true;


        }
        private void Convert(Bitmap image, bool widen)
        {
            //Bitmap image = orig;
            Color match = Color.FromArgb(39, 104, 90, 75);
            Color black = Color.FromArgb(0, 0, 0);
            Color white = Color.FromArgb(255, 255, 255);
            Color yellow = Color.FromArgb(255, 241, 0);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color gotColor = image.GetPixel(x, y);
                    if(MainScreen.ColorDiff(gotColor, black) < 0.05m)
                    {
                        image.SetPixel(x, y, white);
                        //image.SetPixel(x - 1, y, white);
                    }
                    else if (MainScreen.ColorDiff(gotColor, match) < 0.05m)
                    {
                        image.SetPixel(x, y, white);
                        //image.SetPixel(x - 1, y, white);
                    }
                    else
                    {
                        image.SetPixel(x, y, black);
                        if(widen && x-1 > 0)
                            image.SetPixel(x-1, y, black);
                    }
                }
            }
        }
        private void Convert2(Bitmap image, bool widen)
        {
            //Bitmap image = orig;
            Color match = Color.FromArgb(39, 104, 90, 75);
            Color black = Color.FromArgb(0, 0, 0);
            Color white = Color.FromArgb(255, 255, 255);
            Color yellow = Color.FromArgb(255, 241, 0);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color gotColor = image.GetPixel(x, y);
                    if (MainScreen.ColorDiff(gotColor, black) < 250m)
                    {
                        image.SetPixel(x, y, black);
                        if (widen)
                            image.SetPixel(x - 1, y, black);
                    }
                    else
                    {
                        //Console.WriteLine("Other: " + MainScreen.ColorDiff(gotColor, black));
                        image.SetPixel(x, y, white);
                    }
                }
            }
        }

        private void btnWoodcut2_Click(object sender, EventArgs e)
        {
            if (Woodcutting)
            {
                StopAutoClicker();
                return;
            }
            Woodcutting = true;
            LogWrite("Staring wood cutting");
            WoodcutTimer.Start();
            long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            while(DateTimeOffset.Now.ToUnixTimeMilliseconds() - milliseconds < 6000)
            {

            }
            InvFullTimer.Start();

        }

        private  void btnSeersAgil_Click(object sender, EventArgs e)
        {
            //var screenshot = MainScreen.CaptureScreen();

            var report = new Progress<string>(value => LogWrite(value));

            agilOn = !agilOn;
            if (agilOn)
            {
                LogWrite("Starting Seers Agility");
                //AgilityTimer.Start(); 
                workerSeersAgility.RunWorkerAsync(report);
            }
            else
            {
                LogWrite("Ending Seers Agility");
                //AgilityTimer.Stop();
                workerSeersAgility.CancelAsync();
            }


        }

        private async void workerSeersAgility_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //var image = Properties.Resources.Capture;
            //var offsetX = image.Width / 2;
            //var offsetY = image.Height / 2;

            //var colorRange = 360m * (20 / 100m);
            //Point point;

            ////bank
            //point = MainScreen.FindImage(new Point(950, 350), new Point(1150, 450), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            //Thread.Sleep(RandomGenerate.Next(7000,8500));
            ////First Ledge
            //point = MainScreen.FindImage(new Point(600, 350), new Point(750, 500), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            //Thread.Sleep(RandomGenerate.Next(7000, 8500));
            ////tight rope
            //point = MainScreen.FindImage(new Point(800, 650), new Point(900, 750), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            //Thread.Sleep(RandomGenerate.Next(10000, 12000));
            ////Second Ledge
            //point = MainScreen.FindImage(new Point(900, 600), new Point(1150, 700), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            //Thread.Sleep(RandomGenerate.Next(5000, 6500));
            ////Third Ledge
            //point = MainScreen.FindImage(new Point(550, 575), new Point(750, 675), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            //Thread.Sleep(RandomGenerate.Next(6000, 8000));
            ////Finish
            //point = MainScreen.FindImage(new Point(950, 500), new Point(1050, 700), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            //Thread.Sleep(RandomGenerate.Next(4000, 5500));

            ////LogWrite(String.Format("X:{0} Y:{1}", point.X, point.Y));

            ////Teleport
            //point = new Point(1867, 817);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));

            //Thread.Sleep(RandomGenerate.Next(3500,6500));


            var report = e.Argument as IProgress<string>;

            while (!workerSeersAgility.CancellationPending)
            {
                await RunSeersAgility(report);
            }


        }

        private Task RunSeersAgility(IProgress<string> report)
        {
            report.Report("TEST");
            //await Task.Run(() => RunSeersCourse(report));
            RunSeersCourse(report);


            Thread.Sleep(RandomGenerate.Next(4000, 5500));
            //LogWrite(String.Format("X:{0} Y:{1}", point.X, point.Y));

            //Teleport
            Point point = new Point(1867, 817);
            point.X += RandomGenerate.Next(-3, 3);
            point.Y += RandomGenerate.Next(-3, 3);
            DoMouseClickAsync(report, 0, new Point(point.X, point.Y));

            Thread.Sleep(RandomGenerate.Next(3500, 6500));

            return Task.CompletedTask;
        }

        private void RunSeersCourse(IProgress<string> report)
        {
            report.Report("TEST2");
            var MaxX = Screen.PrimaryScreen.Bounds.Width;
            var MaxY = Screen.PrimaryScreen.Bounds.Height;
            var MidX = Screen.PrimaryScreen.Bounds.Width / 2;
            var MidY = Screen.PrimaryScreen.Bounds.Height / 2;
            var offsetX = 0;
            var offsetY = 0;
            var attackDot = Color.FromArgb(180, 255, 0, 255);

            //workerBarbFish.RunWorkerAsync();
            report.Report("Check For Targets");
            //var screenShot = MainScreen.CaptureScreen();

            var colorRange = 360m * (35 / 100m);

            Point point;

            //bank
            //point = MainScreen.FindImage(new Point(950, 350), new Point(1150, 450), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreenRange(new Point(MidX, 0), new Point(MaxX, MidY), attackDot, 8, colorRange);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-5, 15);
                offsetY = RandomGenerate.Next(-5, 15);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(7000, 8500));

            //First Ledge
            //point = MainScreen.FindImage(new Point(600, 350), new Point(750, 500), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreenRange(new Point(0, 0), new Point(MidX, MidY), attackDot, 8, colorRange);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-5, 15);
                offsetY = RandomGenerate.Next(-5, 15);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(7000, 8500));

            //tight rope
            //point = MainScreen.FindImage(new Point(800, 650), new Point(900, 750), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreenRange(new Point(0, MidY), new Point(MidX, MaxY), attackDot, 8, colorRange);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-5, 15);
                offsetY = RandomGenerate.Next(-5, 15);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(10000, 12000));

            //Second Ledge
            //point = MainScreen.FindImage(new Point(900, 600), new Point(1150, 700), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreenRange(new Point(MidX, MidY), new Point(MaxX, MaxY), attackDot, 8, colorRange);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-5, 15);
                offsetY = RandomGenerate.Next(-5, 15);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(5000, 6500));

            //Third Ledge
            //point = MainScreen.FindImage(new Point(550, 575), new Point(750, 675), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreenRange(new Point(0, MidY), new Point(MidX, MaxY), attackDot, 8, colorRange);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-5, 15);
                offsetY = RandomGenerate.Next(-5, 15);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(6000, 8000));

            //Finish
            //point = MainScreen.FindImage(new Point(950, 500), new Point(1050, 700), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreenRange(new Point(MidX, MidY), new Point(MaxX, MaxY), attackDot, 8, colorRange);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-5, 15);
                offsetY = RandomGenerate.Next(-5, 15);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
        }

        private void PopulateSeersAgilityClicks()
        {
            Clicks.Clear();
            Clicks.Add(new Click(new Point(920, 785), 0, 1000));

            Clicks.Add(new Click(new Point(968, 785), 0, 1000));

            Clicks.Add(new Click(new Point(1018, 785), 1, 2000));

            Clicks.Add(new Click(new Point(977, 880), 0, 1000));

            //run / leave bak
            Clicks.Add(new Click(new Point(964, 960), 0, 1000));

            //eat
            Clicks.Add(new Click(new Point(1791, 761), 0, 2000));

            Clicks.Add(new Click(new Point(1837, 755), 0, 2000));

            //continue run
            Clicks.Add(new Click(new Point(964, 960), 0, 5000));

            Clicks.Add(new Click(new Point(964, 960), 0, 5000));

            Clicks.Add(new Click(new Point(1209, 960), 0, 5000));

            Clicks.Add(new Click(new Point(1529, 486), 0, 7000));

            Clicks.Add(new Click(new Point(1753, 620), 0, 11000));

            Clicks.Add(new Click(new Point(200, 518), 0, 10000));

            Clicks.Add(new Click(new Point(492, 190), 0, 11000));

            Clicks.Add(new Click(new Point(960, 197), 0, 6000));

            Clicks.Add(new Click(new Point(961, 246), 0, 6000));

            Clicks.Add(new Click(new Point(1034, 825), 0, 1000));
        }

        private void workerSeersAgility_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //this.progressBar1.Value = e.ProgressPercentage;
        }

        private void workerSeersAgility_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.progressBar1.Value = e.ProgressPercentage;
            LogWrite("Seers Lap Complete");

            if (UseRandomTimeouts)
            {
                RandomTimeoutCount--;
                if (RandomTimeoutCount <= 0)
                {
                    LogWrite("Doing Timeout");
                    RandomTimeoutCount = GetRandomTimeoutCount();
                    long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    long timeout = GetRandomTimeout() + milliseconds;
                    while (timeout > DateTimeOffset.Now.ToUnixTimeMilliseconds())
                    {

                    }
                }
            }

            if (agilOn)
            {
                
                workerSeersAgility.RunWorkerAsync();
            }
                

            System.GC.Collect();
        }

        private void workerCanifisAgility_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.progressBar1.Value = e.ProgressPercentage;
            System.GC.Collect();
        }

        private void AgilityTimer_Tick(object sender, EventArgs e)
        {

            workerSeersAgility.RunWorkerAsync();

        }

        private void BarbFishTimer_Tick(object sender, EventArgs e)
        {

            //workerBarbFish.RunWorkerAsync();
            LogWrite("Check Fishing");
            var screenShot = MainScreen.CaptureScreen();
            var image = Properties.Resources.NotFishing;
            var colorRange = 360m * (20 / 100m); //sliderColorRange.Value
            Point point;

            point = MainScreen.FindImage(screenShot, new Point(1530, 80), new Point(1700, 120), image, colorRange);
            if (!point.IsEmpty)
            {
                image = Properties.Resources.EmptyInvSpace;
                point = MainScreen.FindImage(screenShot, new Point(1850, 940), new Point(1900, 985), image, colorRange);
                if (point.IsEmpty)
                {
                    ClickInventory(true);
                }
                else
                {
                    image = Properties.Resources.BarbFish11;
                    point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
                    if (point.IsEmpty)
                    {
                        image = Properties.Resources.BarbFish12;
                        point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
                    }
                    if (point.IsEmpty)
                    {
                        image = Properties.Resources.BarbFish13;
                        point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
                    }
                    if (point.IsEmpty)
                    {
                        image = Properties.Resources.BarbFish;
                        point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
                    }
                    var offsetX = image.Width / 2;
                    var offsetY = image.Height / 2;
                    DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
                }
            }
            System.GC.Collect();
            LogWrite("Fish Ended");

        }

        private void AutoShootTimer_Tick(object sender, EventArgs e)
        {

            //R-38
            //G-30
            //B-23

            //var FightBorder = Color.FromArgb(56, 48, 35);
            var MonsterHealth = Color.FromArgb(4, 136, 52);
            var attackDot = Color.FromArgb(212, 0, 255);

            //workerBarbFish.RunWorkerAsync();
            LogWrite("Check For Targets");
            var screenShot = MainScreen.CaptureScreen();
            var testPixel = screenShot.GetPixel(9, 72);

            if (testPixel != MonsterHealth)
            {
                var point = MainScreen.FindColor(screenShot, new Point(200, 200), new Point(1200, 900), attackDot, 8);
                if (!point.IsEmpty)
                {
                    DoMouseClick(0, new Point(point.X, point.Y));
                }
            }


            //for (int x = 5; x < 12; x++)
            //{
            //    for (int y = 50; y < 60; y++)
            //    {
            //        //for(int a = 0; a < 255; a++)
            //        //{
            //        //var FightBorder = Color.FromArgb(56, 48, 35);
            //        if (screenShot.GetPixel(x, y) == FightBorder)
            //            {
            //                LogWrite("Found: " + x + ":" + y);
            //            }
            //        //}
            //    }
            //}
            LogWrite("Check For Targets Ended");
            return;
            
            //var colorRange = 360m * (20 / 100m); //sliderColorRange.Value
            //Point point;

            //point = MainScreen.FindImage(screenShot, new Point(1530, 80), new Point(1700, 120), image, colorRange);
            //if (!point.IsEmpty)
            //{
            //    image = Properties.Resources.EmptyInvSpace;
            //    point = MainScreen.FindImage(screenShot, new Point(1850, 940), new Point(1900, 985), image, colorRange);
            //    if (point.IsEmpty)
            //    {
            //        ClickInventory(true);
            //    }
            //    else
            //    {
            //        image = Properties.Resources.BarbFish11;
            //        point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
            //        if (point.IsEmpty)
            //        {
            //            image = Properties.Resources.BarbFish12;
            //            point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
            //        }
            //        if (point.IsEmpty)
            //        {
            //            image = Properties.Resources.BarbFish13;
            //            point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
            //        }
            //        if (point.IsEmpty)
            //        {
            //            image = Properties.Resources.BarbFish;
            //            point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
            //        }
            //        var offsetX = image.Width / 2;
            //        var offsetY = image.Height / 2;
            //        DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            //    }
            //}
            //System.GC.Collect();
            //LogWrite("Check For Targets Ended");

        }

        private void WoodCutTimer2_Tick(object sender, EventArgs e)
        {

            //workerBarbFish.RunWorkerAsync();
            LogWrite("Check Woodcut");
            var screenShot = MainScreen.CaptureScreen();
            var image = Properties.Resources.WCOK;
            var colorRange = 360m * (20 / 100m); //sliderColorRange.Value
            Point point;

            point = MainScreen.FindImage(screenShot, new Point(90, 50), new Point(135, 80), image, colorRange);
            if (point.IsEmpty)
            {
                image = Properties.Resources.EmptyInvSpace;
                point = MainScreen.FindImage(screenShot, new Point(1850, 940), new Point(1900, 985), image, colorRange);
                if (point.IsEmpty)
                {
                    ClickInventory(true);
                }
                else
                {
                    image = Properties.Resources.Woodcut1;
                    point = MainScreen.FindImage(screenShot, new Point(800, 400), new Point(1100, 700), image, colorRange);
                    if (point.IsEmpty)
                    {
                        image = Properties.Resources.Woodcut2;
                        point = MainScreen.FindImage(screenShot, new Point(800, 400), new Point(1100, 700), image, colorRange);
                    }
                    //if (point.IsEmpty)
                    //{
                    //    image = Properties.Resources.BarbFish13;
                    //    point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
                    //}
                    //if (point.IsEmpty)
                    //{
                    //    image = Properties.Resources.BarbFish;
                    //    point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
                    //}
                    var offsetX = image.Width / 2;
                    var offsetY = image.Height / 2;
                    DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
                }
            }
            System.GC.Collect();
            LogWrite("Woodcut Ended");

        }

        private void RuneCraftTimer_Tick(object sender, EventArgs e)
        {
            //click in bank
            Clicks.Add(new Click(new Point(920, 785), 0, 1000));

            Clicks.Add(new Click(new Point(968, 785), 0, 1000));

            Clicks.Add(new Click(new Point(1018, 785), 1, 2000));

            Clicks.Add(new Click(new Point(977, 880), 0, 1000));

            //run / leave bak
            Clicks.Add(new Click(new Point(964, 960), 0, 1000));

            //eat
            Clicks.Add(new Click(new Point(1791, 761), 0, 2000));

            Clicks.Add(new Click(new Point(1837, 755), 0, 2000));

            //continue run
            Clicks.Add(new Click(new Point(964, 960), 0, 5000));

            Clicks.Add(new Click(new Point(964, 960), 0, 5000));

            Clicks.Add(new Click(new Point(1209, 960), 0, 5000));

            Clicks.Add(new Click(new Point(1529, 486), 0, 7000));

            Clicks.Add(new Click(new Point(1753, 620), 0, 11000));

            Clicks.Add(new Click(new Point(200, 518), 0, 10000));

            Clicks.Add(new Click(new Point(492, 190), 0, 11000));

            Clicks.Add(new Click(new Point(960, 197), 0, 6000));

            Clicks.Add(new Click(new Point(961, 246), 0, 6000));

            Clicks.Add(new Click(new Point(1034, 825), 0, 1000));
            //Thread.Sleep(1000);
        }
        private void workerBarbFish_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            LogWrite("Check Fishing");
            var screenShot = MainScreen.CaptureScreen();
            var image = Properties.Resources.NotFishing;
            var colorRange = 360m * (10 / 100m);
            Point point;

            point = MainScreen.FindImage(screenShot, new Point(1530, 80), new Point(1700, 120), image, colorRange);
            if(!point.IsEmpty)
            {
                image = Properties.Resources.Empty_Inv_Space;
                point = MainScreen.FindImage(screenShot, new Point(1855, 950), new Point(1900, 985), image, colorRange);
                if (point.IsEmpty)
                {
                    ClickInventory(true);
                }
                else
                {
                    image = Properties.Resources.BarbFish11;
                    point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange);
                    var offsetX = image.Width / 2;
                    var offsetY = image.Height / 2;
                    DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
                }
            }
            LogWrite("Fish Ended");
        }

        private void workerBarbFish_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.progressBar1.Value = e.ProgressPercentage;
            System.GC.Collect();
        }

        private void workerCanifisAgility_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var image = Properties.Resources.Capture;
            var offsetX = image.Width / 2;
            var offsetY = image.Height / 2;

            var colorRange = 360m * (18 / 100m);
            Point point;

            //Climb
            //point = MainScreen.FindImage(new Point(740, 330), new Point(930, 440), image, colorRange);
            DoMouseClick(0, new Point(770, 350));
            Thread.Sleep(8000);
            //First Ledge
            point = MainScreen.FindImage(new Point(900, 290), new Point(980, 400), image, colorRange);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(5500);
            //Second Ledge
            point = MainScreen.FindImage(new Point(750, 480), new Point(800, 575), image, colorRange);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(5500);
            //Third Ledge
            point = MainScreen.FindImage(new Point(700, 650), new Point(750, 750), image, colorRange);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(6000);
            //Fourth Ledge
            point = MainScreen.FindImage(new Point(880, 740), new Point(945, 850), image, colorRange);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(5500);
            //Polevault
            point = MainScreen.FindImage(new Point(980, 600), new Point(1060, 660), image, colorRange);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(7500);

            point = MainScreen.FindImage(new Point(1300, 500), new Point(1570, 600), image, colorRange);
            if (point.IsEmpty) {
                point = new Point(1500, 570);
            }
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(6500);

            point = MainScreen.FindImage(new Point(930, 330), new Point(980, 380), image, colorRange);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(5000);

            //LogWrite(String.Format("X:{0} Y:{1}", point.X, point.Y));

            //Teleport
        }

        private void btnBarbFish_Click(object sender, EventArgs e)
        {
            FishOn = !FishOn;
            if (FishOn)
            {
                LogWrite("Staring Barb Fish!");
                BarbFishTimer.Start();
                LogoutTimer.Start();
            }
            else
            {
                BarbFishTimer.Stop(); // workerSeersAgility.CancelAsync();
                LogWrite("Ending Barb Fish!");
            }
                
        }

        private void btnRC_Click(object sender, EventArgs e)
        {
            //PopulateRCClicks();
            //RuneCraft = !RuneCraft;
            //if (RuneCraft)
            //{
            //    LogWrite("Staring RC!");
            //    StartAutoClicker();
            //}
            //else
            //{
            //    LogWrite("Ending RC!");
            //    StopAutoClicker(); // workerSeersAgility.CancelAsync();
            //}
            var runParams = new RunParams<string>();
            runParams.ReportProgress = new Progress<string>(value => LogWrite(value));
            runParams.Timeouts = chkTimeOut.Checked;
            runParams.TimeoutLow = (int)minRandom.Value;
            runParams.TimeoutHigh = (int)maxRandom.Value;
            runParams.RunLimit = (int)numCount.Value;

            //var report = new Progress<string>(value => LogWrite(value));

            RuneCraft = !RuneCraft;
            if (RuneCraft)
            {
                LogWrite("Starting RC");
                //AgilityTimer.Start(); 
                workerRC.RunWorkerAsync(runParams);
            }
            else
            {
                LogWrite("Ending RC");
                //AgilityTimer.Stop();
                workerRC.CancelAsync();
            }


        }

        private void PopulateRCClicks()
        {
            Clicks.Clear();
            Clicks.Add(new Click(new Point(920, 785), 0, 1000));

            Clicks.Add(new Click(new Point(968, 785), 0, 1000));

            Clicks.Add(new Click(new Point(1018, 785), 1, 2000));

            Clicks.Add(new Click(new Point(977, 880), 0, 1000));

            //run / leave bak
            Clicks.Add(new Click(new Point(964, 960), 0, 1000));

            //eat
            Clicks.Add(new Click(new Point(1791, 761), 0, 2000));

            Clicks.Add(new Click(new Point(1837, 755), 0, 2000));

            //continue run
            Clicks.Add(new Click(new Point(964, 960), 0, 5000));

            Clicks.Add(new Click(new Point(964, 960), 0, 5000));

            Clicks.Add(new Click(new Point(1209, 960), 0, 5000));

            Clicks.Add(new Click(new Point(1529, 486), 0, 7000));

            Clicks.Add(new Click(new Point(1753, 620), 0, 11000));

            Clicks.Add(new Click(new Point(200, 518), 0, 10000));

            Clicks.Add(new Click(new Point(492, 190), 0, 11000));

            Clicks.Add(new Click(new Point(960, 197), 0, 6000));

            Clicks.Add(new Click(new Point(961, 246), 0, 6000));

            Clicks.Add(new Click(new Point(1034, 825), 0, 1000));
        }

        private void btnWC2_Click(object sender, EventArgs e)
        {
            WoodCutOn = !WoodCutOn;
            if (WoodCutOn)
            {
                LogWrite("Staring WoodCutOn!");
                WoodCutTimer2.Start();
            }
            else
            {
                WoodCutTimer2.Stop(); // workerSeersAgility.CancelAsync();
                LogWrite("Ending WoodCutOn!");
            }
        }

        private void btnCanAgi_Click(object sender, EventArgs e)
        {
            //var screenshot = MainScreen.CaptureScreen();
            agilOn = !agilOn;
            if (agilOn)
            {
                AgilityTimer.Start();
                workerCanifisAgility.RunWorkerAsync();
            }
            else
                AgilityTimer.Stop(); // workerSeersAgility.CancelAsync();
        }

        private void chkClicks_CheckedChanged(object sender, EventArgs e)
        {
            logClicks = chkClicks.Checked;
        }

        private void btnAutoShoot_Click(object sender, EventArgs e)
        {
            AutoShootOn = !AutoShootOn;
            if (AutoShootOn)
            {
                LogWrite("Staring AutoShoot!");
                AutoShootTimer.Start();
                LogoutTimer.Start();
            }
            else
            {
                AutoShootTimer.Stop(); // workerSeersAgility.CancelAsync();
                LogoutTimer.Stop();
                LogWrite("Ending AutoShoot!");
            }
        }

        public void Report(string value)
        {
            throw new NotImplementedException();
        }

        private async void workerRC_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            //var report = e.Argument as IProgress<string>;
            var timeoutCount = RandomGenerate.Next(runParams.TimeoutLow, runParams.TimeoutHigh);
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            if (runCount == 0)
                runCount = 99999;

            var eatFish = false;
            var useSprint = true;
            var sprintCount = 3;
            //var flipSprint = false;

            report.Report("Total Runs = " + runCount);
            Thread.Sleep(3000);
            while (!workerRC.CancellationPending && runCount > 0)
            {
                report.Report(string.Format("Sprint:{0}, Count:{1}", useSprint, sprintCount));
                await RunRC(report, useSprint, eatFish);
                runCount--;
                sprintCount--;
                if (sprintCount <= 0)
                {
                    if (ToggleRun(report, !useSprint))
                    {
                        useSprint = !useSprint;
                        sprintCount = useSprint ? 3 : 2;
                    }
                }
                if (runCount % 3 == 0)
                    eatFish = true;
                else
                    eatFish = false;

                if(runParams.Timeouts)
                {
                    timeoutCount--;
                    if(timeoutCount <= 0)
                    {
                        timeoutCount = RandomGenerate.Next(runParams.TimeoutLow, runParams.TimeoutHigh);
                        var timeoutnum = RandomGenerate.Next(20000, 90000);
                        report.Report("Pausing for : " + timeoutnum);
                        report.Report("New count limit : " + timeoutCount);
                        Thread.Sleep(timeoutnum);
                    }
                }
            }

            report.Report("Ending RC");
        }

        private Task RunRC(IProgress<string> report, bool useSprint, bool eatFish)
        {
            //await Task.Run(() => RunSeersCourse(report));
            //if(flipSprint)
            //    ToggleRun(report);

            //1713,154
            //1745,176

            RepairBags(report);

            RunRCCourse(report, useSprint, eatFish);

            FinishRC(report, useSprint);

            return Task.CompletedTask;
        }

        private bool ToggleRun(IProgress<string> report, bool runOn)
        {
            var offsetX = 0;
            var offsetY = 0;
            Point point;

            var fullEnergy = Properties.Resources._100Energy;

            var colorRange = 360m * (20 / 100m);

            

            point = new Point(1754, 161);
            offsetX = RandomGenerate.Next(-5, 5);
            offsetY = RandomGenerate.Next(-5, 5);
            point.X += offsetX;
            point.Y += offsetY;
            DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            Thread.Sleep(RandomGenerate.Next(800,1500));

            if (runOn)
            {
                point = MainScreen.FindImage(new Point(1713, 154), new Point(1745, 176), fullEnergy, colorRange);
                if (point.IsEmpty)
                    return false;

                var attackDot = Color.FromArgb(206, 168, 1);
                point = MainScreen.FindColorScreenRange(new Point(1746, 156), new Point(1763, 171), attackDot, 1, colorRange);
                if (point.IsEmpty)
                    return false;
            }
            else
            {
                var attackDot = Color.FromArgb(171, 172, 162);
                point = MainScreen.FindColorScreenRange(new Point(1746, 156), new Point(1763, 171), attackDot, 1, colorRange);
                if (point.IsEmpty)
                    return false;
            }

            return true;
            
        }

        private void RepairBags(IProgress<string> report)
        {
            var offsetX = 0;
            var offsetY = 0;
            var MaxX = Screen.PrimaryScreen.Bounds.Width;
            var MaxY = Screen.PrimaryScreen.Bounds.Height;
            var X1 = Screen.PrimaryScreen.Bounds.Width / 3;
            var Y1 = Screen.PrimaryScreen.Bounds.Height / 3;
            var X2 = (Screen.PrimaryScreen.Bounds.Width / 3) * 2;
            var Y2 = (Screen.PrimaryScreen.Bounds.Height / 3) * 2;
            var brokenBag = Properties.Resources.BrokenBag;
            var colorRange = 360m * (0 / 100m);
            Point point;

            point = MainScreen.FindImage(new Point(X2, Y2), new Point(MaxX, MaxY), brokenBag, colorRange);
            if (point.IsEmpty)
                return;

            //Open Spells
            Keyboard.Keyboard.Send(Keyboard.Keyboard.ScanCodeShort.F2);

            Thread.Sleep(RandomGenerate.Next(1000, 2000));

            //NPC Contact Spell
            point = new Point(1736, 773);
            offsetX = RandomGenerate.Next(-5, 5);
            offsetY = RandomGenerate.Next(-5, 5);
            point.X += offsetX;
            point.Y += offsetY;
            DoMouseClickAsync(report, 0, new Point(point.X, point.Y));

            Thread.Sleep(RandomGenerate.Next(1000, 1500));

            //Dark Mage
            point = new Point(841, 387);
            offsetX = RandomGenerate.Next(-10, 10);
            offsetY = RandomGenerate.Next(-10, 10);
            point.X += offsetX;
            point.Y += offsetY;
            DoMouseClickAsync(report, 0, new Point(point.X, point.Y));

            Thread.Sleep(RandomGenerate.Next(5000, 6500));

            Keyboard.Keyboard.Send(Keyboard.Keyboard.ScanCodeShort.SPACE);

            Thread.Sleep(RandomGenerate.Next(800, 1500));

            Keyboard.Keyboard.Send(Keyboard.Keyboard.ScanCodeShort.SPACE);

            Thread.Sleep(RandomGenerate.Next(800, 1500));

            Keyboard.Keyboard.Send(Keyboard.Keyboard.ScanCodeShort.SPACE);

            Thread.Sleep(RandomGenerate.Next(800, 1500));

            //Open Inv
            Keyboard.Keyboard.Send(Keyboard.Keyboard.ScanCodeShort.F1);

            Thread.Sleep(RandomGenerate.Next(1000, 2000));
        }

        private void RunRCCourse(IProgress<string> report, bool useSprint, bool eatFish)
        {
            var sprintMult = useSprint ? 0.5 : 1;
            var MaxX = Screen.PrimaryScreen.Bounds.Width;
            var MaxY = Screen.PrimaryScreen.Bounds.Height;
            var X1 = Screen.PrimaryScreen.Bounds.Width / 3;
            var Y1 = Screen.PrimaryScreen.Bounds.Height / 3;
            var X2 = (Screen.PrimaryScreen.Bounds.Width / 3) * 2;
            var Y2 = (Screen.PrimaryScreen.Bounds.Height / 3) * 2;
            var offsetX = 0;
            var offsetY = 0;
            var attackDot = Color.FromArgb(255, 0, 255);
            var StartCheck = Color.FromArgb(241, 198, 10);
            var bankCloseButton = Properties.Resources.BankCloseButton;

            var colorRange = 360m * (0 / 100m);

            Point point;

            //CHeck At STart
            point = MainScreen.FindColorScreen(new Point(0, 0), new Point(X1, Y1), StartCheck, 8);
            if (point.IsEmpty)
                return;

            //bank
            point = new Point(1023,426);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-6, 6);
                offsetY = RandomGenerate.Next(-6, 6);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(3000, 4000));

            //BankCheck
            point = MainScreen.FindImage(new Point(X1, 0), new Point(X2, Y1), bankCloseButton, colorRange);
            if (point.IsEmpty)
                return;

            //Deposit All 
            point = new Point(1035, 830);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-10, 10);
                offsetY = RandomGenerate.Next(-10, 10);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(1000, 2000));

            if (eatFish)
            {
                //Withdraw Fish
                point = new Point(726, 219);

                offsetX = RandomGenerate.Next(-8, 8);
                offsetY = RandomGenerate.Next(-8, 8);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 1, new Point(point.X, point.Y));
  
                Thread.Sleep(RandomGenerate.Next(500, 1200));

                offsetX = RandomGenerate.Next(-8, 8);
                offsetY = RandomGenerate.Next(37, 43);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));

                Thread.Sleep(RandomGenerate.Next(500, 1200));
            }

            //Withdraw All 
            point = new Point(680, 216);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-10, 10);
                offsetY = RandomGenerate.Next(-10, 10);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(1000, 2000));

            //Fill Bag 1 
            point = new Point(1750, 755);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-8, 8);
                offsetY = RandomGenerate.Next(-8, 8);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(500, 1200));

            //Fill Bag 2
            point = new Point(1792, 755);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-8, 8);
                offsetY = RandomGenerate.Next(-8, 8);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(500, 1200));

            //Fill Bag 3 
            point = new Point(1833, 755);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-8, 8);
                offsetY = RandomGenerate.Next(-8, 8);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(500, 1200));

            //Withdraw All 
            point = new Point(680, 216);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-10, 10);
                offsetY = RandomGenerate.Next(-10, 10);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(1000, 2000));

            Keyboard.Keyboard.Send(Keyboard.Keyboard.ScanCodeShort.ESCAPE);

            Thread.Sleep(RandomGenerate.Next(1500, 2000));

            if (eatFish)
            {
                //Eat Fish
                point = new Point(1878, 758);
                if (!point.IsEmpty)
                {
                    offsetX = RandomGenerate.Next(-8, 8);
                    offsetY = RandomGenerate.Next(-8, 8);
                    point.X += offsetX;
                    point.Y += offsetY;
                    DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
                }
                Thread.Sleep(RandomGenerate.Next(500, 1200));
            }

            //First Square  830,260 SOUTH
            //point = MainScreen.FindImage(new Point(600, 350), new Point(750, 500), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreen(new Point(X1, 100), new Point(X2, Y1), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-10, 20);
                offsetY = RandomGenerate.Next(-10, 20);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next((int)(9500 * sprintMult), (int)(10500 * sprintMult)));

            //2nd Square 845,197 SOUTH
            //point = MainScreen.FindImage(new Point(600, 350), new Point(750, 500), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreen(new Point(X1, 100), new Point(X2, Y1), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-10, 20);
                offsetY = RandomGenerate.Next(-10, 20);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next((int)(12000 * sprintMult), (int)(13000 * sprintMult)));

            //3rd Square 755,180 SOUTH
            //point = MainScreen.FindImage(new Point(600, 350), new Point(750, 500), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreen(new Point(X1, 0), new Point(X2, Y1), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-10, 20);
                offsetY = RandomGenerate.Next(-10, 20);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next((int)(13000 * sprintMult), (int)(14500 * sprintMult)));

            //4th Square 106,540  EAST
            //point = MainScreen.FindImage(new Point(600, 350), new Point(750, 500), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreen(new Point(0, Y1), new Point(X1, Y2), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(2, 20);
                offsetY = RandomGenerate.Next(-10, 20);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next((int)(15000 * sprintMult), (int)(16000 * sprintMult)));

            //Altar 106,540 
            //point = MainScreen.FindImage(new Point(600, 350), new Point(750, 500), image, colorRange);
            //DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            point = MainScreen.FindColorScreen(new Point(0, Y1), new Point(X1, Y2), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-10, 30);
                offsetY = RandomGenerate.Next(-10, 30);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;

            Thread.Sleep(RandomGenerate.Next((int)(14000 * sprintMult), (int)(16000 * sprintMult)));
            //Altar Casting
            Thread.Sleep(RandomGenerate.Next(2000, 3000));


            //Empty Bag 1 
            point = new Point(1750, 755);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-8, 8);
                offsetY = RandomGenerate.Next(-8, 8);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(500, 1200));

            //Empty Bag 2
            point = new Point(1792, 755);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-8, 8);
                offsetY = RandomGenerate.Next(-8, 8);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(500, 1200));

            //Altar 765,530
            point = MainScreen.FindColorScreen(new Point(X1, Y1), new Point(X2, Y2), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-10, 30);
                offsetY = RandomGenerate.Next(-10, 30);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(3000, 4000));

            //Empty Bag 3 
            point = new Point(1833, 755);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-8, 8);
                offsetY = RandomGenerate.Next(-8, 8);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(1000, 2000));

            //Altar 765,530
            point = MainScreen.FindColorScreen(new Point(X1, Y1), new Point(X2, Y2), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-10, 30);
                offsetY = RandomGenerate.Next(-10, 30);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(1000, 2000));
 
        }

        private void FinishRC(IProgress<string> report, bool useSprint)
        {
            var sprintMult = useSprint ? 0.5 : 1;
            var MaxX = Screen.PrimaryScreen.Bounds.Width;
            var MaxY = Screen.PrimaryScreen.Bounds.Height;
            var X1 = Screen.PrimaryScreen.Bounds.Width / 3;
            var Y1 = Screen.PrimaryScreen.Bounds.Height / 3;
            var X2 = (Screen.PrimaryScreen.Bounds.Width / 3) * 2;
            var Y2 = (Screen.PrimaryScreen.Bounds.Height / 3) * 2;
            var offsetX = 0;
            var offsetY = 0;
            var attackDot = Color.FromArgb(255, 0, 255);
            Point point;

            Thread.Sleep(RandomGenerate.Next(1000, 2000));

            //Open Magic
            //Keyboard.Keyboard.PressKey(Keys.F2);
            Keyboard.Keyboard.Send(Keyboard.Keyboard.ScanCodeShort.F2);


            Thread.Sleep(RandomGenerate.Next(1000, 2000));

            //Teleport
            point = new Point(1774, 798);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-5, 5);
                offsetY = RandomGenerate.Next(-5, 5);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(3500, 5000));

            //Tile Up Hill 1260,161
            point = MainScreen.FindColorScreen(new Point(X2, 0), new Point(MaxX, Y1), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(-5, 30);
                offsetY = RandomGenerate.Next(-5, 30);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;

            Thread.Sleep(RandomGenerate.Next(2000, 3000));

            //Open Inv
            Keyboard.Keyboard.Send(Keyboard.Keyboard.ScanCodeShort.F1);

            Thread.Sleep(RandomGenerate.Next((int)(17000 * sprintMult), (int)(19000 * sprintMult)));

            ////Altar Up Hill 838,420
            //point = MainScreen.FindColorScreenRange(new Point(X2, 0), new Point(MaxX, Y1), attackDot, 8, colorRange);
            //if (!point.IsEmpty)
            //{
            //    offsetX = RandomGenerate.Next(-5, 5);
            //    offsetY = RandomGenerate.Next(-5, 5);
            //    point.X += offsetX;
            //    point.Y += offsetY;
            //    DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            //}
            //else
            //    return;
            //Thread.Sleep(RandomGenerate.Next(19000, 20000));

            //Ladder Up Hill 935,425
            point = MainScreen.FindColorScreen(new Point(X1, Y1), new Point(X2, Y2), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(5, 15);
                offsetY = RandomGenerate.Next(8, 15);
                point.X += offsetX;
                point.Y += offsetY;
                DoMouseClickAsync(report, 0, new Point(point.X, point.Y));
            }
            else
                return;
            Thread.Sleep(RandomGenerate.Next(6000, 8000));
        }

        private void btnMine_Click(object sender, EventArgs e)
        {
            var runParams = new RunParams<string>();
            runParams.ReportProgress = new Progress<string>(value => LogWrite(value));
            runParams.Timeouts = chkTimeOut.Checked;
            runParams.TimeoutLow = (int)minRandom.Value;
            runParams.TimeoutHigh = (int)maxRandom.Value;
            runParams.RunLimit = (int)numCount.Value;

            //var report = new Progress<string>(value => LogWrite(value));

            RuneCraft = !RuneCraft;
            if (RuneCraft)
            {
                LogWrite("Starting Mining");
                workerMining.RunWorkerAsync(runParams);
            }
            else
            {
                LogWrite("Ending Mining");
                workerMining.CancelAsync();
            }
        }

        private void workerMining_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            //var report = e.Argument as IProgress<string>;
            var timeoutCount = RandomGenerate.Next(runParams.TimeoutLow, runParams.TimeoutHigh);
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            if (runCount == 0)
                runCount = 99999;

            report.Report("Total Runs = " + runCount);
            Thread.Sleep(3000);

            while (!workerMining.CancellationPending && runCount > 0)
            {
                //await Task.Run(() => CheckFullInv());
                //await Task.Run(() => MineRock());
                CheckFullInv();
                MineRock();
                runCount--;

                if (runParams.Timeouts)
                {
                    timeoutCount--;
                    if (timeoutCount <= 0)
                    {
                        timeoutCount = RandomGenerate.Next(runParams.TimeoutLow, runParams.TimeoutHigh);
                        var timeoutnum = RandomGenerate.Next(20000, 90000);
                        report.Report("Pausing for : " + timeoutnum);
                        report.Report("New count limit : " + timeoutCount);
                        Thread.Sleep(timeoutnum);
                    }
                }
            }

            report.Report("Ending Mining");
        }

        private void MineRock()
        {
            var attackDot = Color.FromArgb(255, 0, 255);
            int offsetX = 0;
            int offsetY = 0;
            Point point = MainScreen.FindColorScreen(new Point(670, 420), new Point(1200, 820), attackDot, 8);
            if (!point.IsEmpty)
            {
                offsetX = RandomGenerate.Next(0, 20);
                offsetY = RandomGenerate.Next(25, 50);
                point.X += offsetX;
                point.Y += offsetY;
                Mouse.Mouse.MoveTo(point.X, point.Y);

                Thread.Sleep(100);

                Mouse.Mouse.LeftClick();
            }

            Thread.Sleep(RandomGenerate.Next(2000, 3500));

            //return Task.CompletedTask;
        }

        private void CheckFullInv()
        {
            int offsetX = 0;
            int offsetY = 0;
            var colorRange = 360m * (10 / 100m);
            var image = Properties.Resources.EmptyInvNew;
            var point = MainScreen.FindImage(new Point(1850, 953), new Point(1906, 990), image, colorRange);
            if (point.IsEmpty)
            {
                Keyboard.Keyboard.HoldKey(Keys.LShiftKey);
                for(int i = 0; i < Inventory.Count; i++)
                {
                    point = Inventory[i];
                    offsetX = RandomGenerate.Next(-5, 5);
                    if(i % 4 == 0)
                        offsetY = RandomGenerate.Next(-5, 5);
                    point.X += offsetX;
                    point.Y += offsetY + RandomGenerate.Next(-2, 2);
                    Mouse.Mouse.MoveTo(point.X, point.Y);

                    Thread.Sleep(100);

                    Mouse.Mouse.LeftClick();

                    Thread.Sleep(RandomGenerate.Next(300, 800));

                }
                Keyboard.Keyboard.ReleaseKey(Keys.LShiftKey);
            }
            //return Task.CompletedTask;
        }

        private void btnSetAlch_Click(object sender, EventArgs e)
        {
            SettingAlchPoint = true;
            btnSetAlch.BackColor = Color.Red;
        }

        private void btnStartAlch_Click(object sender, EventArgs e)
        {
            var runParams = new RunParams<string>();
            runParams.ReportProgress = new Progress<string>(value => LogWrite(value));
            runParams.Timeouts = chkTimeOut.Checked;
            runParams.TimeoutLow = (int)minRandom.Value;
            runParams.TimeoutHigh = (int)maxRandom.Value;
            runParams.RunLimit = (int)numCount.Value;

            RuneCraft = !RuneCraft;
            if (RuneCraft)
            {
                LogWrite("Starting Alching");
                RunProgram = true;
                workerAlch.RunWorkerAsync(runParams);
            }
            else
            {
                RunProgram = false;
                LogWrite("Ending Alching");
                workerAlch.CancelAsync();
            }
        }

        private void workerAlch_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            var timeoutCount = RandomGenerate.Next(runParams.TimeoutLow, runParams.TimeoutHigh);
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            if (runCount == 0)
                runCount = 99999;

            var ClickPoint = AlchPoint;

            int sleep = 0;
            int tempSleep = 0;

            report.Report("Total Runs = " + runCount);
            Mouse.Mouse.MoveTo(ClickPoint.X, ClickPoint.Y);
            Thread.Sleep(3000);

            while (!workerAlch.CancellationPending && runCount > 0)
            {
                //DoMouseClickAsync(report, 0, ClickPoint);

                Mouse.Mouse.MoveTo(ClickPoint.X, ClickPoint.Y);
                Mouse.Mouse.LeftClick();
                runCount--;

                if (runParams.Timeouts)
                {
                    timeoutCount--;
                    if (timeoutCount <= 0)
                    {
                        timeoutCount = RandomGenerate.Next(runParams.TimeoutLow, runParams.TimeoutHigh);
                        var timeoutnum = RandomGenerate.Next(10000, 60000);
                        ClickPoint.X = AlchPoint.X + RandomGenerate.Next(-3, 3);
                        ClickPoint.Y = AlchPoint.Y + RandomGenerate.Next(-3, 3);
                        Mouse.Mouse.MoveTo(ClickPoint.X, ClickPoint.Y);
                        report.Report("Pausing for : " + timeoutnum);
                        report.Report("New count limit : " + timeoutCount);
                        Thread.Sleep(timeoutnum);
                    }
                }

                if(runCount % 2 == 0)
                {
                    var min = 3100 - tempSleep;
                    sleep = RandomGenerate.Next(min, min + 1000);
                }
                else
                {
                    sleep = RandomGenerate.Next(500, 2000);
                    tempSleep = sleep;
                }
                

                Thread.Sleep(sleep);
            }

            report.Report("Ending Alching");
        }
    }
}

