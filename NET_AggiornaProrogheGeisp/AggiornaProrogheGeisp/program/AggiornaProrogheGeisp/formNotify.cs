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
    public partial class formNotify : Form
    {
        formMain formMain;

        public formNotify()
        {
            InitializeComponent();
        }

        private void notifyForm_Load(object sender, EventArgs e)
        {
            formMain = new formMain();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!formMain.Visible)
                formMain.ShowDialog();
            else
                formMain.Focus();
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startToolStripMenuItem.Enabled = false;
            workerAutomation.RunWorkerAsync();
        }

        private void plannedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerExec.Start();
        }

        private void timerExec_Tick(object sender, EventArgs e)
        {
            formMain.setStatusNr();

            if (Automation.running)
            {
                formMain.setStatusRun();
                startToolStripMenuItem.Enabled = false;
            }
            else
            {
                formMain.setStatusWait();
                startToolStripMenuItem.Enabled = true;

                if (formMain.nextDate <= DateTime.Now)
                {
                    workerAutomation.RunWorkerAsync();
                    formMain.nextDate = formMain.nextDate.AddSeconds(formMain.interval);
                }
            }
        }

        private void workerAutomation_DoWork(object sender, DoWorkEventArgs e)
        {
            Automation.loadDatabases(null);
            Automation.execute();
        }

        private void workerAutomation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Automation completed");
        }

    }
}
