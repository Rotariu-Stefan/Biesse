using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AggiornaProrogheGeisp.GEISP;
using System.IO;

namespace AggiornaProrogheGeisp
{
    public partial class AddGeispDb : Form
    {
        Configurazione cfg;

        public AddGeispDb()
        {
            InitializeComponent();
        }
        public AddGeispDb(Configurazione cfg)
        {
            InitializeComponent();
            this.cfg = cfg;
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

        private void SalvaButton_Click(object sender, EventArgs e)
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
            GeispUtils.addDatabase(db);
            cfg.Refr();
            this.Close();
        }
    }
}
