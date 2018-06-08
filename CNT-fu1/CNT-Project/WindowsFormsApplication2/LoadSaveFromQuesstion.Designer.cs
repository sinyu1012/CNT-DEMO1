namespace 彩牛通
{
    partial class LoadSaveFromQuesstion
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
            this.btn_tempSave = new System.Windows.Forms.Button();
            this.btn_TotalSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_tempSave
            // 
            this.btn_tempSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_tempSave.Location = new System.Drawing.Point(35, 64);
            this.btn_tempSave.Name = "btn_tempSave";
            this.btn_tempSave.Size = new System.Drawing.Size(109, 52);
            this.btn_tempSave.TabIndex = 0;
            this.btn_tempSave.Text = "载入暂保存";
            this.btn_tempSave.UseVisualStyleBackColor = true;
            this.btn_tempSave.Click += new System.EventHandler(this.btn_tempSave_Click);
            // 
            // btn_TotalSave
            // 
            this.btn_TotalSave.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btn_TotalSave.Location = new System.Drawing.Point(211, 64);
            this.btn_TotalSave.Name = "btn_TotalSave";
            this.btn_TotalSave.Size = new System.Drawing.Size(109, 52);
            this.btn_TotalSave.TabIndex = 1;
            this.btn_TotalSave.Text = "载入总保存";
            this.btn_TotalSave.UseVisualStyleBackColor = true;
            this.btn_TotalSave.Click += new System.EventHandler(this.btn_TotalSave_Click);
            // 
            // LoadSaveFromQuesstion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 171);
            this.Controls.Add(this.btn_TotalSave);
            this.Controls.Add(this.btn_tempSave);
            this.Name = "LoadSaveFromQuesstion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请选择";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_tempSave;
        private System.Windows.Forms.Button btn_TotalSave;
    }
}