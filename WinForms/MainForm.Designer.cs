namespace WinForms {
    partial class MainForm {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.TB_MathFunc = new System.Windows.Forms.TextBox();
			this.B_Calculate = new System.Windows.Forms.Button();
			this.B_LoadFile = new System.Windows.Forms.Button();
			this.GB_MathPars = new System.Windows.Forms.GroupBox();
			this.B_Update = new System.Windows.Forms.Button();
			this.TB_A = new System.Windows.Forms.TextBox();
			this.TB_B = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.CLB_Methods = new System.Windows.Forms.CheckedListBox();
			this.CMS_MainForm = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toggleALLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GB_IntegrationOptions = new System.Windows.Forms.GroupBox();
			this.LB_Methods = new System.Windows.Forms.Label();
			this.TB_Delta = new System.Windows.Forms.TextBox();
			this.LB_Delta = new System.Windows.Forms.Label();
			this.enableALLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GB_MathPars.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.CMS_MainForm.SuspendLayout();
			this.GB_IntegrationOptions.SuspendLayout();
			this.SuspendLayout();
			// 
			// TB_MathFunc
			// 
			this.TB_MathFunc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TB_MathFunc.Location = new System.Drawing.Point(77, 79);
			this.TB_MathFunc.Name = "TB_MathFunc";
			this.TB_MathFunc.Size = new System.Drawing.Size(240, 27);
			this.TB_MathFunc.TabIndex = 0;
			this.TB_MathFunc.Text = "sin(x)";
			this.TB_MathFunc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_MathFunc_KeyPress);
			// 
			// B_Calculate
			// 
			this.B_Calculate.Location = new System.Drawing.Point(323, 79);
			this.B_Calculate.Name = "B_Calculate";
			this.B_Calculate.Size = new System.Drawing.Size(55, 27);
			this.B_Calculate.TabIndex = 3;
			this.B_Calculate.Text = "=";
			this.B_Calculate.UseVisualStyleBackColor = true;
			this.B_Calculate.Click += new System.EventHandler(this.B_Calculate_Click);
			// 
			// B_LoadFile
			// 
			this.B_LoadFile.Location = new System.Drawing.Point(284, 188);
			this.B_LoadFile.Name = "B_LoadFile";
			this.B_LoadFile.Size = new System.Drawing.Size(94, 29);
			this.B_LoadFile.TabIndex = 4;
			this.B_LoadFile.Text = "Load File";
			this.B_LoadFile.UseVisualStyleBackColor = true;
			this.B_LoadFile.Click += new System.EventHandler(this.B_LoadFile_Click);
			// 
			// GB_MathPars
			// 
			this.GB_MathPars.Controls.Add(this.B_Update);
			this.GB_MathPars.Controls.Add(this.B_LoadFile);
			this.GB_MathPars.Controls.Add(this.TB_A);
			this.GB_MathPars.Controls.Add(this.TB_B);
			this.GB_MathPars.Controls.Add(this.pictureBox1);
			this.GB_MathPars.Controls.Add(this.TB_MathFunc);
			this.GB_MathPars.Controls.Add(this.B_Calculate);
			this.GB_MathPars.Location = new System.Drawing.Point(12, 12);
			this.GB_MathPars.Name = "GB_MathPars";
			this.GB_MathPars.Size = new System.Drawing.Size(385, 223);
			this.GB_MathPars.TabIndex = 3;
			this.GB_MathPars.TabStop = false;
			this.GB_MathPars.Text = "Parser";
			// 
			// B_Update
			// 
			this.B_Update.Location = new System.Drawing.Point(284, 153);
			this.B_Update.Name = "B_Update";
			this.B_Update.Size = new System.Drawing.Size(94, 29);
			this.B_Update.TabIndex = 5;
			this.B_Update.Text = "Update";
			this.B_Update.UseVisualStyleBackColor = true;
			this.B_Update.Visible = false;
			this.B_Update.Click += new System.EventHandler(this.B_Update_Click);
			// 
			// TB_A
			// 
			this.TB_A.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TB_A.Location = new System.Drawing.Point(6, 137);
			this.TB_A.Name = "TB_A";
			this.TB_A.Size = new System.Drawing.Size(65, 27);
			this.TB_A.TabIndex = 1;
			this.TB_A.Text = "-1";
			this.TB_A.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TB_B
			// 
			this.TB_B.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TB_B.Location = new System.Drawing.Point(6, 23);
			this.TB_B.Name = "TB_B";
			this.TB_B.Size = new System.Drawing.Size(65, 27);
			this.TB_B.TabIndex = 2;
			this.TB_B.Text = "10";
			this.TB_B.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::WinForms.Properties.Resources.integral;
			this.pictureBox1.Location = new System.Drawing.Point(6, 56);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(65, 75);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// CLB_Methods
			// 
			this.CLB_Methods.CheckOnClick = true;
			this.CLB_Methods.ContextMenuStrip = this.CMS_MainForm;
			this.CLB_Methods.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CLB_Methods.Items.AddRange(new object[] {
            "Left Rectangle",
            "Right Rectangle",
            "Midpoint Rectangle",
            "Trapezoid",
            "Simpson"});
			this.CLB_Methods.Location = new System.Drawing.Point(6, 103);
			this.CLB_Methods.Name = "CLB_Methods";
			this.CLB_Methods.Size = new System.Drawing.Size(173, 114);
			this.CLB_Methods.TabIndex = 6;
			this.CLB_Methods.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CLB_Methods_ItemCheck);
			// 
			// CMS_MainForm
			// 
			this.CMS_MainForm.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.CMS_MainForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleALLToolStripMenuItem,
            this.enableALLToolStripMenuItem});
			this.CMS_MainForm.Name = "CMS_MainForm";
			this.CMS_MainForm.Size = new System.Drawing.Size(211, 80);
			// 
			// toggleALLToolStripMenuItem
			// 
			this.toggleALLToolStripMenuItem.Name = "toggleALLToolStripMenuItem";
			this.toggleALLToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
			this.toggleALLToolStripMenuItem.Text = "Disable ALL";
			this.toggleALLToolStripMenuItem.Click += new System.EventHandler(this.toggleALLToolStripMenuItem_Click);
			// 
			// GB_IntegrationOptions
			// 
			this.GB_IntegrationOptions.Controls.Add(this.LB_Methods);
			this.GB_IntegrationOptions.Controls.Add(this.TB_Delta);
			this.GB_IntegrationOptions.Controls.Add(this.CLB_Methods);
			this.GB_IntegrationOptions.Controls.Add(this.LB_Delta);
			this.GB_IntegrationOptions.Location = new System.Drawing.Point(403, 12);
			this.GB_IntegrationOptions.Name = "GB_IntegrationOptions";
			this.GB_IntegrationOptions.Size = new System.Drawing.Size(185, 223);
			this.GB_IntegrationOptions.TabIndex = 5;
			this.GB_IntegrationOptions.TabStop = false;
			this.GB_IntegrationOptions.Text = "Integration Options";
			// 
			// LB_Methods
			// 
			this.LB_Methods.AutoSize = true;
			this.LB_Methods.Location = new System.Drawing.Point(6, 80);
			this.LB_Methods.Name = "LB_Methods";
			this.LB_Methods.Size = new System.Drawing.Size(147, 20);
			this.LB_Methods.TabIndex = 6;
			this.LB_Methods.Text = "Integration Methods:";
			// 
			// TB_Delta
			// 
			this.TB_Delta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.TB_Delta.Location = new System.Drawing.Point(60, 32);
			this.TB_Delta.Name = "TB_Delta";
			this.TB_Delta.Size = new System.Drawing.Size(94, 27);
			this.TB_Delta.TabIndex = 5;
			this.TB_Delta.Text = "0,001";
			// 
			// LB_Delta
			// 
			this.LB_Delta.AutoSize = true;
			this.LB_Delta.Location = new System.Drawing.Point(6, 35);
			this.LB_Delta.Name = "LB_Delta";
			this.LB_Delta.Size = new System.Drawing.Size(48, 20);
			this.LB_Delta.TabIndex = 0;
			this.LB_Delta.Text = "Delta:";
			// 
			// enableALLToolStripMenuItem
			// 
			this.enableALLToolStripMenuItem.Name = "enableALLToolStripMenuItem";
			this.enableALLToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
			this.enableALLToolStripMenuItem.Text = "Enable ALL";
			this.enableALLToolStripMenuItem.Click += new System.EventHandler(this.toggleALLToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(599, 246);
			this.Controls.Add(this.GB_IntegrationOptions);
			this.Controls.Add(this.GB_MathPars);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Numerical Integration";
			this.GB_MathPars.ResumeLayout(false);
			this.GB_MathPars.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.CMS_MainForm.ResumeLayout(false);
			this.GB_IntegrationOptions.ResumeLayout(false);
			this.GB_IntegrationOptions.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private TextBox TB_MathFunc;
        private Button B_Calculate;
        private Button B_LoadFile;
        private GroupBox GB_MathPars;
        private PictureBox pictureBox1;
        private TextBox TB_A;
        private TextBox TB_B;
        private CheckedListBox CLB_Methods;
        private GroupBox GB_IntegrationOptions;
        private TextBox TB_Delta;
        private Label LB_Delta;
        private Label LB_Methods;
		private Button B_Update;
		private ContextMenuStrip CMS_MainForm;
		private ToolStripMenuItem toggleALLToolStripMenuItem;
		private ToolStripMenuItem enableALLToolStripMenuItem;
	}
}