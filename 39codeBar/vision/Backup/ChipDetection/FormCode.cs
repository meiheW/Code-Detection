using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using MiscFunctions;

namespace ChipDetection
{
    public partial class FormCode : Form
    {
        //private Parameter Parameter.GetInstance();
        private string mDirectoryName;
        public FormCode()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;

           // mMiscFunction = MiscFunction.CreateInstance();
            //Parameter.GetInstance() = Parameter.CreateInstance();

            mDirectoryName = MiscFunction.GetInstance().GetAssemblyPath() + Parameter.GetInstance().TemplateDirectory + "\\";

            string[] directories = Directory.GetDirectories(mDirectoryName);
            cmbTemplate.Items.Add("(默认模板)");
            foreach (string directory in directories)
            {
                string[] strings = directory.Split(new string[] {"\\"}, StringSplitOptions.None);
                if (strings.Length>=2)
                {
                    cmbTemplate.Items.Add(strings[strings.Length-1]);
                }
            }
            if (cmbTemplate.Items.Contains(Parameter.GetInstance().DefaultTemplateName))
            {
                cmbTemplate.SelectedItem = (Parameter.GetInstance().DefaultTemplateName);
            }
            else
            {
                cmbTemplate.SelectedIndex = 0;
            }
        }
        public string GetTemplateName()
        {
            return cmbTemplate.Text;
        }
        public string GetTemplateDirectoryName()
        {
            if (cmbTemplate.SelectedIndex==0)
            {
                return cmbTemplate.Text;
            }
            else
            {
                return mDirectoryName + cmbTemplate.Text + "\\";
            }
        }

        public string GetCodeString()
        {

            if (CheckCodeFormat())
            {
                return txtCode.Text;
            }
            return string.Empty;
        }
        public string GetOperationIDString()
        {
            return txtOperationID.Text.Trim();
        }
        public string GetDeviceNameString()
        {
            return txtDeviceName.Text.Trim();
        }
        private bool CheckCodeFormat()
        {
            return true;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (CheckCodeFormat())
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("条码错误！");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }

    }
}
