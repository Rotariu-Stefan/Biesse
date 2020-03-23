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
    public partial class DetailsForm : Form
    {
        public DetailsForm()
        {
            InitializeComponent();
        }
        public DetailsForm(string name)
        {
            InitializeComponent();
            GeispDatabase gDet = new GeispDatabase();
            gDet = GeispUtils.getDatbase(name);

            NameTextBox.Text = gDet.name;
            CodBaseTextBox.Text = gDet.codBas;
            SocietaTextBox.Text = gDet.getSocietaName();
            PathTextBox.Text = gDet.path;
        }

        private void DetailesOkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
