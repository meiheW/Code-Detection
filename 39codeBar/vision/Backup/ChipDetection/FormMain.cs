using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using MiscFunctions;
using FlyCapture2Managed;
using Timer = System.Timers.Timer;
using System.Diagnostics;


namespace ChipDetection
{
    [Flags]
    public enum SizeChangeDirection
    {
        NA = 0x00,
        LEFT=0x01,
        RIGHT = 0x02,
        TOP = 0x04,
        BOTTOM = 0x08,
        TOP_LEFT = LEFT + TOP,
        TOP_RIGHT = RIGHT+TOP,
        BOTTOM_LEFT = LEFT+BOTTOM,
        BOTTOM_RIGHT = RIGHT+BOTTOM,
        ALL=LEFT + TOP+BOTTOM+RIGHT
    }
    public partial class FormChipDetection : Form
    {
    
        private UserAuthority mUserAuthority;
        private Configuration mConfiguration; 

          
        private AccessExcel mAccessExcel;
        private ChipDetection mChipDetection;



        private Timer mInitializationTimer;
        //private Timer mTriggerTimer;
        private Timer mTestTimer;
        private DateTime mStartTime;      


        private string mExcelModalFileName;
        private string mDestinationExcelFile;
        private int mCurrentDetectionCount = 0;
        private DateTime mPreviousTestTime;
        private int mPreviousTestCount;
        private double mTestLineRate;     

        private Point mFeaturePreviousMouseDownPoint;
        private Point mObjectPreviousMouseDownPoint;
        
        private SizeChangeDirection mFeatureSizeDirection;
        private SizeChangeDirection mObjectSizeDirection;

        private ChipTemplate mCurrentChipTemplate;
        private bool mIsInCalibrationMode;
        private double mCurrentMotorRate;
        private int mNoChipFoundCount;
        private int mPreviousSecondChipCount;
        private bool mIsResetConfiguration;
        private string mOperationID;
        private string mDeviceName;


        private ManagedImage m_rawImage;
        private ManagedImage m_processedImage;
        private bool m_grabImages;
        private AutoResetEvent m_grabThreadExited;
        private BackgroundWorker m_grabThread;
        private ManagedCameraBase m_camera = null;

        public FormChipDetection(bool isResetConfiguration)
        {

            mIsResetConfiguration = isResetConfiguration;
            InitializeComponent();


            Parameter.GetInstance().LoadOptionFile();
            mConfiguration = new Configuration(isResetConfiguration);

            if (isResetConfiguration)
            {
                UserConfiguration.GetInstance().CreateDefualtUserConfigurationFile();
            }
            else
            {
                UserConfiguration.GetInstance().LoadUserConfigurationFile();
            }
            mChipDetection = new ChipDetection();

            Parameter.GetInstance().WidthRatio = ((double)Parameter.GetInstance().WidthPixels) / picImage.Width;
            Parameter.GetInstance().HeightRatio = ((double)Parameter.GetInstance().HeightPixels) / picImage.Height;


            string returnString = EM9636B.GetInstance().Connect();
            if (!string.IsNullOrEmpty(returnString))
            {
                MessageBox.Show("连接数据采集卡失败！" + returnString);
            }
            else
            {
                returnString = EM9636B.GetInstance().SetIOPinsDirection(EM9636_IO_DIRECTION.OUT, EM9636_IO_SELECTION.IO1_16);
                if (!string.IsNullOrEmpty(returnString))
                {
                    MessageBox.Show("连接数据采集卡失败！" + returnString); ;
                }
                returnString = SetMotorStop();
                if (!string.IsNullOrEmpty(returnString))
                {
                    MessageBox.Show("电机解锁失败！" + returnString);
                }
            }

            lblMark.BackColor = Color.Transparent;
            Form.CheckForIllegalCrossThreadCalls = false;

            ResetTestStatus();


            //刷新
            //mChipDetection.RefreshDisplay+=new ChipDetection.RefreshDisplayEventHandler(ChipDetection_RefreshDisplay);
            picImage.Paint += new PaintEventHandler(PicImage_Paint);
            mExcelModalFileName = MiscFunction.GetInstance().GetAssemblyPath() + Parameter.GetInstance().ExcelFileName + ".xlsx";

            mPreviousTestCount = 0;
            mPreviousTestTime = DateTime.MinValue;

            mFeaturePreviousMouseDownPoint = Point.Empty;
            mObjectPreviousMouseDownPoint = Point.Empty;

            mCurrentChipTemplate = (ChipTemplate)Parameter.GetInstance().ChipTemplate.Clone();
            mCurrentChipTemplate.InitializeDisplaySize();

            mCurrentMotorRate = Parameter.GetInstance().MotorDefaultRate;

            InitializeTimers();
            mTestTimer.Start();
            UserLogin(false);
        }


        public bool CAM_connect()
        {
            try
            {
                ManagedBusManager busMgr = new ManagedBusManager();
                uint numCameras = busMgr.GetNumOfCameras();
                ManagedPGRGuid guid = busMgr.GetCameraFromIndex(0);
                m_camera = new ManagedCamera();
                m_camera.Connect(guid);

                //m_camera.IsConnected()

                CameraInfo camInfo = m_camera.GetCameraInfo();
            }
            catch (FC2Exception ex)
            {
                Debug.WriteLine("Failed to load form successfully: " + ex.Message);
                return false;
            }
            return true;
        }

        private void CAM_StartGrab()
        {
            m_camera.StartCapture();
            m_grabImages = true;
            m_grabThread = new BackgroundWorker();
            m_grabThread.ProgressChanged += new ProgressChangedEventHandler(UpdateUI);
            m_grabThread.DoWork += new DoWorkEventHandler(GrabLoop);
            m_grabThread.WorkerReportsProgress = true;
            m_grabThread.RunWorkerAsync();
        }

        private void GrabLoop(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (m_grabImages)
            {
                try
                {
                    m_camera.RetrieveBuffer(m_rawImage);
                }
                catch (FC2Exception ex)
                {
                    Debug.WriteLine("Error: " + ex.Message);
                    continue;
                }

                lock (this)
                {
                    //Image  processing //m_rawImage.Convert(FlyCapture2Managed.PixelFormat.PixelFormatBgr, m_processedImage);
                    cvProcess(m_rawImage);             
                }
                worker.ReportProgress(0);
            }

            m_grabThreadExited.Set();
        }

        public string CAM_StopGrab()
        {
             m_grabImages = false;
            try
            {
                m_camera.StopCapture();
            }
            catch (FC2Exception ex)
            {
                Debug.WriteLine("Failed to stop camera: " + ex.Message);
            }
            catch (NullReferenceException)
            {
                Debug.WriteLine("Camera is null");
            }

            return string.Empty;
        }
        public void CAM_Close()
        {
            try
            {

                m_camera.Disconnect();
            }
            catch (FC2Exception ex)
            {
                // Nothing to do here
            }
            catch (NullReferenceException ex)
            {
                // Nothing to do here
            }
        }

        private void cvProcess(  ManagedImage img )
        {

            //mChipDetection
        }

        private void UpdateUI(object sender, ProgressChangedEventArgs e)
        {
          
             ChipDetection_RefreshDisplay();
             picImage.Image = m_processedImage.bitmap;
             picImage.Invalidate();
        }
             
        private void UserLogin(bool isReset)
        {
            FormUser formUser = new FormUser(isReset);
            if (formUser.ShowDialog() == DialogResult.OK)
            {
                mUserAuthority = formUser.GetUserAuthority();
                RefreshUserAuthorityMenu();
            }
        }
        private void RefreshUserAuthorityMenu()
        {
            if (mUserAuthority == null)
            {
                return;
            }
            if (mUserAuthority.UserType == UserType.Administrator)
            {
                mnuUserManager.Enabled = true;
                grpCalibration.Enabled = true;
                btnTest.Enabled = true;
            }
            else
            {
                mnuUserManager.Enabled = false;
                grpCalibration.Enabled = false;
                btnTest.Enabled = false;


            }
            this.Text = "芯片检测(" + mConfiguration.Version + ")---" + "当前用户: " + mUserAuthority.Name
                            + "(" + (mUserAuthority.UserType.Equals(UserType.Administrator) ? "管理员" : "操作员") + ")";
        }


        private void ResetTestStatus()
        {
            lblCodeValue.Text = "未知";
            lblSummary.Text = "0";
            lblDetectionTime.Text = "00:00:00";
            lblFailureCount.Text = "0";
            lblSummary.Text = "0";
            lblPassCount.Text = "0";
            mCurrentDetectionCount = 0;
            mNoChipFoundCount = 0;
            mPreviousSecondChipCount = 0;
            mCurrentMotorRate = 0;
            mPreviousTestCount = 0;
            mPreviousTestTime = DateTime.MinValue;
            mTestLineRate = 0;
            mCurrentMotorRate = Parameter.GetInstance().MotorDefaultRate;

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnTest.Enabled = true;

            btnStartCalibration.Enabled = true;
            btnSaveCalibration.Enabled = false;
            btnStopCalibration.Enabled = false;

            btnStartMotor.Enabled = true;
            btnStopMotor.Enabled = false;
        }

        private int CheckSystem()
        {
            //计算机名
            string cName = Environment.GetEnvironmentVariable("ComputerName");
            //if (!cName.Equals("ADMIN-PC"))
            if (!cName.Equals(mConfiguration.PCName))
            {
                return 1;
            }
            //运行时间
            try
            {
                if (mConfiguration.MaxRunningDays < 1000)
                {
                    RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\ChipDetection");
                    string authorityString = registryKey.GetValue("Authority", string.Empty).ToString();
                    if (string.IsNullOrEmpty(authorityString))
                    {
                        registryKey.SetValue("Authority", DateTime.Now);
                    }
                    DateTime installDateTime = Convert.ToDateTime(registryKey.GetValue("Authority", string.Empty));
                    int interval = (int)DateTime.Now.Subtract(installDateTime).TotalDays;
                    if (interval < 0 || interval > mConfiguration.MaxRunningDays)
                    {
                        return interval;
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Console.Out.WriteLine(ex.Message);
                return 2;
            }
            return 0;
        }

        private void InitializeTimers()
        {
            mInitializationTimer = new Timer();
            mInitializationTimer.Interval = 20;
            mInitializationTimer.AutoReset = false;
            mInitializationTimer.Enabled = false;
            mInitializationTimer.SynchronizingObject = this;
            mInitializationTimer.Elapsed += new System.Timers.ElapsedEventHandler(mInitializationTimer_Elapsed);

            mTestTimer = new Timer();
            mTestTimer.Interval =1000;
            mTestTimer.AutoReset = false;
            mTestTimer.Enabled = false;
            mTestTimer.SynchronizingObject = this;
            mTestTimer.Elapsed += new System.Timers.ElapsedEventHandler(mTestTimer_Elapsed);           
        }

        private void mInitializationTimer_Elapsed(object sender, EventArgs e)
        {
            mInitializationTimer.Enabled = false;
            //WaitDialogForm waitDialog = new WaitDialogForm(string.Empty, "初始化设备...", new Size(150, 100));

            try
            {
                Application.DoEvents();
                string errorString = string.Empty;
                bool isAllSuccess = true;
               // waitDialog.Caption = "检查系统配置...";
                try
                {
                    if (mConfiguration.CheckSystem)
                    {
                        int returnValue = CheckSystem();
                        //returnValue = 0;
                        Application.DoEvents();
                        Thread.Sleep(1000);
                        if (returnValue != 0)
                        {
                            MessageBox.Show("致命错误，系统无法启动! " + returnValue.ToString());
                            Application.Exit();
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString() + "\r\n 致命错误，系统无法启动!  0xFF");
                    Application.Exit();
                }
                Application.DoEvents();
               // waitDialog.Caption = "初始化相机...";
                try
                {
                    Application.DoEvents();
                    Thread.Sleep(500);
                }
                catch (System.Exception ex)
                {
                    isAllSuccess = false;
                   // waitDialog.Caption += "失败" + "\r\n" + ex.Message;
                    errorString += "初始化相机失败!" + "\r\n";
                    Application.DoEvents();
                    Thread.Sleep(2000);
                }
                Application.DoEvents();
               // waitDialog.Caption = "初始化采集卡...";
                try
                {
                    Application.DoEvents();
                    Thread.Sleep(500);
                    string returnString = EM9636B.GetInstance().Connect();
                    if (!string.IsNullOrEmpty(returnString))
                    {
                        MessageBox.Show("连接数据采集卡失败！" + returnString);
                        return;
                    }
                    returnString = SetMotorStop();
                    if (!string.IsNullOrEmpty(returnString))
                    {
                        MessageBox.Show("连接数据采集卡失败！" + returnString);
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.Out.WriteLine(ex.Message);
                    isAllSuccess = false;
                  //  waitDialog.Caption += "初始化采集卡失败!" + "\r\n";
                    Application.DoEvents();
                    Thread.Sleep(2000);
                }
                Application.DoEvents();
               // waitDialog.Caption = "读取用户权限...";
                try
                {
                    //mUserConfiguration = UserConfiguration.CreateInstance(false);
                    
                   // waitDialog.Caption += "";
                    Application.DoEvents();
                    Thread.Sleep(500);
                }
                catch (System.Exception ex)
                {
                    isAllSuccess = false;
                 //   waitDialog.Caption += "失败!" + "\r\n" + ex.Message;
                    errorString += "读取用户权限失败！" + "\r\n";
                    Application.DoEvents();
                    Thread.Sleep(500);

                }
                if (isAllSuccess)
                {
                 //   waitDialog.Caption = "启动初始化成功！";
                    Application.DoEvents();
                    Thread.Sleep(500);
                    mTestTimer.Start();
                }
                else
                {
                    //RefreshTestStatus(TestStatus.Error);
                    //btnTestControl.Enabled = false;
                    MessageBox.Show(errorString, "错误");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Application.Exit();
            }
            finally
            {
              //  waitDialog.Close();
            }

        }

        //测试的定时器
        private void mTestTimer_Elapsed(object sender, EventArgs e)
        {
            try
            {
	            mTestTimer.Stop();
                lblMark.Text = DateTime.Now.ToLongTimeString();
       
                if (btnStop.Enabled)
                {
                    lblDetectionTime.Text  = new DateTime(DateTime.Now.Subtract(mStartTime).Ticks).ToLongTimeString();

                }
                //已经开始测量，并且已经计数了
                if (mChipDetection.SummaryCount>0 && !btnStart.Enabled)
                {
                    //计数没有变化
                    if (mPreviousSecondChipCount == mChipDetection.SummaryCount)
                    {
                        //统计没有计数的次数
                        mNoChipFoundCount++;
                        if (mNoChipFoundCount>=Parameter.GetInstance().MotorAutoStopInterval)
                        {
                            btnStop.PerformClick();
                        }
                    }
                    else
                    {
                        mNoChipFoundCount = 0;
                        mPreviousSecondChipCount = mChipDetection.SummaryCount;
                    }
                    
                }
	            mTestTimer.Start();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetMotorRate()
        {
            double rate = (mCurrentMotorRate / 60.0);
            int frequency = (int)(Parameter.GetInstance().PulsesOneCycle * rate);

            EM9636B.GetInstance().SetPWMEnable(false, false, false);

            string returnString = EM9636B.GetInstance().SetPWMParameter(1, frequency, 50);
            if (!string.IsNullOrEmpty(returnString))
            {
                MessageBox.Show(returnString);
            }
            else
            {
                returnString = EM9636B.GetInstance().SetPWMEnable(true, true, true);
            }
        }
        private string SetMotorStart(double motorRate)
        {
            string returnString;
            double rate = (motorRate / 60.0);
            int frequency = (int)(Parameter.GetInstance().PulsesOneCycle * rate);

            returnString = EM9636B.GetInstance().SetIOPinsDirection(EM9636_IO_DIRECTION.OUT, EM9636_IO_SELECTION.IO1_16);
            if (!string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }
            //方向//锁定

            returnString = EM9636B.GetInstance().SetIO1_16((byte)(0x08 | 0x10), 0);
            if (!string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }
            returnString = EM9636B.GetInstance().SetPWMParameter(1, frequency, 50);
            if (string.IsNullOrEmpty(returnString))
            {
                returnString = EM9636B.GetInstance().SetPWMEnable(true, true, true);
                if (string.IsNullOrEmpty(returnString))
                {
                    return string.Empty;
                }
            }
            return returnString;
        }

        private string SetMotorStop()
        {
            string returnString = EM9636B.GetInstance().SetPWMEnable(false, false, false);
            if (!string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }
            //方向,锁定
            byte first, second;
            returnString = EM9636B.GetInstance().GetIO1_16(out first, out second);
            if (!string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }
            returnString = EM9636B.GetInstance().SetIO1_16((byte)(first & (~0x10) & (~0x08)), second);
            if (!string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }
            EM9636B.GetInstance().Disconnect();

            return string.Empty;
        }

        //开始测试
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {

               bool isGetCode = false;
	            FormCode formCode = new FormCode();

                if (formCode.ShowDialog() == DialogResult.OK)
                {
                    isGetCode = true;
                }
                if (!isGetCode && !chkCalibration.Checked)
                {
                    return;
                }
                
                string templateDirectoryName = formCode.GetTemplateDirectoryName();

                if (!templateDirectoryName.Equals("(默认模板)"))
                {
                    string templateFileName = templateDirectoryName + "template.xml";
                    if (File.Exists(templateFileName))
                    {
                        mCurrentChipTemplate = (ChipTemplate)MiscFunction.GetInstance().DeSerializeXMLFile(templateFileName, typeof(ChipTemplate));
                    }
                    else
                    {
                        MessageBox.Show("无法找到模板文件！");
                        return;
                    }
                }

                Graphics g = picImage.CreateGraphics();
                g.Clear(Color.White);
                
                Parameter.GetInstance().DefaultTemplateName = formCode.GetTemplateName();

                ResetTestStatus();


                //加载模版
                mChipDetection.CreateChipTemplate(mCurrentChipTemplate);
                mChipDetection.ResetDectionStatus();
                mChipDetection.ChangeToNAStatusDelayIndex = 0;



                lblCodeValue.Text = formCode.GetCodeString();
                mOperationID = formCode.GetOperationIDString();
                mDeviceName = formCode.GetDeviceNameString();



                if ( !CAM_connect())
                {
                     MessageBox.Show("相机初始化失败！");
                     return;
                }

                CAM_StartGrab();

                if (Parameter.GetInstance().IsSaveExcel && !chkCalibration.Checked)
                {
                    if (!File.Exists(mExcelModalFileName))
                    {
                        MessageBox.Show("无法找到Map模板文件！" + mExcelModalFileName);
                        return;
                    }
                    Application.DoEvents();
                    mAccessExcel = AccessExcel.CreateInstance();
                    mDestinationExcelFile = MiscFunction.GetInstance().GetAssemblyPath() + Parameter.GetInstance().ReportDirectory + "\\" + "temp.xlsx";
                    File.Copy(mExcelModalFileName, mDestinationExcelFile, true);
                    Application.DoEvents();
                    mAccessExcel.OpenExcelFile(mDestinationExcelFile);                 
                }
                if ((!chkCalibration.Checked && Parameter.GetInstance().IsControlMotor))
                {
                    string returnString = EM9636B.GetInstance().Connect();
                    if (!string.IsNullOrEmpty(returnString))
                    {
                        MessageBox.Show("连接数据采集卡失败！" + returnString);
                        return;
                    }
                    returnString = SetMotorStart(mCurrentMotorRate);
                    if (!string.IsNullOrEmpty(returnString))
                    {
                        MessageBox.Show("启动电机失败！" + returnString);
                        return;
                    }
                }
                
                btnStop.Enabled = true;
                btnTest.Enabled = false;
                btnStart.Enabled = false;
                grpMotor.Enabled = false;
                mnuMain.Enabled = false;

                btnStartCalibration.Enabled = false;
                btnSaveCalibration.Enabled = false;
                btnStopCalibration.Enabled = false;
                mIsInCalibrationMode = false;

                lblStatusIndicator.Text = "正在检测";
                lblStatusIndicator.BackColor = Color.Green;
                mStartTime = DateTime.Now;
                //mTriggerTimer.Start();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
	            FormFunctionTest functionTest=new FormFunctionTest();
	            functionTest.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormChipDetection_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (mAccessExcel!=null)
                {
                    mAccessExcel.Close();
                }
                int index = 0;
                do
                {
                    Thread.Sleep(50);
                    index++;
                    if (index>=20)
                    {
                        break;
                    }
                } while (!mTestTimer.Enabled);
                mTestTimer.Stop();

                Parameter.GetInstance().SaveOptionFile();

                CAM_Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        //保存测试结果文件
        private void SaveResultToExcelFile()
        {
           
            mAccessExcel.SaveDetectionResult(  lblCodeValue.Text, mChipDetection.PassCount,mChipDetection.FailureCount,
                                                                    mChipDetection.SummaryCount, mDeviceName, mOperationID);

            mAccessExcel.SaveExcelFile(mDestinationExcelFile);


            string createTime = MiscFunction.GetInstance().TimeToFileFormatString(DateTime.Now, "");
            mAccessExcel.Close();

            Application.DoEvents();
            Thread.Sleep(500);

            FileInfo destinationExcelfileInfo = new FileInfo(mDestinationExcelFile);
            string finalExcelFile = MiscFunction.GetInstance().GetAssemblyPath() + Parameter.GetInstance().ReportDirectory + "\\" + lblCodeValue.Text + ".xlsx";

            if (File.Exists(finalExcelFile))
            {
                FileInfo finalExcelFileInfo = new FileInfo(finalExcelFile);
                string fileName = Path.GetFileNameWithoutExtension(finalExcelFile);
                string newFileName = MiscFunction.GetInstance().GetAssemblyPath() + Parameter.GetInstance().ReportDirectory + "\\" + fileName + "_" + createTime + ".xlsx";
                //File.Copy(finalExcelFile, newFileName, true);
                finalExcelFileInfo.MoveTo(newFileName);
                if (Parameter.GetInstance().IsSaveRemoteFile)
                {
                    string remoteNewFile = @Parameter.GetInstance().RemoteFilePath + fileName + "_" + createTime + ".xlsx";
                    File.Copy(newFileName, remoteNewFile, true);
                }
            }
            destinationExcelfileInfo.MoveTo(finalExcelFile);
            if (Parameter.GetInstance().IsSaveRemoteFile)
            {
                string remoteFile = @Parameter.GetInstance().RemoteFilePath + lblCodeValue.Text + ".xlsx";
                File.Copy(finalExcelFile, remoteFile, true);
            }

        }
        //定制测试
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                //mTriggerTimer.Stop();
                string returnString;
                if ((!chkCalibration.Checked && Parameter.GetInstance().IsControlMotor))
                {
                    returnString = SetMotorStop();
                    if (!string.IsNullOrEmpty(returnString))
                    {
                        MessageBox.Show("停止电机失败！" + returnString);
                    }
                }

                //returnString = mHIKCamera.StopGrabbing();
                //if (!string.IsNullOrEmpty(returnString))
                //{
                //    MessageBox.Show("停止图像获取功能失败！" + returnString);
                //}
                CAM_StopGrab();


                if (Parameter.GetInstance().IsSaveExcel && !chkCalibration.Checked)
                {
                    if (mChipDetection.SummaryCount > 0)
                    {
                        SaveResultToExcelFile();
                    }
                    else
                    {
                        mAccessExcel.Close();
                    }
                    
                }

                btnStart.Enabled = true;
                btnTest.Enabled = true;
                btnStop.Enabled = false;
                grpMotor.Enabled = true;
                mnuMain.Enabled = true;

                btnStartCalibration.Enabled = true;
                btnSaveCalibration.Enabled = false;
                btnStopCalibration.Enabled = false;

                lblStatusIndicator.Text = "停止运行";
                lblStatusIndicator.BackColor = Color.Red;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

      

        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Excel测试
                mAccessExcel = AccessExcel.CreateInstance();
                //string excelFile = mMiscFunction.GetAssemblyPath() + Parameter.GetInstance().ExcelFileName + ".xlsx";
                mDestinationExcelFile = MiscFunction.GetInstance().GetAssemblyPath() + Parameter.GetInstance().ReportDirectory + "\\" + "temp3.xlsx";
                File.Copy(mExcelModalFileName, mDestinationExcelFile, true);
                Application.DoEvents();
                string ret = mAccessExcel.OpenExcelFile(mDestinationExcelFile);
                for (int i = 0; i < 500; i++)
                {
                    mAccessExcel.AddChipStatus(Parameter.GetInstance().PassLabel, i + 1, true);
                }
                SaveResultToExcelFile();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //绘制图像以及结果
        private void PicImage_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color color;

            

            if (mIsInCalibrationMode)
            {
                //显示标定的图像
                using (Pen pen1 = new Pen(Color.Yellow, 2))
                {
                    pen1.DashStyle = DashStyle.Dash;
                    g.DrawString("特征", new Font("Microsoft YaHei", 10), Brushes.Yellow, mCurrentChipTemplate.FeatureDisplayRectangle.Left-4, mCurrentChipTemplate.FeatureDisplayRectangle.Top-20);
                    g.DrawRectangle(pen1, mCurrentChipTemplate.FeatureDisplayRectangle);
                    pen1.DashStyle = DashStyle.Solid;
                    g.DrawRectangle(pen1, mCurrentChipTemplate.FeatureCaptureDisplayRectangle);

                }
                using (Pen pen2 = new Pen(Color.White, 2))
                {
                    pen2.DashStyle = DashStyle.Dash;
                    g.DrawString("对象", new Font("Microsoft YaHei", 10), Brushes.White, mCurrentChipTemplate.ObjectDisplayRectangle.Left - 4, mCurrentChipTemplate.ObjectDisplayRectangle.Top - 20);
                    g.DrawRectangle(pen2, mCurrentChipTemplate.ObjectDisplayRectangle);
                    pen2.DashStyle = DashStyle.Solid;
                    g.DrawRectangle(pen2, mCurrentChipTemplate.ObjectCaptureDisplayRectangle);
                }
            }
            else if( btnStart.Enabled == false )
            {
                //在测试的时候显示结果
                if (mChipDetection.DetectionStatusValue == DetectionStatus.Pass)
                {
                    color = Color.Lime;
                }
                else if (mChipDetection.DetectionStatusValue == DetectionStatus.Failure)
                {
                    color = Color.Red;
                    if (Parameter.GetInstance().IsSavePhoto)
                    {
                        SaveCurrentImage();
                    }
                }
                else
                {
                    color = Color.Yellow;

                }
                using (Pen pen = new Pen(color, 2))
                {
                    g.DrawRectangle(pen, (int)(mChipDetection.ChipOutLeft / Parameter.GetInstance().WidthRatio), (int)(mChipDetection.ChipOutTop / Parameter.GetInstance().HeightRatio), (int)(mChipDetection.ChipOutWidth / Parameter.GetInstance().WidthRatio), (int)(mChipDetection.ChipOutHeight / Parameter.GetInstance().HeightRatio));
                    g.DrawRectangle(pen, mCurrentChipTemplate.FeatureCaptureDisplayRectangle);
                }
                using (Pen pen1 = new Pen(color, 2))
                { 
                    pen1.DashStyle = DashStyle.Dash;
                    g.DrawRectangle(pen1, mCurrentChipTemplate.FeatureDisplayRectangle);
                    g.DrawRectangle(pen1, mCurrentChipTemplate.ObjectDisplayRectangle);
                }              
            }


        }
        //监测结果
        private void ChipDetection_RefreshDisplay()
        {
            //调试输出
            if (Parameter.GetInstance().IsShowDebug)
            {
                lblDebug.Text = "Frame" + "\r\n";
                lblDebug.Text += "L:" + mChipDetection.FrameOutLeft + "      " + "R:" + mChipDetection.FrameOutTop + "\r\n";
                lblDebug.Text += "W:" + mChipDetection.FrameOutWidth + "      " + "H:" + mChipDetection.FrameOutHeight + "\r\n";
                lblDebug.Text += "T:" + mChipDetection.ProcessTime.ToString("##.###") + "\r\n";
            }
            if (mChipDetection.DetectionStatusValue == DetectionStatus.NA)
            {
                if (Parameter.GetInstance().IsShowDebug)
                {
                    lblDebug.Text += "Chip N/A";
                }
                
            }
            else
            {
                //调试输出
                if (Parameter.GetInstance().IsShowDebug)
                {
                    lblDebug.Text += "Chip" + "\r\n";
                    lblDebug.Text += "L:" + mChipDetection.ChipOutLeft + "      " + "R:" + mChipDetection.ChipOutTop + "\r\n";
                    lblDebug.Text += "W:" + mChipDetection.ChipOutWidth + "      " + "H:" + mChipDetection.ChipOutHeight;
                }
                
                if (mChipDetection.SummaryCount>mCurrentDetectionCount)
                {
                    /////////////////////////////////////////保存到文件
                    mCurrentDetectionCount = mChipDetection.SummaryCount;
                    if (mChipDetection.DetectionStatusValue == DetectionStatus.Pass)
                    {
                       
                        if (Parameter.GetInstance().IsSaveExcel && !chkCalibration.Checked)
                        {
                            mAccessExcel.AddChipStatus(Parameter.GetInstance().PassLabel, mCurrentDetectionCount, true);
                        }

                    }
                    else
                    {
                        
                        if (Parameter.GetInstance().IsSaveExcel && !chkCalibration.Checked)
                        {
                            mAccessExcel.AddChipStatus(Parameter.GetInstance().FailLabel, mCurrentDetectionCount, false);
                        }
                    }
                    //////////////////////////////////////////////////////////////////

                    ///////////////调整速度///////////////////////////////////////////////
                    if (mPreviousTestTime.Equals(DateTime.MinValue))
                    {
                        mPreviousTestCount = mChipDetection.SummaryCount;
                        mPreviousTestTime = DateTime.Now;
                    }
                    else
                    {
                        double testIntervalSeconds = DateTime.Now.Subtract(mPreviousTestTime).TotalSeconds;
                        if (testIntervalSeconds >= Parameter.GetInstance().MotorRateTestInterval)
                        {
                            double length = Parameter.GetInstance().ChipLength*(mChipDetection.SummaryCount - mPreviousTestCount)/100;
                            mTestLineRate = length/testIntervalSeconds;
                            mPreviousTestCount = mChipDetection.SummaryCount;
                            mPreviousTestTime = DateTime.Now;
                            if (mTestLineRate >= Parameter.GetInstance().MotorRateThreshold)
                            {
                                mCurrentMotorRate -= Parameter.GetInstance().MotorRateDecreaseValue;
                                SetMotorRate();
                            }
                        }
                    }
                    ///////////////////////////////////////////////////////////////////////////////////////////////////

                    ////////////////////////////////////显示统计结果//////////////////////////
                    lblPassCount.Text = mChipDetection.PassCount.ToString();
                    lblFailureCount.Text = mChipDetection.FailureCount.ToString();
                    lblSummary.Text = mChipDetection.SummaryCount.ToString();
                    lblCalibrationStatus.Text = mTestLineRate.ToString("0.000") + "\r\n" + mCurrentMotorRate.ToString("0.00");
                    picImage.Refresh();
                }
            }
        }

        private void SaveCurrentImage()
        {
            Bitmap bmp = (Bitmap)picImage.Image;
            string fileName = mChipDetection.SummaryCount + "_" + mChipDetection.FailureCount + "_" + MiscFunction.GetInstance().TimeToFileFormatString(DateTime.Now, "_") + ".jpg";
            bmp.Save(MiscFunction.GetInstance().GetScreenShotPath() + fileName, System.Drawing.Imaging.ImageFormat.Bmp);
        }

      

        private void chkCalibration_Click(object sender, EventArgs e)
        {
            grpParameter.Enabled = chkCalibration.Checked;
        }
        //启动标定
        private void btnStartCalibration_Click(object sender, EventArgs e)
        {
            try
            {
	            lblCodeValue.Text = "标定模式";
	     

                CAM_StartGrab();

                mIsInCalibrationMode = true;
	            btnStop.Enabled = false;
	            btnTest.Enabled = false;
	            btnStart.Enabled = false;
                grpMotor.Enabled = false;
                mnuMain.Enabled = false;
	            btnStartCalibration.Enabled = false;
	            btnSaveCalibration.Enabled = true;
	            btnStopCalibration.Enabled = true;	
	            lblStatusIndicator.Text = "正在标定";
	            lblStatusIndicator.BackColor = Color.Green;

	           Bitmap modelPic = new Bitmap(mCurrentChipTemplate.PhotoFile);
	           picImage.Image = modelPic;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnStopCalibration_Click(object sender, EventArgs e)
        {

            CAM_StopGrab();

            btnStart.Enabled = true;
            btnTest.Enabled = true;
            btnStop.Enabled = false;
            btnStartCalibration.Enabled = true;
            btnSaveCalibration.Enabled = false;
            btnStopCalibration.Enabled = false;
            mIsInCalibrationMode = false;
            grpMotor.Enabled = true;
            mnuMain.Enabled = true;
            lblStatusIndicator.Text = "停止运行";
            lblStatusIndicator.BackColor = Color.Red;
        }

        private void btnSaveCalibration_Click(object sender, EventArgs e)
        {
            try
            {
	            FormNewTemplate newTemplate=new FormNewTemplate();
	            if (newTemplate.ShowDialog()==DialogResult.OK)
	            {
                    string directoryName = MiscFunction.GetInstance().GetAssemblyPath() + Parameter.GetInstance().TemplateDirectory + "\\" + newTemplate.TemplateName + "\\";
	                if (Directory.Exists(directoryName))
	                {
	                    DialogResult result=MessageBox.Show("模板已存在，是否要继续创建并覆盖当前模板目录？","提示", MessageBoxButtons.YesNo);
	                    if (result == DialogResult.Yes)
	                    {
	                    }
	                    else
	                    {
	                        return;
	                    }
	
	                }
	                else
	                {
                        Directory.CreateDirectory(directoryName);
	                }
	                
	                mCurrentChipTemplate.PhotoFile = directoryName + "Template.bmp";
                    string templateFile= directoryName + "Template.xml";
	                Bitmap bmp = (Bitmap)picImage.Image;
                    //保存模版文件和xml
                    bmp.Save(mCurrentChipTemplate.PhotoFile, System.Drawing.Imaging.ImageFormat.Bmp);
                    MiscFunction.GetInstance().SerializeXMLFile(templateFile, FileMode.Create, mCurrentChipTemplate, typeof(ChipTemplate));
	            }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void mnuUserLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserLogin(true);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
            }
        }

        private void btnStartMotor_Click(object sender, EventArgs e)
        {
            try
            {
                string returnString = EM9636B.GetInstance().Connect();
                if (!string.IsNullOrEmpty(returnString))
                {
                    MessageBox.Show("连接数据采集卡失败！" + returnString);
                    return;
                }
                returnString = SetMotorStart(Parameter.GetInstance().EmptyReelMotorRate);
                if (!string.IsNullOrEmpty(returnString))
                {
                    MessageBox.Show("启动电机失败！" + returnString);
                    return;
                }
                btnStartMotor.Enabled = false;
                btnStopMotor.Enabled = true;

                btnStop.Enabled = false;
                btnTest.Enabled = false;
                btnStart.Enabled = false;
                mnuMain.Enabled = false;

                btnStartCalibration.Enabled = false;
                btnSaveCalibration.Enabled = false;
                btnStopCalibration.Enabled = false;
                mIsInCalibrationMode = false;

                lblStatusIndicator.Text = "正在卷带";
                lblStatusIndicator.BackColor = Color.Green;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
            }
        }

        private void btnStopMotor_Click(object sender, EventArgs e)
        {
            try
            {
	            string returnString = SetMotorStop();
	            if (!string.IsNullOrEmpty(returnString))
	            {
	                MessageBox.Show("停止电机失败！" + returnString);
	            }
	
	            btnStart.Enabled = true;
	            btnTest.Enabled = true;
	            btnStop.Enabled = false;
	            mnuMain.Enabled = true;
	
	            btnStartCalibration.Enabled = true;
	            btnSaveCalibration.Enabled = false;
	            btnStopCalibration.Enabled = false;
	
	            btnStartMotor.Enabled = true;
	            btnStopMotor.Enabled = false;
	
	            lblStatusIndicator.Text = "停止运行";
	            lblStatusIndicator.BackColor = Color.Red;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
            }
        }

        private void mnuUserManager_Click(object sender, EventArgs e)
        {
            try
            {
                FormUserAuthority formUserAuthority = new FormUserAuthority();
                if (formUserAuthority.ShowDialog() == DialogResult.OK)
                {
                    ;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
            }
        }





        private Rectangle ProcessMouseMove(MouseEventArgs e, Rectangle captureRectangle, ref SizeChangeDirection sizeChangeDirection, ref Point previousMouseDownPoint)
        {
            if (e.Button != MouseButtons.Left)
            {
                sizeChangeDirection = SizeChangeDirection.NA;
                if (e.Location.X >= captureRectangle.Left && e.Location.X <= captureRectangle.Left + 2 && e.Location.Y >= captureRectangle.Top && e.Location.Y <= captureRectangle.Bottom)
                {
                    //this.Cursor = Cursors.SizeWE;
                    sizeChangeDirection |= SizeChangeDirection.LEFT;
                }
                else if (e.Location.X >= captureRectangle.Right - 2 && e.Location.X <= captureRectangle.Right && e.Location.Y >= captureRectangle.Top && e.Location.Y <= captureRectangle.Bottom)
                {
                    //this.Cursor = Cursors.SizeWE;
                    sizeChangeDirection |= SizeChangeDirection.RIGHT;
                }
                if (e.Location.Y >= captureRectangle.Top && e.Location.Y <= captureRectangle.Top + 2 && e.Location.X >= captureRectangle.Left && e.Location.Y <= captureRectangle.Right)
                {
                    sizeChangeDirection |= SizeChangeDirection.TOP;
                }
                else if (e.Location.Y <= captureRectangle.Bottom && e.Location.Y >= captureRectangle.Bottom - 2 && e.Location.X >= captureRectangle.Left && e.Location.Y <= captureRectangle.Right)
                {
                    sizeChangeDirection |= SizeChangeDirection.BOTTOM;
                }

                if (sizeChangeDirection == SizeChangeDirection.NA)
                {
                    if (captureRectangle.Contains(e.Location))
                    {
                        this.Cursor = Cursors.SizeAll;
                        sizeChangeDirection = SizeChangeDirection.ALL;
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        sizeChangeDirection = SizeChangeDirection.NA;
                    }
                }
                else if (sizeChangeDirection == SizeChangeDirection.LEFT || sizeChangeDirection == SizeChangeDirection.RIGHT)
                {
                    this.Cursor = Cursors.SizeWE;
                }
                else if (sizeChangeDirection == SizeChangeDirection.TOP || sizeChangeDirection == SizeChangeDirection.BOTTOM)
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else if (sizeChangeDirection == SizeChangeDirection.TOP_LEFT || sizeChangeDirection == SizeChangeDirection.BOTTOM_RIGHT)
                {
                    this.Cursor = Cursors.SizeNWSE;
                }
                else if (sizeChangeDirection == SizeChangeDirection.TOP_RIGHT || sizeChangeDirection == SizeChangeDirection.BOTTOM_LEFT)
                {
                    this.Cursor = Cursors.SizeNESW;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                lblCalibrationStatus.Text = e.X.ToString() + " " + e.Y.ToString();

                if (sizeChangeDirection == SizeChangeDirection.LEFT)
                {
                    this.Cursor = Cursors.SizeWE;
                    captureRectangle.Width += captureRectangle.X - e.X;
                    captureRectangle.Location = new Point(e.X, captureRectangle.Y);
                    //picImage.Refresh();
                }
                else if (sizeChangeDirection == SizeChangeDirection.RIGHT)
                {
                    this.Cursor = Cursors.SizeWE;
                    captureRectangle.Width = e.X - captureRectangle.X;
                    //picImage.Refresh();
                }
                else if (sizeChangeDirection == SizeChangeDirection.TOP)
                {
                    this.Cursor = Cursors.SizeNS;
                    captureRectangle.Height += captureRectangle.Y - e.Y;
                    captureRectangle.Location = new Point(captureRectangle.X, e.Y);
                    //picImage.Refresh();
                }
                else if (sizeChangeDirection == SizeChangeDirection.BOTTOM)
                {
                    this.Cursor = Cursors.SizeNS;
                    captureRectangle.Height = e.Y - captureRectangle.Y;
                    //picImage.Refresh();
                }
                else if (sizeChangeDirection == SizeChangeDirection.TOP_LEFT)
                {
                    this.Cursor = Cursors.SizeNWSE;
                    captureRectangle.Width += captureRectangle.X - e.X;
                    captureRectangle.Height += captureRectangle.Y - e.Y;
                    captureRectangle.Location = new Point(e.X, e.Y);
                    //picImage.Refresh();
                }
                else if (sizeChangeDirection == SizeChangeDirection.TOP_RIGHT)
                {
                    this.Cursor = Cursors.SizeNESW;
                    captureRectangle.Width = e.X - captureRectangle.X;
                    captureRectangle.Height += captureRectangle.Y - e.Y;
                    captureRectangle.Location = new Point(captureRectangle.X, e.Y);
                    //picImage.Refresh();
                }
                else if (sizeChangeDirection == SizeChangeDirection.BOTTOM_LEFT)
                {
                    this.Cursor = Cursors.SizeNESW;
                    captureRectangle.Width += captureRectangle.X - e.X;
                    captureRectangle.Height = e.Y - captureRectangle.Y;
                    captureRectangle.Location = new Point(e.X, captureRectangle.Y);
                    //picImage.Refresh();
                }
                else if (sizeChangeDirection == SizeChangeDirection.BOTTOM_RIGHT)
                {
                    this.Cursor = Cursors.SizeNWSE;
                    captureRectangle.Width = e.X - captureRectangle.X;
                    captureRectangle.Height = e.Y - captureRectangle.Y;
                    //picImage.Refresh();
                }
                else if (sizeChangeDirection == SizeChangeDirection.ALL)
                {
                    this.Cursor = Cursors.SizeAll;
                    if (previousMouseDownPoint == Point.Empty)
                    {
                        previousMouseDownPoint.X = e.X;
                        previousMouseDownPoint.Y = e.Y;
                    }
                    else
                    {
                        captureRectangle.Offset(e.X - previousMouseDownPoint.X, e.Y - previousMouseDownPoint.Y);
                        previousMouseDownPoint.X = e.X;
                        previousMouseDownPoint.Y = e.Y;
                        //picImage.Refresh();
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
            return captureRectangle;
        }

        private void picImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mIsInCalibrationMode)
            {
                return;
            }
            Rectangle rectangle;
            if (mFeatureSizeDirection == SizeChangeDirection.NA)
            {
                rectangle = mCurrentChipTemplate.ObjectDisplayRectangle;
                mCurrentChipTemplate.ObjectDisplayRectangle = ProcessMouseMove(e, rectangle, ref mObjectSizeDirection, ref mObjectPreviousMouseDownPoint);

                if (mObjectSizeDirection != SizeChangeDirection.NA)
                {
                    mCurrentChipTemplate.ObjectRectangle = mCurrentChipTemplate.GenerateRealRectangle(mCurrentChipTemplate.ObjectDisplayRectangle);
                    mCurrentChipTemplate.GenerateObjectCaptureRectangle();
                }
            }
            if (mObjectSizeDirection == SizeChangeDirection.NA)
            {
                rectangle = mCurrentChipTemplate.FeatureDisplayRectangle;
                mCurrentChipTemplate.FeatureDisplayRectangle = ProcessMouseMove(e, rectangle, ref mFeatureSizeDirection, ref mFeaturePreviousMouseDownPoint);
                if (mFeatureSizeDirection != SizeChangeDirection.NA)
                {
                    mCurrentChipTemplate.FeatureRectangle = mCurrentChipTemplate.GenerateRealRectangle(mCurrentChipTemplate.FeatureDisplayRectangle);
                    mCurrentChipTemplate.GenerateFeatureCaptureRectangle();
                }

            }

            if (this.Cursor != Cursors.Default && (mObjectSizeDirection != SizeChangeDirection.NA || mFeatureSizeDirection != SizeChangeDirection.NA))
            {
                picImage.Refresh();
            }

            lblDebug.Text = string.Empty;
            lblDebug.Text += "L:" + mCurrentChipTemplate.ObjectDisplayRectangle.Left + "      " + "T:" + mCurrentChipTemplate.ObjectDisplayRectangle.Top + "\r\n";
            lblDebug.Text += "W:" + mCurrentChipTemplate.ObjectDisplayRectangle.Width + "      " + "H:" + mCurrentChipTemplate.ObjectDisplayRectangle.Height + "\r\n";
            lblDebug.Text += e.X.ToString() + "      " + e.Y.ToString();
        }

        private void picImage_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void picImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mFeaturePreviousMouseDownPoint = Point.Empty;
                mFeatureSizeDirection = SizeChangeDirection.NA;
                mObjectPreviousMouseDownPoint = Point.Empty;
                mObjectSizeDirection = SizeChangeDirection.NA;
            }

        }
    }
}
