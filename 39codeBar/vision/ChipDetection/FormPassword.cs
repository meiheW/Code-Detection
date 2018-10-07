using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WheelSensorTest
{
    public partial class FormPassword : Form
    {
        private UserConfiguration mUserConfiguration;
        private ComponentResourceManager mResources;

        public FormPassword()
        {
            InitializeComponent();
            mUserConfiguration = UserConfiguration.CreateInstance();
            mResources = new ComponentResourceManager(typeof(FormPassword));
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
	            //this.DialogResult = DialogResult.Cancel;
	            string userName = txtUserName.Text.Trim();
	            if(string.IsNullOrEmpty(userName))
	            {
                    txtUserName.Focus();
                    txtUserName.SelectAll();

                    MessageBox.Show(mResources.GetString("txtUserNameEmpty"), mResources.GetString("error"));
	                return;
	            }
	            string oldPassword = txtOldPassword.Text.Trim();
                string newPassword = txtNewPassword.Text.Trim();

	            if (oldPassword.Length!=6)
	            {
                    txtOldPassword.Focus();
                    txtOldPassword.SelectAll();
                    MessageBox.Show(mResources.GetString("txtPasswordLengthError"), mResources.GetString("error"));
	                return;
	            }
                if (newPassword.Length != 6)
                {
                    txtNewPassword.Focus();
                    txtNewPassword.SelectAll();
                    MessageBox.Show(mResources.GetString("txtPasswordLengthError"), mResources.GetString("error"));
                    return;
                }
	            if (mUserConfiguration.User.ContainsKey(userName))
	            {
                    UserAuthority userAuthority = mUserConfiguration.User[userName];
                    if (oldPassword.Equals(userAuthority.Password))
                    {
                        userAuthority.Set(userName,newPassword,userAuthority.UserType);
                        mUserConfiguration.SaveConfigurationFile();
                        MessageBox.Show(mResources.GetString("txtChangePasswordSuccess"), mResources.GetString("hint"));
	                    this.DialogResult = DialogResult.OK;
	                    this.Close();
	                }
                    else
                    {
                        txtOldPassword.Focus();
                        txtOldPassword.SelectAll();

                        MessageBox.Show(mResources.GetString("txtWrongPassword"), mResources.GetString("error"));

                    }
	            }
                else
                {
                    txtUserName.Focus();
                    txtUserName.SelectAll();
                    MessageBox.Show(mResources.GetString("txtWrongUserName"), mResources.GetString("error"));
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), mResources.GetString("error"));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void FormUser_FormClosing(object sender, FormClosingEventArgs e)
        {

            
        }

    }
}
