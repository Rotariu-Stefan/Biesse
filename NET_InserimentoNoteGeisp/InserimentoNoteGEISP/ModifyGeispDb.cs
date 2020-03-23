using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InserimentoNoteGEISP.GEISP;
using System.IO;

namespace InserimentoNoteGEISP
{
    public partial class ModifyGeispDb : Form
    {
        public string name;

        public ModifyGeispDb()
        {
            InitializeComponent();            
        }
        public ModifyGeispDb(string name)
        {
            InitializeComponent();
            this.name = name;
            ControlsInit();
        }
        private void ControlsInit()
        {
            GeispDatabase gDet = new GeispDatabase();
            gDet = GeispUtils.getDatabase(name);

            NameTextBox.Text = gDet.name;
            MdbTextBox.Text = gDet.mdb;
            ExcelTextBox.Text = gDet.excel;
            Mdb2TextBox.Text = gDet.mdb2;
        }

        private void MdbPathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogMdb = new OpenFileDialog();
            if (dialogMdb.ShowDialog() == DialogResult.OK)
                MdbTextBox.Text = dialogMdb.FileName;
        }
        private void ExcelPathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogMdb = new OpenFileDialog();
            if (dialogMdb.ShowDialog() == DialogResult.OK)
                ExcelTextBox.Text = dialogMdb.FileName;
        }
        private void Mdb2Pathbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogMdb = new OpenFileDialog();
            if (dialogMdb.ShowDialog() == DialogResult.OK)
                Mdb2TextBox.Text = dialogMdb.FileName;
        }

        private void ApplicareButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(MdbTextBox.Text) == false || File.Exists(ExcelTextBox.Text) == false || File.Exists(Mdb2TextBox.Text) == false)
            {
                MessageBox.Show("Invalid path format!");
                return;
            }
            if (String.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Name field is required!");
                return;
            }
            var db = new GeispDatabase(NameTextBox.Text, MdbTextBox.Text, ExcelTextBox.Text, Mdb2TextBox.Text);
            GeispUtils.modifyDatabase(name, db);
            this.Close();
        }

        private void AnnulareButton_Click(object sender, EventArgs e)
        {
            ControlsInit();
        }
    }
}
