namespace 彩牛通
{
    partial class CNTMainFrm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CNTMainFrm));
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Summary = new System.Windows.Forms.TextBox();
            this.dgv_Random = new System.Windows.Forms.DataGridView();
            this.方案数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.列一 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.列二 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单条统计标记 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单条统计次数及击打倍数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_export = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_page = new System.Windows.Forms.TextBox();
            this.lb_page = new System.Windows.Forms.Label();
            this.btn_upPage = new System.Windows.Forms.Button();
            this.btn_nextPage = new System.Windows.Forms.Button();
            this.btn_Statistics = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.txt_SXRow2 = new System.Windows.Forms.TextBox();
            this.txt_SXRow1 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tb_SingleMax = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btn_setBeishu = new System.Windows.Forms.Button();
            this.tb_ColumnTwoRows = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tb_ColumnOneRows = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tb_AdditionalColumnCount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btn_Default = new System.Windows.Forms.Button();
            this.btn_SaveSetting = new System.Windows.Forms.Button();
            this.tb_CTMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_CTMin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_COMax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_COMin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Random)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage3);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabMain.ItemSize = new System.Drawing.Size(48, 25);
            this.tabMain.Location = new System.Drawing.Point(4, 28);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1560, 757);
            this.tabMain.TabIndex = 0;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.tabMain_SelectedIndexChanged);
            this.tabMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabMain_KeyDown);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txt_Summary);
            this.tabPage1.Controls.Add(this.dgv_Random);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1552, 724);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "首页";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1059, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "方案汇总统计：";
            // 
            // txt_Summary
            // 
            this.txt_Summary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Summary.Location = new System.Drawing.Point(1014, 102);
            this.txt_Summary.Multiline = true;
            this.txt_Summary.Name = "txt_Summary";
            this.txt_Summary.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Summary.Size = new System.Drawing.Size(505, 616);
            this.txt_Summary.TabIndex = 14;
            // 
            // dgv_Random
            // 
            this.dgv_Random.AllowUserToAddRows = false;
            this.dgv_Random.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgv_Random.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Random.ColumnHeadersHeight = 35;
            this.dgv_Random.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.方案数,
            this.列一,
            this.列二,
            this.单条统计标记,
            this.单条统计次数及击打倍数});
            this.dgv_Random.Location = new System.Drawing.Point(10, 101);
            this.dgv_Random.Name = "dgv_Random";
            this.dgv_Random.RowTemplate.Height = 23;
            this.dgv_Random.Size = new System.Drawing.Size(982, 617);
            this.dgv_Random.TabIndex = 0;
            this.dgv_Random.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_Random_CellPainting);
            // 
            // 方案数
            // 
            this.方案数.HeaderText = "方案数";
            this.方案数.Name = "方案数";
            // 
            // 列一
            // 
            this.列一.HeaderText = "列一";
            this.列一.Name = "列一";
            // 
            // 列二
            // 
            this.列二.HeaderText = "列二";
            this.列二.Name = "列二";
            this.列二.Width = 325;
            // 
            // 单条统计标记
            // 
            this.单条统计标记.HeaderText = "单条统计标记";
            this.单条统计标记.Name = "单条统计标记";
            this.单条统计标记.Width = 150;
            // 
            // 单条统计次数及击打倍数
            // 
            this.单条统计次数及击打倍数.HeaderText = "单条统计次数及击打倍数";
            this.单条统计次数及击打倍数.Name = "单条统计次数及击打倍数";
            this.单条统计次数及击打倍数.Width = 1050;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btn_export);
            this.panel1.Controls.Add(this.btn_Import);
            this.panel1.Controls.Add(this.btn_del);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_page);
            this.panel1.Controls.Add(this.lb_page);
            this.panel1.Controls.Add(this.btn_upPage);
            this.panel1.Controls.Add(this.btn_nextPage);
            this.panel1.Controls.Add(this.btn_Statistics);
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.btn_add);
            this.panel1.Controls.Add(this.txt_SXRow2);
            this.panel1.Controls.Add(this.txt_SXRow1);
            this.panel1.Location = new System.Drawing.Point(10, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1523, 60);
            this.panel1.TabIndex = 13;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btn_export
            // 
            this.btn_export.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_export.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_export.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_export.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_export.Location = new System.Drawing.Point(370, 9);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(58, 47);
            this.btn_export.TabIndex = 26;
            this.btn_export.Text = "导出";
            this.btn_export.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_export.UseVisualStyleBackColor = false;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_Import.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Import.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Import.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Import.Location = new System.Drawing.Point(285, 9);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(58, 47);
            this.btn_Import.TabIndex = 25;
            this.btn_Import.Text = "导入";
            this.btn_Import.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Import.UseVisualStyleBackColor = false;
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_del
            // 
            this.btn_del.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_del.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_del.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_del.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_del.Location = new System.Drawing.Point(944, 6);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(93, 47);
            this.btn_del.TabIndex = 24;
            this.btn_del.Text = "是否删除";
            this.btn_del.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_del.UseVisualStyleBackColor = false;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(450, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 23;
            this.label2.Text = "列一：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(640, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "列二：";
            // 
            // tb_page
            // 
            this.tb_page.Location = new System.Drawing.Point(1181, 15);
            this.tb_page.Name = "tb_page";
            this.tb_page.Size = new System.Drawing.Size(25, 30);
            this.tb_page.TabIndex = 17;
            this.tb_page.Text = "1";
            this.tb_page.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_page_KeyDown);
            // 
            // lb_page
            // 
            this.lb_page.AutoSize = true;
            this.lb_page.Location = new System.Drawing.Point(1207, 19);
            this.lb_page.Name = "lb_page";
            this.lb_page.Size = new System.Drawing.Size(29, 20);
            this.lb_page.TabIndex = 16;
            this.lb_page.Text = "/1";
            // 
            // btn_upPage
            // 
            this.btn_upPage.Location = new System.Drawing.Point(1053, 12);
            this.btn_upPage.Name = "btn_upPage";
            this.btn_upPage.Size = new System.Drawing.Size(96, 30);
            this.btn_upPage.TabIndex = 15;
            this.btn_upPage.Text = "上一页";
            this.btn_upPage.UseVisualStyleBackColor = true;
            this.btn_upPage.Click += new System.EventHandler(this.btn_upPage_Click);
            // 
            // btn_nextPage
            // 
            this.btn_nextPage.Location = new System.Drawing.Point(1253, 12);
            this.btn_nextPage.Name = "btn_nextPage";
            this.btn_nextPage.Size = new System.Drawing.Size(96, 30);
            this.btn_nextPage.TabIndex = 14;
            this.btn_nextPage.Text = "下一页";
            this.btn_nextPage.UseVisualStyleBackColor = true;
            this.btn_nextPage.Click += new System.EventHandler(this.btn_nextPage_Click);
            // 
            // btn_Statistics
            // 
            this.btn_Statistics.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_Statistics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Statistics.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Statistics.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Statistics.Location = new System.Drawing.Point(835, 4);
            this.btn_Statistics.Name = "btn_Statistics";
            this.btn_Statistics.Size = new System.Drawing.Size(93, 47);
            this.btn_Statistics.TabIndex = 12;
            this.btn_Statistics.Text = "是否统计";
            this.btn_Statistics.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Statistics.UseVisualStyleBackColor = false;
            this.btn_Statistics.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_clear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_clear.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_clear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_clear.Location = new System.Drawing.Point(169, 9);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(90, 47);
            this.btn_clear.TabIndex = 13;
            this.btn_clear.Text = "清除数据";
            this.btn_clear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_add
            // 
            this.btn_add.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_add.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_add.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_add.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_add.Location = new System.Drawing.Point(34, 9);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(110, 47);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "增加新方案";
            this.btn_add.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_add.UseVisualStyleBackColor = false;
            this.btn_add.Click += new System.EventHandler(this.btn_Random_Click);
            // 
            // txt_SXRow2
            // 
            this.txt_SXRow2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SXRow2.Location = new System.Drawing.Point(715, 15);
            this.txt_SXRow2.Name = "txt_SXRow2";
            this.txt_SXRow2.Size = new System.Drawing.Size(106, 32);
            this.txt_SXRow2.TabIndex = 11;
            // 
            // txt_SXRow1
            // 
            this.txt_SXRow1.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SXRow1.Location = new System.Drawing.Point(525, 17);
            this.txt_SXRow1.Name = "txt_SXRow1";
            this.txt_SXRow1.Size = new System.Drawing.Size(93, 32);
            this.txt_SXRow1.TabIndex = 4;
            // 
            // tabPage3
            // 
            this.tabPage3.BackgroundImage = global::彩牛通.Properties.Resources._2a5961f6b52f36a8d0b3a95d9a88b299;
            this.tabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPage3.Controls.Add(this.tb_SingleMax);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.btn_setBeishu);
            this.tabPage3.Controls.Add(this.tb_ColumnTwoRows);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.tb_ColumnOneRows);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.tb_AdditionalColumnCount);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Controls.Add(this.btn_Default);
            this.tabPage3.Controls.Add(this.btn_SaveSetting);
            this.tabPage3.Controls.Add(this.tb_CTMax);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.tb_CTMin);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.tb_COMax);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.tb_COMin);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1552, 724);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // tb_SingleMax
            // 
            this.tb_SingleMax.Location = new System.Drawing.Point(764, 332);
            this.tb_SingleMax.Name = "tb_SingleMax";
            this.tb_SingleMax.Size = new System.Drawing.Size(105, 30);
            this.tb_SingleMax.TabIndex = 28;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(668, 339);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 20);
            this.label14.TabIndex = 27;
            this.label14.Text = "最大次数：";
            // 
            // btn_setBeishu
            // 
            this.btn_setBeishu.BackColor = System.Drawing.Color.Gold;
            this.btn_setBeishu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_setBeishu.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_setBeishu.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_setBeishu.Location = new System.Drawing.Point(518, 507);
            this.btn_setBeishu.Name = "btn_setBeishu";
            this.btn_setBeishu.Size = new System.Drawing.Size(123, 47);
            this.btn_setBeishu.TabIndex = 26;
            this.btn_setBeishu.Text = "击打倍数";
            this.btn_setBeishu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_setBeishu.UseVisualStyleBackColor = false;
            this.btn_setBeishu.Click += new System.EventHandler(this.btn_setBeishu_Click);
            // 
            // tb_ColumnTwoRows
            // 
            this.tb_ColumnTwoRows.Location = new System.Drawing.Point(764, 218);
            this.tb_ColumnTwoRows.Name = "tb_ColumnTwoRows";
            this.tb_ColumnTwoRows.Size = new System.Drawing.Size(105, 30);
            this.tb_ColumnTwoRows.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(638, 218);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(139, 20);
            this.label17.TabIndex = 24;
            this.label17.Text = "列2生成行数：";
            // 
            // tb_ColumnOneRows
            // 
            this.tb_ColumnOneRows.Location = new System.Drawing.Point(764, 167);
            this.tb_ColumnOneRows.Name = "tb_ColumnOneRows";
            this.tb_ColumnOneRows.Size = new System.Drawing.Size(105, 30);
            this.tb_ColumnOneRows.TabIndex = 23;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(638, 170);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(139, 20);
            this.label16.TabIndex = 22;
            this.label16.Text = "列1生成行数：";
            // 
            // tb_AdditionalColumnCount
            // 
            this.tb_AdditionalColumnCount.Location = new System.Drawing.Point(764, 277);
            this.tb_AdditionalColumnCount.Name = "tb_AdditionalColumnCount";
            this.tb_AdditionalColumnCount.Size = new System.Drawing.Size(105, 30);
            this.tb_AdditionalColumnCount.TabIndex = 19;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(626, 280);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(159, 20);
            this.label15.TabIndex = 18;
            this.label15.Text = "列2后附加列数：";
            // 
            // btn_Default
            // 
            this.btn_Default.BackColor = System.Drawing.Color.Gold;
            this.btn_Default.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Default.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Default.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Default.Location = new System.Drawing.Point(331, 507);
            this.btn_Default.Name = "btn_Default";
            this.btn_Default.Size = new System.Drawing.Size(123, 47);
            this.btn_Default.TabIndex = 17;
            this.btn_Default.Text = "还原默认";
            this.btn_Default.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Default.UseVisualStyleBackColor = false;
            this.btn_Default.Click += new System.EventHandler(this.btn_Default_Click);
            // 
            // btn_SaveSetting
            // 
            this.btn_SaveSetting.BackColor = System.Drawing.Color.Gold;
            this.btn_SaveSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveSetting.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveSetting.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_SaveSetting.Location = new System.Drawing.Point(704, 507);
            this.btn_SaveSetting.Name = "btn_SaveSetting";
            this.btn_SaveSetting.Size = new System.Drawing.Size(123, 47);
            this.btn_SaveSetting.TabIndex = 16;
            this.btn_SaveSetting.Text = "保存修改";
            this.btn_SaveSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_SaveSetting.UseVisualStyleBackColor = false;
            this.btn_SaveSetting.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_CTMax
            // 
            this.tb_CTMax.Location = new System.Drawing.Point(425, 329);
            this.tb_CTMax.Name = "tb_CTMax";
            this.tb_CTMax.Size = new System.Drawing.Size(105, 30);
            this.tb_CTMax.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(310, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "列2最大数：";
            // 
            // tb_CTMin
            // 
            this.tb_CTMin.Location = new System.Drawing.Point(425, 275);
            this.tb_CTMin.Name = "tb_CTMin";
            this.tb_CTMin.Size = new System.Drawing.Size(105, 30);
            this.tb_CTMin.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(310, 278);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "列2最小数：";
            // 
            // tb_COMax
            // 
            this.tb_COMax.Location = new System.Drawing.Point(425, 221);
            this.tb_COMax.Name = "tb_COMax";
            this.tb_COMax.Size = new System.Drawing.Size(105, 30);
            this.tb_COMax.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(310, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "列1最大数：";
            // 
            // tb_COMin
            // 
            this.tb_COMin.Location = new System.Drawing.Point(425, 167);
            this.tb_COMin.Name = "tb_COMin";
            this.tb_COMin.Size = new System.Drawing.Size(105, 30);
            this.tb_COMin.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(310, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "列1最小数：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1552, 724);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "关于";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(524, 338);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(219, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "软件版本：V2.0 @ 2017";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(524, 388);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(169, 20);
            this.label12.TabIndex = 1;
            this.label12.Text = "版权归属：刘成宏";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(524, 280);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(239, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "软件名称：彩牛通-统计版";
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // CNTMainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1568, 789);
            this.Controls.Add(this.tabMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CNTMainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "彩牛通-统计版";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CNTMainFrm_FormClosing);
            this.Load += new System.EventHandler(this.CNTMainFrm_Load);
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Random)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txt_SXRow1;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.DataGridView dgv_Random;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txt_SXRow2;
        private System.Windows.Forms.Panel panel1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button btn_Statistics;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.TextBox tb_COMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_CTMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_CTMin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_COMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_SaveSetting;
        private System.Windows.Forms.Button btn_Default;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_AdditionalColumnCount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tb_ColumnTwoRows;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tb_ColumnOneRows;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lb_page;
        private System.Windows.Forms.Button btn_upPage;
        private System.Windows.Forms.Button btn_nextPage;
        private System.Windows.Forms.TextBox tb_page;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_setBeishu;
        private System.Windows.Forms.TextBox tb_SingleMax;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Summary;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Button btn_Import;
        private System.Windows.Forms.DataGridViewTextBoxColumn 方案数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 列一;
        private System.Windows.Forms.DataGridViewTextBoxColumn 列二;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单条统计标记;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单条统计次数及击打倍数;
    }
}

