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
    public partial class ModifyGeispDb : Form
    {
        public string name;
        BindingSource bs;
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
        private void LoadDBPathbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog nonInviati = new OpenFileDialog();

            if (DialogResult.OK == nonInviati.ShowDialog())
            {
                PathTextBox.Text = System.IO.Path.GetDirectoryName(nonInviati.FileName) + System.IO.Path.GetFileName(nonInviati.FileName);
            }
        }

        private void AnnulareButton_Click(object sender, EventArgs e)
        {
            ControlsInit();
        }

        private void ControlsInit()
        {
            GeispDatabase gDet = new GeispDatabase();
            gDet = GeispUtils.getDatbase(name);
            bs = new BindingSource();
            bs.DataSource = GeispUtils.getSocietaNames();
            SocietaComboBox.DataSource = bs;

            NameTextBox.Text = gDet.name;
            CodBaseTextBox.Text = gDet.codBas;
            SocietaComboBox.Text = gDet.getSocietaName();
            PathTextBox.Text = gDet.path;

        }

        private void ApplicareButton_Click(object sender, EventArgs e)
        {
            if (
                NameTextBox.Text != string.Empty &&
                PathTextBox.Text != string.Empty &&
                CodBaseTextBox.Text != string.Empty 
                )
            {

                if (MessageBox.Show("Applicare configurazione?", "Confermare", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var db = new GeispDatabase(NameTextBox.Text, CodBaseTextBox.Text, PathTextBox.Text, GeispUtils.getSocietaId(SocietaComboBox.SelectedItem.ToString()));
                    GeispUtils.modifyDatabase(name,db);
                }
                else
                {
                    /// Code for ‘No’
                }
            }
            else
            {

                MessageBox.Show("Si prega di compilare tutti i campi", "Attenzione..");

            }
        }

    }
}
