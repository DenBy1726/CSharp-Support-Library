namespace KMeans
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pb_Cluster = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pb_Source = new System.Windows.Forms.PictureBox();
            this.lb_Color = new DenByComponents.ColorListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClear = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Random = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Remove = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Add = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_FromImageColor = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuCustomLabel7 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.btn_Load = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_Save = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ob_Progress = new MetroFramework.Controls.MetroProgressBar();
            this.cc_Colors = new Bunifu.Framework.UI.BunifuClolorChooser();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.tb_Blue = new MetroFramework.Controls.MetroTextBox();
            this.tb_Green = new MetroFramework.Controls.MetroTextBox();
            this.tb_Red = new MetroFramework.Controls.MetroTextBox();
            this.tb_Hex = new MetroFramework.Controls.MetroTextBox();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel5 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuCustomLabel6 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Start = new CloudToolkitN6.Windows.Vista.CloudStartMenuButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.bunifuCustomLabel8 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl2 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuDragControl3 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bunifuImageButton2 = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton3 = new Bunifu.Framework.UI.BunifuImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Cluster)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Source)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Random)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Remove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Add)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_FromImageColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Load)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Save)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_Cluster
            // 
            this.pb_Cluster.BackColor = System.Drawing.Color.White;
            this.pb_Cluster.ContextMenuStrip = this.contextMenuStrip1;
            this.pb_Cluster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_Cluster.Location = new System.Drawing.Point(0, 0);
            this.pb_Cluster.Margin = new System.Windows.Forms.Padding(0);
            this.pb_Cluster.Name = "pb_Cluster";
            this.pb_Cluster.Size = new System.Drawing.Size(186, 157);
            this.pb_Cluster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Cluster.TabIndex = 12;
            this.pb_Cluster.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(133, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(132, 22);
            this.toolStripMenuItem1.Text = "Сохранить";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.pb_Source, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lb_Color, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.bunifuCustomLabel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.metroPanel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(641, 439);
            this.tableLayoutPanel1.TabIndex = 13;
            this.tableLayoutPanel1.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tableLayoutPanel1_CellPaint);
            // 
            // pb_Source
            // 
            this.pb_Source.BackColor = System.Drawing.Color.White;
            this.pb_Source.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_Source.Location = new System.Drawing.Point(3, 117);
            this.pb_Source.Margin = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.pb_Source.Name = "pb_Source";
            this.pb_Source.Size = new System.Drawing.Size(123, 114);
            this.pb_Source.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Source.TabIndex = 8;
            this.pb_Source.TabStop = false;
            // 
            // lb_Color
            // 
            this.lb_Color.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lb_Color.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb_Color.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lb_Color.FormattingEnabled = true;
            this.lb_Color.Location = new System.Drawing.Point(192, 55);
            this.lb_Color.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lb_Color.Name = "lb_Color";
            this.lb_Color.PressedItemBackColor = System.Drawing.Color.SeaGreen;
            this.lb_Color.Size = new System.Drawing.Size(449, 192);
            this.lb_Color.TabIndex = 6;
            this.lb_Color.SelectedIndexChanged += new System.EventHandler(this.lb_Color_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 250);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.bunifuCustomLabel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pb_Cluster);
            this.splitContainer2.Size = new System.Drawing.Size(186, 186);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 1;
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.BackColor = System.Drawing.Color.White;
            this.bunifuCustomLabel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(0, 5);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(176, 20);
            this.bunifuCustomLabel2.TabIndex = 0;
            this.bunifuCustomLabel2.Text = "После кластеризации";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.SeaGreen;
            this.tableLayoutPanel4.ColumnCount = 10;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel4.Controls.Add(this.btnClear, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Random, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Remove, 9, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Add, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_FromImageColor, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.bunifuCustomLabel7, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Save, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Load, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.metroPanel2, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(192, 30);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(449, 25);
            this.tableLayoutPanel4.TabIndex = 7;
            this.tableLayoutPanel4.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tableLayoutPanel1_CellPaint);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.SeaGreen;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageActive = null;
            this.btnClear.Location = new System.Drawing.Point(299, 5);
            this.btnClear.Margin = new System.Windows.Forms.Padding(0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(25, 20);
            this.btnClear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnClear.TabIndex = 11;
            this.btnClear.TabStop = false;
            this.btnClear.Zoom = 20;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btn_Random
            // 
            this.btn_Random.BackColor = System.Drawing.Color.SeaGreen;
            this.btn_Random.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_Random.Image = ((System.Drawing.Image)(resources.GetObject("btn_Random.Image")));
            this.btn_Random.ImageActive = null;
            this.btn_Random.Location = new System.Drawing.Point(374, 5);
            this.btn_Random.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Random.Name = "btn_Random";
            this.btn_Random.Size = new System.Drawing.Size(25, 20);
            this.btn_Random.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Random.TabIndex = 7;
            this.btn_Random.TabStop = false;
            this.btn_Random.Zoom = 20;
            this.btn_Random.Click += new System.EventHandler(this.btn_Random_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.BackColor = System.Drawing.Color.SeaGreen;
            this.btn_Remove.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_Remove.Image = ((System.Drawing.Image)(resources.GetObject("btn_Remove.Image")));
            this.btn_Remove.ImageActive = null;
            this.btn_Remove.Location = new System.Drawing.Point(424, 5);
            this.btn_Remove.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(25, 20);
            this.btn_Remove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Remove.TabIndex = 6;
            this.btn_Remove.TabStop = false;
            this.btn_Remove.Zoom = 20;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.SeaGreen;
            this.btn_Add.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_Add.Image = ((System.Drawing.Image)(resources.GetObject("btn_Add.Image")));
            this.btn_Add.ImageActive = null;
            this.btn_Add.Location = new System.Drawing.Point(399, 5);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(25, 20);
            this.btn_Add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Add.TabIndex = 1;
            this.btn_Add.TabStop = false;
            this.btn_Add.Zoom = 20;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_FromImageColor
            // 
            this.btn_FromImageColor.BackColor = System.Drawing.Color.SeaGreen;
            this.btn_FromImageColor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_FromImageColor.Image = ((System.Drawing.Image)(resources.GetObject("btn_FromImageColor.Image")));
            this.btn_FromImageColor.ImageActive = null;
            this.btn_FromImageColor.Location = new System.Drawing.Point(349, 5);
            this.btn_FromImageColor.Margin = new System.Windows.Forms.Padding(0);
            this.btn_FromImageColor.Name = "btn_FromImageColor";
            this.btn_FromImageColor.Size = new System.Drawing.Size(25, 20);
            this.btn_FromImageColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_FromImageColor.TabIndex = 8;
            this.btn_FromImageColor.TabStop = false;
            this.btn_FromImageColor.Visible = false;
            this.btn_FromImageColor.Zoom = 20;
            this.btn_FromImageColor.Click += new System.EventHandler(this.btn_FromImageColor_Click);
            // 
            // bunifuCustomLabel7
            // 
            this.bunifuCustomLabel7.AutoSize = true;
            this.bunifuCustomLabel7.BackColor = System.Drawing.Color.White;
            this.bunifuCustomLabel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bunifuCustomLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuCustomLabel7.Location = new System.Drawing.Point(1, 0);
            this.bunifuCustomLabel7.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bunifuCustomLabel7.Name = "bunifuCustomLabel7";
            this.bunifuCustomLabel7.Size = new System.Drawing.Size(84, 25);
            this.bunifuCustomLabel7.TabIndex = 4;
            this.bunifuCustomLabel7.Text = "Кластеры";
            // 
            // btn_Load
            // 
            this.btn_Load.BackColor = System.Drawing.Color.SeaGreen;
            this.btn_Load.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_Load.Image = ((System.Drawing.Image)(resources.GetObject("btn_Load.Image")));
            this.btn_Load.ImageActive = null;
            this.btn_Load.Location = new System.Drawing.Point(249, 5);
            this.btn_Load.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Load.Name = "btn_Load";
            this.btn_Load.Size = new System.Drawing.Size(25, 20);
            this.btn_Load.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Load.TabIndex = 9;
            this.btn_Load.TabStop = false;
            this.btn_Load.Zoom = 20;
            this.btn_Load.Click += new System.EventHandler(this.btn_Load_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.SeaGreen;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.ImageActive = null;
            this.btn_Save.Location = new System.Drawing.Point(274, 5);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(25, 20);
            this.btn_Save.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_Save.TabIndex = 10;
            this.btn_Save.TabStop = false;
            this.btn_Save.Zoom = 20;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // bunifuCustomLabel1
            // 
            this.bunifuCustomLabel1.AllowDrop = true;
            this.bunifuCustomLabel1.BackColor = System.Drawing.Color.White;
            this.bunifuCustomLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(3, 30);
            this.bunifuCustomLabel1.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(188, 25);
            this.bunifuCustomLabel1.TabIndex = 0;
            this.bunifuCustomLabel1.Text = "Исходное изображение";
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // ob_Progress
            // 
            this.ob_Progress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ob_Progress.Location = new System.Drawing.Point(0, 166);
            this.ob_Progress.Margin = new System.Windows.Forms.Padding(0);
            this.ob_Progress.MarqueeAnimationSpeed = 0;
            this.ob_Progress.Maximum = 100000;
            this.ob_Progress.Name = "ob_Progress";
            this.ob_Progress.Size = new System.Drawing.Size(443, 20);
            this.ob_Progress.Style = MetroFramework.MetroColorStyle.Green;
            this.ob_Progress.TabIndex = 6;
            // 
            // cc_Colors
            // 
            this.cc_Colors.BackColor = System.Drawing.Color.White;
            this.cc_Colors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cc_Colors.ForeColor = System.Drawing.Color.SeaGreen;
            this.cc_Colors.Location = new System.Drawing.Point(0, 0);
            this.cc_Colors.Margin = new System.Windows.Forms.Padding(0);
            this.cc_Colors.Name = "cc_Colors";
            this.cc_Colors.Size = new System.Drawing.Size(443, 86);
            this.cc_Colors.TabIndex = 3;
            this.cc_Colors.Value = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(80)))));
            this.cc_Colors.OnValueChange += new System.EventHandler(this.cc_Colors_OnValueChange);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.bunifuCustomLabel6, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.bunifuCustomLabel5, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.bunifuCustomLabel4, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tb_Hex, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tb_Red, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.tb_Green, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.tb_Blue, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.bunifuCustomLabel3, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 86);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(443, 40);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.BackColor = System.Drawing.Color.White;
            this.bunifuCustomLabel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(0, 0);
            this.bunifuCustomLabel3.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(110, 13);
            this.bunifuCustomLabel3.TabIndex = 4;
            this.bunifuCustomLabel3.Text = "Hex";
            // 
            // tb_Blue
            // 
            // 
            // 
            // 
            this.tb_Blue.CustomButton.Image = null;
            this.tb_Blue.CustomButton.Location = new System.Drawing.Point(95, 2);
            this.tb_Blue.CustomButton.Name = "";
            this.tb_Blue.CustomButton.Size = new System.Drawing.Size(15, 15);
            this.tb_Blue.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tb_Blue.CustomButton.TabIndex = 1;
            this.tb_Blue.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tb_Blue.CustomButton.UseSelectable = true;
            this.tb_Blue.CustomButton.Visible = false;
            this.tb_Blue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Blue.ForeColor = System.Drawing.Color.SeaGreen;
            this.tb_Blue.Lines = new string[0];
            this.tb_Blue.Location = new System.Drawing.Point(330, 20);
            this.tb_Blue.Margin = new System.Windows.Forms.Padding(0);
            this.tb_Blue.MaxLength = 32767;
            this.tb_Blue.Name = "tb_Blue";
            this.tb_Blue.PasswordChar = '\0';
            this.tb_Blue.ReadOnly = true;
            this.tb_Blue.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tb_Blue.SelectedText = "";
            this.tb_Blue.SelectionLength = 0;
            this.tb_Blue.SelectionStart = 0;
            this.tb_Blue.ShortcutsEnabled = true;
            this.tb_Blue.Size = new System.Drawing.Size(113, 20);
            this.tb_Blue.TabIndex = 3;
            this.tb_Blue.UseCustomForeColor = true;
            this.tb_Blue.UseSelectable = true;
            this.tb_Blue.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tb_Blue.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tb_Green
            // 
            // 
            // 
            // 
            this.tb_Green.CustomButton.Image = null;
            this.tb_Green.CustomButton.Location = new System.Drawing.Point(92, 2);
            this.tb_Green.CustomButton.Name = "";
            this.tb_Green.CustomButton.Size = new System.Drawing.Size(15, 15);
            this.tb_Green.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tb_Green.CustomButton.TabIndex = 1;
            this.tb_Green.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tb_Green.CustomButton.UseSelectable = true;
            this.tb_Green.CustomButton.Visible = false;
            this.tb_Green.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Green.ForeColor = System.Drawing.Color.SeaGreen;
            this.tb_Green.Lines = new string[0];
            this.tb_Green.Location = new System.Drawing.Point(220, 20);
            this.tb_Green.Margin = new System.Windows.Forms.Padding(0);
            this.tb_Green.MaxLength = 32767;
            this.tb_Green.Name = "tb_Green";
            this.tb_Green.PasswordChar = '\0';
            this.tb_Green.ReadOnly = true;
            this.tb_Green.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tb_Green.SelectedText = "";
            this.tb_Green.SelectionLength = 0;
            this.tb_Green.SelectionStart = 0;
            this.tb_Green.ShortcutsEnabled = true;
            this.tb_Green.Size = new System.Drawing.Size(110, 20);
            this.tb_Green.TabIndex = 2;
            this.tb_Green.UseCustomForeColor = true;
            this.tb_Green.UseSelectable = true;
            this.tb_Green.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tb_Green.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tb_Red
            // 
            // 
            // 
            // 
            this.tb_Red.CustomButton.Image = null;
            this.tb_Red.CustomButton.Location = new System.Drawing.Point(92, 2);
            this.tb_Red.CustomButton.Name = "";
            this.tb_Red.CustomButton.Size = new System.Drawing.Size(15, 15);
            this.tb_Red.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tb_Red.CustomButton.TabIndex = 1;
            this.tb_Red.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tb_Red.CustomButton.UseSelectable = true;
            this.tb_Red.CustomButton.Visible = false;
            this.tb_Red.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Red.ForeColor = System.Drawing.Color.SeaGreen;
            this.tb_Red.Lines = new string[0];
            this.tb_Red.Location = new System.Drawing.Point(110, 20);
            this.tb_Red.Margin = new System.Windows.Forms.Padding(0);
            this.tb_Red.MaxLength = 32767;
            this.tb_Red.Name = "tb_Red";
            this.tb_Red.PasswordChar = '\0';
            this.tb_Red.ReadOnly = true;
            this.tb_Red.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tb_Red.SelectedText = "";
            this.tb_Red.SelectionLength = 0;
            this.tb_Red.SelectionStart = 0;
            this.tb_Red.ShortcutsEnabled = true;
            this.tb_Red.Size = new System.Drawing.Size(110, 20);
            this.tb_Red.TabIndex = 1;
            this.tb_Red.UseCustomForeColor = true;
            this.tb_Red.UseSelectable = true;
            this.tb_Red.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tb_Red.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // tb_Hex
            // 
            // 
            // 
            // 
            this.tb_Hex.CustomButton.Image = null;
            this.tb_Hex.CustomButton.Location = new System.Drawing.Point(92, 2);
            this.tb_Hex.CustomButton.Name = "";
            this.tb_Hex.CustomButton.Size = new System.Drawing.Size(15, 15);
            this.tb_Hex.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.tb_Hex.CustomButton.TabIndex = 1;
            this.tb_Hex.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.tb_Hex.CustomButton.UseSelectable = true;
            this.tb_Hex.CustomButton.Visible = false;
            this.tb_Hex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_Hex.ForeColor = System.Drawing.Color.SeaGreen;
            this.tb_Hex.Lines = new string[0];
            this.tb_Hex.Location = new System.Drawing.Point(0, 20);
            this.tb_Hex.Margin = new System.Windows.Forms.Padding(0);
            this.tb_Hex.MaxLength = 32767;
            this.tb_Hex.Name = "tb_Hex";
            this.tb_Hex.PasswordChar = '\0';
            this.tb_Hex.ReadOnly = true;
            this.tb_Hex.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.tb_Hex.SelectedText = "";
            this.tb_Hex.SelectionLength = 0;
            this.tb_Hex.SelectionStart = 0;
            this.tb_Hex.ShortcutsEnabled = true;
            this.tb_Hex.Size = new System.Drawing.Size(110, 20);
            this.tb_Hex.TabIndex = 0;
            this.tb_Hex.UseCustomForeColor = true;
            this.tb_Hex.UseSelectable = true;
            this.tb_Hex.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.tb_Hex.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.AutoSize = true;
            this.bunifuCustomLabel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(113, 0);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(104, 13);
            this.bunifuCustomLabel4.TabIndex = 5;
            this.bunifuCustomLabel4.Text = "Red";
            // 
            // bunifuCustomLabel5
            // 
            this.bunifuCustomLabel5.AutoSize = true;
            this.bunifuCustomLabel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuCustomLabel5.Location = new System.Drawing.Point(223, 0);
            this.bunifuCustomLabel5.Name = "bunifuCustomLabel5";
            this.bunifuCustomLabel5.Size = new System.Drawing.Size(104, 13);
            this.bunifuCustomLabel5.TabIndex = 6;
            this.bunifuCustomLabel5.Text = "Green";
            // 
            // bunifuCustomLabel6
            // 
            this.bunifuCustomLabel6.AutoSize = true;
            this.bunifuCustomLabel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.bunifuCustomLabel6.Location = new System.Drawing.Point(333, 0);
            this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
            this.bunifuCustomLabel6.Size = new System.Drawing.Size(107, 13);
            this.bunifuCustomLabel6.TabIndex = 7;
            this.bunifuCustomLabel6.Text = "Blue";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cc_Colors, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ob_Progress, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.btn_Start, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(195, 250);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(443, 186);
            this.tableLayoutPanel2.TabIndex = 5;
            this.tableLayoutPanel2.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tableLayoutPanel1_CellPaint);
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // btn_Start
            // 
            this.btn_Start.AnimationOpacityChange = 0.1D;
            this.btn_Start.BackColor = System.Drawing.Color.Transparent;
            this.btn_Start.ButtonText = "Запустить кластеризацию";
            this.btn_Start.ClickedColors_1 = System.Drawing.Color.SeaGreen;
            this.btn_Start.ClickedColors_2 = System.Drawing.Color.SeaGreen;
            this.btn_Start.ClickedColors_3 = System.Drawing.Color.SeaGreen;
            this.btn_Start.ClickedColors_4 = System.Drawing.Color.SeaGreen;
            this.btn_Start.ControlOpacity = 255;
            this.btn_Start.CornerRadius = 8;
            this.btn_Start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Start.DrawHoverOverIcon = false;
            this.btn_Start.Enabled = false;
            this.btn_Start.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.btn_Start.ForeColor = System.Drawing.Color.White;
            this.btn_Start.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(215)))), ((int)(((byte)(226)))));
            this.btn_Start.Icon = null;
            this.btn_Start.IsAnimated = true;
            this.btn_Start.Location = new System.Drawing.Point(0, 126);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Start.MaximumGlowOpacity = 0.8D;
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.NormalColors_1 = System.Drawing.Color.MediumSeaGreen;
            this.btn_Start.NormalColors_2 = System.Drawing.Color.SeaGreen;
            this.btn_Start.NormalColors_3 = System.Drawing.Color.SeaGreen;
            this.btn_Start.NormalColors_4 = System.Drawing.Color.SeaGreen;
            this.btn_Start.Size = new System.Drawing.Size(443, 40);
            this.btn_Start.StartMenuIndex = 0;
            this.btn_Start.TabIndex = 7;
            this.btn_Start.TextCentered = true;
            this.btn_Start.TextDistanceFromBorder = 5;
            this.btn_Start.TextIconRelation = CloudToolkitN6.Windows.Vista.CloudStartMenuButton.TextIconRelation_Enum.TextOverIcon;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.BackColor = System.Drawing.Color.SeaGreen;
            this.tableLayoutPanel1.SetColumnSpan(this.metroPanel1, 2);
            this.metroPanel1.Controls.Add(this.tableLayoutPanel5);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = false;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(0, 0);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(641, 30);
            this.metroPanel1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroPanel1.TabIndex = 9;
            this.metroPanel1.UseCustomBackColor = true;
            this.metroPanel1.VerticalScrollbarBarColor = false;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton1.Image")));
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(615, 0);
            this.bunifuImageButton1.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(20, 20);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton1.TabIndex = 2;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 20;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 8;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel5.Controls.Add(this.bunifuCustomLabel8, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.bunifuImageButton1, 7, 0);
            this.tableLayoutPanel5.Controls.Add(this.bunifuImageButton2, 6, 0);
            this.tableLayoutPanel5.Controls.Add(this.bunifuImageButton3, 5, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(641, 30);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // bunifuCustomLabel8
            // 
            this.bunifuCustomLabel8.AutoSize = true;
            this.bunifuCustomLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuCustomLabel8.ForeColor = System.Drawing.Color.White;
            this.bunifuCustomLabel8.Location = new System.Drawing.Point(245, 0);
            this.bunifuCustomLabel8.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuCustomLabel8.Name = "bunifuCustomLabel8";
            this.bunifuCustomLabel8.Size = new System.Drawing.Size(136, 22);
            this.bunifuCustomLabel8.TabIndex = 3;
            this.bunifuCustomLabel8.Text = "Кластеризатор";
            // 
            // metroPanel2
            // 
            this.metroPanel2.BackColor = System.Drawing.Color.SeaGreen;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(85, 0);
            this.metroPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(159, 25);
            this.metroPanel2.TabIndex = 12;
            this.metroPanel2.UseCustomBackColor = true;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.tableLayoutPanel5;
            this.bunifuDragControl1.Vertical = true;
            // 
            // bunifuDragControl2
            // 
            this.bunifuDragControl2.Fixed = true;
            this.bunifuDragControl2.Horizontal = true;
            this.bunifuDragControl2.TargetControl = this.tableLayoutPanel4;
            this.bunifuDragControl2.Vertical = true;
            // 
            // bunifuDragControl3
            // 
            this.bunifuDragControl3.Fixed = true;
            this.bunifuDragControl3.Horizontal = true;
            this.bunifuDragControl3.TargetControl = this.metroPanel2;
            this.bunifuDragControl3.Vertical = true;
            // 
            // bunifuImageButton2
            // 
            this.bunifuImageButton2.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton2.Image")));
            this.bunifuImageButton2.ImageActive = null;
            this.bunifuImageButton2.Location = new System.Drawing.Point(590, 0);
            this.bunifuImageButton2.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuImageButton2.Name = "bunifuImageButton2";
            this.bunifuImageButton2.Size = new System.Drawing.Size(20, 20);
            this.bunifuImageButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton2.TabIndex = 4;
            this.bunifuImageButton2.TabStop = false;
            this.bunifuImageButton2.Zoom = 20;
            this.bunifuImageButton2.Click += new System.EventHandler(this.bunifuImageButton2_Click);
            // 
            // bunifuImageButton3
            // 
            this.bunifuImageButton3.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton3.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton3.Image")));
            this.bunifuImageButton3.ImageActive = null;
            this.bunifuImageButton3.Location = new System.Drawing.Point(565, 0);
            this.bunifuImageButton3.Margin = new System.Windows.Forms.Padding(0);
            this.bunifuImageButton3.Name = "bunifuImageButton3";
            this.bunifuImageButton3.Size = new System.Drawing.Size(20, 20);
            this.bunifuImageButton3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton3.TabIndex = 5;
            this.bunifuImageButton3.TabStop = false;
            this.bunifuImageButton3.Zoom = 20;
            this.bunifuImageButton3.Click += new System.EventHandler(this.bunifuImageButton3_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 439);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(657, 455);
            this.Name = "Form1";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Cluster)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Source)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Random)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Remove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Add)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_FromImageColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Load)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_Save)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.metroPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_Cluster;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private DenByComponents.ColorListBox lb_Color;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private Bunifu.Framework.UI.BunifuImageButton btn_Add;
        private Bunifu.Framework.UI.BunifuImageButton btn_FromImageColor;
        private Bunifu.Framework.UI.BunifuImageButton btn_Random;
        private Bunifu.Framework.UI.BunifuImageButton btn_Remove;
        private System.Windows.Forms.PictureBox pb_Source;
        private System.Windows.Forms.ToolTip toolTip1;
        private Bunifu.Framework.UI.BunifuImageButton btn_Save;
        private Bunifu.Framework.UI.BunifuImageButton btn_Load;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel7;
        private Bunifu.Framework.UI.BunifuImageButton btnClear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel6;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel5;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private MetroFramework.Controls.MetroTextBox tb_Hex;
        private MetroFramework.Controls.MetroTextBox tb_Red;
        private MetroFramework.Controls.MetroTextBox tb_Green;
        private MetroFramework.Controls.MetroTextBox tb_Blue;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuClolorChooser cc_Colors;
        private MetroFramework.Controls.MetroProgressBar ob_Progress;
        private CloudToolkitN6.Windows.Vista.CloudStartMenuButton btn_Start;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel8;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl2;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl3;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton2;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton3;
    }
}

