namespace ChatClient
{
    partial class Register
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
            this.passBox = new System.Windows.Forms.TextBox();
            this.registerAcc = new System.Windows.Forms.Button();
            this.usrBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // passBox
            // 
            this.passBox.Location = new System.Drawing.Point(8, 35);
            this.passBox.Name = "passBox";
            this.passBox.Size = new System.Drawing.Size(351, 20);
            this.passBox.TabIndex = 11;
            // 
            // registerAcc
            // 
            this.registerAcc.Location = new System.Drawing.Point(8, 61);
            this.registerAcc.Name = "registerAcc";
            this.registerAcc.Size = new System.Drawing.Size(351, 23);
            this.registerAcc.TabIndex = 10;
            this.registerAcc.Text = "register";
            this.registerAcc.UseVisualStyleBackColor = true;
            this.registerAcc.Click += new System.EventHandler(this.registerAcc_Click);
            // 
            // usrBox
            // 
            this.usrBox.Location = new System.Drawing.Point(8, 9);
            this.usrBox.Name = "usrBox";
            this.usrBox.Size = new System.Drawing.Size(351, 20);
            this.usrBox.TabIndex = 9;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 90);
            this.Controls.Add(this.passBox);
            this.Controls.Add(this.registerAcc);
            this.Controls.Add(this.usrBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Register";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.Button registerAcc;
        private System.Windows.Forms.TextBox usrBox;
    }
}