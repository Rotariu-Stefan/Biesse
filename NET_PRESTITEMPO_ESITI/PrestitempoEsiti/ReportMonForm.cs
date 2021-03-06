﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using PrestitempoInserimento.Utils;
using System.Globalization;
using CFG = System.Configuration;
namespace PrestitempoInserimento
{
    public partial class ReportMonForm : Form
    {

        private bool isRunMan = false;
        private bool isRunSch = false;

        public ReportMonForm()
        {
            InitializeComponent();
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            //startToolStripMenuItem.Image = Image.FromFile(directory + "Images\\semaphore_green.png");
            if (ConfigUtils.GetExcelPath() == string.Empty)
            {
                ConfigUtils config = new ConfigUtils();
                config.SetExcelPath(directory);
                config.cfg.Save(System.Configuration.ConfigurationSaveMode.Full);
                CFG.ConfigurationManager.RefreshSection("appSettings");
            }
            if (ConfigUtils.GetLogPath() == string.Empty)
            {
                ConfigUtils config = new ConfigUtils();
                config.SetLogPath(directory);
                config.cfg.Save(System.Configuration.ConfigurationSaveMode.Full);
                CFG.ConfigurationManager.RefreshSection("appSettings");
            }
        }
        private void ReportMonForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
        }



        #region Worker
            private void SendMailsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
            {
                if (isRunMan)
                    isRunMan = false;

          string directory = AppDomain.CurrentDomain.BaseDirectory;
            startToolStripMenuItem.Image = Image.FromFile(directory+"Images\\semaphore_green.png");
                manualeToolStripMenuItem.Enabled = true;
                ReportMonNotifyIcon.ShowBalloonTip(5000, "Informazioni..", "Processo finito", ToolTipIcon.Info);
            }
            private void SendMailWorker_DoWork(object sender, DoWorkEventArgs e)
            {
                Logger.Log("Starting Opertation \n" , LogType.Operation);
                try
                {
                    string data = DateTime.Now.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var au = new Automation(ConfigUtils.GetWebSiteUser(),ConfigUtils.GetWebSitePassword(),data,(BackgroundWorker) sender);
                    au.Run();
                    Logger.Log("Operation ended \n", LogType.Operation);
                }
                catch (Exception ex)
                {
                    Logger.Log("Could not complete the operation "+ "\n" + ex.StackTrace.ToString(), LogType.Error);
                }
            }
        #endregion

        #region ToolStripMenu
            private void exitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (DialogResult.OK == MessageBox.Show("Sei sicuro di voler uscire dal programma?", "Attenzione..", MessageBoxButtons.OKCancel))
                {
                    this.Close();
                }
            }

            private void pianificataStartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (pianificataToolStripMenuItem.Text.Contains("Start"))
                {

                    pianificataToolStripMenuItem.Text = "Pianificata Stop";
                    isRunSch = true;
                    if (!isRunMan)
                    {
                        ReportMonScheduler.Enabled = true;
                        ReportMonScheduler.Start();
                    }
                    else
                    {
                        MessageBox.Show("Un'altra sessione già eseguito, si prega di attendere la sessione corrente a finire!");
                    }
                }
                else
                {
                    pianificataToolStripMenuItem.Text = "Pianificata Start";
                    ReportMonScheduler.Stop();
                    ReportMonScheduler.Enabled = false;
                    isRunSch = false;
                }
            }
            private void manualeToolStripMenuItem_Click(object sender, EventArgs e)
            {
                isRunMan = true;

                if (!isRunSch)
                {
                    try
                    {
                        string directory = AppDomain.CurrentDomain.BaseDirectory;
                        startToolStripMenuItem.Image = Image.FromFile(directory + "Images\\semaphore_red.png");
                        manualeToolStripMenuItem.Enabled = false;
                        ReportMonNotifyIcon.ShowBalloonTip(5000, "Informazioni..", "Processo iniziato", ToolTipIcon.Info);
                        SendMailsWorker.WorkerSupportsCancellation = true;
                        SendMailsWorker.RunWorkerAsync();
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message, LogType.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Una sessione programmata già eseguito, si prega di interrompere prima di iniziare un'altra sessione!");
                }
            }

            private void configurazioniToolStripMenuItem_Click(object sender, EventArgs e)
            {
                Configurazione cfg = new Configurazione();
                cfg.Show();
            }
            private void informazioniToolStripMenuItem_Click(object sender, EventArgs e)
            {
                MessageBox.Show("Acea: report monitoraggio.");
            }
        #endregion

        #region Scheduler
        private void ReportMonScheduler_Tick(object sender, EventArgs e)
        {
            string minutes = ConfigUtils.GetMinutes().Trim();
            string hours = ConfigUtils.GetHours().Trim();
            string dayweek = DateTime.Now.DayOfWeek.ToString();
            if (DateTime.Now.Minute.ToString().Trim() == minutes
                && DateTime.Now.Hour.ToString().Trim() == hours)
            {
                var days = ConfigUtils.GetScheduledDays();
                string d = DateTime.Now.Day.ToString().Trim();
                if (d.Length == 1) 
                {
                    d = "0" + d;
                }
                if (days.Contains(d))
                {
                    ReportMonNotifyIcon.ShowBalloonTip(5000, "Informazioni..", "Processo  iniziato", ToolTipIcon.Info);

                    SendMailsWorker.RunWorkerAsync();
                }
            }
        }
        #endregion

        private void interrompiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendMailsWorker.CancelAsync();
        }



    }
}
