using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using MiscFunctions;
using Timer = System.Timers.Timer;

namespace ChipDetection
{

    public partial class FormFunctionTest : Form
    {

     

        private Timer mTimer;
        private byte mDOStatus;
        private byte mDIStatus;
        private Label[] mDILabels;
        private CheckBox[] mDOChecks;

        private byte mIO1_8Status;
        private byte mIO9_16Status;

        private Label[] mDIOLabels;
        private CheckBox[] mDIOChecks;

        private DAQEvent mEvent;
        private byte mDAQIndex;
        private HIKCamera mHIKCamera;

        public enum DAQEvent
        {
            NA,
            SetDO,
            SetDIO,
            SetPWMValue,
            SetMotorStart,
            SetMotorStop,
            SetPWMEnable,
            SetPWMDisable,
        }
        public FormFunctionTest()
        {
            InitializeComponent();
            //Parameter.GetInstance() = Parameter.CreateInstance();
            //mMisFunction = MiscFunction.CreateInstance();
            //mEM9636B = EM9636B.CreateInstance();

           // mHIKCamera = HIKCamera.CreateInstance();

            mEvent = DAQEvent.NA;

            numMotorRate.Value = (decimal)Parameter.GetInstance().MotorDefaultRate;
            numPulseCycle.Value = Parameter.GetInstance().PulsesOneCycle;
            rdbClockWise.Checked = (Parameter.GetInstance().MotorDirection == 1);
            rdbAntiClockWise.Checked = !rdbClockWise.Checked;

            mDILabels = new Label[8];
            mDILabels[0] = lblDI1;
            mDILabels[1] = lblDI2;
            mDILabels[2] = lblDI3;
            mDILabels[3] = lblDI4;
            mDILabels[4] = lblDI5;
            mDILabels[5] = lblDI6;
            mDILabels[6] = lblDI7;
            mDILabels[7] = lblDI8;

            mDIOLabels = new Label[8];
            mDIOLabels[0] = lblDIO1;
            mDIOLabels[1] = lblDIO2;
            mDIOLabels[2] = lblDIO3;
            mDIOLabels[3] = lblDIO4;
            mDIOLabels[4] = lblDIO5;
            mDIOLabels[5] = lblDIO6;
            mDIOLabels[6] = lblDIO7;
            mDIOLabels[7] = lblDIO8;

            mDOChecks = new CheckBox[8];
            mDOChecks[0] = chkDO1;
            mDOChecks[1] = chkDO2;
            mDOChecks[2] = chkDO3;
            mDOChecks[3] = chkDO4;
            mDOChecks[4] = chkDO5;
            mDOChecks[5] = chkDO6;
            mDOChecks[6] = chkDO7;
            mDOChecks[7] = chkDO8;

            mDIOChecks = new CheckBox[8];
            mDIOChecks[0] = chkDIO1;
            mDIOChecks[1] = chkDIO2;
            mDIOChecks[2] = chkDIO3;
            mDIOChecks[3] = chkDIO4;
            mDIOChecks[4] = chkDIO5;
            mDIOChecks[5] = chkDIO6;
            mDIOChecks[6] = chkDIO7;
            mDIOChecks[7] = chkDIO8;

            tabPageDAQ.Text = "数据采集卡-未连接";
            grpPWM.Enabled = false;
            grpOptoDIO.Enabled = false;
            grpDIO.Enabled = false;

            grpMotor.Enabled = false;

            lblDAQCardIP.Text = Parameter.GetInstance().DAQIP + ":" + Parameter.GetInstance().DAQPort.ToString();
            rdbIOIn.CheckedChanged+=new EventHandler(IODirection_Changed);
            rdbIOOut.CheckedChanged += new EventHandler(IODirection_Changed);

        }

        private void RefreshDIOStatus()
        {
            for (int i = 0; i < mDIOChecks.Length; i++)
            {
                mDIOChecks[i].Enabled = rdbIOOut.Checked;
            }
        }

        private void InitializeDODispaly()
        {
            byte index = 0x01;
            byte status = mDOStatus;
            for (int i = 0; i < mDOChecks.Length; i++)
            {
                if ((status & index) == index)
                {
                    mDOChecks[i].Checked = true;
                }
                else
                {
                    mDOChecks[i].Checked = false;
                }
                index = (byte)(index << 1);
                mDOChecks[i].Click += new EventHandler(chkDO_Click);
                mDOChecks[i].Tag = i;
            }
        }

        private void InitializeDIODispaly()
        {
            byte index = 0x01;
            byte status = mIO1_8Status;
            for (int i = 0; i < mDIOChecks.Length; i++)
            {
                if ((status & index) == index)
                {
                    mDIOChecks[i].Checked = true;
                }
                else
                {
                    mDIOChecks[i].Checked = false;
                }
                index = (byte)(index << 1);
                mDIOChecks[i].Click += new EventHandler(chkDIO_Click);
                mDIOChecks[i].Tag = i;
            }
        }

        private void RefreshDIDisplay()
        {
            byte status = mDIStatus;
            byte index = 0x01;

            if (EM9636B.GetInstance().IsConnect)
            {
                EM9636B.GetInstance().GetDIO(out mDOStatus, out mDIStatus);
                status = mDIStatus;

                for (int i = 0; i < 8; i++)
                {
                    if ((status & index) == index)
                    {
                        mDILabels[i].Text = "ON";
                        mDILabels[i].BackColor = Color.Green;
                    }
                    else
                    {
                        mDILabels[i].Text = "OFF";
                        mDILabels[i].BackColor = Color.Red;
                    }
                    index = (byte)(index << 1);
                }
            }
        }

        private void RefreshIODisplay()
        {
            byte status = mDIStatus;
            byte index = 0x01;

            if (EM9636B.GetInstance().IsConnect)
            {
                EM9636B.GetInstance().GetIO1_16(out mIO1_8Status, out mIO9_16Status);
                status = mIO1_8Status;

                for (int i = 0; i < 8; i++)
                {
                    if ((status & index) == index)
                    {
                        mDIOLabels[i].Text = "ON";
                        mDIOLabels[i].BackColor = Color.Green;
                        if (rdbIOOut.Checked)
                        {
                            mDIOChecks[i].Checked = true;
                        }
                    }
                    else
                    {
                        mDIOLabels[i].Text = "OFF";
                        mDIOLabels[i].BackColor = Color.Red;
                        if (rdbIOOut.Checked)
                        {
                            mDIOChecks[i].Checked = false;
                        }
                    }
                    index = (byte)(index << 1);
                }
            }
        }

        private void InitializeTimers()
        {
            mTimer = new Timer();
            mTimer.Interval = 1000;
            mTimer.AutoReset = false;
            mTimer.Enabled = false;
            mTimer.SynchronizingObject = this;
            mTimer.Elapsed += new System.Timers.ElapsedEventHandler(mTimer_Elapsed);
        }

        private void mTimer_Elapsed(object sender, EventArgs e)
        {
            mTimer.Stop();
            try
            {
                if(mEvent == DAQEvent.NA)
                {
                    RefreshDIDisplay();
                    RefreshIODisplay();
                }
                else
                {
                    string returnString=string.Empty;
                    if (mEvent == DAQEvent.SetDO)
                    {
                        returnString=SetDO();
                    }
                    else if (mEvent == DAQEvent.SetDIO)
                    {
                        returnString=SetDIO();
                    }
                    else if (mEvent == DAQEvent.SetPWMEnable)
                    {
                        returnString = EM9636B.GetInstance().SetPWMEnable(true, true, true);
                    }
                    else if (mEvent == DAQEvent.SetPWMDisable)
                    {
                        returnString=SetPWMStop();
                        //mEM9636B.SetPWMEnable(false, false, false);
                    }
                    else if (mEvent == DAQEvent.SetPWMValue)
                    {
                        returnString=SetPWMStart();
                    }
                    else if (mEvent == DAQEvent.SetMotorStart)
                    {
                        returnString=SetMotorStart();
                    }
                    else if (mEvent == DAQEvent.SetMotorStop)
                    {
                        returnString=SetMotorStop();
                    }
                    if (!string.IsNullOrEmpty(returnString))
                    {
                        MessageBox.Show(returnString);
                    }
                    mEvent = DAQEvent.NA;
                }

            }
            catch (System.Exception ex)
            {
                System.Console.Out.WriteLine(ex.Message);
            }
            mTimer.Start();
        }

        private string SetDO()
        {
            byte index = (byte)(1 << mDAQIndex);
            string returnString;
            byte tempValue;
            if (mDOChecks[mDAQIndex].Checked)
            {
                tempValue = (byte)(mDOStatus | index);
            }
            else
            {
                tempValue = (byte)(mDOStatus & (~index));
            }
            returnString = EM9636B.GetInstance().SetDO(tempValue);
            if (!string.IsNullOrEmpty(returnString))
            {
                return (returnString);
            }
            else
            {
                mDOStatus = tempValue;
                return string.Empty;

            }
            //mEM9636B.GetDIO(out mDOStatus, out mDIStatus);
        }

        private string SetDIO()
        {
            byte index = (byte)(1 << mDAQIndex);
            string returnString;
            byte tempValue;
            if (mDIOChecks[mDAQIndex].Checked)
            {
                tempValue = (byte)(mIO1_8Status | index);
            }
            else
            {
                tempValue = (byte)(mIO1_8Status & (~index));
            }
            returnString = EM9636B.GetInstance().SetIO1_16(tempValue, 0);
            if (!string.IsNullOrEmpty(returnString))
            {
                return (returnString);
            }
            else
            {
                mIO1_8Status = tempValue;
                return string.Empty;
            }
        }

        private string SetPWMStart()
        {
            string returnString;
            //returnString = mEM9636B.SetIOPinsDirection(EM9636_IO_DIRECTION.OUT, EM9636_IO_SELECTION.IO1_16);
            //if (!string.IsNullOrEmpty(returnString))
            //{
            //    return returnString;
            //}

            returnString = EM9636B.GetInstance().SetPWMParameter((byte)numPWMChannel.Value, (int)numPWMFrequency.Value, (int)numPWMDuty.Value);
            if (string.IsNullOrEmpty(returnString))
            {
                bool isChannelOne = ((byte)numPWMChannel.Value == 1);
                bool isChannelTwo = ((byte)numPWMChannel.Value == 2);
                bool isChannelThree = ((byte)numPWMChannel.Value == 3);

                //returnString = mEM9636B.SetPWMEnable(isChannelOne, isChannelTwo, isChannelThree);
                returnString = EM9636B.GetInstance().SetPWMEnable(true, true, true);

                if (string.IsNullOrEmpty(returnString))
                {
                    return string.Empty;
                }
            }
           return returnString;
        }

        private string SetPWMStop()
        {
            string returnString = EM9636B.GetInstance().SetPWMEnable(false, false, false);
            if (string.IsNullOrEmpty(returnString))
            {
                return string.Empty;
            }
            return returnString;
        }

        private string SetMotorStart()
        {
            string returnString;
            double rate = (int)numMotorRate.Value/60.0;

            returnString = EM9636B.GetInstance().SetIOPinsDirection(EM9636_IO_DIRECTION.OUT, EM9636_IO_SELECTION.IO1_16);
            if (!string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }
            //方向//锁定
            returnString = EM9636B.GetInstance().SetIO1_16((byte)(mIO1_8Status | 0x08 | 0x10), mIO9_16Status);
            if (!string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }
            
            //returnString = mEM9636B.SetIOPInsOut(0, (byte)(mIO1_8Status | 0x10), mIO9_16Status);
            //if (!string.IsNullOrEmpty(returnString))
            //{
            //    return returnString;
            //}
            int frequency=(int)((int)numPulseCycle.Value*rate);

            returnString = EM9636B.GetInstance().SetPWMParameter(1, frequency, 50);
            if (string.IsNullOrEmpty(returnString))
            {
                bool isChannelOne = ((byte)numPWMChannel.Value == 1);
                bool isChannelTwo = ((byte)numPWMChannel.Value == 2);
                bool isChannelThree = ((byte)numPWMChannel.Value == 3);

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
            string returnString = EM9636B.GetInstance().SetIO1_16((byte)(mIO1_8Status & (~0x10) & (~0x08)), mIO9_16Status);
            if (!string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }

            returnString = EM9636B.GetInstance().SetPWMEnable(false, false, false);
            if (string.IsNullOrEmpty(returnString))
            {
                return returnString;
            }
            return string.Empty;
        }

        private void chkDO_Click(object sender, EventArgs e)
        {
            try
            {
                CheckBox check = (CheckBox) sender;
                mDAQIndex = Convert.ToByte(check.Tag);
                mEvent=DAQEvent.SetDO;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void chkDIO_Click(object sender, EventArgs e)
        {
            try
            {
                CheckBox check = (CheckBox) sender;
                mDAQIndex = Convert.ToByte(check.Tag);
                mEvent = DAQEvent.SetDIO;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void IODirection_Changed(object sender, EventArgs e)
        {
            try
            {
                RefreshDIOStatus();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void btnPWMStart_Click(object sender, EventArgs e)
        {
            try
            {
                mEvent = DAQEvent.SetPWMValue;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnPWMStop_Click(object sender, EventArgs e)
        {
            try
            {
                mEvent = DAQEvent.SetPWMDisable;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {

                string returnString = EM9636B.GetInstance().Connect();
                if (!string.IsNullOrEmpty(returnString))
                {
                    MessageBox.Show(returnString);
                    tabPageDAQ.Text = "数据采集卡-未连接";
                    grpPWM.Enabled = false;
                    grpOptoDIO.Enabled = false;
                    grpDIO.Enabled = false;
                    grpMotor.Enabled = false;
                }
                else
                {
                    tabPageDAQ.Text = "数据采集卡-已连接";
                    grpPWM.Enabled = true;
                    grpOptoDIO.Enabled = true;
                    grpDIO.Enabled = true;
                    grpMotor.Enabled = true;

                    RefreshDIOStatus();

                    RefreshDIDisplay();
                    RefreshIODisplay();

                    InitializeDODispaly();
                    InitializeDIODispaly();

                    InitializeTimers();
                    mTimer.Start();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCameraStart_Click(object sender, EventArgs e)
        {
            try
            {
                string returnString = mHIKCamera.Initialize();
                if (!string.IsNullOrEmpty(returnString) && !returnString.Equals("Exist"))
                {
                    MessageBox.Show("相机初始化失败！" + returnString);
                    return;
                }
                returnString = mHIKCamera.SetTriggerMode();
                if (!string.IsNullOrEmpty(returnString))
                {
                    MessageBox.Show("设置相机触发模式！" + returnString);
                    return;
                }
                
	            returnString = mHIKCamera.StartGrabImage(pictureBoxTest.Handle, pictureBoxTest);
                if (!string.IsNullOrEmpty(returnString))
                {
                    MessageBox.Show("启动图像获取功能失败！" + returnString);
                    return;
                }
                btnCameraStart.Enabled = false;
                btnCameraStop.Enabled = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormFunctionTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
	            if (mHIKCamera != null)
	            {
	                mHIKCamera.Close();
	            }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void btnCameraStop_Click(object sender, EventArgs e)
        {
            try
            {
	            string returnString = mHIKCamera.StopGrabbing();
	            if (!string.IsNullOrEmpty(returnString))
	            {
	                MessageBox.Show("停止图像获取功能失败！" + returnString);
	                return;
	            }
                btnCameraStart.Enabled = true;
                btnCameraStop.Enabled = false;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        private void chkPWM_Click(object sender, EventArgs e)
        {
            if (chkPWM.Checked)
            {
                mEvent = DAQEvent.SetPWMEnable;
            }
            else
            {
                mEvent = DAQEvent.SetPWMDisable;
            }

        }

        private void btnMotorStart_Click(object sender, EventArgs e)
        {
            mEvent = DAQEvent.SetMotorStart;

        }

        private void btnMotorStop_Click(object sender, EventArgs e)
        {
            mEvent = DAQEvent.SetMotorStop;

        }
    }
}
