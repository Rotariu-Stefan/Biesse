namespace IExplorerControlsTest
{
    partial class Form1
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
            if (w != null)
            {
                w.ClosePage();
                base.Dispose(disposing);
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.sites = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.query = new System.Windows.Forms.TextBox();
            this.IndexLogin = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.forward = new System.Windows.Forms.Button();
            this.BeginAutomation = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.lb = new System.Windows.Forms.ListBox();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 259);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "close Ie";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(208, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "open with IE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // sites
            // 
            this.sites.FormattingEnabled = true;
            this.sites.Items.AddRange(new object[] {
            "www.last.fm",
            "www.youtube.com",
            "www.msdn.com",
            "www.google.com/imghp"});
            this.sites.Location = new System.Drawing.Point(27, 28);
            this.sites.Name = "sites";
            this.sites.Size = new System.Drawing.Size(175, 21);
            this.sites.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1, 272);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // query
            // 
            this.query.Location = new System.Drawing.Point(27, 55);
            this.query.Name = "query";
            this.query.Size = new System.Drawing.Size(147, 20);
            this.query.TabIndex = 7;
            // 
            // IndexLogin
            // 
            this.IndexLogin.Location = new System.Drawing.Point(208, 57);
            this.IndexLogin.Name = "IndexLogin";
            this.IndexLogin.Size = new System.Drawing.Size(75, 23);
            this.IndexLogin.TabIndex = 9;
            this.IndexLogin.Text = "Login";
            this.IndexLogin.UseVisualStyleBackColor = true;
            this.IndexLogin.Click += new System.EventHandler(this.IndexLogin_Click);
            // 
            // back
            // 
            this.back.Location = new System.Drawing.Point(27, 138);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(75, 23);
            this.back.TabIndex = 10;
            this.back.Text = "back";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // forward
            // 
            this.forward.Location = new System.Drawing.Point(127, 138);
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(75, 23);
            this.forward.TabIndex = 11;
            this.forward.Text = "Forward";
            this.forward.UseVisualStyleBackColor = true;
            this.forward.Click += new System.EventHandler(this.forward_Click);
            // 
            // BeginAutomation
            // 
            this.BeginAutomation.Location = new System.Drawing.Point(309, 115);
            this.BeginAutomation.Name = "BeginAutomation";
            this.BeginAutomation.Size = new System.Drawing.Size(116, 23);
            this.BeginAutomation.TabIndex = 12;
            this.BeginAutomation.Text = "BeginAutomation";
            this.BeginAutomation.UseVisualStyleBackColor = true;
            this.BeginAutomation.Click += new System.EventHandler(this.BeginAutomation_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(127, 167);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(116, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "RecentTracks";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // lb
            // 
            this.lb.FormattingEnabled = true;
            this.lb.Location = new System.Drawing.Point(431, 55);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(208, 199);
            this.lb.TabIndex = 14;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(127, 197);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(102, 23);
            this.button5.TabIndex = 15;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 358);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.BeginAutomation);
            this.Controls.Add(this.forward);
            this.Controls.Add(this.back);
            this.Controls.Add(this.IndexLogin);
            this.Controls.Add(this.query);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.sites);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox sites;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox query;
        private System.Windows.Forms.Button IndexLogin;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button forward;
        private System.Windows.Forms.Button BeginAutomation;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox lb;
        private System.Windows.Forms.Button button5;
    }
}

