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
    public partial class FormAddUser : Form
    {
        private ComponentResourceManager mResources;

        private UserAuthority mUserAuthority;
        public UserAuthority UserAuthority
        {
            get { return mUserAuthority; }
        }

        public FormAddUser()
        {
            InitializeComponent();

            mResources = new ComponentResourceManager(typeof(FormAddUser));
            cmbUserType.Items.Add("管理员");
            cmbUserType.Items.Add("操作员");
            cmbUserType.SelectedIndex = 1;
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

                    MessageBox.Show("用户名不能为空！", "错误");
	                return;
	            }
                //string oldPassword = txtOldPassword.Text.Trim();
                string newPassword = txtNewPassword.Text.Trim();

                //if (oldPassword.Length!=6)
                //{
                //    txtOldPassword.Focus();
                //    txtOldPassword.SelectAll();
                //    MessageBox.Show(mResources.GetString("txtPasswordLengthError"), mResources.GetString("error"));
                //    return;
                //}
                if (newPassword.Length != 6)
                {
                    txtNewPassword.Focus();
                    txtNewPassword.SelectAll();
                    MessageBox.Show("密码长度应为6位！", "错误");
                    return;
                }
                if (!UserConfiguration.GetInstance().User.ContainsKey(userName))
	            {
                    mUserAuthority = new UserAuthority(userName, newPassword, (UserType)cmbUserType.SelectedIndex);
                    UserConfiguration.GetInstance().User.Add(mUserAuthority.Name, mUserAuthority);
                    //if (oldPassword.Equals(userAuthority.Password))
                    {
                        //userAuthority.Set(userName,newPassword,userAuthority.UserType);
                        //mUserConfiguration.SaveConfigurationFile();
                        //MessageBox.Show(mResources.GetString("txtChangePasswordSuccess"), mResources.GetString("hint"));
	                    this.DialogResult = DialogResult.OK;
	                    this.Close();
	                }

                    //else
                    //{
                    //    txtOldPassword.Focus();
                    //    txtOldPassword.SelectAll();

                    //    MessageBox.Show(mResources.GetString("txtWrongPassword"), mResources.GetString("error"));

                    //}
	            }
                else
                {
                    txtUserName.Focus();
                    txtUserName.SelectAll();
                    MessageBox.Show("用户名错误！", "错误");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
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
