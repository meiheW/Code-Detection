namespace ChipDetection
{
    partial class FormUserAuthority
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUserAuthority));
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.usrGrid = new System.Windows.Forms.DataGridView();
            this.usrname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usrtype = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.strmemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtons.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usrGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.AccessibleDescription = null;
            this.panelButtons.AccessibleName = null;
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.BackgroundImage = null;
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnAdd);
            this.panelButtons.Controls.Add(this.label2);
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Controls.Add(this.btnConfirm);
            this.panelButtons.Font = null;
            this.panelButtons.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelButtons.Name = "panelButtons";
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleDescription = null;
            this.btnDelete.AccessibleName = null;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackgroundImage = null;
            this.btnDelete.Font = null;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleDescription = null;
            this.btnAdd.AccessibleName = null;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.BackgroundImage = null;
            this.btnAdd.Font = null;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleDescription = null;
            this.btnClose.AccessibleName = null;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackgroundImage = null;
            this.btnClose.Font = null;
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleDescription = null;
            this.btnConfirm.AccessibleName = null;
            resources.ApplyResources(this.btnConfirm, "btnConfirm");
            this.btnConfirm.BackgroundImage = null;
            this.btnConfirm.Font = null;
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblUserName
            // 
            this.lblUserName.AccessibleDescription = null;
            this.lblUserName.AccessibleName = null;
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblUserName.Name = "lblUserName";
            // 
            // txtUserName
            // 
            this.txtUserName.AccessibleDescription = null;
            this.txtUserName.AccessibleName = null;
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.BackgroundImage = null;
            this.txtUserName.Name = "txtUserName";
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AccessibleDescription = null;
            this.lblOldPassword.AccessibleName = null;
            resources.ApplyResources(this.lblOldPassword, "lblOldPassword");
            this.lblOldPassword.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblOldPassword.Name = "lblOldPassword";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.AcceptsTab = true;
            this.txtOldPassword.AccessibleDescription = null;
            this.txtOldPassword.AccessibleName = null;
            resources.ApplyResources(this.txtOldPassword, "txtOldPassword");
            this.txtOldPassword.BackgroundImage = null;
            this.txtOldPassword.Name = "txtOldPassword";
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AccessibleDescription = null;
            this.lblNewPassword.AccessibleName = null;
            resources.ApplyResources(this.lblNewPassword, "lblNewPassword");
            this.lblNewPassword.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNewPassword.Name = "lblNewPassword";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.AcceptsTab = true;
            this.txtNewPassword.AccessibleDescription = null;
            this.txtNewPassword.AccessibleName = null;
            resources.ApplyResources(this.txtNewPassword, "txtNewPassword");
            this.txtNewPassword.BackgroundImage = null;
            this.txtNewPassword.Name = "txtNewPassword";
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.usrGrid);
            this.panel1.Controls.Add(this.txtNewPassword);
            this.panel1.Controls.Add(this.lblNewPassword);
            this.panel1.Controls.Add(this.txtOldPassword);
            this.panel1.Controls.Add(this.lblOldPassword);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.lblUserName);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // usrGrid
            // 
            this.usrGrid.AccessibleDescription = null;
            this.usrGrid.AccessibleName = null;
            resources.ApplyResources(this.usrGrid, "usrGrid");
            this.usrGrid.BackgroundImage = null;
            this.usrGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usrGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usrname,
            this.password,
            this.usrtype,
            this.strmemo});
            this.usrGrid.Font = null;
            this.usrGrid.MultiSelect = false;
            this.usrGrid.Name = "usrGrid";
            this.usrGrid.RowTemplate.Height = 23;
            this.usrGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // usrname
            // 
            resources.ApplyResources(this.usrname, "usrname");
            this.usrname.Name = "usrname";
            // 
            // password
            // 
            resources.ApplyResources(this.password, "password");
            this.password.Name = "password";
            // 
            // usrtype
            // 
            resources.ApplyResources(this.usrtype, "usrtype");
            this.usrtype.Items.AddRange(new object[] {
            "管理员",
            "操作员"});
            this.usrtype.Name = "usrtype";
            // 
            // strmemo
            // 
            resources.ApplyResources(this.strmemo, "strmemo");
            this.strmemo.Name = "strmemo";
            // 
            // FormUserAuthority
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelButtons);
            this.Font = null;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUserAuthority";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.panelButtons.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usrGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelButtons;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.Button btnConfirm;
      //  private FlexCell.Grid grdUser;
        protected System.Windows.Forms.Label lblUserName;
        protected System.Windows.Forms.TextBox txtUserName;
        protected System.Windows.Forms.Label lblOldPassword;
        protected System.Windows.Forms.TextBox txtOldPassword;
        protected System.Windows.Forms.Label lblNewPassword;
        protected System.Windows.Forms.TextBox txtNewPassword;
        protected System.Windows.Forms.Button btnDelete;
        protected System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView usrGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn usrname;
        private System.Windows.Forms.DataGridViewTextBoxColumn password;
        private System.Windows.Forms.DataGridViewComboBoxColumn usrtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn strmemo;
    }
}