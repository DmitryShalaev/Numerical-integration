namespace AuthorizationForm {
    partial class Authorization {
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
            this.label1 = new System.Windows.Forms.Label();
            this.TB_Login = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Password = new System.Windows.Forms.TextBox();
            this.B_LogIn = new System.Windows.Forms.Button();
            this.B_SingUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Login:";
            // 
            // TB_Login
            // 
            this.TB_Login.Location = new System.Drawing.Point(12, 32);
            this.TB_Login.Name = "TB_Login";
            this.TB_Login.Size = new System.Drawing.Size(200, 27);
            this.TB_Login.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // TB_Password
            // 
            this.TB_Password.Location = new System.Drawing.Point(12, 85);
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.Size = new System.Drawing.Size(200, 27);
            this.TB_Password.TabIndex = 2;
            // 
            // B_LogIn
            // 
            this.B_LogIn.Location = new System.Drawing.Point(218, 30);
            this.B_LogIn.Name = "B_LogIn";
            this.B_LogIn.Size = new System.Drawing.Size(94, 29);
            this.B_LogIn.TabIndex = 4;
            this.B_LogIn.Text = "Log In";
            this.B_LogIn.UseVisualStyleBackColor = true;
            this.B_LogIn.Click += new System.EventHandler(this.B_LogIn_Click);
            // 
            // B_SingUp
            // 
            this.B_SingUp.Location = new System.Drawing.Point(218, 83);
            this.B_SingUp.Name = "B_SingUp";
            this.B_SingUp.Size = new System.Drawing.Size(94, 29);
            this.B_SingUp.TabIndex = 5;
            this.B_SingUp.Text = "Sing Up";
            this.B_SingUp.UseVisualStyleBackColor = true;
            // 
            // Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 128);
            this.Controls.Add(this.B_SingUp);
            this.Controls.Add(this.B_LogIn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_Password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Authorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authorization";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TextBox TB_Login;
        private Label label2;
        private TextBox TB_Password;
        private Button B_LogIn;
        private Button B_SingUp;
    }
}