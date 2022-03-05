namespace WindowForm {
    partial class Window {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			this.PB = new System.Windows.Forms.PictureBox();
			this.CMS_WindowForm = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetZizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.L_Answer = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.PB)).BeginInit();
			this.CMS_WindowForm.SuspendLayout();
			this.SuspendLayout();
			// 
			// PB
			// 
			this.PB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PB.Location = new System.Drawing.Point(0, 0);
			this.PB.Margin = new System.Windows.Forms.Padding(0);
			this.PB.Name = "PB";
			this.PB.Size = new System.Drawing.Size(661, 551);
			this.PB.TabIndex = 1;
			this.PB.TabStop = false;
			this.PB.DoubleClick += new System.EventHandler(this.PB_DoubleClick);
			// 
			// CMS_WindowForm
			// 
			this.CMS_WindowForm.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.CMS_WindowForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.resetZizeToolStripMenuItem});
			this.CMS_WindowForm.Name = "CMS_WindowForm";
			this.CMS_WindowForm.Size = new System.Drawing.Size(130, 52);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(129, 24);
			this.saveToolStripMenuItem.Text = "Save As";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// resetZizeToolStripMenuItem
			// 
			this.resetZizeToolStripMenuItem.Name = "resetZizeToolStripMenuItem";
			this.resetZizeToolStripMenuItem.Size = new System.Drawing.Size(129, 24);
			this.resetZizeToolStripMenuItem.Text = "Reset";
			this.resetZizeToolStripMenuItem.Click += new System.EventHandler(this.resetSizeToolStripMenuItem_Click);
			// 
			// L_Answer
			// 
			this.L_Answer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.L_Answer.AutoSize = true;
			this.L_Answer.Location = new System.Drawing.Point(0, 531);
			this.L_Answer.Margin = new System.Windows.Forms.Padding(0);
			this.L_Answer.Name = "L_Answer";
			this.L_Answer.Size = new System.Drawing.Size(60, 20);
			this.L_Answer.TabIndex = 2;
			this.L_Answer.Text = "Answer:";
			// 
			// Window
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 551);
			this.ContextMenuStrip = this.CMS_WindowForm;
			this.Controls.Add(this.L_Answer);
			this.Controls.Add(this.PB);
			this.MinimizeBox = false;
			this.Name = "Window";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "WindowForm";
			this.SizeChanged += new System.EventHandler(this.Item_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.PB)).EndInit();
			this.CMS_WindowForm.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        public PictureBox PB;
        private Label L_Answer;
        private ContextMenuStrip CMS_WindowForm;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem resetZizeToolStripMenuItem;
    }
}