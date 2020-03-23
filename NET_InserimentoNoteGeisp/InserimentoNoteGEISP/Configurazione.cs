using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InserimentoNoteGEISP.Utils;
using CFG = System.Configuration;
using InserimentoNoteGEISP.GEISP;

namespace InserimentoNoteGEISP
{
    public partial class Configurazione : Form
    {
        BindingSource bs;
        public Configurazione()
        {
            InitializeComponent();
        }

        private void Configurazione_Load(object sender, EventArgs e)
        {
            ControlsInit();
        }
        private void ControlsInit() 
        {
            LogTextBox.Text = ConfigUtils.GetLogPath();
            HourComboBox.Text = ConfigUtils.GetHours();
            MinutesComboBox.Text = ConfigUtils.GetMinutes();
           
            SqlPassTextBox.Text = ConfigUtils.GetSQLPassword();
            SqlServerTextBox.Text = ConfigUtils.GetSQLServer();
            SqlTrustCheckBox.Checked = ConfigUtils.GetTrustedConnection();
            SqlUserTextBox.Text = ConfigUtils.GetSQLUser();

            CaricoTextBox.Text = ConfigUtils.GetCaricoAddress();
            PosteTextBox.Text = ConfigUtils.GetPostAddress();
            EmailServerTextBox.Text = ConfigUtils.GetMailServer();
            SSLcheckBox.Checked = ConfigUtils.GetServerSecurity();
            SenderTextBox.Text = ConfigUtils.GetSenderAddress();
            PortTextBox.Text = ConfigUtils.GetServerPort();
            MailPassTextBox.Text = ConfigUtils.GetSenderPassword();

            bs = new BindingSource();
            bs.DataSource = GeispUtils.getDatabasesNames();
            GeispListBox.DataSource = bs;
        }

        private void ApplicareButton_Click(object sender, EventArgs e)
        {
            if (
                LogTextBox.Text != string.Empty &&
                SqlServerTextBox.Text != string.Empty &&
                SqlUserTextBox.Text != string.Empty &&
                SqlPassTextBox.Text != string.Empty &&
                CaricoTextBox.Text != string.Empty &&
                PosteTextBox.Text != string.Empty &&
                EmailServerTextBox.Text != string.Empty &&
                SenderTextBox.Text != string.Empty &&
                MailPassTextBox.Text != string.Empty &&
                PortTextBox.Text != string.Empty
                )
            {

                if (MessageBox.Show("Applicare configurazione?", "Confermare", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ConfigUtils config = new ConfigUtils();
                    config.SetLogPath(LogTextBox.Text);
                    config.SetHours(HourComboBox.SelectedItem.ToString());
                    config.SetMinutes(MinutesComboBox.SelectedItem.ToString());

                    config.SetSQLServer(SqlServerTextBox.Text);
                    config.SetSQLUser(SqlUserTextBox.Text);
                    config.SetSQLPassword(SqlPassTextBox.Text);
                    config.SetTrustedConnection(SqlTrustCheckBox.Checked);

                    config.SetCaricoAddress(CaricoTextBox.Text);
                    config.SetPostAddress(PosteTextBox.Text);
                    config.SetMailServer(EmailServerTextBox.Text);
                    config.SetSenderAddress(SenderTextBox.Text);
                    config.SetSenderPassword(MailPassTextBox.Text);
                    config.SetServerPort(PortTextBox.Text);
                    config.SetServerSecurity(SSLcheckBox.Checked);

                    config.cfg.Save(System.Configuration.ConfigurationSaveMode.Full);
                    CFG.ConfigurationManager.RefreshSection("appSettings");
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
        private void AnnulareButton_Click(object sender, EventArgs e)
        {
            ControlsInit();
        }

        private void LoadLogPathbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog nonInviati = new FolderBrowserDialog();

            if (DialogResult.OK == nonInviati.ShowDialog())
               LogTextBox.Text = nonInviati.SelectedPath + "\\";
        }

        private void GeispDettagliButton_Click(object sender, EventArgs e)
        {
            DetailsForm df;
            if (GeispListBox.SelectedIndex != -1)
            { 
                df = new DetailsForm(GeispListBox.SelectedValue.ToString());
                df.ShowDialog();
            }
        }

        private void GeispEditButton_Click(object sender, EventArgs e)
        {
            ModifyGeispDb df;
            if (GeispListBox.SelectedIndex != -1)
            {
                df = new ModifyGeispDb(GeispListBox.SelectedValue.ToString());
                df.ShowDialog();
            }
            Refr();
        }

        private void GeispAddButton_Click(object sender, EventArgs e)
        {
            AddGeispDb df = new AddGeispDb(this);
            df.ShowDialog();
            Refr();
        }

        private void GeispRemoveButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Rimouvi " + GeispListBox.SelectedValue.ToString()+"?", "Confermare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GeispUtils.removeDatabase(GeispListBox.SelectedValue.ToString());
                Refr();
            }
        }

        private void GeispRemoveAllButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Rimouvi tuti database?" , "Confermare", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GeispUtils.removeAllDatabases();
                bs = new BindingSource();
                bs.DataSource = GeispUtils.getDatabasesNames();
                GeispListBox.DataSource = bs;
            }
        }

        public void Refr()
        {
            bs = new BindingSource();
            bs.DataSource = GeispUtils.getDatabasesNames();
            GeispListBox.DataSource = bs;
        }



    }
}
