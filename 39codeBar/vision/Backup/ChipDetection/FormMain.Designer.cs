namespace ChipDetection
{
    partial class FormChipDetection
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
            this.picImage = new System.Windows.Forms.PictureBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblMark = new System.Windows.Forms.Label();
            this.lblStatusIndicator = new System.Windows.Forms.Label();
            this.lblCodeValue = new System.Windows.Forms.Label();
            this.lblCodeString = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpMotor = new System.Windows.Forms.GroupBox();
            this.btnStopMotor = new System.Windows.Forms.Button();
            this.btnStartMotor = new System.Windows.Forms.Button();
            this.grpCalibration = new System.Windows.Forms.GroupBox();
            this.btnStopCalibration = new System.Windows.Forms.Button();
            this.btnSaveCalibration = new System.Windows.Forms.Button();
            this.btnStartCalibration = new System.Windows.Forms.Button();
            this.lblCalibrationStatus = new System.Windows.Forms.Label();
            this.grpParameter = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numHeightMin = new System.Windows.Forms.NumericUpDown();
            this.numWidthMin = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWidthMax = new System.Windows.Forms.Label();
            this.numHeightMax = new System.Windows.Forms.NumericUpDown();
            this.numWidthMax = new System.Windows.Forms.NumericUpDown();
            this.chkCalibration = new System.Windows.Forms.CheckBox();
            this.lblDebug = new System.Windows.Forms.Label();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.lblDetectionTime = new System.Windows.Forms.Label();
            this.lblDetectionTimeString = new System.Windows.Forms.Label();
            this.lblFailureCount = new System.Windows.Forms.Label();
            this.lblFailureString = new System.Windows.Forms.Label();
            this.lblPassCount = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.lblPassString = new System.Windows.Forms.Label();
            this.lblSummaryString = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuUser = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUserManager = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUserLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.相机测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpMotor.SuspendLayout();
            this.grpCalibration.SuspendLayout();
            this.grpParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeightMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeightMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMax)).BeginInit();
            this.pnlControl.SuspendLayout();
            this.pnlResult.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picImage
            // 
            this.picImage.BackColor = System.Drawing.Color.White;
            this.picImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picImage.Location = new System.Drawing.Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(904, 675);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            this.picImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseMove);
            this.picImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseDown);
            this.picImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picImage_MouseUp);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.lblMark);
            this.pnlTop.Controls.Add(this.lblStatusIndicator);
            this.pnlTop.Controls.Add(this.lblCodeValue);
            this.pnlTop.Controls.Add(this.lblCodeString);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1269, 34);
            this.pnlTop.TabIndex = 1;
            // 
            // lblMark
            // 
            this.lblMark.AutoSize = true;
            this.lblMark.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMark.ForeColor = System.Drawing.Color.Red;
            this.lblMark.Location = new System.Drawing.Point(330, -1);
            this.lblMark.Name = "lblMark";
            this.lblMark.Size = new System.Drawing.Size(89, 39);
            this.lblMark.TabIndex = 5;
            this.lblMark.Text = "3999";
            // 
            // lblStatusIndicator
            // 
            this.lblStatusIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusIndicator.BackColor = System.Drawing.Color.Red;
            this.lblStatusIndicator.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStatusIndicator.ForeColor = System.Drawing.Color.White;
            this.lblStatusIndicator.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblStatusIndicator.Location = new System.Drawing.Point(1168, -1);
            this.lblStatusIndicator.Name = "lblStatusIndicator";
            this.lblStatusIndicator.Size = new System.Drawing.Size(100, 34);
            this.lblStatusIndicator.TabIndex = 2;
            this.lblStatusIndicator.Text = "停 止 运 行";
            this.lblStatusIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCodeValue
            // 
            this.lblCodeValue.AutoSize = true;
            this.lblCodeValue.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCodeValue.Location = new System.Drawing.Point(99, 9);
            this.lblCodeValue.Name = "lblCodeValue";
            this.lblCodeValue.Size = new System.Drawing.Size(142, 22);
            this.lblCodeValue.TabIndex = 1;
            this.lblCodeValue.Text = "FM03603002-08";
            // 
            // lblCodeString
            // 
            this.lblCodeString.AutoSize = true;
            this.lblCodeString.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCodeString.Location = new System.Drawing.Point(12, 9);
            this.lblCodeString.Name = "lblCodeString";
            this.lblCodeString.Size = new System.Drawing.Size(90, 22);
            this.lblCodeString.TabIndex = 0;
            this.lblCodeString.Text = "产品编码：";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.groupBox1);
            this.pnlRight.Controls.Add(this.pnlControl);
            this.pnlRight.Controls.Add(this.pnlResult);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(361, 675);
            this.pnlRight.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpMotor);
            this.groupBox1.Controls.Add(this.grpCalibration);
            this.groupBox1.Controls.Add(this.lblCalibrationStatus);
            this.groupBox1.Controls.Add(this.grpParameter);
            this.groupBox1.Controls.Add(this.chkCalibration);
            this.groupBox1.Controls.Add(this.lblDebug);
            this.groupBox1.Location = new System.Drawing.Point(6, 211);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 407);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // grpMotor
            // 
            this.grpMotor.Controls.Add(this.btnStopMotor);
            this.grpMotor.Controls.Add(this.btnStartMotor);
            this.grpMotor.Location = new System.Drawing.Point(6, 14);
            this.grpMotor.Name = "grpMotor";
            this.grpMotor.Size = new System.Drawing.Size(336, 71);
            this.grpMotor.TabIndex = 9;
            this.grpMotor.TabStop = false;
            this.grpMotor.Text = "电机控制";
            // 
            // btnStopMotor
            // 
            this.btnStopMotor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStopMotor.Location = new System.Drawing.Point(107, 26);
            this.btnStopMotor.Name = "btnStopMotor";
            this.btnStopMotor.Size = new System.Drawing.Size(87, 29);
            this.btnStopMotor.TabIndex = 13;
            this.btnStopMotor.Text = "电机停止";
            this.btnStopMotor.UseVisualStyleBackColor = true;
            this.btnStopMotor.Click += new System.EventHandler(this.btnStopMotor_Click);
            // 
            // btnStartMotor
            // 
            this.btnStartMotor.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStartMotor.Location = new System.Drawing.Point(14, 26);
            this.btnStartMotor.Name = "btnStartMotor";
            this.btnStartMotor.Size = new System.Drawing.Size(87, 29);
            this.btnStartMotor.TabIndex = 12;
            this.btnStartMotor.Text = "电机启动";
            this.btnStartMotor.UseVisualStyleBackColor = true;
            this.btnStartMotor.Click += new System.EventHandler(this.btnStartMotor_Click);
            // 
            // grpCalibration
            // 
            this.grpCalibration.Controls.Add(this.btnStopCalibration);
            this.grpCalibration.Controls.Add(this.btnSaveCalibration);
            this.grpCalibration.Controls.Add(this.btnStartCalibration);
            this.grpCalibration.Location = new System.Drawing.Point(5, 86);
            this.grpCalibration.Name = "grpCalibration";
            this.grpCalibration.Size = new System.Drawing.Size(337, 59);
            this.grpCalibration.TabIndex = 8;
            this.grpCalibration.TabStop = false;
            this.grpCalibration.Text = "标定";
            // 
            // btnStopCalibration
            // 
            this.btnStopCalibration.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStopCalibration.Location = new System.Drawing.Point(108, 20);
            this.btnStopCalibration.Name = "btnStopCalibration";
            this.btnStopCalibration.Size = new System.Drawing.Size(87, 29);
            this.btnStopCalibration.TabIndex = 11;
            this.btnStopCalibration.Text = "停止标定";
            this.btnStopCalibration.UseVisualStyleBackColor = true;
            this.btnStopCalibration.Click += new System.EventHandler(this.btnStopCalibration_Click);
            // 
            // btnSaveCalibration
            // 
            this.btnSaveCalibration.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveCalibration.Location = new System.Drawing.Point(200, 20);
            this.btnSaveCalibration.Name = "btnSaveCalibration";
            this.btnSaveCalibration.Size = new System.Drawing.Size(127, 29);
            this.btnSaveCalibration.TabIndex = 3;
            this.btnSaveCalibration.Text = "保存标定结果";
            this.btnSaveCalibration.UseVisualStyleBackColor = true;
            this.btnSaveCalibration.Click += new System.EventHandler(this.btnSaveCalibration_Click);
            // 
            // btnStartCalibration
            // 
            this.btnStartCalibration.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStartCalibration.Location = new System.Drawing.Point(15, 20);
            this.btnStartCalibration.Name = "btnStartCalibration";
            this.btnStartCalibration.Size = new System.Drawing.Size(87, 29);
            this.btnStartCalibration.TabIndex = 10;
            this.btnStartCalibration.Text = "开始标定";
            this.btnStartCalibration.UseVisualStyleBackColor = true;
            this.btnStartCalibration.Click += new System.EventHandler(this.btnStartCalibration_Click);
            // 
            // lblCalibrationStatus
            // 
            this.lblCalibrationStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCalibrationStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCalibrationStatus.Location = new System.Drawing.Point(205, 148);
            this.lblCalibrationStatus.Name = "lblCalibrationStatus";
            this.lblCalibrationStatus.Size = new System.Drawing.Size(138, 250);
            this.lblCalibrationStatus.TabIndex = 6;
            this.lblCalibrationStatus.Text = "L: 300 R:200";
            // 
            // grpParameter
            // 
            this.grpParameter.Controls.Add(this.label3);
            this.grpParameter.Controls.Add(this.label4);
            this.grpParameter.Controls.Add(this.numHeightMin);
            this.grpParameter.Controls.Add(this.numWidthMin);
            this.grpParameter.Controls.Add(this.label2);
            this.grpParameter.Controls.Add(this.lblWidthMax);
            this.grpParameter.Controls.Add(this.numHeightMax);
            this.grpParameter.Controls.Add(this.numWidthMax);
            this.grpParameter.Location = new System.Drawing.Point(8, 46);
            this.grpParameter.Name = "grpParameter";
            this.grpParameter.Size = new System.Drawing.Size(338, 17);
            this.grpParameter.TabIndex = 5;
            this.grpParameter.TabStop = false;
            this.grpParameter.Text = "参数";
            this.grpParameter.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "芯片高度下限";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "芯片宽度下限";
            // 
            // numHeightMin
            // 
            this.numHeightMin.Location = new System.Drawing.Point(251, 79);
            this.numHeightMin.Maximum = new decimal(new int[] {
            960,
            0,
            0,
            0});
            this.numHeightMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHeightMin.Name = "numHeightMin";
            this.numHeightMin.Size = new System.Drawing.Size(73, 21);
            this.numHeightMin.TabIndex = 5;
            this.numHeightMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numWidthMin
            // 
            this.numWidthMin.Location = new System.Drawing.Point(94, 79);
            this.numWidthMin.Maximum = new decimal(new int[] {
            1280,
            0,
            0,
            0});
            this.numWidthMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWidthMin.Name = "numWidthMin";
            this.numWidthMin.Size = new System.Drawing.Size(73, 21);
            this.numWidthMin.TabIndex = 4;
            this.numWidthMin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "芯片高度上限";
            // 
            // lblWidthMax
            // 
            this.lblWidthMax.AutoSize = true;
            this.lblWidthMax.Location = new System.Drawing.Point(15, 54);
            this.lblWidthMax.Name = "lblWidthMax";
            this.lblWidthMax.Size = new System.Drawing.Size(77, 12);
            this.lblWidthMax.TabIndex = 2;
            this.lblWidthMax.Text = "芯片宽度上限";
            // 
            // numHeightMax
            // 
            this.numHeightMax.Location = new System.Drawing.Point(251, 52);
            this.numHeightMax.Maximum = new decimal(new int[] {
            960,
            0,
            0,
            0});
            this.numHeightMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHeightMax.Name = "numHeightMax";
            this.numHeightMax.Size = new System.Drawing.Size(73, 21);
            this.numHeightMax.TabIndex = 1;
            this.numHeightMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numWidthMax
            // 
            this.numWidthMax.Location = new System.Drawing.Point(94, 52);
            this.numWidthMax.Maximum = new decimal(new int[] {
            1280,
            0,
            0,
            0});
            this.numWidthMax.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWidthMax.Name = "numWidthMax";
            this.numWidthMax.Size = new System.Drawing.Size(73, 21);
            this.numWidthMax.TabIndex = 0;
            this.numWidthMax.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkCalibration
            // 
            this.chkCalibration.AutoSize = true;
            this.chkCalibration.Location = new System.Drawing.Point(6, 69);
            this.chkCalibration.Name = "chkCalibration";
            this.chkCalibration.Size = new System.Drawing.Size(72, 16);
            this.chkCalibration.TabIndex = 4;
            this.chkCalibration.Text = "标定模式";
            this.chkCalibration.UseVisualStyleBackColor = true;
            this.chkCalibration.Visible = false;
            this.chkCalibration.Click += new System.EventHandler(this.chkCalibration_Click);
            // 
            // lblDebug
            // 
            this.lblDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDebug.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDebug.Location = new System.Drawing.Point(5, 151);
            this.lblDebug.Name = "lblDebug";
            this.lblDebug.Size = new System.Drawing.Size(184, 247);
            this.lblDebug.TabIndex = 3;
            this.lblDebug.Text = "L: 300 R:200";
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.Transparent;
            this.pnlControl.Controls.Add(this.button2);
            this.pnlControl.Controls.Add(this.btnTest);
            this.pnlControl.Controls.Add(this.button1);
            this.pnlControl.Controls.Add(this.btnStop);
            this.pnlControl.Controls.Add(this.btnStart);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlControl.Location = new System.Drawing.Point(0, 620);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(361, 55);
            this.pnlControl.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(251, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 22);
            this.button2.TabIndex = 3;
            this.button2.Text = "模版";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // btnTest
            // 
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTest.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTest.Location = new System.Drawing.Point(155, 14);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(90, 29);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "功能测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(308, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.Location = new System.Drawing.Point(89, 14);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(60, 29);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(23, 14);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(60, 29);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pnlResult
            // 
            this.pnlResult.BackColor = System.Drawing.Color.Transparent;
            this.pnlResult.Controls.Add(this.lblDetectionTime);
            this.pnlResult.Controls.Add(this.lblDetectionTimeString);
            this.pnlResult.Controls.Add(this.lblFailureCount);
            this.pnlResult.Controls.Add(this.lblFailureString);
            this.pnlResult.Controls.Add(this.lblPassCount);
            this.pnlResult.Controls.Add(this.lblSummary);
            this.pnlResult.Controls.Add(this.lblPassString);
            this.pnlResult.Controls.Add(this.lblSummaryString);
            this.pnlResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResult.Location = new System.Drawing.Point(0, 0);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(361, 208);
            this.pnlResult.TabIndex = 0;
            // 
            // lblDetectionTime
            // 
            this.lblDetectionTime.AutoSize = true;
            this.lblDetectionTime.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDetectionTime.Location = new System.Drawing.Point(134, 163);
            this.lblDetectionTime.Name = "lblDetectionTime";
            this.lblDetectionTime.Size = new System.Drawing.Size(148, 41);
            this.lblDetectionTime.TabIndex = 8;
            this.lblDetectionTime.Text = "00:00:00";
            // 
            // lblDetectionTimeString
            // 
            this.lblDetectionTimeString.AutoSize = true;
            this.lblDetectionTimeString.Font = new System.Drawing.Font("微软雅黑", 27.25F, System.Drawing.FontStyle.Bold);
            this.lblDetectionTimeString.Location = new System.Drawing.Point(21, 158);
            this.lblDetectionTimeString.Name = "lblDetectionTimeString";
            this.lblDetectionTimeString.Size = new System.Drawing.Size(107, 50);
            this.lblDetectionTimeString.TabIndex = 7;
            this.lblDetectionTimeString.Text = "耗时:";
            // 
            // lblFailureCount
            // 
            this.lblFailureCount.AutoSize = true;
            this.lblFailureCount.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFailureCount.ForeColor = System.Drawing.Color.Red;
            this.lblFailureCount.Location = new System.Drawing.Point(134, 107);
            this.lblFailureCount.Name = "lblFailureCount";
            this.lblFailureCount.Size = new System.Drawing.Size(45, 50);
            this.lblFailureCount.TabIndex = 6;
            this.lblFailureCount.Text = "1";
            // 
            // lblFailureString
            // 
            this.lblFailureString.AutoSize = true;
            this.lblFailureString.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFailureString.Location = new System.Drawing.Point(21, 108);
            this.lblFailureString.Name = "lblFailureString";
            this.lblFailureString.Size = new System.Drawing.Size(92, 50);
            this.lblFailureString.TabIndex = 5;
            this.lblFailureString.Text = "NG:";
            // 
            // lblPassCount
            // 
            this.lblPassCount.AutoSize = true;
            this.lblPassCount.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPassCount.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblPassCount.Location = new System.Drawing.Point(134, 53);
            this.lblPassCount.Name = "lblPassCount";
            this.lblPassCount.Size = new System.Drawing.Size(114, 50);
            this.lblPassCount.TabIndex = 4;
            this.lblPassCount.Text = "3999";
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSummary.Location = new System.Drawing.Point(134, 3);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(114, 50);
            this.lblSummary.TabIndex = 3;
            this.lblSummary.Text = "4000";
            // 
            // lblPassString
            // 
            this.lblPassString.AutoSize = true;
            this.lblPassString.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPassString.Location = new System.Drawing.Point(20, 53);
            this.lblPassString.Name = "lblPassString";
            this.lblPassString.Size = new System.Drawing.Size(89, 50);
            this.lblPassString.TabIndex = 2;
            this.lblPassString.Text = "OK:";
            // 
            // lblSummaryString
            // 
            this.lblSummaryString.AutoSize = true;
            this.lblSummaryString.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSummaryString.Location = new System.Drawing.Point(21, 3);
            this.lblSummaryString.Name = "lblSummaryString";
            this.lblSummaryString.Size = new System.Drawing.Size(107, 50);
            this.lblSummaryString.TabIndex = 1;
            this.lblSummaryString.Text = "总数:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.picImage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlRight);
            this.splitContainer1.Size = new System.Drawing.Size(1269, 675);
            this.splitContainer1.SplitterDistance = 904;
            this.splitContainer1.TabIndex = 3;
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUser,
            this.相机测试ToolStripMenuItem});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(1269, 25);
            this.mnuMain.TabIndex = 4;
            this.mnuMain.Text = "用户权限";
            // 
            // mnuUser
            // 
            this.mnuUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUserManager,
            this.mnuUserLogin});
            this.mnuUser.Name = "mnuUser";
            this.mnuUser.Size = new System.Drawing.Size(68, 21);
            this.mnuUser.Text = "用户权限";
            // 
            // mnuUserManager
            // 
            this.mnuUserManager.Name = "mnuUserManager";
            this.mnuUserManager.Size = new System.Drawing.Size(133, 22);
            this.mnuUserManager.Text = "权限管理...";
            this.mnuUserManager.Click += new System.EventHandler(this.mnuUserManager_Click);
            // 
            // mnuUserLogin
            // 
            this.mnuUserLogin.Name = "mnuUserLogin";
            this.mnuUserLogin.Size = new System.Drawing.Size(133, 22);
            this.mnuUserLogin.Text = "用户登录...";
            this.mnuUserLogin.Click += new System.EventHandler(this.mnuUserLogin_Click);
            // 
            // 相机测试ToolStripMenuItem
            // 
            this.相机测试ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开始ToolStripMenuItem,
            this.停止ToolStripMenuItem});
            this.相机测试ToolStripMenuItem.Name = "相机测试ToolStripMenuItem";
            this.相机测试ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.相机测试ToolStripMenuItem.Text = "相机测试";
            // 
            // 开始ToolStripMenuItem
            // 
            this.开始ToolStripMenuItem.Name = "开始ToolStripMenuItem";
            this.开始ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.开始ToolStripMenuItem.Text = "开始";
            // 
            // 停止ToolStripMenuItem
            // 
            this.停止ToolStripMenuItem.Name = "停止ToolStripMenuItem";
            this.停止ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.停止ToolStripMenuItem.Text = "停止";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.pnlTop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1269, 709);
            this.panel1.TabIndex = 5;
            // 
            // FormChipDetection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 734);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mnuMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.mnuMain;
            this.MaximizeBox = false;
            this.Name = "FormChipDetection";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "芯片检测";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormChipDetection_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpMotor.ResumeLayout(false);
            this.grpCalibration.ResumeLayout(false);
            this.grpParameter.ResumeLayout(false);
            this.grpParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeightMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeightMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidthMax)).EndInit();
            this.pnlControl.ResumeLayout(false);
            this.pnlResult.ResumeLayout(false);
            this.pnlResult.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlControl;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Label lblCodeValue;
        private System.Windows.Forms.Label lblCodeString;
        private System.Windows.Forms.Label lblPassString;
        private System.Windows.Forms.Label lblSummaryString;
        private System.Windows.Forms.Label lblStatusIndicator;
        private System.Windows.Forms.Label lblDetectionTimeString;
        private System.Windows.Forms.Label lblFailureCount;
        private System.Windows.Forms.Label lblFailureString;
        private System.Windows.Forms.Label lblPassCount;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Label lblDetectionTime;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblMark;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblDebug;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpParameter;
        private System.Windows.Forms.Button btnSaveCalibration;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numHeightMin;
        private System.Windows.Forms.NumericUpDown numWidthMin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWidthMax;
        private System.Windows.Forms.NumericUpDown numHeightMax;
        private System.Windows.Forms.NumericUpDown numWidthMax;
        private System.Windows.Forms.Label lblCalibrationStatus;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnStopCalibration;
        private System.Windows.Forms.Button btnStartCalibration;
        private System.Windows.Forms.GroupBox grpCalibration;
        private System.Windows.Forms.CheckBox chkCalibration;
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem mnuUser;
        private System.Windows.Forms.ToolStripMenuItem mnuUserManager;
        private System.Windows.Forms.ToolStripMenuItem mnuUserLogin;
        private System.Windows.Forms.GroupBox grpMotor;
        private System.Windows.Forms.Button btnStopMotor;
        private System.Windows.Forms.Button btnStartMotor;
        private System.Windows.Forms.ToolStripMenuItem 相机测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开始ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止ToolStripMenuItem;
    }
}

