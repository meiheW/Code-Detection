using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace ChipDetection
{
    public partial class FormUser : Form
    {
        private ComponentResourceManager mResources;

        private bool mIsInvalidClose;
        private bool mIsReset;

        private UserAuthority mUserAuthority;
        public UserAuthority GetUserAuthority()
        {
            return mUserAuthority;
        }
        public FormUser(bool isReset)
        {
            InitializeComponent();
            mResources = new ComponentResourceManager(typeof(FormUser));
            txtUserName.Text = "admin";
            txtPassword.Text = "111111";
     
            mIsInvalidClose = true;
            if (isReset)
            {
                btnClose.Text = "取消";
            }
            mIsReset = isReset;

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            mIsInvalidClose = false;
            try
            {
	            //this.DialogResult = DialogResult.Cancel;
	            string userName = txtUserName.Text.Trim();
	            if(string.IsNullOrEmpty(userName))
	            {
                    txtUserName.Focus();
                    txtUserName.SelectAll();

                    MessageBox.Show("用户名不能为空!", "错误");
	                return;
	            }
	            string password = txtPassword.Text.Trim();
	            if (password.Length!=6)
	            {
                    txtPassword.Focus();
                    txtPassword.SelectAll();

                    MessageBox.Show("密码长度为6位！", "错误");
	                return;
	            }
                if (UserConfiguration.GetInstance().User.ContainsKey(userName))
	            {
                    mUserAuthority = UserConfiguration.GetInstance().User[userName];
                    if (password.Equals(mUserAuthority.Password))
	                {
                        MessageBox.Show("登录成功！", "提示");
	                    this.DialogResult = DialogResult.OK;
	                    this.Close();
	                }
                    else
                    {
                        txtPassword.Focus();
                        txtPassword.SelectAll();
                        MessageBox.Show("密码错误！", "错误");

                    }
	            }
	            else
	            {
                    txtUserName.Focus();
                    txtUserName.SelectAll();
                    MessageBox.Show("无效用户名！", "错误");
                    return;

	            }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
            }
            finally
            {
                mIsInvalidClose = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            mIsInvalidClose = false;
            if (mIsReset)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else
            {
                Application.Exit();
            }
            
        }

        private void FormUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mIsReset)
            {
                if (mIsInvalidClose)
                {
                    e.Cancel = true;
                }            
            }            
        }
    }
}
