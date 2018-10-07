using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChipDetection
{
    public partial class FormNewTemplate : Form
    {
        private string mTemplateName;
        public string TemplateName
        {
            get { return mTemplateName; }
            set { mTemplateName = value; }
        }
        public FormNewTemplate()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            mTemplateName = txtName.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }
    }
}
