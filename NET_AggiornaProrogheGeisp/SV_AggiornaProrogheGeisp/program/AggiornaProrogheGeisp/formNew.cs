using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace AggiornaProrogheGeisp
{
    public partial class formNew : Form
    {
        private formMain fm;

        public formNew(formMain pfm)
        {
            InitializeComponent();
            fm = pfm;
        }

        private void buttonLookMdb_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogMdb = new OpenFileDialog();
            dialogMdb.InitialDirectory = Automation.cdir;
            if (dialogMdb.ShowDialog() == DialogResult.OK)
                textBoxMdbPath.Text = dialogMdb.FileName;
        }
        private void buttonLookExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogExcel = new OpenFileDialog();
            dialogExcel.InitialDirectory = Automation.cdir;
            if (dialogExcel.ShowDialog() == DialogResult.OK)
                textBoxExcelPath.Text = dialogExcel.FileName;
        }
        private void buttonLookMdb2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogMdb2 = new OpenFileDialog();
            dialogMdb2.InitialDirectory = Automation.cdir;
            if (dialogMdb2.ShowDialog() == DialogResult.OK)
                textBoxMdb2Path.Text = dialogMdb2.FileName;
        }

        private void buttonNewAsoc_Click(object sender, EventArgs e)
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
            if (formMain.doc.SelectSingleNode("/Root/asoc[@name='" + textBoxName.Text + "']") != null)
            {
                MessageBox.Show("Name already exists!");
                return;
            }

            XmlNode asoc = formMain.doc.CreateNode(XmlNodeType.Element, "asoc", null);
            XmlNode name = formMain.doc.CreateNode(XmlNodeType.Element, "name", null);
            name.InnerText = textBoxName.Text;
            asoc.AppendChild(name);
            XmlNode mdb = formMain.doc.CreateNode(XmlNodeType.Element, "mdb", null);
            mdb.InnerText = textBoxExcelPath.Text;
            asoc.AppendChild(mdb);
            XmlNode excel = formMain.doc.CreateNode(XmlNodeType.Element, "excel", null);
            excel.InnerText = textBoxMdbPath.Text;
            asoc.AppendChild(excel);
            XmlNode mdb2 = formMain.doc.CreateNode(XmlNodeType.Element, "mdb2", null);
            mdb2.InnerText = textBoxMdb2Path.Text;
            asoc.AppendChild(mdb2);
            XmlAttribute nameatr = formMain.doc.CreateAttribute("name");
            nameatr.Value = textBoxName.Text;
            asoc.Attributes.Append(nameatr);
            formMain.doc.SelectSingleNode("Root").AppendChild(asoc);
            formMain.doc.Save(Path.Combine(Automation.cdir, "associations.xml"));

            fm.initlistSV();
            fm.selectNew(textBoxName.Text);
            Dispose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
