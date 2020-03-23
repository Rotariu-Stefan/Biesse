namespace AggiornaProrogheGeisp
{
    partial class NewDB
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
            this.textBoxMdbPath = new System.Windows.Forms.TextBox();
            this.labelAccesFile = new System.Windows.Forms.Label();
            this.buttonLookMdb = new System.Windows.Forms.Button();
            this.labelExcelPath = new System.Windows.Forms.Label();
            this.textBoxExcelPath = new System.Windows.Forms.TextBox();
            this.buttonExcelPath = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelSecondaryMdb = new System.Windows.Forms.Label();
            this.textBoxMdb2Path = new System.Windows.Forms.TextBox();
            this.buttonLookMdb2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxMdbPath
            // 
            this.textBoxMdbPath.Location = new System.Drawing.Point(127, 27);
            this.textBoxMdbPath.Name = "textBoxMdbPath";
            this.textBoxMdbPath.Size = new System.Drawing.Size(298, 20);
            this.textBoxMdbPath.TabIndex = 0;
            // 
            // labelAccesFile
            // 
            this.labelAccesFile.AutoSize = true;
            this.labelAccesFile.Location = new System.Drawing.Point(12, 34);
            this.labelAccesFile.Name = "labelAccesFile";
            this.labelAccesFile.Size = new System.Drawing.Size(56, 13);
            this.labelAccesFile.TabIndex = 1;
            this.labelAccesFile.Text = "Mdb Path:";
            // 
            // buttonLookMdb
            // 
            this.buttonLookMdb.Location = new System.Drawing.Point(454, 24);
            this.buttonLookMdb.Name = "buttonLookMdb";
            this.buttonLookMdb.Size = new System.Drawing.Size(59, 23);
            this.buttonLookMdb.TabIndex = 2;
            this.buttonLookMdb.Text = "...";
            this.buttonLookMdb.UseVisualStyleBackColor = true;
            this.buttonLookMdb.Click += new System.EventHandler(this.buttonLookMdb_Click);
            // 
            // labelExcelPath
            // 
            this.labelExcelPath.AutoSize = true;
            this.labelExcelPath.Location = new System.Drawing.Point(15, 89);
            this.labelExcelPath.Name = "labelExcelPath";
            this.labelExcelPath.Size = new System.Drawing.Size(61, 13);
            this.labelExcelPath.TabIndex = 3;
            this.labelExcelPath.Text = "Excel Path:";
            // 
            // textBoxExcelPath
            // 
            this.textBoxExcelPath.Location = new System.Drawing.Point(127, 81);
            this.textBoxExcelPath.Name = "textBoxExcelPath";
            this.textBoxExcelPath.Size = new System.Drawing.Size(298, 20);
            this.textBoxExcelPath.TabIndex = 4;
            // 
            // buttonExcelPath
            // 
            this.buttonExcelPath.Location = new System.Drawing.Point(454, 77);
            this.buttonExcelPath.Name = "buttonExcelPath";
            this.buttonExcelPath.Size = new System.Drawing.Size(59, 23);
            this.buttonExcelPath.TabIndex = 5;
            this.buttonExcelPath.Text = "...";
            this.buttonExcelPath.UseVisualStyleBackColor = true;
            this.buttonExcelPath.Click += new System.EventHandler(this.buttonExcelPath_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(454, 216);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(59, 44);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(15, 142);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 7;
            this.labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(127, 142);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(298, 20);
            this.textBoxName.TabIndex = 8;
            // 
            // labelSecondaryMdb
            // 
            this.labelSecondaryMdb.AutoSize = true;
            this.labelSecondaryMdb.Location = new System.Drawing.Point(18, 181);
            this.labelSecondaryMdb.Name = "labelSecondaryMdb";
            this.labelSecondaryMdb.Size = new System.Drawing.Size(59, 13);
            this.labelSecondaryMdb.TabIndex = 9;
            this.labelSecondaryMdb.Text = "Mdb2 Path";
            // 
            // textBoxMdb2Path
            // 
            this.textBoxMdb2Path.Location = new System.Drawing.Point(127, 181);
            this.textBoxMdb2Path.Name = "textBoxMdb2Path";
            this.textBoxMdb2Path.Size = new System.Drawing.Size(298, 20);
            this.textBoxMdb2Path.TabIndex = 10;
            // 
            // buttonLookMdb2
            // 
            this.buttonLookMdb2.Location = new System.Drawing.Point(454, 181);
            this.buttonLookMdb2.Name = "buttonLookMdb2";
            this.buttonLookMdb2.Size = new System.Drawing.Size(59, 23);
            this.buttonLookMdb2.TabIndex = 11;
            this.buttonLookMdb2.Text = "...";
            this.buttonLookMdb2.UseVisualStyleBackColor = true;
            this.buttonLookMdb2.Click += new System.EventHandler(this.buttonLookMdb2_Click);
            // 
            // NewDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 261);
            this.Controls.Add(this.buttonLookMdb2);
            this.Controls.Add(this.textBoxMdb2Path);
            this.Controls.Add(this.labelSecondaryMdb);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonExcelPath);
            this.Controls.Add(this.textBoxExcelPath);
            this.Controls.Add(this.labelExcelPath);
            this.Controls.Add(this.buttonLookMdb);
            this.Controls.Add(this.labelAccesFile);
            this.Controls.Add(this.textBoxMdbPath);
            this.Name = "NewDB";
            this.Text = "NewDB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMdbPath;
        private System.Windows.Forms.Label labelAccesFile;
        private System.Windows.Forms.Button buttonLookMdb;
        private System.Windows.Forms.Label labelExcelPath;
        private System.Windows.Forms.TextBox textBoxExcelPath;
        private System.Windows.Forms.Button buttonExcelPath;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelSecondaryMdb;
        private System.Windows.Forms.TextBox textBoxMdb2Path;
        private System.Windows.Forms.Button buttonLookMdb2;
    }
}