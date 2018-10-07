using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using Microsoft.Win32;
using MiscFunctions;
using Basler.Pylon;
using Timer = System.Timers.Timer;
using System.Diagnostics;


namespace ChipDetection
{
  
    public partial class FormChipDetection : Form
    {
        private UserAuthority mUserAuthority;
        private Configuration mConfiguration;          
        private ChipDetection mChipDetection;
        private Timer mInitializationTimer;
        private DateTime mStartTime;  
        private bool mIsResetConfiguration;
        private Bitmap m_BitMap = null;
        private bool m_grabImages;
        private AutoResetEvent m_grabThreadExited;
        private BackgroundWorker m_grabThread;
        //相机
        private IntPtr BufAddress;
        private IntPtr curBufAddress;
        
        //Basler
        private Camera m_camera = null;
        private String m_content;

        private int nSmallerCof = 20;
        private int nshowImageW = 0;
        private int nshowImageH = 0;



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

            lblMark.BackColor = Color.Transparent;
            Form.CheckForIllegalCrossThreadCalls = false;   
            
            picImage.Paint += new PaintEventHandler(PicImage_Paint);

            InitializeTimers();
            //UserLogin(false);
                              
            m_grabThreadExited = new AutoResetEvent(false);
            //CAM_connect();

            //mCom_ = new SerialPort();
            //mCom_Open();

            ResetTestStatus();
          
            lblStatusIndicator.Text = "初始化完成";
          
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
                    //waitDialog.Caption += "失败" + "\r\n" + ex.Message;
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
                    //string returnString = EM9636B.GetInstance().Connect();
                    //if (!string.IsNullOrEmpty(returnString))
                    //{
                    //    MessageBox.Show("连接数据采集卡失败！" + returnString);
                    //    return;
                    //}
                    //returnString = SetMotorStop();
                    //if (!string.IsNullOrEmpty(returnString))
                    //{
                    //    MessageBox.Show("连接数据采集卡失败！" + returnString);
                    //    return;
                    //}
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


        //拍照并处理音片
        private void Snap_Click(object sender, EventArgs e)
        {
            try
            {
                m_camera.StreamGrabber.Start();
                Thread.Sleep(1000);
                IGrabResult grabResult = m_camera.StreamGrabber.RetrieveResult(500, TimeoutHandling.ThrowException);
                m_content = "./Report/" + textBox1.Text.ToString() + ".txt";
                using (grabResult)
                {
                    // Image grabbed successfully?
                    if (grabResult.GrabSucceeded)
                    {
                        // Access the image data.
                        Console.WriteLine("SizeX: {0}", grabResult.Width);
                        Console.WriteLine("SizeY: {0}", grabResult.Height);
                        byte[] buffer = grabResult.PixelData as byte[];
                        IntPtr ptr = Marshal.AllocHGlobal(buffer.Length);
                        Marshal.Copy(buffer, 0, ptr, buffer.Length);
                        //mChipDetection.myfind(ptr, (UInt32)grabResult.Width, (UInt32)grabResult.Height);
                        mChipDetection.MyProcess(ptr, (UInt32)grabResult.Width, (UInt32)grabResult.Height, m_content);
                        lblDebug.Text = string.Empty;
                        lblDebug.Text = "grab successfully." + "\r\n" ;
                        //lblDebug.Text += i.ToString() + "\r\n";


                        //mChipDetection.savebuffer(ptr, (UInt32)grabResult.Width, (UInt32)grabResult.Height, 1227);

                        // Display the grabbed image.
                        //ImageWindow.DisplayImage(0, grabResult);
                    }
                    else
                    {
                        Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                    }
                }

                m_camera.StreamGrabber.Stop();
                Thread.Sleep(500);

                m_BitMap = ReadImageFile("processed.bmp");
                picImage.Image = m_BitMap;

                //lblDebug.Text += "拍照检测到条形码数量为";
                label6.Text = mChipDetection.CodeNum.ToString();

                lblStatusIndicator.Text = "拍照完成";
                lblStatusIndicator.BackColor = Color.Green;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
                LogHelper.WriteLog(typeof(FormChipDetection), ex);

            }
        }

        public static Bitmap ReadImageFile(string path)
        {
            FileStream fs = File.OpenRead(path); //OpenRead
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] image = new Byte[filelength]; //建立一个字节数组 
            fs.Read(image, 0, filelength); //按字节流读取 
            System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            Bitmap bit = new Bitmap(result);
            return bit;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            String AddItem = textBox2.Text.ToString() + "\r\n";

            m_content = "./Report/" + textBox1.Text.ToString() + ".txt";
            FileStream fs = new FileStream(m_content, FileMode.Append);

            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(AddItem);
            sw.Close();
            fs.Close();

            lblDebug.Text += "添加条码：" + AddItem;

        }

        private void Finish_Click(object sender, EventArgs e)
        {
            try
            {
                CAM_Close();
                lblDebug.Text += "相机已关闭，可关闭程序" + "\r\n";
                lblStatusIndicator.Text = "完成检测";
                lblStatusIndicator.BackColor = Color.Green;

                btnSnap.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogHelper.WriteLog(typeof(FormChipDetection), ex);

            }
        }


        public bool CAM_connect()
        {
            try
            {
                m_camera = new Camera();

                m_camera.Open();

                m_camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(1);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(FormChipDetection), ex);
                MessageBox.Show("相机连接失败!" + ex.Message);
                return false;
            }
            return true;
        }

        private void CAM_StartGrab()
        {
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
            //延迟一下，
            Thread.Sleep(50);

            while (m_grabImages)
            {
                lock (this)
                {
                    
                    
                }
                worker.ReportProgress(0);
                Thread.Sleep(1);
            }
            m_grabThreadExited.Set();
        }

        public string CAM_StopGrab()
        {
            
            try
            {
                if (m_camera.IsConnected)
                if (m_camera.IsOpen)
                {
                    // Stop grabbing.
                    m_camera.StreamGrabber.Stop();

                    // Close the connection to the camera device.
                    m_camera.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to stop camera: " + ex.Message);
                return string.Empty;
            }
           

            return string.Empty;
        }
       
        public void CAM_Close()
        {
            try
            {
                if (m_camera.IsConnected)
                {
                    // Stop grabbing.
                    m_camera.StreamGrabber.Stop();
                }
                if (m_camera.IsOpen)
                {
                    // Close the connection to the camera device.
                    m_camera.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(FormChipDetection), ex);                
                MessageBox.Show("Failed to stop camera: " + ex.Message);
            }

        }

        private void cvProcess(IntPtr dataBufAddress, int width, int height)
        {
            unsafe
            {

                //处理音筒
                //mChipDetection.ProcessImage(dataBufAddress, (UInt32)width, (UInt32)height);
                //处理音片
                //mChipDetection.ProcessYinPian(dataBufAddress, (UInt32)buffer.Width, (UInt32)buffer.Height);
                //图像相加
                //mChipDetection.AddImage(dataBufAddress, curBufAddress, (UInt32)width, (UInt32)height);
                //图像反转
                //mChipDetection.InvertImage(dataBufAddress, (UInt32)width, (UInt32)height);
                //m_Buffers.Save("C:\\Users\\Administrator\\Desktop\\yt_0422_11_invert.bmp", "-format bmp");

            }
        }
  
        private void UpdateUI(object sender, ProgressChangedEventArgs e)
        {

            if (m_BitMap != null)
            {

                System.Drawing.Imaging.BitmapData d = m_BitMap.LockBits(
                new Rectangle(0, 0, m_BitMap.Width, m_BitMap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                unsafe
                {

                    //m_Buffers.GetAddress(out BufAddress);

                    //ChipDetection.getimage(BufAddress, d.Scan0, m_Buffers.Width, m_Buffers.Height, m_BitMap.Width, m_BitMap.Height, d.Stride);

                    //m_BitMap.UnlockBits(d);
                }

            }

            picImage.Image = m_BitMap;

            picImage.Invalidate(); 
            ChipDetection_RefreshDisplay();

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
                //grpCalibration.Enabled = true;
                btnTest.Enabled = true;
            }
            else
            {
                mnuUserManager.Enabled = false;
                //grpCalibration.Enabled = false;
                btnTest.Enabled = false;


            }
            this.Text = "芯片检测(" + mConfiguration.Version + ")---" + "当前用户: " + mUserAuthority.Name
                            + "(" + (mUserAuthority.UserType.Equals(UserType.Administrator) ? "管理员" : "操作员") + ")";
        }

        private void ResetTestStatus()
        {
            //lblCodeValue.Text = "未知";
            
            


            btnStart.Enabled = true;
            btnStop.Enabled = true;
            btnTest.Enabled = true;

            //btnGrab.Enabled = true;
           // btnSaveCalibration.Enabled = true;
           // btnStopGrab.Enabled = true;

            btnSnap.Enabled = true;
           // btnStopMotor.Enabled = true;


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
            catch (Exception ex)
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

        }


        /// <summary>
        /// 窗口刷新核心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicImage_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //if (3 == videoMode) 
            //{   
            //    //模板测试
            //    SolidBrush drawBrush = new SolidBrush(Color.Red);
            //    Font MyFont1 = new Font("宋体", 25, FontStyle.Bold);
            //    //模板阈值信息显示
            //    //g.DrawString(mChipDetection.chipThreVal.ToString(), MyFont1, drawBrush, 10, 10);
            //    //g.DrawString(mChipDetection.chipThreVal.ToString(), MyFont1, drawBrush, 10, 60);
            //}

            //if (1 == videoMode || 3 == videoMode)
            //{
            //    //绘制标定框
            //    mCurrentChipTemplate.Draw(g);
            //}
            //else if(2 == videoMode){
            //    //mChipDetection.Draw(g);
            //}
            
        }
       

        private void SaveCurrentImage()
        {
            //Bitmap bmp = (Bitmap)picImage.Image;
            //string fileName = mChipDetection.SummaryCount + "_" + mChipDetection.FailureCount + "_" + MiscFunction.GetInstance().TimeToFileFormatString(DateTime.Now, "_") + ".jpg";
            //bmp.Save(MiscFunction.GetInstance().GetScreenShotPath() + fileName, System.Drawing.Imaging.ImageFormat.Bmp);
        }


        //private void chkCalibration_Click(object sender, EventArgs e)
        //{
        //    //grpParameter.Enabled = chkCalibration.Checked;
        //}





        //*****下面的5个按钮*****//

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {

                //lbl1Val.Text = mChipDetection.DetectCode().ToString();

                lblStatusIndicator.Text = "正在检测";
                lblStatusIndicator.BackColor = Color.Green;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogHelper.WriteLog(typeof(FormChipDetection), ex);

            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                //mChipDetection.DetectCode(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("音片处理出现异常" + ex.ToString());
                LogHelper.WriteLog(typeof(FormChipDetection), ex);

            }
        }

        private void Mytest1(object sender, EventArgs e)
        {
            try
            {

                CAM_Close();

                //lbl1Val.Text = "camera closed.";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogHelper.WriteLog(typeof(FormChipDetection), ex);

            }
        }

        private void Mytest(object sender, EventArgs e)
        {
            
            IGrabResult grabResult = m_camera.StreamGrabber.RetrieveResult(500, TimeoutHandling.ThrowException);
            
            using (grabResult)
            {
                // Image grabbed successfully?
                if (grabResult.GrabSucceeded)
                {
                    // Access the image data.
                    Console.WriteLine("SizeX: {0}", grabResult.Width);
                    Console.WriteLine("SizeY: {0}", grabResult.Height);
                    byte[] buffer = grabResult.PixelData as byte[];
                    IntPtr ptr = Marshal.AllocHGlobal(buffer.Length);
                    Marshal.Copy(buffer, 0, ptr, buffer.Length);
                    
                    int i = mChipDetection.myfind(ptr, (UInt32)grabResult.Width, (UInt32)grabResult.Height);
                    lblDebug.Text = string.Empty;
                    lblDebug.Text += i.ToString() + "\r\n";


                    //mChipDetection.savebuffer(ptr, (UInt32)grabResult.Width, (UInt32)grabResult.Height);

                    // Display the grabbed image.
                    ImageWindow.DisplayImage(0, grabResult);
                }
                else
                {
                    Console.WriteLine("Error: {0} {1}", grabResult.ErrorCode, grabResult.ErrorDescription);
                }
            }
             

        }

    
        //******以下没什么用******//

        //监测结果
        private void ChipDetection_RefreshDisplay()
        {

            pnlResult.Update();
        }
        //模板特征提取测试
        private void templateTest()
        {

        }
        //保存测试结果文件
        private void SaveResultToExcelFile()
        {


        }
        
        private void mnuUserLogin_Click(object sender, EventArgs e)
        {
            try
            {
                UserLogin(true);
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误");
            }
        }

        private void picImage_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void picImage_MouseMove(object sender, MouseEventArgs e)
        {
            lblDebug.Text = string.Empty;
            lblDebug.Text += e.X.ToString() + "      " + e.Y.ToString();
        }

        private void videorunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CAM_StartGrab();
        }

        private void videostopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CAM_StopGrab();
        }
        
        private void FormChipDetection_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

                //关闭相机
                CAM_Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                LogHelper.WriteLog(typeof(FormChipDetection), ex);
            }

        }
        
        private void FormChipDetection_Load(object sender, EventArgs e)
        {

        }

        

    }

}
