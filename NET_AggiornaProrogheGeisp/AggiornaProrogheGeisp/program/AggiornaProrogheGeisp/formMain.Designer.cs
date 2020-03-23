namespace AggiornaProrogheGeisp
{
    partial class formMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.tabControlAsoc = new System.Windows.Forms.TabControl();
            this.tabPageAsoc = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxAsoc = new System.Windows.Forms.ListBox();
            this.buttonNewAsoc = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.textBoxExcelPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDeleteAsoc = new System.Windows.Forms.Button();
            this.buttonLookMdb = new System.Windows.Forms.Button();
            this.buttonModifyAsoc = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonLookMdb2 = new System.Windows.Forms.Button();
            this.textBoxMdbPath = new System.Windows.Forms.TextBox();
            this.textBoxMdb2Path = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonLookExcel = new System.Windows.Forms.Button();
            this.labelMdb2 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.tabPagePlan = new System.Windows.Forms.TabPage();
            this.panelStopped = new System.Windows.Forms.Panel();
            this.comboBoxInterval = new System.Windows.Forms.ComboBox();
            this.labelInterval = new System.Windows.Forms.Label();
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.timePicker = new System.Windows.Forms.DateTimePicker();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.panelStarted = new System.Windows.Forms.Panel();
            this.labelStatusTitle = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelTimeLeft = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSetPlan = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.timerUpdateTime = new System.Windows.Forms.Timer(this.components);
            this.tabControlAsoc.SuspendLayout();
            this.tabPageAsoc.SuspendLayout();
            this.tabPagePlan.SuspendLayout();
            this.panelStopped.SuspendLayout();
            this.panelStarted.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlAsoc
            // 
            this.tabControlAsoc.Controls.Add(this.tabPageAsoc);
            this.tabControlAsoc.Controls.Add(this.tabPagePlan);
            this.tabControlAsoc.Location = new System.Drawing.Point(3, 2);
            this.tabControlAsoc.Name = "tabControlAsoc";
            this.tabControlAsoc.SelectedIndex = 0;
            this.tabControlAsoc.Size = new System.Drawing.Size(711, 251);
            this.tabControlAsoc.TabIndex = 28;
            // 
            // tabPageAsoc
            // 
            this.tabPageAsoc.Controls.Add(this.button1);
            this.tabPageAsoc.Controls.Add(this.listBoxAsoc);
            this.tabPageAsoc.Controls.Add(this.buttonNewAsoc);
            this.tabPageAsoc.Controls.Add(this.buttonClear);
            this.tabPageAsoc.Controls.Add(this.textBoxExcelPath);
            this.tabPageAsoc.Controls.Add(this.label1);
            this.tabPageAsoc.Controls.Add(this.buttonDeleteAsoc);
            this.tabPageAsoc.Controls.Add(this.buttonLookMdb);
            this.tabPageAsoc.Controls.Add(this.buttonModifyAsoc);
            this.tabPageAsoc.Controls.Add(this.label2);
            this.tabPageAsoc.Controls.Add(this.buttonLookMdb2);
            this.tabPageAsoc.Controls.Add(this.textBoxMdbPath);
            this.tabPageAsoc.Controls.Add(this.textBoxMdb2Path);
            this.tabPageAsoc.Controls.Add(this.label3);
            this.tabPageAsoc.Controls.Add(this.buttonLookExcel);
            this.tabPageAsoc.Controls.Add(this.labelMdb2);
            this.tabPageAsoc.Controls.Add(this.labelName);
            this.tabPageAsoc.Controls.Add(this.textBoxName);
            this.tabPageAsoc.Location = new System.Drawing.Point(4, 22);
            this.tabPageAsoc.Name = "tabPageAsoc";
            this.tabPageAsoc.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAsoc.Size = new System.Drawing.Size(703, 225);
            this.tabPageAsoc.TabIndex = 0;
            this.tabPageAsoc.Text = "Associazioni";
            this.tabPageAsoc.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(613, 183);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 30);
            this.button1.TabIndex = 65;
            this.button1.Text = "Chiudere";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBoxAsoc
            // 
            this.listBoxAsoc.FormattingEnabled = true;
            this.listBoxAsoc.Location = new System.Drawing.Point(6, 17);
            this.listBoxAsoc.Name = "listBoxAsoc";
            this.listBoxAsoc.Size = new System.Drawing.Size(140, 160);
            this.listBoxAsoc.TabIndex = 30;
            this.listBoxAsoc.SelectedIndexChanged += new System.EventHandler(this.listBoxAsoc_SelectedIndexChanged);
            // 
            // buttonNewAsoc
            // 
            this.buttonNewAsoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewAsoc.Location = new System.Drawing.Point(37, 183);
            this.buttonNewAsoc.Name = "buttonNewAsoc";
            this.buttonNewAsoc.Size = new System.Drawing.Size(85, 30);
            this.buttonNewAsoc.TabIndex = 32;
            this.buttonNewAsoc.Text = "Nuovo";
            this.buttonNewAsoc.UseVisualStyleBackColor = true;
            this.buttonNewAsoc.Click += new System.EventHandler(this.buttonNewAsoc_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClear.Location = new System.Drawing.Point(139, 17);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(30, 19);
            this.buttonClear.TabIndex = 64;
            this.buttonClear.Text = "CL";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Visible = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // textBoxExcelPath
            // 
            this.textBoxExcelPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxExcelPath.Location = new System.Drawing.Point(256, 119);
            this.textBoxExcelPath.Name = "textBoxExcelPath";
            this.textBoxExcelPath.Size = new System.Drawing.Size(397, 18);
            this.textBoxExcelPath.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(251, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 26);
            this.label1.TabIndex = 62;
            this.label1.Text = "Associazioni di Database";
            // 
            // buttonDeleteAsoc
            // 
            this.buttonDeleteAsoc.Enabled = false;
            this.buttonDeleteAsoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteAsoc.Location = new System.Drawing.Point(373, 183);
            this.buttonDeleteAsoc.Name = "buttonDeleteAsoc";
            this.buttonDeleteAsoc.Size = new System.Drawing.Size(85, 30);
            this.buttonDeleteAsoc.TabIndex = 60;
            this.buttonDeleteAsoc.Text = "Cancella";
            this.buttonDeleteAsoc.UseVisualStyleBackColor = true;
            this.buttonDeleteAsoc.Click += new System.EventHandler(this.buttonDeleteAsoc_Click);
            // 
            // buttonLookMdb
            // 
            this.buttonLookMdb.Location = new System.Drawing.Point(659, 84);
            this.buttonLookMdb.Name = "buttonLookMdb";
            this.buttonLookMdb.Size = new System.Drawing.Size(37, 20);
            this.buttonLookMdb.TabIndex = 40;
            this.buttonLookMdb.Text = "...";
            this.buttonLookMdb.UseVisualStyleBackColor = true;
            this.buttonLookMdb.Click += new System.EventHandler(this.buttonLookMdb_Click);
            // 
            // buttonModifyAsoc
            // 
            this.buttonModifyAsoc.Enabled = false;
            this.buttonModifyAsoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonModifyAsoc.Location = new System.Drawing.Point(256, 183);
            this.buttonModifyAsoc.Name = "buttonModifyAsoc";
            this.buttonModifyAsoc.Size = new System.Drawing.Size(85, 30);
            this.buttonModifyAsoc.TabIndex = 58;
            this.buttonModifyAsoc.Text = "Modifica";
            this.buttonModifyAsoc.UseVisualStyleBackColor = true;
            this.buttonModifyAsoc.Click += new System.EventHandler(this.buttonModifyAsoc_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(184, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Geisp DB:";
            // 
            // buttonLookMdb2
            // 
            this.buttonLookMdb2.Location = new System.Drawing.Point(659, 146);
            this.buttonLookMdb2.Name = "buttonLookMdb2";
            this.buttonLookMdb2.Size = new System.Drawing.Size(37, 20);
            this.buttonLookMdb2.TabIndex = 56;
            this.buttonLookMdb2.Text = "...";
            this.buttonLookMdb2.UseVisualStyleBackColor = true;
            this.buttonLookMdb2.Click += new System.EventHandler(this.buttonLookMdb2_Click);
            // 
            // textBoxMdbPath
            // 
            this.textBoxMdbPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMdbPath.Location = new System.Drawing.Point(256, 86);
            this.textBoxMdbPath.Name = "textBoxMdbPath";
            this.textBoxMdbPath.Size = new System.Drawing.Size(397, 18);
            this.textBoxMdbPath.TabIndex = 44;
            // 
            // textBoxMdb2Path
            // 
            this.textBoxMdb2Path.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMdb2Path.Location = new System.Drawing.Point(256, 148);
            this.textBoxMdb2Path.Name = "textBoxMdb2Path";
            this.textBoxMdb2Path.Size = new System.Drawing.Size(397, 18);
            this.textBoxMdb2Path.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Input Path:";
            // 
            // buttonLookExcel
            // 
            this.buttonLookExcel.Location = new System.Drawing.Point(659, 117);
            this.buttonLookExcel.Name = "buttonLookExcel";
            this.buttonLookExcel.Size = new System.Drawing.Size(37, 20);
            this.buttonLookExcel.TabIndex = 46;
            this.buttonLookExcel.Text = "...";
            this.buttonLookExcel.UseVisualStyleBackColor = true;
            this.buttonLookExcel.Click += new System.EventHandler(this.buttonLookExcel_Click);
            // 
            // labelMdb2
            // 
            this.labelMdb2.AutoSize = true;
            this.labelMdb2.Location = new System.Drawing.Point(148, 150);
            this.labelMdb2.Name = "labelMdb2";
            this.labelMdb2.Size = new System.Drawing.Size(100, 13);
            this.labelMdb2.TabIndex = 52;
            this.labelMdb2.Text = "Nr_Rapporti DB:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(205, 62);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(43, 13);
            this.labelName.TabIndex = 48;
            this.labelName.Text = "Nome:";
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(256, 59);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(269, 18);
            this.textBoxName.TabIndex = 50;
            // 
            // tabPagePlan
            // 
            this.tabPagePlan.Controls.Add(this.panelStopped);
            this.tabPagePlan.Controls.Add(this.panelStarted);
            this.tabPagePlan.Controls.Add(this.buttonClose);
            this.tabPagePlan.Controls.Add(this.buttonSetPlan);
            this.tabPagePlan.Controls.Add(this.labelTitle);
            this.tabPagePlan.Location = new System.Drawing.Point(4, 22);
            this.tabPagePlan.Name = "tabPagePlan";
            this.tabPagePlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlan.Size = new System.Drawing.Size(703, 225);
            this.tabPagePlan.TabIndex = 1;
            this.tabPagePlan.Text = "Pianificazione";
            this.tabPagePlan.UseVisualStyleBackColor = true;
            // 
            // panelStopped
            // 
            this.panelStopped.Controls.Add(this.comboBoxInterval);
            this.panelStopped.Controls.Add(this.labelInterval);
            this.panelStopped.Controls.Add(this.datePicker);
            this.panelStopped.Controls.Add(this.timePicker);
            this.panelStopped.Controls.Add(this.labelDate);
            this.panelStopped.Controls.Add(this.labelTime);
            this.panelStopped.Location = new System.Drawing.Point(11, 32);
            this.panelStopped.Name = "panelStopped";
            this.panelStopped.Size = new System.Drawing.Size(358, 120);
            this.panelStopped.TabIndex = 39;
            // 
            // comboBoxInterval
            // 
            this.comboBoxInterval.FormattingEnabled = true;
            this.comboBoxInterval.Items.AddRange(new object[] {
            "1 giorno",
            "2 giorni",
            "3 giorni",
            "4 giorni",
            "5 giorni",
            "6 giorni",
            "7 giorni",
            "8 giorni",
            "9 giorni",
            "10 giorni",
            "11 giorni",
            "12 giorni",
            "13 giorni",
            "14 giorni",
            "15 giorni",
            "16 giorni",
            "17 giorni",
            "18 giorni",
            "19 giorni",
            "20 giorni",
            "21 giorni",
            "22 giorni",
            "23 giorni",
            "24 giorni",
            "25 giorni",
            "26 giorni",
            "27 giorni",
            "28 giorni"});
            this.comboBoxInterval.Location = new System.Drawing.Point(104, 90);
            this.comboBoxInterval.Name = "comboBoxInterval";
            this.comboBoxInterval.Size = new System.Drawing.Size(93, 21);
            this.comboBoxInterval.TabIndex = 32;
            this.comboBoxInterval.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // labelInterval
            // 
            this.labelInterval.AutoSize = true;
            this.labelInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInterval.Location = new System.Drawing.Point(29, 91);
            this.labelInterval.Name = "labelInterval";
            this.labelInterval.Size = new System.Drawing.Size(70, 15);
            this.labelInterval.TabIndex = 31;
            this.labelInterval.Text = "Intervallo:";
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(104, 24);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(240, 20);
            this.datePicker.TabIndex = 0;
            this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
            // 
            // timePicker
            // 
            this.timePicker.Location = new System.Drawing.Point(104, 57);
            this.timePicker.Name = "timePicker";
            this.timePicker.Size = new System.Drawing.Size(240, 20);
            this.timePicker.TabIndex = 30;
            this.timePicker.ValueChanged += new System.EventHandler(this.timePicker_ValueChanged);
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDate.Location = new System.Drawing.Point(3, 28);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(96, 15);
            this.labelDate.TabIndex = 27;
            this.labelDate.Text = "Primo Giorno:";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime.Location = new System.Drawing.Point(65, 61);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(34, 15);
            this.labelTime.TabIndex = 28;
            this.labelTime.Text = "Ora:";
            // 
            // panelStarted
            // 
            this.panelStarted.Controls.Add(this.labelStatusTitle);
            this.panelStarted.Controls.Add(this.labelStatus);
            this.panelStarted.Controls.Add(this.labelTimeLeft);
            this.panelStarted.Location = new System.Drawing.Point(427, 6);
            this.panelStarted.Name = "panelStarted";
            this.panelStarted.Size = new System.Drawing.Size(270, 146);
            this.panelStarted.TabIndex = 38;
            // 
            // labelStatusTitle
            // 
            this.labelStatusTitle.AutoSize = true;
            this.labelStatusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatusTitle.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.labelStatusTitle.Location = new System.Drawing.Point(4, 50);
            this.labelStatusTitle.Name = "labelStatusTitle";
            this.labelStatusTitle.Size = new System.Drawing.Size(127, 13);
            this.labelStatusTitle.TabIndex = 34;
            this.labelStatusTitle.Text = "Prossimo eseguire in:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.labelStatus.Location = new System.Drawing.Point(3, 0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(66, 24);
            this.labelStatus.TabIndex = 33;
            this.labelStatus.Text = "Status";
            // 
            // labelTimeLeft
            // 
            this.labelTimeLeft.AutoSize = true;
            this.labelTimeLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeLeft.ForeColor = System.Drawing.Color.OliveDrab;
            this.labelTimeLeft.Location = new System.Drawing.Point(-1, 63);
            this.labelTimeLeft.Name = "labelTimeLeft";
            this.labelTimeLeft.Size = new System.Drawing.Size(252, 46);
            this.labelTimeLeft.TabIndex = 33;
            this.labelTimeLeft.Text = "00. 00:00:00";
            // 
            // buttonClose
            // 
            this.buttonClose.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(589, 177);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(84, 30);
            this.buttonClose.TabIndex = 37;
            this.buttonClose.Text = "Chiudere";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSetPlan
            // 
            this.buttonSetPlan.BackColor = System.Drawing.SystemColors.ControlDark;
            this.buttonSetPlan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetPlan.Location = new System.Drawing.Point(102, 168);
            this.buttonSetPlan.Name = "buttonSetPlan";
            this.buttonSetPlan.Size = new System.Drawing.Size(204, 39);
            this.buttonSetPlan.TabIndex = 36;
            this.buttonSetPlan.Text = "Impostare Planification";
            this.buttonSetPlan.UseVisualStyleBackColor = false;
            this.buttonSetPlan.Click += new System.EventHandler(this.buttonSetPlan_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(6, 3);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(276, 26);
            this.labelTitle.TabIndex = 35;
            this.labelTitle.Text = "Selezionare Planification";
            // 
            // timerUpdateTime
            // 
            this.timerUpdateTime.Tick += new System.EventHandler(this.timerUpdateTime_Tick);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(716, 256);
            this.Controls.Add(this.tabControlAsoc);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formMain";
            this.Text = "AggiornaProrogheGeisp - Associations List";
            this.tabControlAsoc.ResumeLayout(false);
            this.tabPageAsoc.ResumeLayout(false);
            this.tabPageAsoc.PerformLayout();
            this.tabPagePlan.ResumeLayout(false);
            this.tabPagePlan.PerformLayout();
            this.panelStopped.ResumeLayout(false);
            this.panelStopped.PerformLayout();
            this.panelStarted.ResumeLayout(false);
            this.panelStarted.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlAsoc;
        private System.Windows.Forms.TabPage tabPageAsoc;
        private System.Windows.Forms.ListBox listBoxAsoc;
        private System.Windows.Forms.Button buttonNewAsoc;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.TextBox textBoxExcelPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDeleteAsoc;
        private System.Windows.Forms.Button buttonLookMdb;
        private System.Windows.Forms.Button buttonModifyAsoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonLookMdb2;
        private System.Windows.Forms.TextBox textBoxMdbPath;
        private System.Windows.Forms.TextBox textBoxMdb2Path;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonLookExcel;
        private System.Windows.Forms.Label labelMdb2;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TabPage tabPagePlan;
        private System.Windows.Forms.Panel panelStopped;
        private System.Windows.Forms.ComboBox comboBoxInterval;
        private System.Windows.Forms.Label labelInterval;
        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.DateTimePicker timePicker;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Panel panelStarted;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelTimeLeft;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSetPlan;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Timer timerUpdateTime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelStatusTitle;

    }
}

