using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InserimentoNoteGEISP.GEISP;

namespace InserimentoNoteGEISP
{
    public partial class AddGeispDb : Form
    {
        Configurazione cfg;
        BindingSource bs;
        public AddGeispDb()
        {
            InitializeComponent();
            bs = new BindingSource();
            bs.DataSource = GeispUtils.getSocietaNames();
            SocietaComboBox.DataSource = bs;
        }
        public AddGeispDb(Configurazione cfg)
        {
            InitializeComponent();
            this.cfg = cfg;
            bs = new BindingSource();
            bs.DataSource = GeispUtils.getSocietaNames();
            SocietaComboBox.DataSource = bs;
        }
        private void LoadDBPathbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog nonInviati = new OpenFileDialog();

            if (DialogResult.OK == nonInviati.ShowDialog())
            {
                PathTextBox.Text = System.IO.Path.GetFullPath(nonInviati.FileName);
                //System.IO.Path.GetDirectoryName(nonInviati.FileName) +
            }
        }

        private void SalvaButton_Click(object sender, EventArgs e)
        {
            if (
                NameTextBox.Text != string.Empty &&
                PathTextBox.Text != string.Empty &&
                CodBaseTextBox.Text != string.Empty
                )
            {

                //if (MessageBox.Show("Salva database?", "Confermare", MessageBoxButtons.YesNo) == DialogResult.Yes)
                //{
                    var db = new GeispDatabase(NameTextBox.Text, CodBaseTextBox.Text,  PathTextBox.Text, GeispUtils.getSocietaId(SocietaComboBox.SelectedItem.ToString()));
                    GeispUtils.addDatabase(db);
                    cfg.Refr();
                    this.Close();
                //}
                //else
                //{
                    /// Code for ‘No’
                //}
            }
            else
            {

                MessageBox.Show("Si prega di compilare tutti i campi", "Attenzione..");

            }
        }
    }
}
