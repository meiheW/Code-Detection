namespace WheelSensorTest
{
    partial class FormPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPassword));
            this.panelButtons = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.AccessibleDescription = null;
            this.panelButtons.AccessibleName = null;
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.BackgroundImage = null;
            this.panelButtons.Controls.Add(this.label2);
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Controls.Add(this.btnConfirm);
            this.panelButtons.Font = null;
            this.panelButtons.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelButtons.Name = "panelButtons";
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
            // txtOldPassword
            // 
            this.txtOldPassword.AcceptsTab = true;
            this.txtOldPassword.AccessibleDescription = null;
            this.txtOldPassword.AccessibleName = null;
            resources.ApplyResources(this.txtOldPassword, "txtOldPassword");
            this.txtOldPassword.BackgroundImage = null;
            this.txtOldPassword.Name = "txtOldPassword";
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AccessibleDescription = null;
            this.lblOldPassword.AccessibleName = null;
            resources.ApplyResources(this.lblOldPassword, "lblOldPassword");
            this.lblOldPassword.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblOldPassword.Name = "lblOldPassword";
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
            // lblNewPassword
            // 
            this.lblNewPassword.AccessibleDescription = null;
            this.lblNewPassword.AccessibleName = null;
            resources.ApplyResources(this.lblNewPassword, "lblNewPassword");
            this.lblNewPassword.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNewPassword.Name = "lblNewPassword";
            // 
            // FormPassword
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.lblNewPassword);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.lblOldPassword);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.panelButtons);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPassword";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormUser_FormClosing);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Panel panelButtons;
        protected System.Windows.Forms.Label label2;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.Button btnConfirm;
        protected System.Windows.Forms.Label lblUserName;
        protected System.Windows.Forms.TextBox txtUserName;
        protected System.Windows.Forms.TextBox txtOldPassword;
        protected System.Windows.Forms.Label lblOldPassword;
        protected System.Windows.Forms.TextBox txtNewPassword;
        protected System.Windows.Forms.Label lblNewPassword;
    }
}