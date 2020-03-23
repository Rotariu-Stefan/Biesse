using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Iauto
{
    public partial class cfgForm : Form
    {
        string cdir;

        public cfgForm()
        {
            InitializeComponent();
            cdir = Directory.GetCurrentDirectory();
            for (int i = 0; i < 3; i++)
                cdir = Directory.GetParent(cdir).ToString();
            fileTb.Text = Path.Combine(cdir, "input.csv");
            inputFileDialog.InitialDirectory = cdir;
        }

        public void AutoStart(int from, int to, int err)
        {
            //string fullcmd = "\"C:\\Sikuli1.0.1\\runIDE.cmd\" -r \"C:\\Users\\StravoS\\Documents\\Visual Studio 2010\\Projects\\Iauto\\Testauto.sikuli\"";

            System.Diagnostics.Process skProcess = new System.Diagnostics.Process();
            skProcess.StartInfo.FileName = "\"C:\\Sikuli1.0.1\\runIDE.cmd\"";
            skProcess.StartInfo.Arguments = "-r \"" + Path.Combine(cdir, "Testauto.sikuli") + "\" --args " + from + " " + to + " " + err;
            skProcess.StartInfo.UseShellExecute = false;
            skProcess.StartInfo.CreateNoWindow = true;
            skProcess.StartInfo.RedirectStandardOutput = true;
            skProcess.Start();

            string total = "";
            while (skProcess.HasExited == false)
                if (skProcess.StandardOutput.ReadLine().Contains(cdir))
                    break;
            while (skProcess.HasExited == false)
                total += skProcess.StandardOutput.ReadLine() + "\n";
            MessageBox.Show(total);

            //skProcess.WaitForExit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int from = -1, to = -1, err;
            string ifile = fileTb.Text;

            if (File.Exists(ifile) == false)
            {
                MessageBox.Show("Invalid path format!");
                return;
            }
            try
            {
                if (yesRadio.Checked == false)
                {
                    if (fromCheck.Checked == false)
                        from = Convert.ToInt32(fromTb.Text);
                    else
                        from = 1;
                    if (toCheck.Checked == false)
                        to = Convert.ToInt32(toTb.Text);
                }
                err = Convert.ToInt32(errTb.Text);
            }
            catch
            {
                MessageBox.Show("Invalid to/from/err format!");
                return;
            }
            if (to < -1 || from < -1 || from == 0 || to > from || err < 0)
                MessageBox.Show("Incorect to/from/err numbers!");

            AutoStart(from, to, err);
        }

        private void fromCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (fromCheck.Checked == false)
                fromTb.ReadOnly = false;
            else
            {
                fromTb.Text = "1";
                fromTb.ReadOnly = true;
            }
        }

        private void toCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (toCheck.Checked == false)
                toTb.ReadOnly = false;
            else
            {
                toTb.Text = "End";
                toTb.ReadOnly = true;
            }
        }

        private void errCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (errCheck.Checked == false)
                errTb.ReadOnly = false;
            else
            {
                errTb.Text = "50";
                errTb.ReadOnly = true;
            }
        }

        private void yesRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (yesRadio.Checked == false)
            {
                toCheck.Enabled = true;
                fromCheck.Enabled = true;
                fromTb.Text = "1";
            }
            else
            {
                toCheck.Enabled = false;
                fromCheck.Enabled = false;
                toCheck.Checked = true;
                fromCheck.Checked = true;
                fromTb.ReadOnly = true;
                toTb.ReadOnly = true;
                fromTb.Text = "Last";
                toTb.Text = "End";
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = inputFileDialog.ShowDialog();
            if (result == DialogResult.OK)
                fileTb.Text = inputFileDialog.FileName;
        }
    }
}
