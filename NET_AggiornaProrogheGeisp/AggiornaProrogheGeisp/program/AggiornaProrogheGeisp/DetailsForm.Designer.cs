namespace AggiornaProrogheGeisp
{
    partial class DetailsForm
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
            this.DetailesOkButton = new System.Windows.Forms.Button();
            this.ExcelTextBox = new System.Windows.Forms.TextBox();
            this.Mdb2TextBox = new System.Windows.Forms.TextBox();
            this.MdbTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DetailesOkButton
            // 
            this.DetailesOkButton.Location = new System.Drawing.Point(502, 139);
            this.DetailesOkButton.Name = "DetailesOkButton";
            this.DetailesOkButton.Size = new System.Drawing.Size(75, 23);
            this.DetailesOkButton.TabIndex = 8;
            this.DetailesOkButton.Text = "OK";
            this.DetailesOkButton.UseVisualStyleBackColor = true;
            this.DetailesOkButton.Click += new System.EventHandler(this.DetailesOkButton_Click);
            // 
            // ExcelTextBox
            // 
            this.ExcelTextBox.Location = new System.Drawing.Point(147, 71);
            this.ExcelTextBox.Name = "ExcelTextBox";
            this.ExcelTextBox.Size = new System.Drawing.Size(466, 20);
            this.ExcelTextBox.TabIndex = 37;
            // 
            // Mdb2TextBox
            // 
            this.Mdb2TextBox.Location = new System.Drawing.Point(147, 105);
            this.Mdb2TextBox.Name = "Mdb2TextBox";
            this.Mdb2TextBox.Size = new System.Drawing.Size(466, 20);
            this.Mdb2TextBox.TabIndex = 36;
            // 
            // MdbTextBox
            // 
            this.MdbTextBox.Location = new System.Drawing.Point(147, 37);
            this.MdbTextBox.Name = "MdbTextBox";
            this.MdbTextBox.Size = new System.Drawing.Size(466, 20);
            this.MdbTextBox.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(48, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 18);
            this.label4.TabIndex = 34;
            this.label4.Text = "Input Path :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 18);
            this.label3.TabIndex = 33;
            this.label3.Text = "Nr Rapporti DB :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 18);
            this.label2.TabIndex = 32;
            this.label2.Text = "Geisp DB :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 18);
            this.label1.TabIndex = 31;
            this.label1.Text = "Nome :";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.NameTextBox.Location = new System.Drawing.Point(147, 7);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(466, 20);
            this.NameTextBox.TabIndex = 30;
            // 
            // DetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 169);
            this.Controls.Add(this.ExcelTextBox);
            this.Controls.Add(this.Mdb2TextBox);
            this.Controls.Add(this.MdbTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.DetailesOkButton);
            this.Name = "DetailsForm";
            this.Text = "Dettagli Geisp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DetailesOkButton;
        private System.Windows.Forms.TextBox ExcelTextBox;
        private System.Windows.Forms.TextBox Mdb2TextBox;
        private System.Windows.Forms.TextBox MdbTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NameTextBox;
    }
}