using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PrestitempoInserimento.Utils;
using CFG = System.Configuration;
namespace PrestitempoInserimento
{
    public partial class Configurazione : Form
    {
        List<string> days = new List<string>();
        BindingSource bs = new BindingSource();
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
            days = ConfigUtils.GetScheduledDays();
            LogTextBox.Text = ConfigUtils.GetLogPath();
            ExcelTextBox.Text = ConfigUtils.GetExcelPath();
            HourComboBox.Text = ConfigUtils.GetHours();
            MinutesComboBox.Text = ConfigUtils.GetMinutes();
            DaysToAddComboBox.Text = "01";
            //  DayComboBox.Text = ConfigUtils.GetDay();

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

            WebSiteUserTextBox.Text = ConfigUtils.GetWebSiteUser();
            WebSitePassTextBox.Text = ConfigUtils.GetWebSitePassword();

            days.Sort();
            bs.DataSource = days;
            DaysListBox.DataSource = bs;


        }
        private void LoadLogPathbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog nonInviati = new FolderBrowserDialog();

            if (DialogResult.OK == nonInviati.ShowDialog())
            {
                LogTextBox.Text = nonInviati.SelectedPath + "\\";
            }
        }
        private void LoadExcelPathbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog nonInviati = new FolderBrowserDialog();

            if (DialogResult.OK == nonInviati.ShowDialog())
            {
                ExcelTextBox.Text = nonInviati.SelectedPath + "\\";
            }
        }

        private void AnnulareButton_Click(object sender, EventArgs e)
        {
            ControlsInit();
        }

        private void ApplicareButton_Click(object sender, EventArgs e)
        {
            if (
                LogTextBox.Text != string.Empty &&
                SqlServerTextBox.Text != string.Empty &&
                SqlUserTextBox.Text != string.Empty &&
                SqlPassTextBox.Text != string.Empty &&
                PosteTextBox.Text != string.Empty &&
                EmailServerTextBox.Text != string.Empty &&
                SenderTextBox.Text != string.Empty &&
                MailPassTextBox.Text != string.Empty &&
                PortTextBox.Text != string.Empty &&
                WebSiteUserTextBox.Text != string.Empty &&
                WebSitePassTextBox.Text != string.Empty &&
                ExcelTextBox.Text != string.Empty
                )
            {

                if (MessageBox.Show("Applicare configurazione?", "Confermare", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ConfigUtils config = new ConfigUtils();
                    config.SetLogPath(LogTextBox.Text);
                    //   config.SetDay(DayComboBox.SelectedItem.ToString());
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

                    config.SetWebSiteUser(WebSiteUserTextBox.Text);
                    config.SetWebSitePassword(WebSitePassTextBox.Text);
                    config.SetDays(days);
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

        private void AddDaysButton_Click(object sender, EventArgs e)
        {
            if (!days.Contains(DaysToAddComboBox.SelectedItem.ToString()))
            {
                days.Add(DaysToAddComboBox.SelectedItem.ToString());
                days.Sort();
                bs = new BindingSource();
                bs.DataSource = days;
                DaysListBox.DataSource = bs;
            }

        }

        private void RemoveDaysButton_Click(object sender, EventArgs e)
        {
            if (DaysListBox.SelectedIndex > -1)
            {
                string d = DaysListBox.SelectedValue.ToString();
                days.Remove(d);
                bs = new BindingSource();
                bs.DataSource = days;
                DaysListBox.DataSource = bs;
                
            }
        }



    }
}
