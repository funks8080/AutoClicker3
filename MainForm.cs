using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AutoClicker.ImageFinder;
using gma.System.Windows;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace AutoClicker 
{
    class MainForm : System.Windows.Forms.Form, IProgress<string>
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelMousePosition;
        private TextBox textBox;
        private bool RunProgram;
        private bool logClicks = true;
        private Color SearchColor;
        private Color SearchColor2;
        private Color MonsterHealth = Color.FromArgb(4, 136, 52);
        private Point TopLeft;
        private Point BottomRight;
        private Point InvTopLeft;
        private Point InvBottomRight;
        private decimal ColorRange;
        private decimal ImageRange;
        private int SelectedMonitor = 2;
        private int PixelSkip = 10;
        private int TimeoutLengthMin;
        private int TimeoutLengthMax;
        private bool FindingColor;

        private static string FilePath = "c:\\AppData\\AutoClicker\\Inventory.txt";
        private static string AppFolder = @"c:\AppData\AutoClicker\";
        private string OpenFile;
        private string OpenInventoryFile;
        private bool HideButtons;
        private bool Ctrl;
        private bool LogInfo;
        private bool RecordClicks;
        private bool SettingAlchPoint;
        private int ClickOffset;
        private int ClickCountPos;
        private Stopwatch ClickStopwatch;
        private BindingList<Click> Clicks;
        private Random RandomGenerate;
        private Inventory InventoryInfo;

        private Label label1;
        private Label label2;
        private GroupBox groupBox2;
        private CheckBox chkDropInverse;
        private GroupBox groupBox3;
        private NumericUpDown numCount;
        private CheckBox chkLog;
        private Label lblClickOffsetNumber;
        private Label label7;
        private Label lblClickOffset;
        private TrackBar sliderClickOffset;
        private CheckBox chkTimeOut;
        private ContextMenuStrip contextMenuStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem toggleLoggingToolStripMenuItem;
        private Label lblTotalInventory;
        private Label label8;
        private Label label4;
        private NumericUpDown numInventoryCount;
        private GroupBox groupBox1;
        private Label lblColorRange;
        private CheckBox chkClicks;
        private BackgroundWorker worker_Gem_Mining;
        private BackgroundWorker worker_Auto_Attack;
        private TrackBar sliderColorRange;
        private BackgroundWorker workerAlch;
        private CheckBox chk_End_Timeout_Only;
        private TabPage ClickTab;
        private TabPage Buttons;
        private GroupBox groupBox7;
        private Label label19;
        private Label label20;
        private Label label21;
        private TextBox txt_Color_B_2;
        private TextBox txt_Color_G_2;
        private TextBox txt_Color_R_2;
        private Label label18;

        private Button buttonRecord;
        private Button btn_Start;
        private Button btn_Find_Image;
        private Button btnHide;
        private Button btnSetupInventory;
        private Button btnSingleClickInv;
        private Button btn_Monitor_3;
        private Button btn_Monitor_2;
        private Button btn_Monitor_1;
        private Button btn_Gem_Mine;
        private Button btnStartAlch;
        private Button btnSetAlch;
        private Button btn_Auto_Attack;
        private Button btn_Find_Color;
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
        private TabControl TabController;
        private DataGridView dg_Clicks;
        private Button btn_Add_Click;
        private BackgroundWorker worker_Normal_Clicks;
        private BackgroundWorker worker_Inventory_Clicks;
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
        private Label label3;
        private TextBox txt_Step_Start;
        private Label label5;
        private Label label6;
        private TextBox txt_Inv_Height;
        private TextBox txt_Inv_Width;
        private ToolStripMenuItem inventoryToolStripMenuItem;
        private ToolStripMenuItem saveInventoryToolStripMenuItem;
        private ToolStripMenuItem saveAsInventoryToolStripMenuItem;
        private ToolStripMenuItem loadInventoryToolStripMenuItem;
        BackgroundWorker CurrentWorker;

        public MainForm()
        {
            try
            {
                InventoryInfo = new Inventory();
                HideButtons = false;
                ClickOffset = 3;
                ClickStopwatch = new Stopwatch();
                LogInfo = true;
                Ctrl = false;
                ClickCountPos = 0;
                Clicks = new BindingList<Click>();
                RecordClicks = false;
                RunProgram = false;
                RandomGenerate = new Random();
                InitializeComponent();
                this.btn_Start.Click += new System.EventHandler((sender, e) => ButtonStart(sender, e, null));
                this.btn_Gem_Mine.Click += new System.EventHandler((sender, e) => ButtonStart(sender, e, worker_Gem_Mining));
                this.btn_Auto_Attack.Click += new System.EventHandler((sender, e) => ButtonStart(sender, e, worker_Auto_Attack));
                stopToolStripMenuItem.Enabled = false;
                AlchPoint = new Point();
                SettingAlchPoint = false;
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
                //LoadInventory();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Step_Start = new System.Windows.Forms.TextBox();
            this.lblClickOffsetNumber = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblClickOffset = new System.Windows.Forms.Label();
            this.sliderClickOffset = new System.Windows.Forms.TrackBar();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTotalInventory = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numInventoryCount = new System.Windows.Forms.NumericUpDown();
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
            this.toggleLoggingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clicksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chkClicks = new System.Windows.Forms.CheckBox();
            this.worker_Gem_Mining = new System.ComponentModel.BackgroundWorker();
            this.worker_Auto_Attack = new System.ComponentModel.BackgroundWorker();
            this.workerAlch = new System.ComponentModel.BackgroundWorker();
            this.ClickTab = new System.Windows.Forms.TabPage();
            this.btn_Move_Click_Down = new System.Windows.Forms.Button();
            this.btn_Move_Click_Up = new System.Windows.Forms.Button();
            this.btn_Add_Click = new System.Windows.Forms.Button();
            this.dg_Clicks = new System.Windows.Forms.DataGridView();
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
            this.Buttons = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
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
            this.label6 = new System.Windows.Forms.Label();
            this.txt_Inv_Height = new System.Windows.Forms.TextBox();
            this.txt_Inv_Width = new System.Windows.Forms.TextBox();
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
            this.btn_Auto_Attack = new System.Windows.Forms.Button();
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
            this.worker_Inventory_Clicks = new System.ComponentModel.BackgroundWorker();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderClickOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
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
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txt_Step_Start);
            this.groupBox3.Controls.Add(this.lblClickOffsetNumber);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblClickOffset);
            this.groupBox3.Controls.Add(this.sliderClickOffset);
            this.groupBox3.Controls.Add(this.numCount);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(6, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(383, 282);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Clicks";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Step Start Number";
            // 
            // txt_Step_Start
            // 
            this.txt_Step_Start.Location = new System.Drawing.Point(15, 154);
            this.txt_Step_Start.MaxLength = 6;
            this.txt_Step_Start.Name = "txt_Step_Start";
            this.txt_Step_Start.Size = new System.Drawing.Size(49, 20);
            this.txt_Step_Start.TabIndex = 43;
            this.txt_Step_Start.Text = "0";
            // 
            // lblClickOffsetNumber
            // 
            this.lblClickOffsetNumber.AutoSize = true;
            this.lblClickOffsetNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickOffsetNumber.Location = new System.Drawing.Point(327, 92);
            this.lblClickOffsetNumber.Name = "lblClickOffsetNumber";
            this.lblClickOffsetNumber.Size = new System.Drawing.Size(14, 16);
            this.lblClickOffsetNumber.TabIndex = 23;
            this.lblClickOffsetNumber.Text = "3";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(319, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Pixels";
            // 
            // lblClickOffset
            // 
            this.lblClickOffset.AutoSize = true;
            this.lblClickOffset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClickOffset.Location = new System.Drawing.Point(26, 73);
            this.lblClickOffset.Name = "lblClickOffset";
            this.lblClickOffset.Size = new System.Drawing.Size(85, 16);
            this.lblClickOffset.TabIndex = 21;
            this.lblClickOffset.Text = "Click Offset";
            // 
            // sliderClickOffset
            // 
            this.sliderClickOffset.LargeChange = 1;
            this.sliderClickOffset.Location = new System.Drawing.Point(12, 92);
            this.sliderClickOffset.Name = "sliderClickOffset";
            this.sliderClickOffset.Size = new System.Drawing.Size(286, 45);
            this.sliderClickOffset.TabIndex = 20;
            this.sliderClickOffset.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.sliderClickOffset.Value = 3;
            this.sliderClickOffset.Scroll += new System.EventHandler(this.sliderClickOffset_Scroll);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblTotalInventory);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numInventoryCount);
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
            // chkDropInverse
            // 
            this.chkDropInverse.AutoSize = true;
            this.chkDropInverse.Location = new System.Drawing.Point(12, 19);
            this.chkDropInverse.Name = "chkDropInverse";
            this.chkDropInverse.Size = new System.Drawing.Size(111, 17);
            this.chkDropInverse.TabIndex = 0;
            this.chkDropInverse.Text = "Drop From Bottom";
            this.chkDropInverse.UseVisualStyleBackColor = true;
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
            this.clicksToolStripMenuItem,
            this.inventoryToolStripMenuItem});
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
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // inventoryToolStripMenuItem
            // 
            this.inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveInventoryToolStripMenuItem,
            this.saveAsInventoryToolStripMenuItem,
            this.loadInventoryToolStripMenuItem});
            this.inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
            this.inventoryToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.inventoryToolStripMenuItem.Text = "Inventory";
            // 
            // saveInventoryToolStripMenuItem
            // 
            this.saveInventoryToolStripMenuItem.Name = "saveInventoryToolStripMenuItem";
            this.saveInventoryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveInventoryToolStripMenuItem.Text = "Save";
            this.saveInventoryToolStripMenuItem.Click += new System.EventHandler(this.saveInventoryToolStripMenuItem_Click);
            // 
            // saveAsInventoryToolStripMenuItem
            // 
            this.saveAsInventoryToolStripMenuItem.Name = "saveAsInventoryToolStripMenuItem";
            this.saveAsInventoryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveAsInventoryToolStripMenuItem.Text = "Save As";
            this.saveAsInventoryToolStripMenuItem.Click += new System.EventHandler(this.saveAsInventoryToolStripMenuItem_Click);
            // 
            // loadInventoryToolStripMenuItem
            // 
            this.loadInventoryToolStripMenuItem.Name = "loadInventoryToolStripMenuItem";
            this.loadInventoryToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadInventoryToolStripMenuItem.Text = "Load";
            this.loadInventoryToolStripMenuItem.Click += new System.EventHandler(this.loadInventoryToolStripMenuItem_Click);
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
            // Buttons
            // 
            this.Buttons.Controls.Add(this.label5);
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
            this.Buttons.Controls.Add(this.btn_Auto_Attack);
            this.Buttons.Location = new System.Drawing.Point(4, 22);
            this.Buttons.Name = "Buttons";
            this.Buttons.Size = new System.Drawing.Size(605, 523);
            this.Buttons.TabIndex = 2;
            this.Buttons.Text = "Buttons";
            this.Buttons.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(526, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Height";
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
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.txt_Inv_Height);
            this.groupBox6.Controls.Add(this.txt_Inv_Width);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(131, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 39;
            this.label6.Text = "Width";
            // 
            // txt_Inv_Height
            // 
            this.txt_Inv_Height.Location = new System.Drawing.Point(134, 89);
            this.txt_Inv_Height.MaxLength = 5;
            this.txt_Inv_Height.Name = "txt_Inv_Height";
            this.txt_Inv_Height.Size = new System.Drawing.Size(49, 20);
            this.txt_Inv_Height.TabIndex = 12;
            this.txt_Inv_Height.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
            // 
            // txt_Inv_Width
            // 
            this.txt_Inv_Width.Location = new System.Drawing.Point(134, 40);
            this.txt_Inv_Width.MaxLength = 5;
            this.txt_Inv_Width.Name = "txt_Inv_Width";
            this.txt_Inv_Width.Size = new System.Drawing.Size(49, 20);
            this.txt_Inv_Width.TabIndex = 11;
            this.txt_Inv_Width.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Number_Only_KeyPress);
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
            // worker_Inventory_Clicks
            // 
            this.worker_Inventory_Clicks.WorkerReportsProgress = true;
            this.worker_Inventory_Clicks.WorkerSupportsCancellation = true;
            this.worker_Inventory_Clicks.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_Inventory_Clicks_DoWork);
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

            if(worker_Inventory_Clicks.IsBusy && !CurrentWorker.IsBusy)
            {
                worker_Inventory_Clicks.CancelAsync();
                return;
            }

            if (CurrentWorker == worker_Normal_Clicks && Clicks.Count < 1 && !RunProgram && !worker_Inventory_Clicks.IsBusy)
            {
                MessageBox.Show("Need to have at least one click recored");
                return;
            }
            else if(CurrentWorker == worker_Normal_Clicks && Clicks.Count < 1 && !RunProgram && worker_Inventory_Clicks.IsBusy)
            {
                worker_Inventory_Clicks.CancelAsync();
                return;
            }


            RunProgram = !RunProgram;
            if (RunProgram)
            {
                UpdateButtons(CurrentWorker, true);
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
                    TimeoutLengthMax = TimeoutLengthMax,
                    EndTimeoutsOnly = chk_End_Timeout_Only.Checked
                };
                runParams.RunLimit = (int)numCount.Value;
                runParams.ClickList = Clicks.ToList();
                runParams.StartStep = int.Parse(txt_Step_Start.Text);
                CurrentWorker.RunWorkerAsync(runParams);
            }
            else
            {
                LogWrite("Cancelling Clicker");
                CurrentWorker.CancelAsync();
                worker_Inventory_Clicks.CancelAsync();
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
            }
        }

        public void MyKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LControlKey)
                Ctrl = false;
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
                        this.btn_Start.PerformClick();
                        break;
                    case Keys.L:
                        chkLog.Checked = !chkLog.Checked;
                        break;
                    default:
                        break;
                }

                Ctrl = false;
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                        Ctrl = true;
                        break;
                }
            }
        }

        public void StopAutoClicker()
        {
            numCount.Enabled = true;
            buttonRecord.Enabled = true;
            RunProgram = false;
            workerAlch.CancelAsync();

            btn_Start.Text = "Start";
            LogWrite("Stopping Auto Clicker");
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

        private void ClickInventory()
        {
            if (InventoryInfo.InventoryClicks.Count < 1)
            {
                MessageBox.Show("Need to setup inventory first.");
                return;
            }

            if (worker_Inventory_Clicks.IsBusy)
                return;

            List<Click> clickOrder = BuildInventoryClickList(InventoryInfo.InventoryClicks);

            var runParams = new RunParams<string>();
            runParams.ReportProgress = new Progress<string>(value => LogWrite(value));
            runParams.RunLimit = (int)numInventoryCount.Value;
            runParams.ClickList = clickOrder;

            worker_Inventory_Clicks.RunWorkerAsync(runParams);
        }

        private void BuildInventoryClicks()
        {
            InventoryInfo.InventoryClicks.Clear();

            InventoryInfo.TopLeftX = int.Parse(txt_Inv_Top_X.Text);
            InventoryInfo.TopLeftY = int.Parse(txt_Inv_Top_Y.Text);
            InventoryInfo.BottomRightX = int.Parse(txt_Inv_Bot_X.Text);
            InventoryInfo.BottomRightY = int.Parse(txt_Inv_Bot_Y.Text);
            InventoryInfo.InventoryWidth = int.Parse(txt_Inv_Width.Text);
            InventoryInfo.InventoryHeight = int.Parse(txt_Inv_Height.Text);

            var totalPixelWidth = Math.Abs(InventoryInfo.BottomRightX - InventoryInfo.TopLeftX);
            var totalPixelHeight = Math.Abs(InventoryInfo.BottomRightY - InventoryInfo.TopLeftY);

            var invSpaceWidth = totalPixelWidth / InventoryInfo.InventoryWidth;
            var invSpaceHeight = totalPixelHeight / InventoryInfo.InventoryHeight;
            var halfWidth = invSpaceWidth / 2;
            var halfHeight = invSpaceHeight / 2;

            var clickSequence = 0;
            for(var j = 0; j < InventoryInfo.InventoryHeight; j++)
            {
                for(var i = 0; i < InventoryInfo.InventoryWidth; i++)
                {
                    InventoryInfo.InventoryClicks.Add(new Click()
                    {
                        ClickPoint = new Point(InventoryInfo.TopLeftX + halfWidth + (invSpaceWidth * i), InventoryInfo.TopLeftY + halfHeight + (invSpaceHeight * j)),
                        DelayAfterClick = RandomGenerate.Next(250, 450),
                        ClickOffset = (halfWidth / 2),
                        ClickSequence = ++clickSequence
                    });
                }
            }

            lblTotalInventory.Text = InventoryInfo.InventoryClicks.Count.ToString();
            numInventoryCount.Value = InventoryInfo.InventoryClicks.Count;
        }

        private List<Click> BuildInventoryClickList(List<Click> inventoryClicks)
        {
            List<Click> returnList = new List<Click>();

            var snake = RandomGenerate.Next(0, 3) > 1;
            var reverse = RandomGenerate.Next(0, 3) > 1;
            var byRow = RandomGenerate.Next(0, 3) > 1;

            var selectedInv = (int)numInventoryCount.Value;
            Click[] clickArray = new Click[inventoryClicks.Count];
            var invTotal = clickArray.Count();
            inventoryClicks.CopyTo(clickArray);
            var invDiff = invTotal - selectedInv;
            if (invDiff > 0)
            {
                if (chkDropInverse.Checked)
                {
                    for(int i = 0; i < invDiff; i++)
                    {
                        clickArray[i] = null;
                    }
                }
                else
                {
                    for (int i = clickArray.Count()-1; i >= selectedInv; i--)
                    {
                        clickArray[i] = null;
                    }
                }
            }

            //Console.WriteLine(string.Format("ByRow: {0}    Reverse: {1}    Snake: {2}", byRow, reverse, snake));

            var step = 1;
            var rowMove = 4;
            var start = 0;
            var moveRight = true;
            var moveDown = true;
            var invChecked = 0;

            if (reverse)
            {
                step = -1;
                rowMove = -4;
                start = invTotal - 1;
                moveRight = !moveRight;
                moveDown = !moveDown;
            }

            //click by row
            if (byRow)
            {
                for (int i = start; invChecked < invTotal;)
                {
                    invChecked++;
                    if (clickArray[i] != null)
                    {
                        returnList.Add(clickArray[i]);
                    }

                    if (snake)
                    {
                        if ((moveRight && i % 4 == 3 ) || (!moveRight && i % 4 == 0))
                        {
                            i += rowMove;
                            step = -step;
                            moveRight = !moveRight;
                        }
                        else
                        {
                            i += step;
                        }
                    }
                    else
                    {
                        i += step;
                    }
                }
            }
            else // Click by column
            {
                for (int i = start; invChecked < invTotal;)
                {
                    invChecked++;
                    if (clickArray[i] != null)
                    {
                        returnList.Add(clickArray[i]);
                    }

                    if (snake)
                    {
                        if ((moveDown && i + rowMove >= invTotal) || (!moveDown && i + rowMove <= 0))
                        {
                            i += step;
                            rowMove = -rowMove;
                            moveDown = !moveDown;
                        }
                        else
                        {
                            i += rowMove;
                        }
                    }
                    else
                    {
                        if((reverse && i + rowMove < 0) || (!reverse && i + rowMove >= invTotal))
                        {
                            if (reverse)
                            {
                                i = invTotal - (4 - i);
                                i += step;
                            }
                            else
                            {
                                i = i % 4;
                                i += step;
                            }
                        }
                        else
                        {
                            i += rowMove;
                        }
                    }
                }
            }

            return returnList;
        }

        private void btnSetupInventory_Click(object sender, EventArgs e)
        {
            //if (SetupInventory)
            //{
            //    btnSetupInventory.Text = "Setup Inventory";
            //    InventoryClicks.RemoveAt(InventoryClicks.Count - 1);
            //    lblTotalInventory.Text = InventoryClicks.Count.ToString();
            //    numInventoryCount.Value = InventoryClicks.Count;
            //}
            //else
            //{
            //    btnSetupInventory.Text = "Recording Inventory...";
            //    InventoryClicks.Clear();
            //    ClickCountPos = 0;
            //}

            //SetupInventory = !SetupInventory;

            BuildInventoryClicks();
        }

        private void DisableAll()
        {
            btn_Start.Enabled = false;
            chkLog.Enabled = false;
            chkDropInverse.Enabled = false;
            btnSetupInventory.Enabled = false;
            btnSingleClickInv.Enabled = false;
            btn_Find_Color.Enabled = false;
        }

        private void EnableAll()
        {
            btn_Start.Enabled = true;
            chkLog.Enabled = true;
            chkDropInverse.Enabled = true;
            btnSetupInventory.Enabled = true;
            btnSingleClickInv.Enabled = true;
            btn_Find_Color.Enabled = true;
        }

        private void btnSingleClickInv_Click(object sender, EventArgs e)
        {
            ClickInventory();
        }

        private void sliderClickOffset_Scroll(object sender, EventArgs e)
        {
            TrackBar slider = (TrackBar)sender;
            lblClickOffsetNumber.Text = slider.Value.ToString();
            ClickOffset = slider.Value;
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

        private void numInventoryCount_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown counter = (NumericUpDown)sender;
            if ((int)counter.Value > InventoryInfo.InventoryClicks.Count)
            {
                counter.Value = InventoryInfo.InventoryClicks.Count;
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

            LogWrite(string.Format("X:{0} Y:{1}", point.X, point.Y));

            PerformLeftClick(new Point(point.X + offsetX, point.Y + offsetY), null);
        }

        private void sliderColorRange_Scroll(object sender, EventArgs e)
        {
            lblColorRange.Text = (100 - sliderColorRange.Value).ToString() + "%";
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
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }

                report.Report("Check For Targets Ended");
            }

            report.Report("Ending Auto Attack Worker");

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
            PerformLeftClick(new Point(point.X, point.Y), report);
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

            if (!workerAlch.IsBusy)
            {
                LogWrite("Starting Alching");
                RunProgram = true;
                workerAlch.RunWorkerAsync(runParams);
            }
            else if(workerAlch.IsBusy && !workerAlch.CancellationPending)
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

        private void worker_Normal_Clicks_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            var report = runParams.ReportProgress;
            var runCount = runParams.RunLimit;
            Timeouts timeouts = runParams.Timeouts;
            var timeoutCount = RandomGenerate.Next(timeouts.TimeoutCountMin, timeouts.TimeoutCountMax);
            var timeoutPos = RandomGenerate.Next(0, Clicks.Count - 1);
            var clickList = runParams.ClickList;
            bool result = false;

            if (runCount == 0)
                runCount = 99999;

            var step = 0;

            if(runParams.StartStep != 0)
                step = runParams.StartStep;

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
                    timeoutPos = RandomGenerate.Next(0, Clicks.Count - 1);
                    report.Report("Pausing for : " + timeoutnum);
                    report.Report("New count limit : " + timeoutCount);
                    Thread.Sleep(timeoutnum);
                }

                result = PerformClick(clickList[step], report);

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
                        timeoutPos = RandomGenerate.Next(0, Clicks.Count - 1);
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
                else if(clickScript.ResultAction == Action.PRESS_KEY)
                {

                }
                else if (clickScript.ResultAction == Action.GOTO)
                {
                    if (clickScript.GoToSequence == 0)
                    {
                        report.Report("GoToSequence is 0 or not set. Continuing to next step.");
                        step++;
                    }
                    else
                    {
                        step = clickScript.GoToSequence - 1;
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

        private bool PerformClick(Click click, IProgress<string> report)
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

            if (click.ClickType == 3)
            {
                if (click.ClickScript != null && click.ClickScript.PressKey != Keys.None)
                {
                    var keys = "";
                    if (click.ClickScript.Uppercase)
                        keys = click.ClickScript.PressKey.ToString();
                    else
                        keys = click.ClickScript.PressKey.ToString().ToLower();
                    SendKeys.SendWait(keys);
                }
                    
                report.Report(string.Format("Step {0} : Key pressed: {1} ", click.ClickSequence, click.ClickScript.PressKey.ToString()));

                Thread.Sleep(RandomGenerate.Next((int)click.DelayAfterClick - 200, (int)click.DelayAfterClick + 200));
                return true;
            }
            else
            {

                if (!click.ClickColor.IsEmpty)
                {
                    using (var screenShot = MainScreen.CaptureScreen(SelectedMonitor))
                    {
                        point = MainScreen.FindColorCenterOut(screenShot, topLeftPoint, botRigthPoint, click.ClickColor, ColorRange, PixelSkip, SelectedMonitor);

                        if (point.IsEmpty && !click.ClickColor2.IsEmpty)
                        {
                            point = MainScreen.FindColorCenterOut(screenShot, topLeftPoint, botRigthPoint, click.ClickColor2, ColorRange, PixelSkip, SelectedMonitor);
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
                            point.X += RandomGenerate.Next(-click.ClickOffset, click.ClickOffset);
                            point.Y += RandomGenerate.Next(-click.ClickOffset, click.ClickOffset);
                            Mouse.Mouse.MoveTo(point.X, point.Y);
                            Thread.Sleep(100);
                        }

                        if(click.ClickType == 0)
                            Mouse.Mouse.LeftClick();
                        else if(click.ClickType == 1)
                            Mouse.Mouse.RightClick();
                        report.Report(string.Format("Step {0} : Clicked at X:{1} Y:{2}", click.ClickSequence, point.X, point.Y));

                        Thread.Sleep(RandomGenerate.Next((int)click.DelayAfterClick - 200, (int)click.DelayAfterClick + 200));
                    }
                    else
                    {
                        report.Report(string.Format("Step {0} : Check found at X:{1} Y:{2}", click.ClickSequence, point.X, point.Y));
                        if (click.DelayAfterClick != 0)
                        {
                            Thread.Sleep(RandomGenerate.Next((int)click.DelayAfterClick - 200, (int)click.DelayAfterClick + 200));
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        private void worker_Inventory_Clicks_DoWork(object sender, DoWorkEventArgs e)
        {
            var runParams = e.Argument as RunParams<string>;
            var report = runParams.ReportProgress;
            var clickList = runParams.ClickList;
            bool result = false;

            var step = 0;

            report.Report("Starting Inventory Clicks Worker");

            Thread.Sleep(1000);


            while (!worker_Inventory_Clicks.CancellationPending)
            {
               
                result = PerformClick(clickList[step], report);

                step++;

                if (step > clickList.Count - 1)
                {
                    break;
                }
            }

            report.Report("Ending Inventory Clicks Worker");
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
                ClickScript = new UserScript()
                {
                    ClickResult = Result.EMPTY,
                    ResultAction = Action.EMPTY,
                    GoToSequence = 2,
                    PressKey = Keys.D3,
                    CheckOnlyNoClick = false,
                    ClickOptions = new ClickOptions()
                    {
                        SearchAreaTopLeft = new Point(0, 0),
                        SearchAreaBottomRight = new Point(0, 0)
                    }
                }
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

        private void SaveInventoryFile(string filename)
        {
            using (StreamWriter myStream = new StreamWriter(filename, false))
            {
                var data = ToXML(InventoryInfo);
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

        private void loadInventoryToolStripMenuItem_Click(object sender, EventArgs e)
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
                using (StreamReader reader = new StreamReader(fileName))
                {
                    var data = reader.ReadToEnd();
                    InventoryInfo = FromXML<Inventory>(data);
                }

                OpenInventoryFile = fileName;
            }

            txt_Inv_Top_X.Text = InventoryInfo.TopLeftX.ToString();
            txt_Inv_Top_Y.Text = InventoryInfo.TopLeftY.ToString();
            txt_Inv_Bot_X.Text = InventoryInfo.BottomRightX.ToString();
            txt_Inv_Bot_Y.Text = InventoryInfo.BottomRightY.ToString();
            txt_Inv_Width.Text = InventoryInfo.InventoryWidth.ToString();
            txt_Inv_Height.Text = InventoryInfo.InventoryHeight.ToString();
        }

        private void saveAsInventoryToolStripMenuItem_Click(object sender, EventArgs e)
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
                SaveInventoryFile(fileName);
                OpenInventoryFile = fileName;
            }
        }

        private void saveInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(OpenInventoryFile))
            {
                saveAsInventoryToolStripMenuItem.PerformClick();
                return;
            }
            else
            {
                var fileName = Path.GetFileName(OpenInventoryFile);
                if (MessageBox.Show(string.Format("Overwrite current file '{0}' ?", fileName), "Overwrite", MessageBoxButtons.YesNo) == DialogResult.No)
                    return;

                SaveInventoryFile(OpenInventoryFile);
            }
        }
    }
}

