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
using System.Diagnostics.Eventing.Reader;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Remoting.Contexts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml.Serialization;

namespace AutoClicker 
{
    class MainForm : System.Windows.Forms.Form, IProgress<string>
    {
        private int delayTime = 1000;
        //private Tesseract ocr = new Tesseract();
        private System.ComponentModel.IContainer components = null;
        private Button buttonRecord;
        private Button btn_Start;
        private Label labelMousePosition;
        private TextBox textBox;
        private bool RunProgram;
        private int EatFailCount;
        private int EatFailCountMax = 10;
        private bool logClicks = true;
        private Color SearchColor;
        private Color SearchColor2;
        private Color MonsterHealth = Color.FromArgb(4, 136, 52);
        private Point TopLeft;
        private Point BottomRight;
        private Point InvTopLeft;
        private Point InvBottomRight;
        private int Absorb_offsetX;
        private int Absorb_offsetY;
        private decimal ColorRange;
        private decimal ImageRange;
        private int SelectedMonitor = 2;
        private int RandomTimeoutStart;
        private int RandomTimeoutEnd;
        private int PixelSkip = 10;
        private int TimeoutLengthMin;
        private int TimeoutLengthMax;
        private bool FindingColor;

        private int TotalInventoryClickCount;
        private int CurrentInventoryClickCount;
        private static string FilePath = "c:\\AppData\\AutoClicker\\Inventory.txt";
        private static string AppFolder = @"c:\AppData\AutoClicker\";
        private string OpenFile;
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
        private bool EndTimeoutsOnly;
        private bool DropInverse;
        private bool LogInfo;
        private bool RecordClicks;
        private bool NMZEnabled;
        private bool PickPocket;
        private bool Woodcutting;
        private bool SettingAlchPoint;
        private int RandomTimeoutCount;
        private int TimeoutPos;
        private int ClickOffset;
        private int DropClickPos;
        private int ClickCountPos;
        private int IterationCount;
        private TrackBar ActiveSlider;
        private Stopwatch ClickStopwatch;
        private BindingList<Click> Clicks;
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
        private System.Windows.Forms.Timer RuneCraftTimer;
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
        private Button btn_Find_Image;
        private Label lblColorRange;
        private BackgroundWorker workerSeersAgility;
        private BackgroundWorker workerCanifisAgility;
        private BackgroundWorker workerBarbFish;
        private CheckBox chkClicks;
        private BackgroundWorker worker_RC;
        private BackgroundWorker worker_Mining;
        private BackgroundWorker worker_Gem_Mining;
        private BackgroundWorker worker_Auto_Attack;
        private BackgroundWorker worker_Woodcut;
        private TrackBar sliderColorRange;
        private BackgroundWorker workerAlch;
        private CheckBox chk_End_Timeout_Only;
        private TabPage ClickTab;
        private TabPage Buttons;
        private Button btn_Find_Color;
        private GroupBox groupBox7;
        private Label label19;
        private Label label20;
        private Label label21;
        private TextBox txt_Color_B_2;
        private TextBox txt_Color_G_2;
        private TextBox txt_Color_R_2;
        private Label label18;
        private Button btn_Monitor_3;
        private Button btn_Monitor_2;
        private Button btn_Monitor_1;
        private Button btn_Gem_Mine;
        private GroupBox groupBox6;
        private TextBox txt_Inv_Bot_Y;
        private TextBox txt_Inv_Bot_X;
        private TextBox txt_Inv_Top_Y;
        private Label label15;
        private Label label17;
        private TextBox txt_Inv_Top_X;
        private GroupBox groupBox5;
        private TextBox txt_Screen_Bot_Y;
        private TextBox txt_Screen_Bot_X;
        private TextBox txt_Screen_Top_Y;
        private Label label14;
        private Label label16;
        private TextBox txt_Screen_Top_X;
        private GroupBox groupBox4;
        private Label label13;
        private Label label12;
        private Label label11;
        private TextBox txt_Color_B;
        private TextBox txt_Color_G;
        private TextBox txt_Color_R;
        private Button btnStartAlch;
        private Button btnSetAlch;
        private Button btn_Mine;
        private Button btn_Auto_Attack;
        private Button btnCanAgi;
        private Button btn_Woodcut;
        private Button btnRC;
        private Button btnBarbFish;
        private Button btnSeersAgil;
        private Button btnPickPocket;
        private Button btnNightmare;
        private TabControl TabController;
        private DataGridView dg_Clicks;
        private Button btn_Add_Click;
        private BackgroundWorker worker_Normal_Clicks;
        private Label label10;
        private TextBox txt_Pixel_Skip;
        private RadioButton radio_Long_Timeouts;
        private RadioButton radio_Short_Timeouts;
        private Label label25;
        private TextBox txt_Long_Timeout_Max;
        private TextBox txt_Long_Timeout_Min;
        private Label label24;
        private TextBox txt_Short_Timeout_Max;
        private TextBox txt_Short_Timeout_Min;
        private Label label23;
        private Label label22;
        private GroupBox groupBox8;
        private Label label26;
        private TextBox txt_Timeout_Cycle_Min;
        private TextBox txt_Timeout_Cycle_Max;
        private Label label9;
        private ToolStripMenuItem clicksToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private Button btn_Move_Click_Down;
        private Button btn_Move_Click_Up;
        private GroupBox groupBox9;
        private Label label30;
        private TextBox txt_Find_Color_A;
        private Label label27;
        private Label label28;
        private Label label29;
        private TextBox txt_Find_Color_B;
        private TextBox txt_Find_Color_G;
        private TextBox txt_Find_Color_R;
        private Label label32;
        private TextBox txt_Color_A_2;
        private Label label31;
        private TextBox txt_Color_A;
        private Point AlchPoint;

        Func<string, string> CurrentFunction;
        private GroupBox groupBox10;
        private Button btn_Copy_Color;
        private GroupBox groupBox11;
        private TrackBar slider_Image_Range;
        private Label lbl_Image_Range;
        private DataGridViewTextBoxColumn Click_Sequence;
        private DataGridViewTextBoxColumn Click_Name;
        private DataGridViewTextBoxColumn Click_Delay;
        private DataGridViewTextBoxColumn Click_Type;
        private DataGridViewTextBoxColumn Click_X;
        private DataGridViewTextBoxColumn Click_Y;
        private DataGridViewTextBoxColumn Click_Offset;
        private DataGridViewTextBoxColumn Click_Empty_Point;
        private DataGridViewTextBoxColumn Click_Color;
        private DataGridViewTextBoxColumn Click_Color_2;
        private DataGridViewTextBoxColumn Click_Image;
        private DataGridViewTextBoxColumn Click_Script;
        BackgroundWorker CurrentWorker;

        public MainForm()
        {
            try
            {
                RandomTimeoutStart = 5000;
                RandomTimeoutEnd = 30000;
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
                Clicks = new BindingList<Click>();
                Inventory = new List<Point>();
                RecordClicks = false;
                RunProgram = false;
                RandomGenerate = new Random();
                IterationCount = 0;
                InfiniteLoop = false;
                InitializeComponent();
                this.btn_Start.Click += new System.EventHandler((sender, e) => ButtonStart(sender, e, null));
                this.btn_Gem_Mine.Click += new System.EventHandler((sender, e) => ButtonStart(sender, e, worker_Gem_Mining));
                this.btn_Mine.Click += new System.EventHandler((sender, e) => ButtonStart(sender, e, worker_Mining));
                this.btn_Auto_Attack.Click += new System.EventHandler((sender, e) => ButtonStart(sender, e, worker_Auto_Attack));
                this.btn_Woodcut.Click += new System.EventHandler((sender, e) => ButtonStart(sender, e, worker_Woodcut));
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
                dg_Clicks.AutoGenerateColumns = false;
                dg_Clicks.DataSource = Clicks;
                dg_Clicks.Columns[0].DataPropertyName = "ClickSequence";
                dg_Clicks.Columns[1].DataPropertyName = "ClickName";
                dg_Clicks.Columns[2].DataPropertyName = "DelayAfterClick";
                dg_Clicks.Columns[3].DataPropertyName = "ClickType";
                dg_Clicks.Columns[4].DataPropertyName = "ClickPointX";
                dg_Clicks.Columns[5].DataPropertyName = "ClickPointY";
                dg_Clicks.Columns[6].DataPropertyName = "ClickOffset";
                dg_Clicks.Columns[7].DataPropertyName = "ClickEmptyPoint";
                dg_Clicks.Columns[8].DataPropertyName = "ClickColorText";
                dg_Clicks.Columns[9].DataPropertyName = "ClickColor2Text";
                dg_Clicks.Columns[10].DataPropertyName = "ClickImagePath";
                dg_Clicks.Columns[11].DataPropertyName = "ClickScript";
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
            this.btn_Start = new System.Windows.Forms.Button();
            this.buttonRecord = new System.Windows.Forms.Button();
            this.LogoutTimer = new System.Windows.Forms.Timer(this.components);
            this.ClickTimer = new System.Windows.Forms.Timer(this.components);
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
            this.RuneCraftTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackLabel1 = new System.Windows.Forms.Label();
            this.lblClickSeconds = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_Timeout_Cycle_Min = new System.Windows.Forms.TextBox();
            this.txt_Timeout_Cycle_Max = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.radio_Short_Timeouts = new System.Windows.Forms.RadioButton();
            this.label23 = new System.Windows.Forms.Label();
            this.chkTimeOut = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.radio_Long_Timeouts = new System.Windows.Forms.RadioButton();
            this.label25 = new System.Windows.Forms.Label();
            this.chk_End_Timeout_Only = new System.Windows.Forms.CheckBox();
            this.txt_Long_Timeout_Max = new System.Windows.Forms.TextBox();
            this.txt_Short_Timeout_Min = new System.Windows.Forms.TextBox();
            this.txt_Long_Timeout_Min = new System.Windows.Forms.TextBox();
            this.txt_Short_Timeout_Max = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblClickOffsetNumber = new System.Windows.Forms.Label();
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
            this.label10 = new System.Windows.Forms.Label();
            this.txt_Pixel_Skip = new System.Windows.Forms.TextBox();
            this.lblColorRange = new System.Windows.Forms.Label();
            this.sliderColorRange = new System.Windows.Forms.TrackBar();
            this.btn_Find_Image = new System.Windows.Forms.Button();
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
            this.clicksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workerSeersAgility = new System.ComponentModel.BackgroundWorker();
            this.workerCanifisAgility = new System.ComponentModel.BackgroundWorker();
            this.workerBarbFish = new System.ComponentModel.BackgroundWorker();
            this.chkClicks = new System.Windows.Forms.CheckBox();
            this.worker_RC = new System.ComponentModel.BackgroundWorker();
            this.worker_Mining = new System.ComponentModel.BackgroundWorker();
            this.worker_Gem_Mining = new System.ComponentModel.BackgroundWorker();
            this.worker_Auto_Attack = new System.ComponentModel.BackgroundWorker();
            this.worker_Woodcut = new System.ComponentModel.BackgroundWorker();
            this.workerAlch = new System.ComponentModel.BackgroundWorker();
            this.ClickTab = new System.Windows.Forms.TabPage();
            this.btn_Move_Click_Down = new System.Windows.Forms.Button();
            this.btn_Move_Click_Up = new System.Windows.Forms.Button();
            this.btn_Add_Click = new System.Windows.Forms.Button();
            this.dg_Clicks = new System.Windows.Forms.DataGridView();
            this.Buttons = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.slider_Image_Range = new System.Windows.Forms.TrackBar();
            this.lbl_Image_Range = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txt_Color_A_2 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_Color_B_2 = new System.Windows.Forms.TextBox();
            this.txt_Color_G_2 = new System.Windows.Forms.TextBox();
            this.txt_Color_R_2 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btn_Monitor_3 = new System.Windows.Forms.Button();
            this.btn_Monitor_2 = new System.Windows.Forms.Button();
            this.btn_Monitor_1 = new System.Windows.Forms.Button();
            this.btn_Gem_Mine = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txt_Inv_Bot_Y = new System.Windows.Forms.TextBox();
            this.txt_Inv_Bot_X = new System.Windows.Forms.TextBox();
            this.txt_Inv_Top_Y = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_Inv_Top_X = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txt_Screen_Bot_Y = new System.Windows.Forms.TextBox();
            this.txt_Screen_Bot_X = new System.Windows.Forms.TextBox();
            this.txt_Screen_Top_Y = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txt_Screen_Top_X = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txt_Color_A = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Color_B = new System.Windows.Forms.TextBox();
            this.txt_Color_G = new System.Windows.Forms.TextBox();
            this.txt_Color_R = new System.Windows.Forms.TextBox();
            this.btnStartAlch = new System.Windows.Forms.Button();
            this.btnSetAlch = new System.Windows.Forms.Button();
            this.btn_Mine = new System.Windows.Forms.Button();
            this.btn_Auto_Attack = new System.Windows.Forms.Button();
            this.btnCanAgi = new System.Windows.Forms.Button();
            this.btn_Woodcut = new System.Windows.Forms.Button();
            this.btnRC = new System.Windows.Forms.Button();
            this.btnBarbFish = new System.Windows.Forms.Button();
            this.btnSeersAgil = new System.Windows.Forms.Button();
            this.btnPickPocket = new System.Windows.Forms.Button();
            this.btnNightmare = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btn_Copy_Color = new System.Windows.Forms.Button();
            this.label30 = new System.Windows.Forms.Label();
            this.txt_Find_Color_A = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.btn_Find_Color = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_Find_Color_B = new System.Windows.Forms.TextBox();
            this.txt_Find_Color_G = new System.Windows.Forms.TextBox();
            this.txt_Find_Color_R = new System.Windows.Forms.TextBox();
            this.TabController = new System.Windows.Forms.TabControl();
            this.worker_Normal_Clicks = new System.ComponentModel.BackgroundWorker();
            this.Click_Sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Delay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Offset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Empty_Point = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Color_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Image = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Click_Script = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClickOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderCycles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClicks)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInventoryCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderColorRange)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.ClickTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_Clicks)).BeginInit();
            this.Buttons.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slider_Image_Range)).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.TabController.SuspendLayout();
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
            this.textBox.Size = new System.Drawing.Size(310, 654);
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
            // btn_Start
            // 
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Start.Location = new System.Drawing.Point(453, 672);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "Start";
            // 
            // buttonRecord
            // 
            this.buttonRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRecord.Location = new System.Drawing.Point(327, 672);
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
            this.label1.Size = new System.Drawing.Size(185, 16);
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
            this.lblClickSeconds.Size = new System.Drawing.Size(14, 16);
            this.lblClickSeconds.TabIndex = 10;
            this.lblClickSeconds.Text = "1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox8);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(320, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 642);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label26);
            this.groupBox8.Controls.Add(this.txt_Timeout_Cycle_Min);
            this.groupBox8.Controls.Add(this.txt_Timeout_Cycle_Max);
            this.groupBox8.Controls.Add(this.label9);
            this.groupBox8.Controls.Add(this.radio_Short_Timeouts);
            this.groupBox8.Controls.Add(this.label23);
            this.groupBox8.Controls.Add(this.chkTimeOut);
            this.groupBox8.Controls.Add(this.label22);
            this.groupBox8.Controls.Add(this.radio_Long_Timeouts);
            this.groupBox8.Controls.Add(this.label25);
            this.groupBox8.Controls.Add(this.chk_End_Timeout_Only);
            this.groupBox8.Controls.Add(this.txt_Long_Timeout_Max);
            this.groupBox8.Controls.Add(this.txt_Short_Timeout_Min);
            this.groupBox8.Controls.Add(this.txt_Long_Timeout_Min);
            this.groupBox8.Controls.Add(this.txt_Short_Timeout_Max);
            this.groupBox8.Controls.Add(this.label24);
            this.groupBox8.Location = new System.Drawing.Point(7, 408);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(382, 228);
            this.groupBox8.TabIndex = 42;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Timeouts";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(52, 41);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(84, 13);
            this.label26.TabIndex = 45;
            this.label26.Text = "Every # of Cyles";
            // 
            // txt_Timeout_Cycle_Min
            // 
            this.txt_Timeout_Cycle_Min.Location = new System.Drawing.Point(144, 38);
            this.txt_Timeout_Cycle_Min.MaxLength = 6;
            this.txt_Timeout_Cycle_Min.Name = "txt_Timeout_Cycle_Min";
            this.txt_Timeout_Cycle_Min.Size = new System.Drawing.Size(49, 20);
            this.txt_Timeout_Cycle_Min.TabIndex = 42;
            this.txt_Timeout_Cycle_Min.Text = "0";
            this.txt_Timeout_Cycle_Min.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Timeout_Cycle_Max
            // 
            this.txt_Timeout_Cycle_Max.Location = new System.Drawing.Point(227, 38);
            this.txt_Timeout_Cycle_Max.MaxLength = 6;
            this.txt_Timeout_Cycle_Max.Name = "txt_Timeout_Cycle_Max";
            this.txt_Timeout_Cycle_Max.Size = new System.Drawing.Size(49, 20);
            this.txt_Timeout_Cycle_Max.TabIndex = 43;
            this.txt_Timeout_Cycle_Max.Text = "0";
            this.txt_Timeout_Cycle_Max.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(199, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "TO";
            // 
            // radio_Short_Timeouts
            // 
            this.radio_Short_Timeouts.AutoSize = true;
            this.radio_Short_Timeouts.Checked = true;
            this.radio_Short_Timeouts.Location = new System.Drawing.Point(14, 69);
            this.radio_Short_Timeouts.Name = "radio_Short_Timeouts";
            this.radio_Short_Timeouts.Size = new System.Drawing.Size(124, 17);
            this.radio_Short_Timeouts.TabIndex = 38;
            this.radio_Short_Timeouts.TabStop = true;
            this.radio_Short_Timeouts.Text = "Short Timeouts ( ms )";
            this.radio_Short_Timeouts.UseVisualStyleBackColor = true;
            this.radio_Short_Timeouts.CheckedChanged += new System.EventHandler(this.radio_Short_Timeouts_CheckedChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(238, 22);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(30, 13);
            this.label23.TabIndex = 41;
            this.label23.Text = "MAX";
            // 
            // chkTimeOut
            // 
            this.chkTimeOut.AutoSize = true;
            this.chkTimeOut.Location = new System.Drawing.Point(14, 19);
            this.chkTimeOut.Name = "chkTimeOut";
            this.chkTimeOut.Size = new System.Drawing.Size(125, 17);
            this.chkTimeOut.TabIndex = 14;
            this.chkTimeOut.Text = "Use random timeouts";
            this.chkTimeOut.UseVisualStyleBackColor = true;
            this.chkTimeOut.CheckedChanged += new System.EventHandler(this.chkTimeOut_CheckedChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(152, 22);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(27, 13);
            this.label22.TabIndex = 40;
            this.label22.Text = "MIN";
            // 
            // radio_Long_Timeouts
            // 
            this.radio_Long_Timeouts.AutoSize = true;
            this.radio_Long_Timeouts.Location = new System.Drawing.Point(13, 99);
            this.radio_Long_Timeouts.Name = "radio_Long_Timeouts";
            this.radio_Long_Timeouts.Size = new System.Drawing.Size(123, 17);
            this.radio_Long_Timeouts.TabIndex = 39;
            this.radio_Long_Timeouts.Text = "Long Timeouts ( ms )";
            this.radio_Long_Timeouts.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(199, 99);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(22, 13);
            this.label25.TabIndex = 37;
            this.label25.Text = "TO";
            // 
            // chk_End_Timeout_Only
            // 
            this.chk_End_Timeout_Only.AutoSize = true;
            this.chk_End_Timeout_Only.Checked = true;
            this.chk_End_Timeout_Only.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_End_Timeout_Only.Location = new System.Drawing.Point(14, 128);
            this.chk_End_Timeout_Only.Name = "chk_End_Timeout_Only";
            this.chk_End_Timeout_Only.Size = new System.Drawing.Size(147, 17);
            this.chk_End_Timeout_Only.TabIndex = 27;
            this.chk_End_Timeout_Only.Text = "Timeout end of cycle only";
            this.chk_End_Timeout_Only.UseVisualStyleBackColor = true;
            this.chk_End_Timeout_Only.CheckedChanged += new System.EventHandler(this.chk_End_Timeout_Only_CheckedChanged);
            // 
            // txt_Long_Timeout_Max
            // 
            this.txt_Long_Timeout_Max.Location = new System.Drawing.Point(227, 96);
            this.txt_Long_Timeout_Max.MaxLength = 6;
            this.txt_Long_Timeout_Max.Name = "txt_Long_Timeout_Max";
            this.txt_Long_Timeout_Max.Size = new System.Drawing.Size(49, 20);
            this.txt_Long_Timeout_Max.TabIndex = 36;
            this.txt_Long_Timeout_Max.Text = "240000";
            // 
            // txt_Short_Timeout_Min
            // 
            this.txt_Short_Timeout_Min.Location = new System.Drawing.Point(144, 67);
            this.txt_Short_Timeout_Min.MaxLength = 6;
            this.txt_Short_Timeout_Min.Name = "txt_Short_Timeout_Min";
            this.txt_Short_Timeout_Min.Size = new System.Drawing.Size(49, 20);
            this.txt_Short_Timeout_Min.TabIndex = 11;
            this.txt_Short_Timeout_Min.Text = "5000";
            // 
            // txt_Long_Timeout_Min
            // 
            this.txt_Long_Timeout_Min.Location = new System.Drawing.Point(144, 96);
            this.txt_Long_Timeout_Min.MaxLength = 6;
            this.txt_Long_Timeout_Min.Name = "txt_Long_Timeout_Min";
            this.txt_Long_Timeout_Min.Size = new System.Drawing.Size(49, 20);
            this.txt_Long_Timeout_Min.TabIndex = 35;
            this.txt_Long_Timeout_Min.Text = "30000";
            // 
            // txt_Short_Timeout_Max
            // 
            this.txt_Short_Timeout_Max.Location = new System.Drawing.Point(227, 67);
            this.txt_Short_Timeout_Max.MaxLength = 6;
            this.txt_Short_Timeout_Max.Name = "txt_Short_Timeout_Max";
            this.txt_Short_Timeout_Max.Size = new System.Drawing.Size(49, 20);
            this.txt_Short_Timeout_Max.TabIndex = 30;
            this.txt_Short_Timeout_Max.Text = "30000";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(199, 70);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(22, 13);
            this.label24.TabIndex = 34;
            this.label24.Text = "TO";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblClickOffsetNumber);
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
            this.groupBox3.Size = new System.Drawing.Size(383, 282);
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
            this.lblClickOffsetNumber.Size = new System.Drawing.Size(14, 16);
            this.lblClickOffsetNumber.TabIndex = 23;
            this.lblClickOffsetNumber.Text = "3";
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
            this.lblClickOffset.Size = new System.Drawing.Size(85, 16);
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
            this.radioCycles.Size = new System.Drawing.Size(171, 20);
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
            this.radioClicks.Size = new System.Drawing.Size(166, 20);
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
            this.lblCycleSeconds.Size = new System.Drawing.Size(14, 16);
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
            this.label4.Size = new System.Drawing.Size(155, 16);
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(227, 456);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Pixel Skip";
            // 
            // txt_Pixel_Skip
            // 
            this.txt_Pixel_Skip.Location = new System.Drawing.Point(231, 472);
            this.txt_Pixel_Skip.MaxLength = 3;
            this.txt_Pixel_Skip.Name = "txt_Pixel_Skip";
            this.txt_Pixel_Skip.Size = new System.Drawing.Size(39, 20);
            this.txt_Pixel_Skip.TabIndex = 20;
            this.txt_Pixel_Skip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // lblColorRange
            // 
            this.lblColorRange.AutoSize = true;
            this.lblColorRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorRange.Location = new System.Drawing.Point(196, 28);
            this.lblColorRange.Name = "lblColorRange";
            this.lblColorRange.Size = new System.Drawing.Size(44, 20);
            this.lblColorRange.TabIndex = 19;
            this.lblColorRange.Text = "95%";
            // 
            // sliderColorRange
            // 
            this.sliderColorRange.Location = new System.Drawing.Point(25, 28);
            this.sliderColorRange.Maximum = 50;
            this.sliderColorRange.Name = "sliderColorRange";
            this.sliderColorRange.Size = new System.Drawing.Size(166, 45);
            this.sliderColorRange.SmallChange = 5;
            this.sliderColorRange.TabIndex = 18;
            this.sliderColorRange.TickFrequency = 5;
            this.sliderColorRange.Value = 5;
            this.sliderColorRange.Scroll += new System.EventHandler(this.sliderColorRange_Scroll);
            // 
            // btn_Find_Image
            // 
            this.btn_Find_Image.Location = new System.Drawing.Point(124, 349);
            this.btn_Find_Image.Name = "btn_Find_Image";
            this.btn_Find_Image.Size = new System.Drawing.Size(75, 23);
            this.btn_Find_Image.TabIndex = 17;
            this.btn_Find_Image.Text = "Find Image";
            this.btn_Find_Image.UseVisualStyleBackColor = true;
            this.btn_Find_Image.Click += new System.EventHandler(this.btn_Find_Image_Click);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(550, 672);
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
            this.fileToolStripMenuItem,
            this.clicksToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1346, 24);
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
            // clicksToolStripMenuItem
            // 
            this.clicksToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.clicksToolStripMenuItem.Name = "clicksToolStripMenuItem";
            this.clicksToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.clicksToolStripMenuItem.Text = "Clicks";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
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
            // worker_RC
            // 
            this.worker_RC.WorkerReportsProgress = true;
            this.worker_RC.WorkerSupportsCancellation = true;
            this.worker_RC.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_RC_DoWork);
            // 
            // worker_Mining
            // 
            this.worker_Mining.WorkerReportsProgress = true;
            this.worker_Mining.WorkerSupportsCancellation = true;
            this.worker_Mining.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_Mining_DoWork);
            // 
            // worker_Gem_Mining
            // 
            this.worker_Gem_Mining.WorkerReportsProgress = true;
            this.worker_Gem_Mining.WorkerSupportsCancellation = true;
            this.worker_Gem_Mining.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_Gem_Mining_DoWork);
            // 
            // worker_Auto_Attack
            // 
            this.worker_Auto_Attack.WorkerReportsProgress = true;
            this.worker_Auto_Attack.WorkerSupportsCancellation = true;
            this.worker_Auto_Attack.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_Auto_Attack_DoWork);
            // 
            // workerAlch
            // 
            this.workerAlch.WorkerReportsProgress = true;
            this.workerAlch.WorkerSupportsCancellation = true;
            this.workerAlch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerAlch_DoWork);
            // 
            // ClickTab
            // 
            this.ClickTab.Controls.Add(this.btn_Move_Click_Down);
            this.ClickTab.Controls.Add(this.btn_Move_Click_Up);
            this.ClickTab.Controls.Add(this.btn_Add_Click);
            this.ClickTab.Controls.Add(this.dg_Clicks);
            this.ClickTab.Location = new System.Drawing.Point(4, 22);
            this.ClickTab.Name = "ClickTab";
            this.ClickTab.Padding = new System.Windows.Forms.Padding(3);
            this.ClickTab.Size = new System.Drawing.Size(605, 523);
            this.ClickTab.TabIndex = 3;
            this.ClickTab.Text = "Clicks";
            this.ClickTab.UseVisualStyleBackColor = true;
            // 
            // btn_Move_Click_Down
            // 
            this.btn_Move_Click_Down.Location = new System.Drawing.Point(270, 481);
            this.btn_Move_Click_Down.Name = "btn_Move_Click_Down";
            this.btn_Move_Click_Down.Size = new System.Drawing.Size(38, 23);
            this.btn_Move_Click_Down.TabIndex = 3;
            this.btn_Move_Click_Down.Text = "↓";
            this.btn_Move_Click_Down.UseVisualStyleBackColor = true;
            this.btn_Move_Click_Down.Click += new System.EventHandler(this.btn_Move_Click_Down_Click);
            // 
            // btn_Move_Click_Up
            // 
            this.btn_Move_Click_Up.Location = new System.Drawing.Point(226, 481);
            this.btn_Move_Click_Up.Name = "btn_Move_Click_Up";
            this.btn_Move_Click_Up.Size = new System.Drawing.Size(38, 23);
            this.btn_Move_Click_Up.TabIndex = 2;
            this.btn_Move_Click_Up.Text = "↑";
            this.btn_Move_Click_Up.UseVisualStyleBackColor = true;
            this.btn_Move_Click_Up.Click += new System.EventHandler(this.btn_Move_Click_Up_Click);
            // 
            // btn_Add_Click
            // 
            this.btn_Add_Click.Location = new System.Drawing.Point(17, 481);
            this.btn_Add_Click.Name = "btn_Add_Click";
            this.btn_Add_Click.Size = new System.Drawing.Size(75, 23);
            this.btn_Add_Click.TabIndex = 1;
            this.btn_Add_Click.Text = "Add";
            this.btn_Add_Click.UseVisualStyleBackColor = true;
            this.btn_Add_Click.Click += new System.EventHandler(this.btn_Add_Click_Click);
            // 
            // dg_Clicks
            // 
            this.dg_Clicks.AllowUserToResizeRows = false;
            this.dg_Clicks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_Clicks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Click_Sequence,
            this.Click_Name,
            this.Click_Delay,
            this.Click_Type,
            this.Click_X,
            this.Click_Y,
            this.Click_Offset,
            this.Click_Empty_Point,
            this.Click_Color,
            this.Click_Color_2,
            this.Click_Image,
            this.Click_Script});
            this.dg_Clicks.Location = new System.Drawing.Point(3, 6);
            this.dg_Clicks.MultiSelect = false;
            this.dg_Clicks.Name = "dg_Clicks";
            this.dg_Clicks.RowHeadersWidth = 25;
            this.dg_Clicks.Size = new System.Drawing.Size(594, 469);
            this.dg_Clicks.TabIndex = 0;
            // 
            // Buttons
            // 
            this.Buttons.Controls.Add(this.groupBox11);
            this.Buttons.Controls.Add(this.groupBox10);
            this.Buttons.Controls.Add(this.groupBox7);
            this.Buttons.Controls.Add(this.label18);
            this.Buttons.Controls.Add(this.label10);
            this.Buttons.Controls.Add(this.btn_Monitor_3);
            this.Buttons.Controls.Add(this.btn_Monitor_2);
            this.Buttons.Controls.Add(this.txt_Pixel_Skip);
            this.Buttons.Controls.Add(this.btn_Monitor_1);
            this.Buttons.Controls.Add(this.btn_Gem_Mine);
            this.Buttons.Controls.Add(this.groupBox6);
            this.Buttons.Controls.Add(this.groupBox5);
            this.Buttons.Controls.Add(this.groupBox4);
            this.Buttons.Controls.Add(this.btn_Find_Image);
            this.Buttons.Controls.Add(this.btnStartAlch);
            this.Buttons.Controls.Add(this.btnSetAlch);
            this.Buttons.Controls.Add(this.btn_Mine);
            this.Buttons.Controls.Add(this.btn_Auto_Attack);
            this.Buttons.Controls.Add(this.btnCanAgi);
            this.Buttons.Controls.Add(this.btn_Woodcut);
            this.Buttons.Controls.Add(this.btnRC);
            this.Buttons.Controls.Add(this.btnBarbFish);
            this.Buttons.Controls.Add(this.btnSeersAgil);
            this.Buttons.Controls.Add(this.btnPickPocket);
            this.Buttons.Controls.Add(this.btnNightmare);
            this.Buttons.Location = new System.Drawing.Point(4, 22);
            this.Buttons.Name = "Buttons";
            this.Buttons.Size = new System.Drawing.Size(605, 523);
            this.Buttons.TabIndex = 2;
            this.Buttons.Text = "Buttons";
            this.Buttons.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.slider_Image_Range);
            this.groupBox11.Controls.Add(this.lbl_Image_Range);
            this.groupBox11.Location = new System.Drawing.Point(310, 283);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(249, 112);
            this.groupBox11.TabIndex = 38;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Image Match";
            // 
            // slider_Image_Range
            // 
            this.slider_Image_Range.Location = new System.Drawing.Point(25, 28);
            this.slider_Image_Range.Maximum = 50;
            this.slider_Image_Range.Name = "slider_Image_Range";
            this.slider_Image_Range.Size = new System.Drawing.Size(166, 45);
            this.slider_Image_Range.SmallChange = 5;
            this.slider_Image_Range.TabIndex = 18;
            this.slider_Image_Range.TickFrequency = 5;
            this.slider_Image_Range.Value = 5;
            this.slider_Image_Range.Scroll += new System.EventHandler(this.slider_Image_Range_Scroll);
            // 
            // lbl_Image_Range
            // 
            this.lbl_Image_Range.AutoSize = true;
            this.lbl_Image_Range.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Image_Range.Location = new System.Drawing.Point(196, 28);
            this.lbl_Image_Range.Name = "lbl_Image_Range";
            this.lbl_Image_Range.Size = new System.Drawing.Size(44, 20);
            this.lbl_Image_Range.TabIndex = 19;
            this.lbl_Image_Range.Text = "95%";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.sliderColorRange);
            this.groupBox10.Controls.Add(this.lblColorRange);
            this.groupBox10.Location = new System.Drawing.Point(310, 401);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(249, 112);
            this.groupBox10.TabIndex = 32;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Color Match";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Controls.Add(this.label19);
            this.groupBox7.Controls.Add(this.txt_Color_A_2);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Controls.Add(this.label21);
            this.groupBox7.Controls.Add(this.txt_Color_B_2);
            this.groupBox7.Controls.Add(this.txt_Color_G_2);
            this.groupBox7.Controls.Add(this.txt_Color_R_2);
            this.groupBox7.Location = new System.Drawing.Point(200, 94);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(189, 73);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Secondary Color";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(27, 23);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(14, 13);
            this.label32.TabIndex = 9;
            this.label32.Text = "A";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(146, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(14, 13);
            this.label19.TabIndex = 5;
            this.label19.Text = "B";
            // 
            // txt_Color_A_2
            // 
            this.txt_Color_A_2.Location = new System.Drawing.Point(16, 40);
            this.txt_Color_A_2.MaxLength = 3;
            this.txt_Color_A_2.Name = "txt_Color_A_2";
            this.txt_Color_A_2.Size = new System.Drawing.Size(35, 20);
            this.txt_Color_A_2.TabIndex = 8;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(107, 24);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(15, 13);
            this.label20.TabIndex = 4;
            this.label20.Text = "G";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(65, 24);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "R";
            // 
            // txt_Color_B_2
            // 
            this.txt_Color_B_2.Location = new System.Drawing.Point(138, 40);
            this.txt_Color_B_2.MaxLength = 3;
            this.txt_Color_B_2.Name = "txt_Color_B_2";
            this.txt_Color_B_2.Size = new System.Drawing.Size(35, 20);
            this.txt_Color_B_2.TabIndex = 2;
            this.txt_Color_B_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Color_G_2
            // 
            this.txt_Color_G_2.Location = new System.Drawing.Point(98, 40);
            this.txt_Color_G_2.MaxLength = 3;
            this.txt_Color_G_2.Name = "txt_Color_G_2";
            this.txt_Color_G_2.Size = new System.Drawing.Size(35, 20);
            this.txt_Color_G_2.TabIndex = 1;
            this.txt_Color_G_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Color_R_2
            // 
            this.txt_Color_R_2.Location = new System.Drawing.Point(58, 40);
            this.txt_Color_R_2.MaxLength = 3;
            this.txt_Color_R_2.Name = "txt_Color_R_2";
            this.txt_Color_R_2.Size = new System.Drawing.Size(35, 20);
            this.txt_Color_R_2.TabIndex = 0;
            this.txt_Color_R_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(55, 411);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 13);
            this.label18.TabIndex = 37;
            this.label18.Text = "Monitor";
            // 
            // btn_Monitor_3
            // 
            this.btn_Monitor_3.Location = new System.Drawing.Point(102, 434);
            this.btn_Monitor_3.Name = "btn_Monitor_3";
            this.btn_Monitor_3.Size = new System.Drawing.Size(37, 23);
            this.btn_Monitor_3.TabIndex = 36;
            this.btn_Monitor_3.Text = "3";
            this.btn_Monitor_3.UseVisualStyleBackColor = true;
            this.btn_Monitor_3.Click += new System.EventHandler(this.btn_Monitor_3_Click);
            // 
            // btn_Monitor_2
            // 
            this.btn_Monitor_2.Location = new System.Drawing.Point(59, 434);
            this.btn_Monitor_2.Name = "btn_Monitor_2";
            this.btn_Monitor_2.Size = new System.Drawing.Size(37, 23);
            this.btn_Monitor_2.TabIndex = 35;
            this.btn_Monitor_2.Text = "2";
            this.btn_Monitor_2.UseVisualStyleBackColor = true;
            this.btn_Monitor_2.Click += new System.EventHandler(this.btn_Monitor_2_Click);
            // 
            // btn_Monitor_1
            // 
            this.btn_Monitor_1.Location = new System.Drawing.Point(16, 434);
            this.btn_Monitor_1.Name = "btn_Monitor_1";
            this.btn_Monitor_1.Size = new System.Drawing.Size(37, 23);
            this.btn_Monitor_1.TabIndex = 34;
            this.btn_Monitor_1.Text = "1";
            this.btn_Monitor_1.UseVisualStyleBackColor = true;
            this.btn_Monitor_1.Click += new System.EventHandler(this.btn_Monitor_1_Click);
            // 
            // btn_Gem_Mine
            // 
            this.btn_Gem_Mine.Location = new System.Drawing.Point(16, 268);
            this.btn_Gem_Mine.Name = "btn_Gem_Mine";
            this.btn_Gem_Mine.Size = new System.Drawing.Size(75, 23);
            this.btn_Gem_Mine.TabIndex = 33;
            this.btn_Gem_Mine.Text = "Gem Mine";
            this.btn_Gem_Mine.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txt_Inv_Bot_Y);
            this.groupBox6.Controls.Add(this.txt_Inv_Bot_X);
            this.groupBox6.Controls.Add(this.txt_Inv_Top_Y);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.txt_Inv_Top_X);
            this.groupBox6.Location = new System.Drawing.Point(395, 137);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(189, 115);
            this.groupBox6.TabIndex = 32;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Inventory Coords";
            // 
            // txt_Inv_Bot_Y
            // 
            this.txt_Inv_Bot_Y.Location = new System.Drawing.Point(72, 89);
            this.txt_Inv_Bot_Y.MaxLength = 5;
            this.txt_Inv_Bot_Y.Name = "txt_Inv_Bot_Y";
            this.txt_Inv_Bot_Y.Size = new System.Drawing.Size(49, 20);
            this.txt_Inv_Bot_Y.TabIndex = 10;
            this.txt_Inv_Bot_Y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Inv_Bot_X
            // 
            this.txt_Inv_Bot_X.Location = new System.Drawing.Point(16, 89);
            this.txt_Inv_Bot_X.MaxLength = 5;
            this.txt_Inv_Bot_X.Name = "txt_Inv_Bot_X";
            this.txt_Inv_Bot_X.Size = new System.Drawing.Size(49, 20);
            this.txt_Inv_Bot_X.TabIndex = 9;
            this.txt_Inv_Bot_X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Inv_Top_Y
            // 
            this.txt_Inv_Top_Y.Location = new System.Drawing.Point(72, 40);
            this.txt_Inv_Top_Y.MaxLength = 5;
            this.txt_Inv_Top_Y.Name = "txt_Inv_Top_Y";
            this.txt_Inv_Top_Y.Size = new System.Drawing.Size(49, 20);
            this.txt_Inv_Top_Y.TabIndex = 8;
            this.txt_Inv_Top_Y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Bot Right X + Y";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 24);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Top Left X + Y";
            // 
            // txt_Inv_Top_X
            // 
            this.txt_Inv_Top_X.Location = new System.Drawing.Point(16, 40);
            this.txt_Inv_Top_X.MaxLength = 5;
            this.txt_Inv_Top_X.Name = "txt_Inv_Top_X";
            this.txt_Inv_Top_X.Size = new System.Drawing.Size(49, 20);
            this.txt_Inv_Top_X.TabIndex = 0;
            this.txt_Inv_Top_X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txt_Screen_Bot_Y);
            this.groupBox5.Controls.Add(this.txt_Screen_Bot_X);
            this.groupBox5.Controls.Add(this.txt_Screen_Top_Y);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.txt_Screen_Top_X);
            this.groupBox5.Location = new System.Drawing.Point(395, 16);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(189, 115);
            this.groupBox5.TabIndex = 31;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Screen Coords";
            // 
            // txt_Screen_Bot_Y
            // 
            this.txt_Screen_Bot_Y.Location = new System.Drawing.Point(72, 89);
            this.txt_Screen_Bot_Y.MaxLength = 5;
            this.txt_Screen_Bot_Y.Name = "txt_Screen_Bot_Y";
            this.txt_Screen_Bot_Y.Size = new System.Drawing.Size(49, 20);
            this.txt_Screen_Bot_Y.TabIndex = 10;
            this.txt_Screen_Bot_Y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Screen_Bot_X
            // 
            this.txt_Screen_Bot_X.Location = new System.Drawing.Point(16, 89);
            this.txt_Screen_Bot_X.MaxLength = 5;
            this.txt_Screen_Bot_X.Name = "txt_Screen_Bot_X";
            this.txt_Screen_Bot_X.Size = new System.Drawing.Size(49, 20);
            this.txt_Screen_Bot_X.TabIndex = 9;
            this.txt_Screen_Bot_X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Screen_Top_Y
            // 
            this.txt_Screen_Top_Y.Location = new System.Drawing.Point(72, 40);
            this.txt_Screen_Top_Y.MaxLength = 5;
            this.txt_Screen_Top_Y.Name = "txt_Screen_Top_Y";
            this.txt_Screen_Top_Y.Size = new System.Drawing.Size(49, 20);
            this.txt_Screen_Top_Y.TabIndex = 8;
            this.txt_Screen_Top_Y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 73);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Bot Right X + Y";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(76, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Top Left X + Y";
            // 
            // txt_Screen_Top_X
            // 
            this.txt_Screen_Top_X.Location = new System.Drawing.Point(16, 40);
            this.txt_Screen_Top_X.MaxLength = 5;
            this.txt_Screen_Top_X.Name = "txt_Screen_Top_X";
            this.txt_Screen_Top_X.Size = new System.Drawing.Size(49, 20);
            this.txt_Screen_Top_X.TabIndex = 0;
            this.txt_Screen_Top_X.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.txt_Color_A);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txt_Color_B);
            this.groupBox4.Controls.Add(this.txt_Color_G);
            this.groupBox4.Controls.Add(this.txt_Color_R);
            this.groupBox4.Location = new System.Drawing.Point(200, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(189, 73);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Primary Color";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(27, 23);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(14, 13);
            this.label31.TabIndex = 7;
            this.label31.Text = "A";
            // 
            // txt_Color_A
            // 
            this.txt_Color_A.Location = new System.Drawing.Point(16, 40);
            this.txt_Color_A.MaxLength = 3;
            this.txt_Color_A.Name = "txt_Color_A";
            this.txt_Color_A.Size = new System.Drawing.Size(35, 20);
            this.txt_Color_A.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(146, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "B";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(107, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 13);
            this.label12.TabIndex = 4;
            this.label12.Text = "G";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(65, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "R";
            // 
            // txt_Color_B
            // 
            this.txt_Color_B.Location = new System.Drawing.Point(139, 40);
            this.txt_Color_B.MaxLength = 3;
            this.txt_Color_B.Name = "txt_Color_B";
            this.txt_Color_B.Size = new System.Drawing.Size(35, 20);
            this.txt_Color_B.TabIndex = 2;
            this.txt_Color_B.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Color_G
            // 
            this.txt_Color_G.Location = new System.Drawing.Point(98, 40);
            this.txt_Color_G.MaxLength = 3;
            this.txt_Color_G.Name = "txt_Color_G";
            this.txt_Color_G.Size = new System.Drawing.Size(35, 20);
            this.txt_Color_G.TabIndex = 1;
            this.txt_Color_G.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Color_R
            // 
            this.txt_Color_R.Location = new System.Drawing.Point(57, 40);
            this.txt_Color_R.MaxLength = 3;
            this.txt_Color_R.Name = "txt_Color_R";
            this.txt_Color_R.Size = new System.Drawing.Size(35, 20);
            this.txt_Color_R.TabIndex = 0;
            this.txt_Color_R.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
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
            // btn_Mine
            // 
            this.btn_Mine.Location = new System.Drawing.Point(16, 238);
            this.btn_Mine.Name = "btn_Mine";
            this.btn_Mine.Size = new System.Drawing.Size(75, 23);
            this.btn_Mine.TabIndex = 27;
            this.btn_Mine.Text = "Mining";
            this.btn_Mine.UseVisualStyleBackColor = true;
            // 
            // btn_Auto_Attack
            // 
            this.btn_Auto_Attack.Location = new System.Drawing.Point(102, 89);
            this.btn_Auto_Attack.Name = "btn_Auto_Attack";
            this.btn_Auto_Attack.Size = new System.Drawing.Size(75, 23);
            this.btn_Auto_Attack.TabIndex = 26;
            this.btn_Auto_Attack.Text = "AutoShoot";
            this.btn_Auto_Attack.UseVisualStyleBackColor = true;
            this.btn_Auto_Attack.Click += new System.EventHandler(this.btn_Auto_Attack_Click);
            // 
            // btnCanAgi
            // 
            this.btnCanAgi.Location = new System.Drawing.Point(102, 178);
            this.btnCanAgi.Name = "btnCanAgi";
            this.btnCanAgi.Size = new System.Drawing.Size(75, 23);
            this.btnCanAgi.TabIndex = 25;
            this.btnCanAgi.Text = "Canifis Agi";
            this.btnCanAgi.UseVisualStyleBackColor = true;
            this.btnCanAgi.Click += new System.EventHandler(this.btnCanAgi_Click);
            // 
            // btn_Woodcut
            // 
            this.btn_Woodcut.Location = new System.Drawing.Point(102, 50);
            this.btn_Woodcut.Name = "btn_Woodcut";
            this.btn_Woodcut.Size = new System.Drawing.Size(75, 23);
            this.btn_Woodcut.TabIndex = 24;
            this.btn_Woodcut.Text = "Woodcut";
            this.btn_Woodcut.UseVisualStyleBackColor = true;
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
            this.btnBarbFish.Location = new System.Drawing.Point(102, 15);
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
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btn_Copy_Color);
            this.groupBox9.Controls.Add(this.label30);
            this.groupBox9.Controls.Add(this.txt_Find_Color_A);
            this.groupBox9.Controls.Add(this.label27);
            this.groupBox9.Controls.Add(this.label28);
            this.groupBox9.Controls.Add(this.btn_Find_Color);
            this.groupBox9.Controls.Add(this.label29);
            this.groupBox9.Controls.Add(this.txt_Find_Color_B);
            this.groupBox9.Controls.Add(this.txt_Find_Color_G);
            this.groupBox9.Controls.Add(this.txt_Find_Color_R);
            this.groupBox9.Location = new System.Drawing.Point(925, 589);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(189, 106);
            this.groupBox9.TabIndex = 31;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Find Color";
            // 
            // btn_Copy_Color
            // 
            this.btn_Copy_Color.Location = new System.Drawing.Point(99, 66);
            this.btn_Copy_Color.Name = "btn_Copy_Color";
            this.btn_Copy_Color.Size = new System.Drawing.Size(75, 23);
            this.btn_Copy_Color.TabIndex = 41;
            this.btn_Copy_Color.Text = "Copy";
            this.btn_Copy_Color.UseVisualStyleBackColor = true;
            this.btn_Copy_Color.Click += new System.EventHandler(this.btn_Copy_Color_Click);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(27, 22);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(14, 13);
            this.label30.TabIndex = 40;
            this.label30.Text = "A";
            // 
            // txt_Find_Color_A
            // 
            this.txt_Find_Color_A.Enabled = false;
            this.txt_Find_Color_A.Location = new System.Drawing.Point(16, 40);
            this.txt_Find_Color_A.MaxLength = 3;
            this.txt_Find_Color_A.Name = "txt_Find_Color_A";
            this.txt_Find_Color_A.Size = new System.Drawing.Size(35, 20);
            this.txt_Find_Color_A.TabIndex = 39;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(146, 22);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(14, 13);
            this.label27.TabIndex = 5;
            this.label27.Text = "B";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(107, 22);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(15, 13);
            this.label28.TabIndex = 4;
            this.label28.Text = "G";
            // 
            // btn_Find_Color
            // 
            this.btn_Find_Color.Location = new System.Drawing.Point(17, 66);
            this.btn_Find_Color.Name = "btn_Find_Color";
            this.btn_Find_Color.Size = new System.Drawing.Size(75, 23);
            this.btn_Find_Color.TabIndex = 38;
            this.btn_Find_Color.Text = "Find Color";
            this.btn_Find_Color.UseVisualStyleBackColor = true;
            this.btn_Find_Color.Click += new System.EventHandler(this.btn_Find_Color_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(65, 22);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(15, 13);
            this.label29.TabIndex = 3;
            this.label29.Text = "R";
            // 
            // txt_Find_Color_B
            // 
            this.txt_Find_Color_B.Enabled = false;
            this.txt_Find_Color_B.Location = new System.Drawing.Point(139, 40);
            this.txt_Find_Color_B.MaxLength = 3;
            this.txt_Find_Color_B.Name = "txt_Find_Color_B";
            this.txt_Find_Color_B.Size = new System.Drawing.Size(35, 20);
            this.txt_Find_Color_B.TabIndex = 2;
            // 
            // txt_Find_Color_G
            // 
            this.txt_Find_Color_G.Enabled = false;
            this.txt_Find_Color_G.Location = new System.Drawing.Point(98, 40);
            this.txt_Find_Color_G.MaxLength = 3;
            this.txt_Find_Color_G.Name = "txt_Find_Color_G";
            this.txt_Find_Color_G.Size = new System.Drawing.Size(35, 20);
            this.txt_Find_Color_G.TabIndex = 1;
            // 
            // txt_Find_Color_R
            // 
            this.txt_Find_Color_R.Enabled = false;
            this.txt_Find_Color_R.Location = new System.Drawing.Point(57, 40);
            this.txt_Find_Color_R.MaxLength = 3;
            this.txt_Find_Color_R.Name = "txt_Find_Color_R";
            this.txt_Find_Color_R.Size = new System.Drawing.Size(35, 20);
            this.txt_Find_Color_R.TabIndex = 0;
            // 
            // TabController
            // 
            this.TabController.Controls.Add(this.Buttons);
            this.TabController.Controls.Add(this.ClickTab);
            this.TabController.Location = new System.Drawing.Point(721, 28);
            this.TabController.Name = "TabController";
            this.TabController.SelectedIndex = 0;
            this.TabController.Size = new System.Drawing.Size(613, 549);
            this.TabController.TabIndex = 16;
            // 
            // worker_Normal_Clicks
            // 
            this.worker_Normal_Clicks.WorkerReportsProgress = true;
            this.worker_Normal_Clicks.WorkerSupportsCancellation = true;
            this.worker_Normal_Clicks.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_Normal_Clicks_DoWork);
            this.worker_Normal_Clicks.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // Click_Sequence
            // 
            this.Click_Sequence.HeaderText = "#";
            this.Click_Sequence.Name = "Click_Sequence";
            this.Click_Sequence.Width = 30;
            // 
            // Click_Name
            // 
            this.Click_Name.HeaderText = "Name";
            this.Click_Name.Name = "Click_Name";
            // 
            // Click_Delay
            // 
            this.Click_Delay.HeaderText = "Delay";
            this.Click_Delay.Name = "Click_Delay";
            this.Click_Delay.Width = 60;
            // 
            // Click_Type
            // 
            this.Click_Type.HeaderText = "Button";
            this.Click_Type.Name = "Click_Type";
            this.Click_Type.Width = 50;
            // 
            // Click_X
            // 
            this.Click_X.HeaderText = "X";
            this.Click_X.Name = "Click_X";
            this.Click_X.Width = 50;
            // 
            // Click_Y
            // 
            this.Click_Y.HeaderText = "Y";
            this.Click_Y.Name = "Click_Y";
            this.Click_Y.Width = 50;
            // 
            // Click_Offset
            // 
            this.Click_Offset.HeaderText = "Offset";
            this.Click_Offset.Name = "Click_Offset";
            this.Click_Offset.Width = 50;
            // 
            // Click_Empty_Point
            // 
            this.Click_Empty_Point.HeaderText = "Click When Point is Empty";
            this.Click_Empty_Point.Name = "Click_Empty_Point";
            // 
            // Click_Color
            // 
            this.Click_Color.HeaderText = "Primary Color (A,R,G,B)";
            this.Click_Color.Name = "Click_Color";
            this.Click_Color.Width = 150;
            // 
            // Click_Color_2
            // 
            this.Click_Color_2.HeaderText = "Second Color (A,R,G,B)";
            this.Click_Color_2.Name = "Click_Color_2";
            this.Click_Color_2.Width = 150;
            // 
            // Click_Image
            // 
            this.Click_Image.HeaderText = "Image";
            this.Click_Image.Name = "Click_Image";
            this.Click_Image.Width = 300;
            // 
            // Click_Script
            // 
            this.Click_Script.HeaderText = "Click Script";
            this.Click_Script.Name = "Click_Script";
            this.Click_Script.Width = 200;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1346, 707);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.chkClicks);
            this.Controls.Add(this.TabController);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.chkLog);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.buttonRecord);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Auto Clicker";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.groupBox2.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClickOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderCycles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClicks)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInventoryCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sliderColorRange)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ClickTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_Clicks)).EndInit();
            this.Buttons.ResumeLayout(false);
            this.Buttons.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slider_Image_Range)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.TabController.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        [STAThread]
        public static void Main(string[] args)
        {
            System.Windows.Forms.Application.Run(new MainForm());
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
                if (Clicks.Count > 0)
                {
                    if (MessageBox.Show("Erase current clicks?", "Erase", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                }
                buttonRecord.Text = "Recording...";
                ClickCountPos = 0;
                Clicks.Clear();
                ClickStopwatch.Reset();
            }

            RecordClicks = !RecordClicks;
        }

        void ButtonStart(object sender, System.EventArgs e, BackgroundWorker worker = null)
        {
            //if(worker == null && CurrentWorker == null)
            //{
            //    CurrentWorker = worker_Normal_Clicks;
            //}

            //else 

            if(CurrentWorker != null && CurrentWorker.IsBusy && CurrentWorker.CancellationPending)
            {
                LogWrite("Waiting for current worker to cancel.");
                return;
            }
            if (worker != null && !RunProgram)
            {
                CurrentWorker = worker;
            }
            else if(CurrentWorker != null && RunProgram)
            {
                //contiue
            }
            else
            {
                CurrentWorker = worker_Normal_Clicks;
            }


            if (CurrentWorker == worker_Normal_Clicks && Clicks.Count < 1 && !RunProgram)
            {
                MessageBox.Show("Need to have at least one click recored");
                return;
            }


            RunProgram = !RunProgram;
            if (RunProgram)
            {
                UpdateButtons(CurrentWorker, true);
                //btn_Start.Text = "Stop";
                LogWrite("Starting Clicker");
                SetGlobalDetails();
                var runParams = new RunParams<string>();
                runParams.ReportProgress = new Progress<string>(value => LogWrite(value));
                runParams.Timeouts = new Timeouts()
                {
                    Active = chkTimeOut.Checked,
                    TimeoutCountMin = int.Parse(txt_Timeout_Cycle_Min.Text),
                    TimeoutCountMax = int.Parse(txt_Timeout_Cycle_Max.Text),
                    TimeoutLengthMin = TimeoutLengthMin,
                    TimeoutLengthMax = TimeoutLengthMax
                };
                runParams.RunLimit = (int)numCount.Value;
                runParams.ScreenshotInfo = new ScreenshotInfo() { ColorRange = ColorRange };
                runParams.ClickList = Clicks.ToList(); ;
                CurrentWorker.RunWorkerAsync(runParams);
            }
            else
            {
                //UpdateButtons(CurrentWorker, false);
                //btn_Start.Text = "Start";
                LogWrite("Cancelling Clicker");
                CurrentWorker.CancelAsync();
            }
        }

        private void UpdateButtons(BackgroundWorker currentWorker, bool v)
        {
            Color btnColor = Color.Transparent;
            if (v)
                btnColor = Color.Red;
            if(CurrentWorker == worker_Normal_Clicks)
            {
                btn_Start.BackColor = btnColor;
            }
            else if(CurrentWorker == worker_Gem_Mining)
            {
                btn_Gem_Mine.BackColor = btnColor;
            }
            else if(CurrentWorker == worker_Normal_Clicks)
            {

            }
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
                if (FindingColor)
                {
                    var color = MainScreen.GetColorAt(Cursor.Position);
                    txt_Find_Color_R.Text = color.R.ToString();
                    txt_Find_Color_G.Text = color.G.ToString();
                    txt_Find_Color_B.Text = color.B.ToString();
                    txt_Find_Color_A.Text = color.A.ToString();
                    btn_Find_Color.PerformClick();
                }

                if (RecordClicks)
                {
                    ClickStopwatch.Stop();
                    if (Clicks.Count > 0)
                    {

                        Clicks[Clicks.Count - 1].DelayAfterClick = ClickStopwatch.ElapsedMilliseconds;
                        LogWrite("Delay from click " + (Clicks.Count - 1) + ": " + ClickStopwatch.ElapsedMilliseconds);
                    }

                    Clicks.Add(new Click(++ClickCountPos,
                        Cursor.Position,
                        e.Button == MouseButtons.Left ? 0 : 1,
                        0,
                        ClickOffset));
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
                        //StartAutoClicker();
                        this.btn_Start.PerformClick();
                        Ctrl = false;
                        break;
                    case Keys.D2:
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

            if (UseRandomTimeouts && !EndTimeoutsOnly && RandomTimeoutCount <= 0 && ClickCountPos == TimeoutPos)
            {
                RandomTimeoutCount = GetRandomTimeoutCount();
                ClickTimer.Interval = GetRandomTimeout();
                TimedOut = true;
                TimeoutPos = RandomGenerate.Next(1, Clicks.Count - 1);
            }
            else if (radioCycles.Checked)
                ClickTimer.Interval = (int)currentClick.DelayAfterClick + RandomGenerate.Next(-200, 200);
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
                    if (RandomTimeoutCount <= 0 && EndTimeoutsOnly)
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
                        //RapidClickTimer.Stop();
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
            var screenShot = MainScreen.CaptureScreen(SelectedMonitor);
            var lowerX = screenShot.Width / 4;
            var lowerY = screenShot.Height / 4;
            Point point = MainScreen.FindImageFromList(screenShot, new Point(screenShot.Width - lowerX, screenShot.Height / 2), new Point(screenShot.Width, screenShot.Height), imageList, colorRange);
            if (!point.IsEmpty)
            {
                //RapidClickTimer.Stop();
                point.X += 10;
                point.Y += 20;
                LogWrite("FOUND");
                DoMouseClick(0, point);

                Thread.Sleep(RandomGenerate.Next(500, 2000));

                Mouse.Mouse.MoveTo(origMousePoint.X + RandomGenerate.Next(-5, 5), origMousePoint.Y + RandomGenerate.Next(-5, 5));
            }

            if (!MainScreen.FindColorPointRange(screenShot, AlchPoint, healthBar, colorRange))
            {
                EatFailCount++;
                //RapidClickTimer.Stop();
                if (CurrentInventoryClickCount >= TotalInventoryClickCount && EatFailCount < 9)
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

            if (EatFailCount >= EatFailCountMax)
            {
                StopAutoClicker();
                return;
            }


            //if (!RapidClickTimer.Enabled)
            //RapidClickTimer.Start();
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
            var point = MainScreen.FindImage(new Point(750, 300), new Point(delayTime, 500), image, colorRange, SelectedMonitor);

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
            var point = MainScreen.FindImage(new Point(1835, 925), new Point(1900, 990), image, colorRange, SelectedMonitor);
            if (point.IsEmpty)
            {
                WoodcutTimer.Stop();
                ClickInventory(true);
                WoodcutTimer.Start();
            }

        }
        private void NMZDrinkTimer_Tick(object sender, EventArgs e)
        {
            var screenshot = MainScreen.CaptureScreen(SelectedMonitor);
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



            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var point = MainScreen.FindInInventory(Properties.Resources.Absorb_1, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += Absorb_offsetX;
                point.Y += Absorb_offsetY;
                LogWrite("found 1 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Absorb_2, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += Absorb_offsetX;
                point.Y += Absorb_offsetY;
                LogWrite("found 2 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Absorb_3, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += Absorb_offsetX;
                point.Y += Absorb_offsetY;
                LogWrite("found 3 dose");
                DoMouseClick(0, point);
                return;
            }
            point = MainScreen.FindInInventory(Properties.Resources.Absorb_4, screenshot, colorRange);
            if (!point.IsEmpty)
            {
                point.X += Absorb_offsetX;
                point.Y += Absorb_offsetY;
                LogWrite("found 4 dose");
                DoMouseClick(0, point);
                return;
            }
        }

        private void NMZOverloadTimer_Tick(object sender, EventArgs e)
        {
            LogWrite("Checking for overload");
            var screenshot = MainScreen.CaptureScreen(SelectedMonitor);

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
            if (point != Point.Empty)
            {
                point.X += GetRandomOffset();
                point.Y += GetRandomOffset();
                if (logClicks)
                    LogWrite("Clicked at X:" + point.X + "  Y:" + point.Y);
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

        public void StopAutoClicker()
        {
            BarbFishTimer.Stop();
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

            btn_Start.Text = "Start";
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
                PerformLeftClick(Inventory[DropClickPos], null);
                Thread.Sleep(100);
                PerformLeftClick(Inventory[DropClickPos], null);
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
            DropTimer.Interval = RandomGenerate.Next(50, 200);
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
            btn_Start.Enabled = false;
            //buttonRecord.Enabled = false;
            chkLog.Enabled = false;
            chkDropInverse.Enabled = false;
            radioClicks.Enabled = false;
            radioCycles.Enabled = false;
            btnSetupInventory.Enabled = false;
            btnSingleClickInv.Enabled = false;
            btnDropInventory.Enabled = false;
            btn_Find_Color.Enabled = false;
        }

        private void EnableAll()
        {
            btn_Start.Enabled = true;
            //buttonRecord.Enabled = true;
            chkLog.Enabled = true;
            chkDropInverse.Enabled = true;
            radioClicks.Enabled = true;
            radioCycles.Enabled = true;
            btnSetupInventory.Enabled = true;
            btnSingleClickInv.Enabled = true;
            btnDropInventory.Enabled = true;
            btn_Find_Color.Enabled = true;
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
            return RandomGenerate.Next(int.Parse(txt_Timeout_Cycle_Min.Text), int.Parse(txt_Timeout_Cycle_Max.Text));
        }
        private int GetRandomTimeout()
        {
            return RandomGenerate.Next(3000, 8000);
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

            btn_Start.PerformClick();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = true;
            btn_Start.PerformClick();
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

        private void btn_Find_Image_Click(object sender, EventArgs e)
        {
            var image = Properties.Resources.tanner_head; //. //MainScreen.LoadImageFile("c:\\AppData\\AutoClicker\\Images\\TreeSwatch.png");
            var checkedImage = TabController.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            var offsetX = image.Width / 2;
            var offsetY = image.Height / 2;
            var colorRange = 360m * (sliderColorRange.Value / 100m);
            var point = MainScreen.FindImage(new Point(5, 35), new Point(1910, 990), image, colorRange, SelectedMonitor);

            LogWrite(String.Format("X:{0} Y:{1}", point.X, point.Y));

            //Mouse.Mouse.MoveTo(point.X + offsetX, point.Y + offsetY);
            DoMouseClick(1, new Point(point.X + offsetX, point.Y + offsetY));
        }

        private void sliderColorRange_Scroll(object sender, EventArgs e)
        {
            lblColorRange.Text = (100 - sliderColorRange.Value).ToString() + "%";
        }

        private void btnNightmare_Click(object sender, EventArgs e)
        {
            if (NMZEnabled)
            {
                LogWrite("Stopping Nightmare Zone!");
                StopAutoClicker();
                btn_Start.Enabled = true;
                return;
            }

            InitializeNightmareValues();
            btn_Start.Enabled = false;
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

        private void InitializeNightmareValues()
        {
            var image = Properties.Resources.Absorb_1;
            var Absorb_offsetX = image.Width / 2;
            var Absorb_offsetY = image.Height / 2;
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
                    if (MainScreen.ColorDiff(gotColor, black) < 0.05m)
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
                        if (widen && x - 1 > 0)
                            image.SetPixel(x - 1, y, black);
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

        private void btnSeersAgil_Click(object sender, EventArgs e)
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
            point = MainScreen.FindColorScreenRange(new Point(MidX, 0), new Point(MaxX, MidY), attackDot, 8, colorRange, SelectedMonitor);
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
            point = MainScreen.FindColorScreenRange(new Point(0, 0), new Point(MidX, MidY), attackDot, 8, colorRange, SelectedMonitor);
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
            point = MainScreen.FindColorScreenRange(new Point(0, MidY), new Point(MidX, MaxY), attackDot, 8, colorRange, SelectedMonitor);
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
            point = MainScreen.FindColorScreenRange(new Point(MidX, MidY), new Point(MaxX, MaxY), attackDot, 8, colorRange, SelectedMonitor);
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
            point = MainScreen.FindColorScreenRange(new Point(0, MidY), new Point(MidX, MaxY), attackDot, 8, colorRange, SelectedMonitor);
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
            point = MainScreen.FindColorScreenRange(new Point(MidX, MidY), new Point(MaxX, MaxY), attackDot, 8, colorRange, SelectedMonitor);
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
            Clicks.Add(new Click(0, new Point(920, 785), 0, 1000, ClickOffset));

            Clicks.Add(new Click(1, new Point(968, 785), 0, 1000, ClickOffset));

            Clicks.Add(new Click(2, new Point(1018, 785), 1, 2000, ClickOffset));

            Clicks.Add(new Click(3, new Point(977, 880), 0, 1000, ClickOffset));

            //run / leave bak
            Clicks.Add(new Click(4, new Point(964, 960), 0, 1000, ClickOffset));

            //eat
            Clicks.Add(new Click(5, new Point(1791, 761), 0, 2000, ClickOffset));

            Clicks.Add(new Click(6, new Point(1837, 755), 0, 2000, ClickOffset));

            //continue run
            Clicks.Add(new Click(7, new Point(964, 960), 0, 5000, ClickOffset));

            Clicks.Add(new Click(8, new Point(964, 960), 0, 5000, ClickOffset));

            Clicks.Add(new Click(9, new Point(1209, 960), 0, 5000, ClickOffset));

            Clicks.Add(new Click(10, new Point(1529, 486), 0, 7000, ClickOffset));

            Clicks.Add(new Click(11, new Point(1753, 620), 0, 11000, ClickOffset));

            Clicks.Add(new Click(12, new Point(200, 518), 0, 10000, ClickOffset));

            Clicks.Add(new Click(13, new Point(492, 190), 0, 11000, ClickOffset));

            Clicks.Add(new Click(14, new Point(960, 197), 0, 6000, ClickOffset));

            Clicks.Add(new Click(15, new Point(961, 246), 0, 6000, ClickOffset));

            Clicks.Add(new Click(16, new Point(1034, 825), 0, 1000, ClickOffset));
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
            var screenShot = MainScreen.CaptureScreen(SelectedMonitor);
            var image = Properties.Resources.NotFishing;
            var colorRange = 360m * (20 / 100m); //sliderColorRange.Value
            Point point;

            point = MainScreen.FindImage(screenShot, new Point(1530, 80), new Point(1700, 120), image, colorRange, SelectedMonitor);
            if (!point.IsEmpty)
            {
                image = Properties.Resources.EmptyInvSpace;
                point = MainScreen.FindImage(screenShot, new Point(1850, 940), new Point(1900, 985), image, colorRange, SelectedMonitor);
                if (point.IsEmpty)
                {
                    ClickInventory(true);
                }
                else
                {
                    image = Properties.Resources.BarbFish11;
                    point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange, SelectedMonitor);
                    if (point.IsEmpty)
                    {
                        image = Properties.Resources.BarbFish12;
                        point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange, SelectedMonitor);
                    }
                    if (point.IsEmpty)
                    {
                        image = Properties.Resources.BarbFish13;
                        point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange, SelectedMonitor);
                    }
                    if (point.IsEmpty)
                    {
                        image = Properties.Resources.BarbFish;
                        point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange, SelectedMonitor);
                    }
                    var offsetX = image.Width / 2;
                    var offsetY = image.Height / 2;
                    DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
                }
            }
            System.GC.Collect();
            LogWrite("Fish Ended");

        }

        private void worker_Auto_Attack_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            Timeouts timeouts = runParams.Timeouts;
            var timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
            var timeoutPos = RandomGenerate.Next(0, Clicks.Count - 1);

            if (runCount == 0)
                runCount = 99999;


            report.Report("Starting Auto Attack Worker");
            report.Report("Total Runs = " + runCount);
            Thread.Sleep(3000);


            while (!worker_Normal_Clicks.CancellationPending && runCount > 0)
            {

                report.Report("Check For Targets");
                using (var screenShot = MainScreen.CaptureScreen(SelectedMonitor))
                {
                    var testPixel = screenShot.GetPixel(9, 72);

                    if (testPixel != MonsterHealth)
                    {
                        var point = MainScreen.FindColor(screenShot, TopLeft, BottomRight, SearchColor, 8);
                        if (!point.IsEmpty)
                        {
                            PerformLeftClick(point, report);
                            Thread.Sleep(RandomGenerate.Next(2000, 4000));
                        }
                        else
                        {
                            //Thread.Sleep(RandomGenerate.Next(3000, 3000));
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        //Thread.Sleep(RandomGenerate.Next(3000, 3000));
                        Thread.Sleep(3000);
                    }
                }

                report.Report("Check For Targets Ended");
            }

            report.Report("Ending Auto Attack Worker");

        }

        private void Worker_Woodcut_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            Timeouts timeouts = runParams.Timeouts;
            var timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
            var timeoutPos = RandomGenerate.Next(0, Clicks.Count - 1);

            report.Report("Starting Woodcut Worker");
            report.Report("Total Runs = " + runCount);
            Thread.Sleep(3000);


            while (!worker_Normal_Clicks.CancellationPending && runCount > 0)
            {
                LogWrite("Check Woodcut");
                using (var screenShot = MainScreen.CaptureScreen(SelectedMonitor))
                {
                    var image = Properties.Resources.WCOK;
                    Point point;

                    point = MainScreen.FindImage(screenShot, new Point(90, 50), new Point(135, 80), image, ColorRange, SelectedMonitor);
                    if (point.IsEmpty)
                    {
                        image = Properties.Resources.EmptyInvSpace;
                        point = MainScreen.FindImage(screenShot, new Point(1850, 940), new Point(1900, 985), image, ColorRange, SelectedMonitor);
                        if (point.IsEmpty)
                        {
                            ClickInventory(true);
                        }
                        else
                        {
                            image = Properties.Resources.Woodcut1;
                            point = MainScreen.FindImage(screenShot, new Point(800, 400), new Point(1100, 700), image, ColorRange, SelectedMonitor);
                            if (point.IsEmpty)
                            {
                                image = Properties.Resources.Woodcut2;
                                point = MainScreen.FindImage(screenShot, new Point(800, 400), new Point(1100, 700), image, ColorRange, SelectedMonitor);
                            }
                            var offsetX = image.Width / 2;
                            var offsetY = image.Height / 2;
                            PerformLeftClick(new Point(point.X + offsetX, point.Y + offsetY), report);
                        }
                    }
                }
            }

            report.Report("Ending Woodcut Worker");
        }

        private void RuneCraftTimer_Tick(object sender, EventArgs e)
        {
            //click in bank
            Clicks.Add(new Click(0, new Point(920, 785), 0, 1000, ClickOffset));

            Clicks.Add(new Click(1, new Point(968, 785), 0, 1000, ClickOffset));

            Clicks.Add(new Click(2, new Point(1018, 785), 1, 2000, ClickOffset));

            Clicks.Add(new Click(3, new Point(977, 880), 0, 1000, ClickOffset));

            //run / leave bak
            Clicks.Add(new Click(4, new Point(964, 960), 0, 1000, ClickOffset));

            //eat
            Clicks.Add(new Click(5, new Point(1791, 761), 0, 2000, ClickOffset));

            Clicks.Add(new Click(6, new Point(1837, 755), 0, 2000, ClickOffset));

            //continue run
            Clicks.Add(new Click(7, new Point(964, 960), 0, 5000, ClickOffset));

            Clicks.Add(new Click(8, new Point(964, 960), 0, 5000, ClickOffset));

            Clicks.Add(new Click(9, new Point(1209, 960), 0, 5000, ClickOffset));

            Clicks.Add(new Click(10, new Point(1529, 486), 0, 7000, ClickOffset));

            Clicks.Add(new Click(11, new Point(1753, 620), 0, 11000, ClickOffset));

            Clicks.Add(new Click(12, new Point(200, 518), 0, 10000, ClickOffset));

            Clicks.Add(new Click(13, new Point(492, 190), 0, 11000, ClickOffset));

            Clicks.Add(new Click(14, new Point(960, 197), 0, 6000, ClickOffset));

            Clicks.Add(new Click(15, new Point(961, 246), 0, 6000, ClickOffset));

            Clicks.Add(new Click(16, new Point(1034, 825), 0, 1000, ClickOffset));
            //Thread.Sleep(1000);
        }
        private void workerBarbFish_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            LogWrite("Check Fishing");
            var screenShot = MainScreen.CaptureScreen(SelectedMonitor);
            var image = Properties.Resources.NotFishing;
            var colorRange = 360m * (10 / 100m);
            Point point;

            point = MainScreen.FindImage(screenShot, new Point(1530, 80), new Point(1700, 120), image, colorRange, SelectedMonitor);
            if (!point.IsEmpty)
            {
                image = Properties.Resources.Empty_Inv_Space;
                point = MainScreen.FindImage(screenShot, new Point(1855, 950), new Point(1900, 985), image, colorRange, SelectedMonitor);
                if (point.IsEmpty)
                {
                    ClickInventory(true);
                }
                else
                {
                    image = Properties.Resources.BarbFish11;
                    point = MainScreen.FindImage(screenShot, new Point(5, 35), new Point(1910, 990), image, colorRange, SelectedMonitor);
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
            point = MainScreen.FindImage(new Point(900, 290), new Point(980, 400), image, colorRange, SelectedMonitor);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(5500);
            //Second Ledge
            point = MainScreen.FindImage(new Point(750, 480), new Point(800, 575), image, colorRange, SelectedMonitor);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(5500);
            //Third Ledge
            point = MainScreen.FindImage(new Point(700, 650), new Point(750, 750), image, colorRange, SelectedMonitor);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(6000);
            //Fourth Ledge
            point = MainScreen.FindImage(new Point(880, 740), new Point(945, 850), image, colorRange, SelectedMonitor);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(5500);
            //Polevault
            point = MainScreen.FindImage(new Point(980, 600), new Point(1060, 660), image, colorRange, SelectedMonitor);
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(7500);

            point = MainScreen.FindImage(new Point(1300, 500), new Point(1570, 600), image, colorRange, SelectedMonitor);
            if (point.IsEmpty) {
                point = new Point(1500, 570);
            }
            DoMouseClick(0, new Point(point.X + offsetX, point.Y + offsetY));
            Thread.Sleep(6500);

            point = MainScreen.FindImage(new Point(930, 330), new Point(980, 380), image, colorRange, SelectedMonitor);
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
            var runParams = new RunParams<string>();
            runParams.ReportProgress = new Progress<string>(value => LogWrite(value));
            runParams.Timeouts = new Timeouts()
            {
                Active = chkTimeOut.Enabled,
                TimeoutCountMin = int.Parse(txt_Timeout_Cycle_Min.Text),
                TimeoutCountMax = int.Parse(txt_Timeout_Cycle_Max.Text)
            };
            runParams.RunLimit = (int)numCount.Value;

            //var report = new Progress<string>(value => LogWrite(value));

            RuneCraft = !RuneCraft;
            if (RuneCraft)
            {
                LogWrite("Starting RC");
                //AgilityTimer.Start(); 
                worker_RC.RunWorkerAsync(runParams);
            }
            else
            {
                LogWrite("Ending RC");
                //AgilityTimer.Stop();
                worker_RC.CancelAsync();
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

        private void btn_Auto_Attack_Click(object sender, EventArgs e)
        {

        }

        public void Report(string value)
        {
            throw new NotImplementedException();
        }

        private async void worker_RC_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            //var report = e.Argument as IProgress<string>;
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            Timeouts timeouts = runParams.Timeouts;
            var timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
            if (runCount == 0)
                runCount = 99999;

            var eatFish = false;
            var useSprint = true;
            var sprintCount = 3;
            //var flipSprint = false;

            report.Report("Total Runs = " + runCount);
            Thread.Sleep(3000);
            while (!worker_RC.CancellationPending && runCount > 0)
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

                if (timeouts.Active)
                {
                    timeoutCount--;
                    if (timeoutCount <= 0)
                    {
                        timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
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
            Thread.Sleep(RandomGenerate.Next(800, 1500));

            if (runOn)
            {
                point = MainScreen.FindImage(new Point(1713, 154), new Point(1745, 176), fullEnergy, colorRange, SelectedMonitor);
                if (point.IsEmpty)
                    return false;

                var attackDot = Color.FromArgb(206, 168, 1);
                point = MainScreen.FindColorScreenRange(new Point(1746, 156), new Point(1763, 171), attackDot, 1, colorRange, SelectedMonitor);
                if (point.IsEmpty)
                    return false;
            }
            else
            {
                var attackDot = Color.FromArgb(171, 172, 162);
                point = MainScreen.FindColorScreenRange(new Point(1746, 156), new Point(1763, 171), attackDot, 1, colorRange, SelectedMonitor);
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

            point = MainScreen.FindImage(new Point(X2, Y2), new Point(MaxX, MaxY), brokenBag, colorRange, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(0, 0), new Point(X1, Y1), StartCheck, 8, SelectedMonitor);
            if (point.IsEmpty)
                return;

            //bank
            point = new Point(1023, 426);
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
            point = MainScreen.FindImage(new Point(X1, 0), new Point(X2, Y1), bankCloseButton, colorRange, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(X1, 100), new Point(X2, Y1), attackDot, 8, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(X1, 100), new Point(X2, Y1), attackDot, 8, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(X1, 0), new Point(X2, Y1), attackDot, 8, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(0, Y1), new Point(X1, Y2), attackDot, 8, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(0, Y1), new Point(X1, Y2), attackDot, 8, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(X1, Y1), new Point(X2, Y2), attackDot, 8, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(X1, Y1), new Point(X2, Y2), attackDot, 8, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(X2, 0), new Point(MaxX, Y1), attackDot, 8, SelectedMonitor);
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
            point = MainScreen.FindColorScreen(new Point(X1, Y1), new Point(X2, Y2), attackDot, 8, SelectedMonitor);
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

        private void worker_Mining_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            Timeouts timeouts = runParams.Timeouts;
            var timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
            if (runCount == 0)
                runCount = 99999;
            report.Report("Starting Mining Worker");
            report.Report("Total Runs = " + runCount);
            Thread.Sleep(3000);

            while (!worker_Mining.CancellationPending && runCount > 0)
            {
                CheckFullInv();
                MineRock();
                runCount--;

                if (timeouts.Active)
                {
                    timeoutCount--;
                    if (timeoutCount <= 0)
                    {
                        timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
                        var timeoutnum = RandomGenerate.Next(20000, 90000);
                        report.Report("Pausing for : " + timeoutnum);
                        report.Report("New count limit : " + timeoutCount);
                        Thread.Sleep(timeoutnum);
                    }
                }
            }

            report.Report("Ending Mining Worker");
        }

        private void MineRock()
        {
            var attackDot = Color.FromArgb(255, 0, 255);
            int offsetX = 0;
            int offsetY = 0;
            Point point = MainScreen.FindColorScreen(new Point(670, 420), new Point(1200, 820), attackDot, 8, SelectedMonitor);
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

        private void MineRock(Bitmap screenShot, Point topLeft, Point bottomRight, decimal colorRange, IProgress<string> report)
        {
            Point point = MainScreen.FindColorScreenCenterOut(topLeft, bottomRight, SearchColor, colorRange, 8, SelectedMonitor);
            if (!point.IsEmpty && (point.X != 0 && point.Y != 0))
            {
                Mouse.Mouse.MoveTo(point.X, point.Y);

                Thread.Sleep(100);

                Mouse.Mouse.LeftClick();
                report.Report("Clicked at X:" + point.X + "  Y:" + point.Y);
            }

            Thread.Sleep(RandomGenerate.Next(4000, 7000));

            //return Task.CompletedTask;
        }

        private void CheckFullInv()
        {
            int offsetX = 0;
            int offsetY = 0;
            var colorRange = 360m * (10 / 100m);
            var image = Properties.Resources.EmptyInvNew;
            var point = MainScreen.FindImage(new Point(1850, 953), new Point(1906, 990), image, colorRange, SelectedMonitor);
            if (point.IsEmpty)
            {
                Keyboard.Keyboard.HoldKey(Keys.LShiftKey);
                for (int i = 0; i < Inventory.Count; i++)
                {
                    point = Inventory[i];
                    offsetX = RandomGenerate.Next(-5, 5);
                    if (i % 4 == 0)
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
        }

        private void CheckFullInv(Bitmap screenshot, Point topLeft, Point bottmRight)
        {
            int offsetX = 0;
            int offsetY = 0;
            var image = Properties.Resources.EmptyInvNew;
            var point = MainScreen.FindImage(screenshot, topLeft, BottomRight, image, ColorRange, SelectedMonitor);
            if (point.IsEmpty)
            {
                Keyboard.Keyboard.HoldKey(Keys.LShiftKey);
                for (int i = 0; i < Inventory.Count; i++)
                {
                    point = Inventory[i];
                    offsetX = RandomGenerate.Next(-5, 5);
                    if (i % 4 == 0)
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
        }

        private bool CheckFullBankInv(Bitmap screenshot, Point topLeft, Point bottomRight)
        {
            var image = Properties.Resources.EmptyInvNew;
            var point = MainScreen.FindImage(screenshot, topLeft, bottomRight, image, ColorRange, SelectedMonitor);
            if (point.IsEmpty || (point.X == -2560 && point.Y == 0) || (point.X == 2560 && point.Y == 0))
            {
                return true;
            }
            return false;
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
            runParams.Timeouts = new Timeouts()
            {
                Active = chkTimeOut.Checked,
                TimeoutCountMin = int.Parse(txt_Timeout_Cycle_Min.Text),
                TimeoutCountMax = int.Parse(txt_Timeout_Cycle_Max.Text)
            };
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
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            Timeouts timeouts = runParams.Timeouts;
            var timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
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

                if (timeouts.Active)
                {
                    timeoutCount--;
                    if (timeoutCount <= 0)
                    {
                        timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
                        var timeoutnum = RandomGenerate.Next(10000, 60000);
                        ClickPoint.X = AlchPoint.X + RandomGenerate.Next(-3, 3);
                        ClickPoint.Y = AlchPoint.Y + RandomGenerate.Next(-3, 3);
                        Mouse.Mouse.MoveTo(ClickPoint.X, ClickPoint.Y);
                        report.Report("Pausing for : " + timeoutnum);
                        report.Report("New count limit : " + timeoutCount);
                        Thread.Sleep(timeoutnum);
                    }
                }

                if (runCount % 2 == 0)
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
        private void txt_Number_Only_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-')
            {
                //The char is not a number or a control key
                //Handle the event so the key press is accepted
                e.Handled = true;
                //Get out of there - make it safe to add stuff after the if statement
                return;
            }
        }

        private void worker_Gem_Mining_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            var colorRange = runParams.ScreenshotInfo.ColorRange;
            Timeouts timeouts = runParams.Timeouts;
            var timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
            if (runCount == 0)
                runCount = 99999;

            var step = 0;

            report.Report("Starting Gem Mining Worker");
            report.Report("Total Runs = " + runCount);
            Thread.Sleep(3000);

            while (!worker_Gem_Mining.CancellationPending && runCount > 0)
            {
                using (var screenShot = MainScreen.CaptureScreen(SelectedMonitor))
                {
                    if (step == 0)
                    {
                        if (CheckFullBankInv(screenShot, InvTopLeft, InvBottomRight))
                        {
                            step = 1;
                            continue;
                        }
                    }
                    if (step == 1)
                    {
                        if (FindBank(screenShot, TopLeft, BottomRight, ColorRange, report))
                            step = 2;
                        continue;
                    }
                    if (step == 2)
                    {
                        if (FindBankAll(screenShot, TopLeft, BottomRight, ImageRange, report))
                            step = 3;
                        else
                            step = 1;
                        continue;
                    }
                    if (step == 3)
                    {
                        if (CloseBank(screenShot, TopLeft, BottomRight, ImageRange, report))
                        {
                            runCount--;
                            step = 0;
                        }

                        continue;
                    }
                    if (step == 0)
                        MineRock(screenShot, TopLeft, BottomRight, ColorRange, report);


                    if (timeouts.Active)
                    {
                        timeoutCount--;
                        if (timeoutCount <= 0)
                        {
                            timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
                            var timeoutnum = RandomGenerate.Next(timeouts.TimeoutLengthMin, timeouts.TimeoutLengthMax);
                            report.Report("Pausing for : " + timeoutnum);
                            report.Report("New count limit : " + timeoutCount);
                            Thread.Sleep(timeoutnum);
                        }
                    }
                }


            }

            report.Report("Ending Gem Mining Worker");
        }

        private bool FindBank(Bitmap screenshot, Point topLeft, Point bottmRight, decimal colorRange, IProgress<string> report)
        {
            var point = MainScreen.FindColorScreenRange(screenshot, topLeft, bottmRight, SearchColor2, 8, colorRange, SelectedMonitor);
            if (!point.IsEmpty && (point.X != 0 && point.Y != 0))
            {
                Mouse.Mouse.MoveTo(point.X, point.Y);

                Thread.Sleep(100);

                Mouse.Mouse.LeftClick();
                report.Report("Clicked at X:" + point.X + "  Y:" + point.Y);

                Thread.Sleep(RandomGenerate.Next(6000, 9000));
                return true;
            }
            Thread.Sleep(RandomGenerate.Next(3000, 3000));
            return false;
        }

        private bool FindBankAll(Bitmap screenshot, Point topLeft, Point bottmRight, decimal colorRange, IProgress<string> report)
        {
            var image = Properties.Resources.Bank_All;
            var point = MainScreen.FindImage(screenshot, topLeft, bottmRight, image, colorRange, SelectedMonitor);
            if (!point.IsEmpty && (point.X != 0 && point.Y != 0))
            {
                var offsetX = RandomGenerate.Next(0, image.Width);
                var offsetY = RandomGenerate.Next(0, image.Height);

                Mouse.Mouse.MoveTo(point.X + offsetX, point.Y + offsetY);

                Thread.Sleep(100);

                Mouse.Mouse.LeftClick();
                report.Report("Clicked at X:" + point.X + "  Y:" + point.Y);

                Thread.Sleep(RandomGenerate.Next(2000, 4000));
                return true;
            }
            Thread.Sleep(RandomGenerate.Next(3000, 3000));
            return false;
        }

        private bool CloseBank(Bitmap screenshot, Point topLeft, Point bottmRight, decimal colorRange, IProgress<string> report)
        {
            var image = Properties.Resources.BankCloseButton;
            var point = MainScreen.FindImage(screenshot, topLeft, bottmRight, image, colorRange, SelectedMonitor);
            //var point = MainScreen.FindColorScreenRange(screenshot, topLeft, BottomRight, SearchColor2, 8, ColorRange, SelectedMonitor);
            if (!point.IsEmpty && (point.X != 0 && point.Y != 0))
            {
                var offsetX = RandomGenerate.Next(0, image.Width);
                var offsetY = RandomGenerate.Next(0, image.Height);
                Mouse.Mouse.MoveTo(point.X + offsetX, point.Y + offsetY);

                Thread.Sleep(100);

                Mouse.Mouse.LeftClick();
                report.Report("Clicked at X:" + point.X + "  Y:" + point.Y);

                Thread.Sleep(RandomGenerate.Next(2000, 4000));
                return true;
            }
            Thread.Sleep(RandomGenerate.Next(3000, 3000));
            return false;
        }

        private void btn_Monitor_1_Click(object sender, EventArgs e)
        {
            btn_Monitor_1.BackColor = Color.Red;
            btn_Monitor_2.BackColor = Color.Transparent;
            btn_Monitor_3.BackColor = Color.Transparent;
            SelectedMonitor = 1;
        }

        private void btn_Monitor_2_Click(object sender, EventArgs e)
        {
            btn_Monitor_2.BackColor = Color.Red;
            btn_Monitor_1.BackColor = Color.Transparent;
            btn_Monitor_3.BackColor = Color.Transparent;
            SelectedMonitor = 2;
        }

        private void btn_Monitor_3_Click(object sender, EventArgs e)
        {
            btn_Monitor_3.BackColor = Color.Red;
            btn_Monitor_1.BackColor = Color.Transparent;
            btn_Monitor_2.BackColor = Color.Transparent;
            SelectedMonitor = 3;
        }

        private void btn_Find_Color_Click(object sender, EventArgs e)
        {
            FindingColor = !FindingColor;
            if (FindingColor)
            {
                btn_Find_Color.BackColor = Color.Red;
            }
            else
            {
                btn_Find_Color.BackColor = Color.Transparent;
            }
        }

        private void SetGlobalDetails()
        {
            if (!string.IsNullOrEmpty(txt_Color_R.Text) || !string.IsNullOrEmpty(txt_Color_G.Text) || !string.IsNullOrEmpty(txt_Color_B.Text))
            {
                SearchColor = Color.FromArgb(int.Parse(txt_Color_R.Text), int.Parse(txt_Color_G.Text), int.Parse(txt_Color_B.Text));
            }
            else
            {
                SearchColor = Color.FromArgb(212, 0, 255);
            }

            if (!string.IsNullOrEmpty(txt_Color_R_2.Text) || !string.IsNullOrEmpty(txt_Color_G_2.Text) || !string.IsNullOrEmpty(txt_Color_B_2.Text))
            {
                SearchColor2 = Color.FromArgb(int.Parse(txt_Color_R_2.Text), int.Parse(txt_Color_G_2.Text), int.Parse(txt_Color_B_2.Text));
            }
            else
            {
                SearchColor2 = Color.FromArgb(255, 255, 0);
            }

            if (!string.IsNullOrEmpty(txt_Inv_Top_X.Text) || !string.IsNullOrEmpty(txt_Inv_Top_Y.Text))
            {
                InvTopLeft = new Point(int.Parse(txt_Inv_Top_X.Text), int.Parse(txt_Inv_Top_Y.Text));
            }
            else
            {
                InvTopLeft = new Point(2090, 1100);
            }

            if (!string.IsNullOrEmpty(txt_Inv_Bot_X.Text) || !string.IsNullOrEmpty(txt_Inv_Bot_Y.Text))
            {
                InvBottomRight = new Point(int.Parse(txt_Inv_Bot_X.Text), int.Parse(txt_Inv_Bot_Y.Text));
            }
            else
            {
                InvBottomRight = new Point(2270, 1350);
            }

            if (!string.IsNullOrEmpty(txt_Screen_Top_X.Text) || !string.IsNullOrEmpty(txt_Screen_Top_Y.Text))
            {
                TopLeft = new Point(int.Parse(txt_Screen_Top_X.Text), int.Parse(txt_Screen_Top_Y.Text));
            }
            else
            {
                TopLeft = new Point(200, 200);
            }

            if (!string.IsNullOrEmpty(txt_Screen_Bot_X.Text) || !string.IsNullOrEmpty(txt_Screen_Bot_Y.Text))
            {
                BottomRight = new Point(int.Parse(txt_Screen_Bot_X.Text), int.Parse(txt_Screen_Bot_Y.Text));
            }
            else
            {
                BottomRight = new Point(2040, 1350);
            }

            if (!string.IsNullOrEmpty(txt_Pixel_Skip.Text))
            {
                PixelSkip = int.Parse(txt_Pixel_Skip.Text);
            }
            else
            {
                PixelSkip = 8;
            }

            TimeoutLengthMin = radio_Short_Timeouts.Checked ? int.Parse(txt_Short_Timeout_Min.Text) : int.Parse(txt_Long_Timeout_Min.Text);
            TimeoutLengthMax = radio_Short_Timeouts.Checked ? int.Parse(txt_Short_Timeout_Max.Text) : int.Parse(txt_Long_Timeout_Max.Text);

            TopLeft = MainScreen.ModifyFromMonitorPoint(TopLeft, SelectedMonitor);
            BottomRight = MainScreen.ModifyFromMonitorPoint(BottomRight, SelectedMonitor);
            InvTopLeft = MainScreen.ModifyFromMonitorPoint(InvTopLeft, SelectedMonitor);
            InvBottomRight = MainScreen.ModifyFromMonitorPoint(InvBottomRight, SelectedMonitor);

            ColorRange = 360m * (sliderColorRange.Value / 100m);
            ImageRange = 360m * (slider_Image_Range.Value / 100m);
        }

        private void chk_End_Timeout_Only_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            if (chkBox.Checked)
            {
                EndTimeoutsOnly = true;
            }

            else
                EndTimeoutsOnly = false;
        }


        private void worker_Normal_Clicks_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            Timeouts timeouts = runParams.Timeouts;
            var timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
            var timeoutPos = RandomGenerate.Next(0, Clicks.Count - 1);
            ScreenshotInfo screenshotInfo = runParams.ScreenshotInfo;
            var clickList = runParams.ClickList;
            bool result = false;

            if (runCount == 0)
                runCount = 99999;

            var step = 0;


            report.Report("Starting Normal Clicks Worker");
            report.Report("Total Runs = " + runCount);
            Thread.Sleep(3000);


            while (!worker_Normal_Clicks.CancellationPending && runCount > 0)
            {
                if (timeouts.Active
                    && !timeouts.EndTimeoutsOnly
                    && timeoutCount <= 0
                    && step == timeoutPos)
                {
                    timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
                    var timeoutnum = RandomGenerate.Next(timeouts.TimeoutLengthMin, timeouts.TimeoutLengthMax);
                    report.Report("Pausing for : " + timeoutnum);
                    report.Report("New count limit : " + timeoutCount);
                    Thread.Sleep(timeoutnum);
                }

                result = PerformClick(clickList[step], screenshotInfo, report);

                if(clickList[step].ClickScript != null)
                {
                    ProcessScript(ref step, clickList[step].ClickScript, result, report);
                }
                else
                {
                    step++;
                }

                if (step == -1)
                    break;

                if (step > clickList.Count - 1)
                {
                    runCount--;
                    step = 0;
                }

                if (timeouts.Active)
                {
                    if(step == 0)
                        timeoutCount--;
                    if (timeouts.EndTimeoutsOnly && timeoutCount <= 0)
                    {
                        timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
                        var timeoutnum = RandomGenerate.Next(timeouts.TimeoutLengthMin, timeouts.TimeoutLengthMax);
                        report.Report("Pausing for : " + timeoutnum);
                        report.Report("New count limit : " + timeoutCount);
                        Thread.Sleep(timeoutnum);
                    }
                }
            }

            report.Report("Ending Normal Clicks Worker");
        }

        private void ProcessScript(ref int step, UserScript clickScript, bool result, IProgress<string> report)
        {
            if((!result && clickScript.ClickResult == Result.FAIL) || (result && clickScript.ClickResult == Result.SUCCESS))
            {
                if (clickScript.ResultAction == Action.REPEAT)
                    return;
                if (clickScript.ResultAction == Action.STOP)
                    step = -1;
                else if (clickScript.ResultAction == Action.GOTO)
                {
                    if (clickScript.GoToSequence == 0)
                    {
                        report.Report("GoToSequence is 0 or not set. Continuing to next step.");
                        step++;
                    }
                    else
                    {
                        step = clickScript.GoToSequence;
                    }
                }
                else
                    step++;
            }
            else
            {
                step++;
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(RunProgram)
                RunProgram = !RunProgram;
            UpdateButtons(CurrentWorker, false);
        }

        private bool PerformClick(Click click, ScreenshotInfo info, IProgress<string> report)
        {
            var topLeftPoint = TopLeft;
            var botRigthPoint = BottomRight;
            var checkOnly = false;
            if(click.ClickScript != null)
            {
                if(click.ClickScript.ClickOptions != null)
                {
                    topLeftPoint = click.ClickScript.ClickOptions.SearchAreaTopLeft;
                    botRigthPoint = click.ClickScript.ClickOptions.SearchAreaBottomRight;
                }
                if (click.ClickScript.CheckOnlyNoClick)
                    checkOnly = true;
                
            }
            var point = click.ClickPoint;
            if (!click.ClickColor.IsEmpty)
            {
                using (var screenShot = MainScreen.CaptureScreen(SelectedMonitor))
                {
                    point = MainScreen.FindColorScreenRange(screenShot, topLeftPoint, botRigthPoint, click.ClickColor, PixelSkip, ColorRange, SelectedMonitor);

                    if(point.IsEmpty && !click.ClickColor2.IsEmpty)
                    {
                        point = MainScreen.FindColorScreenRange(screenShot, topLeftPoint, botRigthPoint, click.ClickColor2, PixelSkip, ColorRange, SelectedMonitor);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(click.ClickImagePath))
            {
                using (var screenShot = MainScreen.CaptureScreen(SelectedMonitor))
                {
                    point = MainScreen.FindImage(screenShot, topLeftPoint, botRigthPoint, click.ClickImage, ImageRange, SelectedMonitor);
                }
            }

            if (!point.IsEmpty || click.ClickEmptyPoint)
            {
                if (!checkOnly)
                {
                    if (!click.ClickEmptyPoint)
                    {
                        var offsetX = RandomGenerate.Next(0, click.ClickOffset);
                        var offsetY = RandomGenerate.Next(0, click.ClickOffset);
                        Mouse.Mouse.MoveTo(point.X + offsetX, point.Y + offsetY);
                        Thread.Sleep(100);
                    }
                   

                    Mouse.Mouse.LeftClick();
                    report.Report(string.Format("Step {0} : Clicked at X:{1} Y:{2}", click.ClickSequence, point.X, point.Y));

                    Thread.Sleep(RandomGenerate.Next((int)click.DelayAfterClick - 200, (int)click.DelayAfterClick + 200));
                }
                else
                {
                    report.Report(string.Format("Step {0} : Check found at X:{1} Y:{2}", click.ClickSequence, point.X, point.Y));
                    if(click.DelayAfterClick != 0)
                    {
                        Thread.Sleep(RandomGenerate.Next((int)click.DelayAfterClick - 200, (int)click.DelayAfterClick + 200));
                    }
                }

                return true;
            }
            //else
            //    Thread.Sleep(1000);

            return false;
        }

        private void PerformLeftClick(Point point, IProgress<string> report)
        {
            Mouse.Mouse.MoveTo(point.X, point.Y);

            Thread.Sleep(100);

            Mouse.Mouse.LeftClick();
            if(report != null)
                report.Report("Clicked at X:" + point.X + "  Y:" + point.Y);
        }

        private void radio_Short_Timeouts_CheckedChanged(object sender, EventArgs e)
        {
            var radio = (RadioButton)sender;
            if (radio.Checked)
            {
                TimeoutLengthMin = int.Parse(txt_Short_Timeout_Min.Text);
                TimeoutLengthMax = int.Parse(txt_Short_Timeout_Max.Text);
            }
            else
            {
                TimeoutLengthMin = int.Parse(txt_Long_Timeout_Min.Text);
                TimeoutLengthMax = int.Parse(txt_Long_Timeout_Max.Text);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OpenFile))
            {
                saveAsToolStripMenuItem.PerformClick();
                return;
            }
            else
            {
                var fileName = Path.GetFileName(OpenFile);
                if (MessageBox.Show(string.Format("Overwrite current file '{0}' ?", fileName), "Overwrite", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SaveFile(OpenFile);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileName = "";
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Save File Dialog";
            sfd.InitialDirectory = AppFolder;
            sfd.Filter = "All files (*.*)|*.*|xml files (*.xml)|*.xml";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = false;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                LogWrite("Saving " + sfd.FileName);
                fileName = sfd.FileName;
                //if ((myStream = sfd.OpenFile()) != null)
                //{
                //    // Code to write the stream goes here.
                //    myStream.Close();
                //}
                SaveFile(fileName);
                OpenFile = fileName;
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fileName = "";
            var fd = new OpenFileDialog();
            fd.Title = "Open File Dialog";
            fd.InitialDirectory = AppFolder;
            fd.Filter = "All files (*.*)|*.*|xml files (*.xml)|*.xml";
            fd.FilterIndex = 2;
            fd.RestoreDirectory = false;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                LogWrite("Loading " + fd.FileName);
                fileName = fd.FileName;
                Clicks.Clear();
                using (StreamReader reader = new StreamReader(fileName))
                {

                    var data = reader.ReadToEnd();
                    var list = FromXML<List<Click>>(data);
                    foreach(var click in list)
                    {
                        Clicks.Add(click);
                    }
                }

                OpenFile = fileName;
            }
                
        }

        private void btn_Add_Click_Click(object sender, EventArgs e)
        {
            Clicks.Add(new Click(Clicks.Count, Point.Empty, 0, 0, ClickOffset)
            {
                //ClickScript = new UserScript()
                //{
                //    ClickResult = 0,
                //    ResultAction = 0,
                //    ClickOptions = new ClickOptions()
                //    {
                //        SearchAreaTopLeft = new Point(0, 0),
                //        SearchAreaBottomRight = new Point(0, 0)
                //    },
                //    GoToSequence = 2
                //}
            }) ;
        }

        private void btn_Move_Click_Up_Click(object sender, EventArgs e)
        {
            if (!SelectFullRow())
                return;
            var selectedRow = dg_Clicks.SelectedRows[0];
            var click = (Click)selectedRow.DataBoundItem;
            if (click.ClickSequence == 0)
                return;
            var index = Clicks.IndexOf(click);
            var swapClick = Clicks[index - 1];
            var temp = click.ClickSequence;
            click.ClickSequence = swapClick.ClickSequence;
            swapClick.ClickSequence = temp;
            Clicks.RemoveAt(index);
            Clicks.Insert(index - 1, click);

            dg_Clicks.ClearSelection();
            dg_Clicks.Rows[index - 1].Selected = true;

        }

        private void btn_Move_Click_Down_Click(object sender, EventArgs e)
        {
            if (!SelectFullRow())
                return;
            var selectedRow = dg_Clicks.SelectedRows[0];
            var click = (Click)selectedRow.DataBoundItem;
            if (click.ClickSequence == Clicks.Count - 1)
                return;
            var index = Clicks.IndexOf(click);
            var swapClick = Clicks[index + 1];
            var temp = click.ClickSequence;
            click.ClickSequence = swapClick.ClickSequence;
            swapClick.ClickSequence = temp;

            Clicks.RemoveAt(index);
            Clicks.Insert(index + 1, click);

            dg_Clicks.ClearSelection();
            dg_Clicks.Rows[index + 1].Selected = true;
        }

        private bool SelectFullRow()
        {
            if(dg_Clicks.SelectedRows.Count > 0)
                return true;
            if (dg_Clicks.SelectedCells.Count > 0)
            {
                var selectedRow = dg_Clicks.SelectedCells[0].OwningRow;
                dg_Clicks.Rows[selectedRow.Index].Selected = true;
                return true;
            }
            else
            {
                MessageBox.Show("Please select a row");
                return false;
            }
        }

        private void SaveFile(string filename)
        {
            using (StreamWriter myStream = new StreamWriter(filename, false))
            {
                var data = ToXML(Clicks);
                myStream.Write(data);
                myStream.Close();
            }
        }

        private static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }

        private string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        private void btn_Copy_Color_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(string.Join(",", new string[] {txt_Find_Color_A.Text, txt_Find_Color_R.Text, txt_Find_Color_G.Text, txt_Find_Color_B.Text}));
        }

        private void slider_Image_Range_Scroll(object sender, EventArgs e)
        {
            lbl_Image_Range.Text = (100 - slider_Image_Range.Value).ToString() + "%";
        }
    }
}

