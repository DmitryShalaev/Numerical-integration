namespace SizeForm {
    partial class SetSizeForm {
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
			this.B_OK = new System.Windows.Forms.Button();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.L_Width = new System.Windows.Forms.Label();
			this.L_Height = new System.Windows.Forms.Label();
			this.TB_Width = new System.Windows.Forms.TextBox();
			this.TB_Height = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// B_OK
			// 
			this.B_OK.Location = new System.Drawing.Point(43, 65);
			this.B_OK.Name = "B_OK";
			this.B_OK.Size = new System.Drawing.Size(94, 29);
			this.B_OK.TabIndex = 0;
			this.B_OK.Text = "OK";
			this.B_OK.UseVisualStyleBackColor = true;
			this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Location = new System.Drawing.Point(174, 65);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(94, 29);
			this.B_Cancel.TabIndex = 1;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// L_Width
			// 
			this.L_Width.AutoSize = true;
			this.L_Width.Location = new System.Drawing.Point(12, 9);
			this.L_Width.Name = "L_Width";
			this.L_Width.Size = new System.Drawing.Size(49, 20);
			this.L_Width.TabIndex = 2;
			this.L_Width.Text = "Width";
			// 
			// L_Height
			// 
			this.L_Height.AutoSize = true;
			this.L_Height.Location = new System.Drawing.Point(143, 9);
			this.L_Height.Name = "L_Height";
			this.L_Height.Size = new System.Drawing.Size(54, 20);
			this.L_Height.TabIndex = 3;
			this.L_Height.Text = "Height";
			// 
			// TB_Width
			// 
			this.TB_Width.Location = new System.Drawing.Point(12, 32);
			this.TB_Width.Name = "TB_Width";
			this.TB_Width.Size = new System.Drawing.Size(125, 27);
			this.TB_Width.TabIndex = 4;
			// 
			// TB_Height
			// 
			this.TB_Height.Location = new System.Drawing.Point(143, 32);
			this.TB_Height.Name = "TB_Height";
			this.TB_Height.Size = new System.Drawing.Size(125, 27);
			this.TB_Height.TabIndex = 5;
			// 
			// SetSizeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(276, 103);
			this.Controls.Add(this.TB_Height);
			this.Controls.Add(this.TB_Width);
			this.Controls.Add(this.L_Height);
			this.Controls.Add(this.L_Width);
			this.Controls.Add(this.B_Cancel);
			this.Controls.Add(this.B_OK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "SetSizeForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Size";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private Button B_OK;
        private Button B_Cancel;
        private Label L_Width;
        private Label L_Height;
        private TextBox TB_Width;
        private TextBox TB_Height;
    }
}