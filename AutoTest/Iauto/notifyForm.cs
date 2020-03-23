using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Iauto
{
    public partial class notifyForm : Form
    {
        cfgForm cfg;

        public notifyForm()
        {
            InitializeComponent();
        }

        private void notifyForm_Load(object sender, EventArgs e)
        {
            cfg = new cfgForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfg.ShowDialog();
        }

        private void automationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cfg.AutoStart(-1, -1, 50);
        }
    }
}
