namespace 彩牛通
{
    partial class AddProgrammeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.dgv_Random = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_ColumnTwo = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.粘贴ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_completion = new System.Windows.Forms.Button();
            this.txt_first = new System.Windows.Forms.TextBox();
            this.行 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.列一 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Random)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ColumnTwo)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_ok.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ok.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_ok.Location = new System.Drawing.Point(957, 356);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(90, 47);
            this.btn_ok.TabIndex = 14;
            this.btn_ok.Text = "确定";
            this.btn_ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_cancel.Location = new System.Drawing.Point(957, 445);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(90, 47);
            this.btn_cancel.TabIndex = 15;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_cancel.UseVisualStyleBackColor = false;
            // 
            // dgv_Random
            // 
            this.dgv_Random.AllowUserToAddRows = false;
            this.dgv_Random.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Random.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Random.ColumnHeadersHeight = 35;
            this.dgv_Random.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.行,
            this.列一});
            this.dgv_Random.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv_Random.Location = new System.Drawing.Point(33, 27);
            this.dgv_Random.Name = "dgv_Random";
            this.dgv_Random.RowTemplate.Height = 23;
            this.dgv_Random.Size = new System.Drawing.Size(354, 536);
            this.dgv_Random.TabIndex = 16;
            this.dgv_Random.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_Random_EditingControlShowing);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.粘贴ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(109, 28);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // dgv_ColumnTwo
            // 
            this.dgv_ColumnTwo.AllowUserToAddRows = false;
            this.dgv_ColumnTwo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_ColumnTwo.BackgroundColor = System.Drawing.Color.White;
            this.dgv_ColumnTwo.ColumnHeadersHeight = 35;
            this.dgv_ColumnTwo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3});
            this.dgv_ColumnTwo.ContextMenuStrip = this.contextMenuStrip2;
            this.dgv_ColumnTwo.Location = new System.Drawing.Point(481, 27);
            this.dgv_ColumnTwo.Name = "dgv_ColumnTwo";
            this.dgv_ColumnTwo.RowTemplate.Height = 23;
            this.dgv_ColumnTwo.Size = new System.Drawing.Size(415, 536);
            this.dgv_ColumnTwo.TabIndex = 17;
            this.dgv_ColumnTwo.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_ColumnTwo_EditingControlShowing);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.粘贴ToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(109, 28);
            // 
            // 粘贴ToolStripMenuItem1
            // 
            this.粘贴ToolStripMenuItem1.Name = "粘贴ToolStripMenuItem1";
            this.粘贴ToolStripMenuItem1.Size = new System.Drawing.Size(108, 24);
            this.粘贴ToolStripMenuItem1.Text = "粘贴";
            this.粘贴ToolStripMenuItem1.Click += new System.EventHandler(this.粘贴ToolStripMenuItem1_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "行";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "列二";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 400;
            // 
            // btn_completion
            // 
            this.btn_completion.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_completion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_completion.Font = new System.Drawing.Font("华文楷体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_completion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_completion.Location = new System.Drawing.Point(957, 272);
            this.btn_completion.Name = "btn_completion";
            this.btn_completion.Size = new System.Drawing.Size(121, 47);
            this.btn_completion.TabIndex = 18;
            this.btn_completion.Text = "列二补全";
            this.btn_completion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_completion.UseVisualStyleBackColor = false;
            this.btn_completion.Click += new System.EventHandler(this.btn_completion_Click);
            // 
            // txt_first
            // 
            this.txt_first.Location = new System.Drawing.Point(957, 216);
            this.txt_first.Name = "txt_first";
            this.txt_first.Size = new System.Drawing.Size(100, 25);
            this.txt_first.TabIndex = 19;
            // 
            // 行
            // 
            this.行.HeaderText = "行";
            this.行.Name = "行";
            // 
            // 列一
            // 
            this.列一.HeaderText = "列一";
            this.列一.Name = "列一";
            this.列一.Width = 200;
            // 
            // AddProgrammeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 589);
            this.Controls.Add(this.txt_first);
            this.Controls.Add(this.btn_completion);
            this.Controls.Add(this.dgv_ColumnTwo);
            this.Controls.Add(this.dgv_Random);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Name = "AddProgrammeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加方案";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Random)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ColumnTwo)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.DataGridView dgv_Random;
        private System.Windows.Forms.DataGridView dgv_ColumnTwo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button btn_completion;
        private System.Windows.Forms.TextBox txt_first;
        private System.Windows.Forms.DataGridViewTextBoxColumn 行;
        private System.Windows.Forms.DataGridViewTextBoxColumn 列一;
    }
}