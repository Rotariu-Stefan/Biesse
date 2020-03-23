namespace AggiornaProrogheGeisp
{
    partial class Configurazione
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configurazione));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGenerale = new System.Windows.Forms.TabPage();
            this.LoadLogPathbutton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.MinutesComboBox = new System.Windows.Forms.ComboBox();
            this.HourComboBox = new System.Windows.Forms.ComboBox();
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.LogLabel = new System.Windows.Forms.Label();
            this.tabPageGEISP = new System.Windows.Forms.TabPage();
            this.GeispRemoveAllButton = new System.Windows.Forms.Button();
            this.GeispAddButton = new System.Windows.Forms.Button();
            this.GeispRemoveButton = new System.Windows.Forms.Button();
            this.GeispEditButton = new System.Windows.Forms.Button();
            this.GeispDettagliButton = new System.Windows.Forms.Button();
            this.GeispListBox = new System.Windows.Forms.ListBox();
            this.AnnulareButton = new System.Windows.Forms.Button();
            this.ApplicareButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageGenerale.SuspendLayout();
            this.tabPageGEISP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGenerale);
            this.tabControl1.Controls.Add(this.tabPageGEISP);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(403, 247);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageGenerale
            // 
            this.tabPageGenerale.Controls.Add(this.LoadLogPathbutton);
            this.tabPageGenerale.Controls.Add(this.label14);
            this.tabPageGenerale.Controls.Add(this.label13);
            this.tabPageGenerale.Controls.Add(this.label12);
            this.tabPageGenerale.Controls.Add(this.MinutesComboBox);
            this.tabPageGenerale.Controls.Add(this.HourComboBox);
            this.tabPageGenerale.Controls.Add(this.LogTextBox);
            this.tabPageGenerale.Controls.Add(this.LogLabel);
            this.tabPageGenerale.Location = new System.Drawing.Point(4, 22);
            this.tabPageGenerale.Name = "tabPageGenerale";
            this.tabPageGenerale.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGenerale.Size = new System.Drawing.Size(395, 221);
            this.tabPageGenerale.TabIndex = 0;
            this.tabPageGenerale.Text = "Generale";
            this.tabPageGenerale.UseVisualStyleBackColor = true;
            // 
            // LoadLogPathbutton
            // 
            this.LoadLogPathbutton.Location = new System.Drawing.Point(342, 15);
            this.LoadLogPathbutton.Name = "LoadLogPathbutton";
            this.LoadLogPathbutton.Size = new System.Drawing.Size(47, 23);
            this.LoadLogPathbutton.TabIndex = 9;
            this.LoadLogPathbutton.Text = "...";
            this.LoadLogPathbutton.UseVisualStyleBackColor = true;
            this.LoadLogPathbutton.Click += new System.EventHandler(this.LoadLogPathbutton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(3, 131);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(122, 18);
            this.label14.TabIndex = 8;
            this.label14.Text = "I minuti                :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 18);
            this.label13.TabIndex = 7;
            this.label13.Text = "Il tempo di invio";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(3, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(121, 18);
            this.label12.TabIndex = 6;
            this.label12.Text = "L\'ora                   :";
            // 
            // MinutesComboBox
            // 
            this.MinutesComboBox.FormattingEnabled = true;
            this.MinutesComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59"});
            this.MinutesComboBox.Location = new System.Drawing.Point(128, 132);
            this.MinutesComboBox.Name = "MinutesComboBox";
            this.MinutesComboBox.Size = new System.Drawing.Size(121, 21);
            this.MinutesComboBox.TabIndex = 5;
            // 
            // HourComboBox
            // 
            this.HourComboBox.FormattingEnabled = true;
            this.HourComboBox.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23"});
            this.HourComboBox.Location = new System.Drawing.Point(128, 97);
            this.HourComboBox.Name = "HourComboBox";
            this.HourComboBox.Size = new System.Drawing.Size(121, 21);
            this.HourComboBox.TabIndex = 4;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(128, 18);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.Size = new System.Drawing.Size(208, 20);
            this.LogTextBox.TabIndex = 1;
            // 
            // LogLabel
            // 
            this.LogLabel.AutoSize = true;
            this.LogLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogLabel.Location = new System.Drawing.Point(3, 20);
            this.LogLabel.Name = "LogLabel";
            this.LogLabel.Size = new System.Drawing.Size(123, 18);
            this.LogLabel.TabIndex = 0;
            this.LogLabel.Text = "Percorso Log     :";
            // 
            // tabPageGEISP
            // 
            this.tabPageGEISP.Controls.Add(this.GeispRemoveAllButton);
            this.tabPageGEISP.Controls.Add(this.GeispAddButton);
            this.tabPageGEISP.Controls.Add(this.GeispRemoveButton);
            this.tabPageGEISP.Controls.Add(this.GeispEditButton);
            this.tabPageGEISP.Controls.Add(this.GeispDettagliButton);
            this.tabPageGEISP.Controls.Add(this.GeispListBox);
            this.tabPageGEISP.Location = new System.Drawing.Point(4, 22);
            this.tabPageGEISP.Name = "tabPageGEISP";
            this.tabPageGEISP.Size = new System.Drawing.Size(395, 221);
            this.tabPageGEISP.TabIndex = 3;
            this.tabPageGEISP.Text = "GEISP";
            this.tabPageGEISP.UseVisualStyleBackColor = true;
            // 
            // GeispRemoveAllButton
            // 
            this.GeispRemoveAllButton.Location = new System.Drawing.Point(192, 114);
            this.GeispRemoveAllButton.Name = "GeispRemoveAllButton";
            this.GeispRemoveAllButton.Size = new System.Drawing.Size(83, 23);
            this.GeispRemoveAllButton.TabIndex = 5;
            this.GeispRemoveAllButton.Text = " Rimuovi tutte";
            this.GeispRemoveAllButton.UseVisualStyleBackColor = true;
            this.GeispRemoveAllButton.Click += new System.EventHandler(this.GeispRemoveAllButton_Click);
            // 
            // GeispAddButton
            // 
            this.GeispAddButton.Location = new System.Drawing.Point(192, 143);
            this.GeispAddButton.Name = "GeispAddButton";
            this.GeispAddButton.Size = new System.Drawing.Size(83, 23);
            this.GeispAddButton.TabIndex = 4;
            this.GeispAddButton.Text = "Aggiungere";
            this.GeispAddButton.UseVisualStyleBackColor = true;
            this.GeispAddButton.Click += new System.EventHandler(this.GeispAddButton_Click);
            // 
            // GeispRemoveButton
            // 
            this.GeispRemoveButton.Location = new System.Drawing.Point(192, 85);
            this.GeispRemoveButton.Name = "GeispRemoveButton";
            this.GeispRemoveButton.Size = new System.Drawing.Size(83, 23);
            this.GeispRemoveButton.TabIndex = 3;
            this.GeispRemoveButton.Text = " Rimuovi";
            this.GeispRemoveButton.UseVisualStyleBackColor = true;
            this.GeispRemoveButton.Click += new System.EventHandler(this.GeispRemoveButton_Click);
            // 
            // GeispEditButton
            // 
            this.GeispEditButton.Location = new System.Drawing.Point(192, 56);
            this.GeispEditButton.Name = "GeispEditButton";
            this.GeispEditButton.Size = new System.Drawing.Size(83, 23);
            this.GeispEditButton.TabIndex = 2;
            this.GeispEditButton.Text = "Modifica";
            this.GeispEditButton.UseVisualStyleBackColor = true;
            this.GeispEditButton.Click += new System.EventHandler(this.GeispEditButton_Click);
            // 
            // GeispDettagliButton
            // 
            this.GeispDettagliButton.Location = new System.Drawing.Point(192, 27);
            this.GeispDettagliButton.Name = "GeispDettagliButton";
            this.GeispDettagliButton.Size = new System.Drawing.Size(83, 23);
            this.GeispDettagliButton.TabIndex = 1;
            this.GeispDettagliButton.Text = "Dettagli";
            this.GeispDettagliButton.UseVisualStyleBackColor = true;
            this.GeispDettagliButton.Click += new System.EventHandler(this.GeispDettagliButton_Click);
            // 
            // GeispListBox
            // 
            this.GeispListBox.FormattingEnabled = true;
            this.GeispListBox.Location = new System.Drawing.Point(26, 27);
            this.GeispListBox.Name = "GeispListBox";
            this.GeispListBox.Size = new System.Drawing.Size(160, 173);
            this.GeispListBox.TabIndex = 0;
            // 
            // AnnulareButton
            // 
            this.AnnulareButton.Location = new System.Drawing.Point(332, 265);
            this.AnnulareButton.Name = "AnnulareButton";
            this.AnnulareButton.Size = new System.Drawing.Size(75, 23);
            this.AnnulareButton.TabIndex = 1;
            this.AnnulareButton.Text = "Annulare";
            this.AnnulareButton.UseVisualStyleBackColor = true;
            this.AnnulareButton.Click += new System.EventHandler(this.AnnulareButton_Click);
            // 
            // ApplicareButton
            // 
            this.ApplicareButton.Location = new System.Drawing.Point(251, 265);
            this.ApplicareButton.Name = "ApplicareButton";
            this.ApplicareButton.Size = new System.Drawing.Size(75, 23);
            this.ApplicareButton.TabIndex = 2;
            this.ApplicareButton.Text = "Applicare";
            this.ApplicareButton.UseVisualStyleBackColor = true;
            this.ApplicareButton.Click += new System.EventHandler(this.ApplicareButton_Click);
            // 
            // Configurazione
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 294);
            this.Controls.Add(this.ApplicareButton);
            this.Controls.Add(this.AnnulareButton);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Configurazione";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Configurazione";
            this.Load += new System.EventHandler(this.Configurazione_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGenerale.ResumeLayout(false);
            this.tabPageGenerale.PerformLayout();
            this.tabPageGEISP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGenerale;
        private System.Windows.Forms.Label LogLabel;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.ComboBox MinutesComboBox;
        private System.Windows.Forms.ComboBox HourComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button LoadLogPathbutton;
        private System.Windows.Forms.Button AnnulareButton;
        private System.Windows.Forms.Button ApplicareButton;
        private System.Windows.Forms.TabPage tabPageGEISP;
        private System.Windows.Forms.ListBox GeispListBox;
        private System.Windows.Forms.Button GeispAddButton;
        private System.Windows.Forms.Button GeispRemoveButton;
        private System.Windows.Forms.Button GeispEditButton;
        private System.Windows.Forms.Button GeispDettagliButton;
        private System.Windows.Forms.Button GeispRemoveAllButton;
    }
}