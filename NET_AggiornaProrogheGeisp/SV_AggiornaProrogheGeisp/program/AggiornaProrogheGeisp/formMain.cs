using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace AggiornaProrogheGeisp
{
    public partial class formMain : Form
    {
        public static List<List<string>> databases;
        public static XmlDocument doc;
        public string cdir;

        public int interval;
        public DateTime nextDate;

        public formMain()
        {
            InitializeComponent();

            cdir = Automation.cdir;
            doc = new XmlDocument();
            doc.Load(Path.Combine(cdir,"associations.xml"));
            databases = new List<List<string>>();
            initlistSV();

            datePicker.MinDate = DateTime.Now;
            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.ShowUpDown = true;
            interval=1;
            comboBoxInterval.SelectedItem = "1 giorno";
            setStatusStop();

            timerUpdateTime.Start();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Asoc
        public void initlistSV()
        {
            listBoxAsoc.Items.Clear();
            XmlNode root = doc.SelectSingleNode("Root");
            foreach (XmlNode asoc in root.ChildNodes)
                listBoxAsoc.Items.Add(asoc.Attributes[0].Value);
        }
        public void selectNew(string text)
        {
            listBoxAsoc.SelectedItem = text;
        }

        private void buttonLookMdb_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogMdb = new OpenFileDialog();
            dialogMdb.InitialDirectory = cdir;
            if (dialogMdb.ShowDialog() == DialogResult.OK)
                textBoxMdbPath.Text = dialogMdb.FileName;
        }
        private void buttonLookExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogExcel = new OpenFileDialog();
            dialogExcel.InitialDirectory = cdir;
            if (dialogExcel.ShowDialog() == DialogResult.OK)
                textBoxExcelPath.Text = dialogExcel.FileName;
        }
        private void buttonLookMdb2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogMdb2 = new OpenFileDialog();
            dialogMdb2.InitialDirectory = cdir;
            if (dialogMdb2.ShowDialog() == DialogResult.OK)
                textBoxMdb2Path.Text = dialogMdb2.FileName;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
            textBoxMdbPath.Clear();
            textBoxExcelPath.Clear();
            textBoxMdb2Path.Clear();
            listBoxAsoc.ClearSelected();
        }

        private void buttonNewAsoc_Click(object sender, EventArgs e)
        {
            formNew fm = new formNew(this);
            fm.ShowDialog();
        }
        private void buttonModifyAsoc_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBoxExcelPath.Text) == false || File.Exists(textBoxMdb2Path.Text) == false || File.Exists(textBoxMdbPath.Text) == false)
            {
                MessageBox.Show("Invalid path format!");
                return;
            }
            if (String.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Name field is required!");
                return;
            }

            XmlNode selectedAsoc = doc.SelectSingleNode("/Root/asoc[@name='" + listBoxAsoc.SelectedItem.ToString() + "']");
            selectedAsoc.Attributes[0].Value = textBoxName.Text;
            selectedAsoc.ChildNodes[0].InnerText = textBoxName.Text;
            selectedAsoc.ChildNodes[1].InnerText = textBoxExcelPath.Text;
            selectedAsoc.ChildNodes[2].InnerText = textBoxMdbPath.Text;
            selectedAsoc.ChildNodes[3].InnerText = textBoxMdb2Path.Text;
            doc.Save(Path.Combine(cdir, "associations.xml"));

            initlistSV();
            listBoxAsoc.SelectedItem = textBoxName.Text;
        }
        private void buttonDeleteAsoc_Click(object sender, EventArgs e)
        {
            XmlNode selectedAsoc = doc.SelectSingleNode("/Root/asoc[@name='" + listBoxAsoc.SelectedItem.ToString() + "']");
            doc.SelectSingleNode("Root").RemoveChild(selectedAsoc);
            doc.Save(Path.Combine(cdir, "associations.xml"));

            buttonClear_Click(null, null);
            initlistSV();
        }

        private void listBoxAsoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxAsoc.SelectedItem == null)
            {
                buttonModifyAsoc.Enabled = false;
                buttonDeleteAsoc.Enabled = false;
            }
            else
            {
                buttonModifyAsoc.Enabled = true;
                buttonDeleteAsoc.Enabled = true;

                XmlNode selectedAsoc = doc.SelectSingleNode("/Root/asoc[@name='" + listBoxAsoc.SelectedItem.ToString() + "']");
                textBoxName.Text = selectedAsoc.ChildNodes[0].InnerText;
                textBoxExcelPath.Text = selectedAsoc.ChildNodes[1].InnerText;
                textBoxMdbPath.Text = selectedAsoc.ChildNodes[2].InnerText;
                textBoxMdb2Path.Text = selectedAsoc.ChildNodes[3].InnerText;
            }
        }
        #endregion

        #region Plan
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            interval = Convert.ToInt32(comboBoxInterval.SelectedItem.ToString().Split(' ')[0]);
        }

        private void datePicker_ValueChanged(object sender, EventArgs e)
        {
            nextDate = new DateTime(datePicker.Value.Year, datePicker.Value.Month, datePicker.Value.Day,
                timePicker.Value.Hour, timePicker.Value.Minute, timePicker.Value.Second);
        }
        private void timePicker_ValueChanged(object sender, EventArgs e)
        {
            nextDate = new DateTime(datePicker.Value.Year, datePicker.Value.Month, datePicker.Value.Day,
                timePicker.Value.Hour, timePicker.Value.Minute, timePicker.Value.Second);
        }

        private void timerUpdateTime_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Date > datePicker.Value.Date)
                datePicker.MinDate = DateTime.Now;
        }

        private void buttonSetPlan_Click(object sender, EventArgs e)
        {
            if (buttonSetPlan.Text == "Impostare Planification")
            {
                buttonSetPlan.Text = "Cambiare Planification";
                datePicker.Enabled = false;
                timePicker.Enabled = false;
                comboBoxInterval.Enabled = false;
                timerUpdateTime.Stop();
            }
            else
            {
                buttonSetPlan.Text = "Impostare Planification";
                datePicker.Enabled = true;
                timePicker.Enabled = true;
                comboBoxInterval.Enabled = true;
                timerUpdateTime.Start();
            }
        }
        public void setStatusNr()
        {
            TimeSpan ts = nextDate - DateTime.Now;
            labelTimeLeft.Text = ts.Days.ToString() + ". " + ts.Hours.ToString() + " : " + ts.Minutes.ToString() + " : " + ts.Seconds.ToString();
        }
        public void setStatusRun()
        {
            labelStatus.ForeColor = Color.Maroon;
            labelTimeLeft.ForeColor = Color.Maroon;
            labelStatusTitle.ForeColor = Color.Maroon;
            labelStatus.Text = "Running...";
            buttonSetPlan.Enabled = false;
        }
        public void setStatusWait()
        {
            labelStatus.ForeColor = Color.DarkOliveGreen;
            labelTimeLeft.ForeColor = Color.DarkOliveGreen;
            labelStatusTitle.ForeColor = Color.DarkOliveGreen;
            labelStatus.Text = "Waiting...";
            buttonSetPlan.Enabled = true;
        }
        public void setStatusStop()
        {
            labelStatus.ForeColor = Color.DarkGray;
            labelTimeLeft.ForeColor = Color.DarkGray;
            labelStatusTitle.ForeColor = Color.DarkGray;
            labelStatus.Text = "Stopped.";
            buttonSetPlan.Enabled = true;
        }
        #endregion
    }
}
