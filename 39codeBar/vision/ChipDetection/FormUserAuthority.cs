using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChipDetection
{
    public partial class FormUserAuthority : Form
    {
 
        private ComponentResourceManager mResources;

        public FormUserAuthority()
        {
            InitializeComponent();

            mResources = new ComponentResourceManager(typeof(FormUserAuthority));
            InitializeGrid();
        }

        private void AddUserAuthority(UserAuthority userAuthority,int rowIndex) 
        {
            usrGrid.Rows[rowIndex].Cells[0].Value = userAuthority.Name.ToString();
            usrGrid.Rows[rowIndex].Cells[1].Value = userAuthority.Password;
            usrGrid.Rows[rowIndex].Cells[2].Value = userAuthority.UserType.Equals(UserType.Administrator) ? "管理员" : "操作员";
            usrGrid.Rows[rowIndex].Cells[3].Value = userAuthority.Remark;


        }
        private void InitializeGrid()
        {

            IDictionaryEnumerator enumerator = UserConfiguration.GetInstance().User.GetEnumerator();
            while (enumerator.MoveNext())
            {
                UserAuthority userAuthority = (UserAuthority)enumerator.Value;
                int index = this.usrGrid.Rows.Add();
                AddUserAuthority(userAuthority, index);
            }
            usrGrid.Update();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int myrow;
                for (int row = 0; row < usrGrid.Rows.Count-1; row++)
                {
                    if (usrGrid.Rows[row].Cells[0].Value == null)
                    {
                        myrow =row+1;
                        MessageBox.Show("第" + myrow.ToString() + "行输入的用户名为空，请重新输入!", "错误");
                        usrGrid.Rows[row].Selected = true;
                        return;
                    }
                    if (usrGrid.Rows[row].Cells[1].Value == null)
                    {
                        myrow = row + 1;
                        MessageBox.Show("第" + myrow.ToString() + "行输入的密码为空，请重新输入!", "错误");
                        usrGrid.Rows[row].Selected = true;
                        return;
                    }
                    if (usrGrid.Rows[row].Cells[2].Value == null)
                    {
                        myrow = row + 1;
                        MessageBox.Show("第" + myrow.ToString() + "行用户类型为空，请重新输入!", "错误");
                        usrGrid.Rows[row].Selected = true;
                        return;
                    }

                    string password = usrGrid.Rows[row].Cells[1].Value.ToString();
                    if (password.Length != 6)
                    {
                        myrow = row + 1;
                        MessageBox.Show("第" + myrow.ToString() + "行输入的密码长度必须会6位，请重新输入!", "错误");
                        usrGrid.Rows[row].Selected = true;
                        return;
                    }
                    string strusrname = usrGrid.Rows[row].Cells[0].Value.ToString();
                    strusrname.Trim();

                    for (int i = 0; i < row; i++)
                    {
                        string prename = usrGrid.Rows[i].Cells[0].Value.ToString();
                        if( prename.Trim().Equals( strusrname ))
                        {
                             myrow = row + 1;
                             MessageBox.Show("第" + myrow.ToString() + "行输入的用户名已存在，请重新输入!", "错误");
                             usrGrid.Rows[row].Selected = true;
                             return;
                        }
                    }
                }

                UserConfiguration.GetInstance().User.Clear();
                for (int row = 0; row < usrGrid.Rows.Count-1; row++)
                {
                    string name = usrGrid.Rows[row].Cells[0].Value.ToString();
                    string password = usrGrid.Rows[row].Cells[1].Value.ToString();
                    UserType type = usrGrid.Rows[row].Cells[2].Value.ToString().Equals("管理员") ? UserType.Administrator : UserType.Operator;
                    UserAuthority user = new UserAuthority(name, password, type);
                    user.Remark = "";
                    if (usrGrid.Rows[row].Cells[3].Value != null)
                    {
                        user.Remark = usrGrid.Rows[row].Cells[3].Value.ToString();
                    }
                    UserConfiguration.GetInstance().User.Add(user.Name, user);
                }
                UserConfiguration.GetInstance().SaveConfigurationFile();
                MessageBox.Show("保存用户权限成功！", "提示");
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


        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                FormAddUser formAddUser = new FormAddUser();
                if (formAddUser.ShowDialog() == DialogResult.OK)
                {
                    UserAuthority userAuthority = formAddUser.UserAuthority;
                    int index = this.usrGrid.Rows.Add();
                    AddUserAuthority(userAuthority, index);
                    MessageBox.Show("添加用户成功！", "提示");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                if (usrGrid.SelectedRows[0].Index >= usrGrid.Rows.Count-1)
                {
                    return;
                }

                if (usrGrid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请选择需要删除的用户！", "错误");
                    return;
                }

                if (1 == usrGrid.Rows.Count)
                {
                    MessageBox.Show("不能删除最后一个用户！", "错误");
                    return;
                }
               

                if (usrGrid.SelectedRows[0].Index == 0)
                {
                    MessageBox.Show("不能删除默认的系统管理员！", "错误");
                    return;
                }
                else
                {

                    DialogResult dialogResult = MessageBox.Show("是否确定需要删除选中的用户！", "错误", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        usrGrid.Rows.RemoveAt(usrGrid.SelectedRows[0].Index);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
            }   
            
        }

    }
}
