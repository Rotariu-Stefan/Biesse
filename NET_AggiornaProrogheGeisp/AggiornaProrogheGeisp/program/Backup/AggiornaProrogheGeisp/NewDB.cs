using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AggiornaProrogheGeisp
{
    public partial class NewDB : Form
    {
        public NewDB()
        {
            InitializeComponent();
        }

        private void buttonLookMdb_Click(object sender, EventArgs e)
        {
            OpenFileDialog nonInviati = new OpenFileDialog();

            if (DialogResult.OK == nonInviati.ShowDialog())
            {
                textBoxMdbPath.Text = System.IO.Path.GetDirectoryName(nonInviati.FileName) + @"\" + System.IO.Path.GetFileName(nonInviati.FileName);
            }
        }

        private void buttonExcelPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog nonInviati = new OpenFileDialog();

            if (DialogResult.OK == nonInviati.ShowDialog())
            {
               textBoxExcelPath.Text = System.IO.Path.GetDirectoryName(nonInviati.FileName) +@"\"+ System.IO.Path.GetFileName(nonInviati.FileName);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            List<string> db = new List<string>();

            foreach( Control ctrl in this.Controls  )
            {
                if (ctrl is TextBox)
                {
                    if(string.IsNullOrEmpty(((TextBox)ctrl).Text))
                    {
                        MessageBox.Show("Casutele trebuie completate!!!! TOATE!!!!");
                        return;
                    }
                }
            }
            db.Add(textBoxName.Text);
            db.Add(textBoxMdbPath.Text);
            db.Add(textBoxExcelPath.Text);
            
            db.Add(textBoxMdb2Path.Text);
            
            Main.databases.Add(db);
            Main.DBnames.Add(db[0]);
            Main.DBnames.Sort();
            Main main = (Main)Application.OpenForms["Main"];
            main.initlist();
            this.Close();
        }

        private void buttonLookMdb2_Click(object sender, EventArgs e)
        {
            OpenFileDialog nonInviati = new OpenFileDialog();

            if (DialogResult.OK == nonInviati.ShowDialog())
            {
                textBoxMdb2Path.Text = System.IO.Path.GetDirectoryName(nonInviati.FileName) + @"\" + System.IO.Path.GetFileName(nonInviati.FileName);
            }
        }
    }
}
