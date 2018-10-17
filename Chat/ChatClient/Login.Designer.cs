namespace ChatClient
{
    partial class Login
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
            this.connectAndLogin = new System.Windows.Forms.Button();
            this.usrBox = new System.Windows.Forms.TextBox();
            this.passBox = new System.Windows.Forms.TextBox();
            this.register = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connectAndLogin
            // 
            this.connectAndLogin.Location = new System.Drawing.Point(8, 61);
            this.connectAndLogin.Name = "connectAndLogin";
            this.connectAndLogin.Size = new System.Drawing.Size(158, 23);
            this.connectAndLogin.TabIndex = 6;
            this.connectAndLogin.Text = "log in";
            this.connectAndLogin.UseVisualStyleBackColor = true;
            this.connectAndLogin.Click += new System.EventHandler(this.connectAndLogin_Click);
            // 
            // usrBox
            // 
            this.usrBox.Location = new System.Drawing.Point(8, 9);
            this.usrBox.Name = "usrBox";
            this.usrBox.Size = new System.Drawing.Size(390, 20);
            this.usrBox.TabIndex = 5;
            // 
            // passBox
            // 
            this.passBox.Location = new System.Drawing.Point(8, 35);
            this.passBox.Name = "passBox";
            this.passBox.Size = new System.Drawing.Size(390, 20);
            this.passBox.TabIndex = 7;
            // 
            // register
            // 
            this.register.Location = new System.Drawing.Point(172, 61);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(226, 23);
            this.register.TabIndex = 8;
            this.register.Text = "no account? register!";
            this.register.UseVisualStyleBackColor = true;
            this.register.Click += new System.EventHandler(this.register_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 90);
            this.Controls.Add(this.register);
            this.Controls.Add(this.passBox);
            this.Controls.Add(this.connectAndLogin);
            this.Controls.Add(this.usrBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectAndLogin;
        private System.Windows.Forms.TextBox usrBox;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.Button register;
    }
}