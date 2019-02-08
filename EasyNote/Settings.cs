using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyNote
{
    public class Settings : Form
    {
        //public EasyNote easyNote = null;

        public Settings(EasyNote easyNote)
        {
            InitializeComponent();

            this.easyNote = easyNote;

            checkBox1.Checked = easyNote.key.GetValue("GlobalPassword").ToString().Length > 0;
            label1.Enabled = checkBox1.Checked;
            textBox1.Enabled = checkBox1.Checked;
            checkBox2.Enabled = checkBox1.Checked;
            if (checkBox1.Checked)
            {
                checkRegValue("GlobalPassword", "password");
                textBox1.Text = easyNote.key.GetValue("GlobalPassword").ToString();
            }
            textBox2.Text = easyNote.savePeriod.ToString();
            textBox3.Text = easyNote.key.GetValue("NotesPath").ToString();

            ShowDialog();
        }

        private void checkRegValue(string value, object defaultValue)
        {
            if (easyNote.key.GetValue(value) == null)
            {
                easyNote.key.SetValue(value, defaultValue, RegistryValueKind.String);
            }
        }

        private void saveDataReg()
        {
            easyNote.key.SetValue("IsGlobalPassword", checkBox1.Checked, RegistryValueKind.String);
            easyNote.key.SetValue("GlobalPassword", textBox1.Text, RegistryValueKind.String);
            easyNote.key.SetValue("NotesPath", textBox3.Text, RegistryValueKind.String);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveDataReg();

            Close();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == checkBox1)
            {
                label1.Enabled = checkBox1.Checked;
                textBox1.Enabled = checkBox1.Checked;
                checkBox2.Enabled = checkBox1.Checked;
            }
            else if (sender == checkBox2)
            {
                textBox1.UseSystemPasswordChar = checkBox2.Checked;
            }
        }
    }
}
