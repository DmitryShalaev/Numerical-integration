namespace WinForms {
    partial class Form {
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
            this.PB_1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PB_1)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_1
            // 
            this.PB_1.Location = new System.Drawing.Point(12, 12);
            this.PB_1.Name = "PB_1";
            this.PB_1.Size = new System.Drawing.Size(800, 800);
            this.PB_1.TabIndex = 0;
            this.PB_1.TabStop = false;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 825);
            this.Controls.Add(this.PB_1);
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.PB_1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox PB_1;
    }
}