namespace Iauto
{
    partial class cfgForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cfgForm));
            this.fileLabel = new System.Windows.Forms.Label();
            this.fileTb = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.fromTb = new System.Windows.Forms.TextBox();
            this.fromLabel = new System.Windows.Forms.Label();
            this.fromCheck = new System.Windows.Forms.CheckBox();
            this.toCheck = new System.Windows.Forms.CheckBox();
            this.toLabel = new System.Windows.Forms.Label();
            this.toTb = new System.Windows.Forms.TextBox();
            this.errCheck = new System.Windows.Forms.CheckBox();
            this.errLabel = new System.Windows.Forms.Label();
            this.errTb = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.continueLabel = new System.Windows.Forms.Label();
            this.yesRadio = new System.Windows.Forms.RadioButton();
            this.noRadio = new System.Windows.Forms.RadioButton();
            this.inputFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // fileLabel
            // 
            this.fileLabel.AutoSize = true;
            this.fileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileLabel.Location = new System.Drawing.Point(14, 98);
            this.fileLabel.Name = "fileLabel";
            this.fileLabel.Size = new System.Drawing.Size(64, 13);
            this.fileLabel.TabIndex = 0;
            this.fileLabel.Text = "Input File:";
            // 
            // fileTb
            // 
            this.fileTb.Location = new System.Drawing.Point(84, 95);
            this.fileTb.Name = "fileTb";
            this.fileTb.Size = new System.Drawing.Size(265, 20);
            this.fileTb.TabIndex = 1;
            // 
            // browseButton
            // 
            this.browseButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.browseButton.Font = new System.Drawing.Font("Microsoft Himalaya", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton.Location = new System.Drawing.Point(355, 95);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(28, 20);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = false;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(126, 8);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(156, 22);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Automation Title";
            // 
            // fromTb
            // 
            this.fromTb.Location = new System.Drawing.Point(84, 124);
            this.fromTb.Name = "fromTb";
            this.fromTb.ReadOnly = true;
            this.fromTb.Size = new System.Drawing.Size(40, 20);
            this.fromTb.TabIndex = 4;
            this.fromTb.Text = "Last";
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromLabel.Location = new System.Drawing.Point(40, 127);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(38, 13);
            this.fromLabel.TabIndex = 5;
            this.fromLabel.Text = "From:";
            // 
            // fromCheck
            // 
            this.fromCheck.AutoSize = true;
            this.fromCheck.Checked = true;
            this.fromCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fromCheck.Enabled = false;
            this.fromCheck.Location = new System.Drawing.Point(130, 126);
            this.fromCheck.Name = "fromCheck";
            this.fromCheck.Size = new System.Drawing.Size(58, 17);
            this.fromCheck.TabIndex = 6;
            this.fromCheck.Text = "default";
            this.fromCheck.UseVisualStyleBackColor = true;
            this.fromCheck.CheckedChanged += new System.EventHandler(this.fromCheck_CheckedChanged);
            // 
            // toCheck
            // 
            this.toCheck.AutoSize = true;
            this.toCheck.Checked = true;
            this.toCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toCheck.Enabled = false;
            this.toCheck.Location = new System.Drawing.Point(130, 149);
            this.toCheck.Name = "toCheck";
            this.toCheck.Size = new System.Drawing.Size(58, 17);
            this.toCheck.TabIndex = 9;
            this.toCheck.Text = "default";
            this.toCheck.UseVisualStyleBackColor = true;
            this.toCheck.CheckedChanged += new System.EventHandler(this.toCheck_CheckedChanged);
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toLabel.Location = new System.Drawing.Point(52, 150);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(26, 13);
            this.toLabel.TabIndex = 8;
            this.toLabel.Text = "To:";
            // 
            // toTb
            // 
            this.toTb.Location = new System.Drawing.Point(84, 147);
            this.toTb.Name = "toTb";
            this.toTb.ReadOnly = true;
            this.toTb.Size = new System.Drawing.Size(40, 20);
            this.toTb.TabIndex = 7;
            this.toTb.Text = "End";
            // 
            // errCheck
            // 
            this.errCheck.AutoSize = true;
            this.errCheck.Checked = true;
            this.errCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.errCheck.Location = new System.Drawing.Point(130, 172);
            this.errCheck.Name = "errCheck";
            this.errCheck.Size = new System.Drawing.Size(58, 17);
            this.errCheck.TabIndex = 12;
            this.errCheck.Text = "default";
            this.errCheck.UseVisualStyleBackColor = true;
            this.errCheck.CheckedChanged += new System.EventHandler(this.errCheck_CheckedChanged);
            // 
            // errLabel
            // 
            this.errLabel.AutoSize = true;
            this.errLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errLabel.Location = new System.Drawing.Point(8, 173);
            this.errLabel.Name = "errLabel";
            this.errLabel.Size = new System.Drawing.Size(70, 13);
            this.errLabel.TabIndex = 11;
            this.errLabel.Text = "Max errors:";
            // 
            // errTb
            // 
            this.errTb.Location = new System.Drawing.Point(84, 170);
            this.errTb.Name = "errTb";
            this.errTb.ReadOnly = true;
            this.errTb.Size = new System.Drawing.Size(40, 20);
            this.errTb.TabIndex = 10;
            this.errTb.Text = "50";
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.LightSlateGray;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(244, 134);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(127, 45);
            this.startButton.TabIndex = 13;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // continueLabel
            // 
            this.continueLabel.AutoSize = true;
            this.continueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continueLabel.Location = new System.Drawing.Point(70, 39);
            this.continueLabel.Name = "continueLabel";
            this.continueLabel.Size = new System.Drawing.Size(279, 15);
            this.continueLabel.TabIndex = 14;
            this.continueLabel.Text = "Continue from last session(recommended)";
            // 
            // yesRadio
            // 
            this.yesRadio.AutoSize = true;
            this.yesRadio.Checked = true;
            this.yesRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yesRadio.Location = new System.Drawing.Point(158, 57);
            this.yesRadio.Name = "yesRadio";
            this.yesRadio.Size = new System.Drawing.Size(56, 17);
            this.yesRadio.TabIndex = 15;
            this.yesRadio.TabStop = true;
            this.yesRadio.Text = "Yes /";
            this.yesRadio.UseVisualStyleBackColor = true;
            this.yesRadio.CheckedChanged += new System.EventHandler(this.yesRadio_CheckedChanged);
            // 
            // noRadio
            // 
            this.noRadio.AutoSize = true;
            this.noRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noRadio.Location = new System.Drawing.Point(208, 57);
            this.noRadio.Name = "noRadio";
            this.noRadio.Size = new System.Drawing.Size(41, 17);
            this.noRadio.TabIndex = 16;
            this.noRadio.Text = "No";
            this.noRadio.UseVisualStyleBackColor = true;
            // 
            // inputFileDialog
            // 
            this.inputFileDialog.FileName = "input.csv";
            // 
            // cfgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(403, 213);
            this.Controls.Add(this.noRadio);
            this.Controls.Add(this.yesRadio);
            this.Controls.Add(this.continueLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.errCheck);
            this.Controls.Add(this.errLabel);
            this.Controls.Add(this.errTb);
            this.Controls.Add(this.toCheck);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.toTb);
            this.Controls.Add(this.fromCheck);
            this.Controls.Add(this.fromLabel);
            this.Controls.Add(this.fromTb);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.fileTb);
            this.Controls.Add(this.fileLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "cfgForm";
            this.Text = "Automation Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fileLabel;
        private System.Windows.Forms.TextBox fileTb;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox fromTb;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.CheckBox fromCheck;
        private System.Windows.Forms.CheckBox toCheck;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.TextBox toTb;
        private System.Windows.Forms.CheckBox errCheck;
        private System.Windows.Forms.Label errLabel;
        private System.Windows.Forms.TextBox errTb;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label continueLabel;
        private System.Windows.Forms.RadioButton yesRadio;
        private System.Windows.Forms.RadioButton noRadio;
        private System.Windows.Forms.OpenFileDialog inputFileDialog;
    }
}

