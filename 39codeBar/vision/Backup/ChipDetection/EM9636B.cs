using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChipDetection
{
    //IO引脚方向
    public enum EM9636_IO_DIRECTION
    {
        IN = 0,   //输入
        OUT = 1,    //输出
    }
    public enum EM9636_IO_SELECTION
    {
        IO1_8 = 0x01,   
        IO9_16 = 0x02,    
        IO1_16 = 0x03,   
    }
     //DA采集范围
    public enum EM9636_DA_RANGE
    {
        N10_10V=0,   //±10V
        N5_5V=1,      //±5V
        S0_10V=2,     //0~10V
        S0_5V=3      //0~5V
    }
   
    //DA输出方式
    public enum EM9636_DA_MODE
    {
        CODE  =0,   //原码值输出
        VALUE=1,    //电压值输出（mV）
    }

    ////IO方向
    //public enum EM9636_IO_DIR
    //{
    //    IN   =0,   //输入
    //    OUT=1,   //输出
    //}

    ////IO是否进FIFO
    //public enum EM9636_IO_FIFO
    //{
    //    NO=0,   //输入
    //   YES=1,   //输出
    //}

    public class EM9636B
    {
        private byte[] mOutBuffer;
        private byte[] mInBuffer;

        private static EM9636B mSingletonEM9636B;
        private Socket mClientSocket;
        //private Parameter Parameter.GetInstance();
        private bool mIsConnect;
        public bool IsConnect
        {
            get { return mIsConnect; }
        }
        private EM9636B()
        {
            //Parameter.GetInstance() = Parameter.CreateInstance();
            mIsConnect = false;
            mOutBuffer = new byte[32];
            mInBuffer = new byte[32];

            mOutBuffer[6] = 1;
        }

        public string Connect()
        {
            try
            {
                //IPAddress IP = IPAddress.Parse(Parameter.GetInstance().DAQIP);
                //IPEndPoint hostEP = new IPEndPoint(IP, Parameter.GetInstance().DAQPort);
                //mClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                ////尝试连接
                //mClientSocket.Connect(hostEP);
                mIsConnect = true;
                return string.Empty;
            }
            catch (SocketException se)
            {
                mClientSocket = null;
                mIsConnect = false;
               return ("连接错误:" + se.Message);
            }
        }

        public string SetIOPinsDirection(EM9636_IO_DIRECTION direction,EM9636_IO_SELECTION selection)
        {
            if (!mIsConnect || mClientSocket == null)
            {
                return "EM9636B未连接";
            }
            mOutBuffer[5] = 0x09;
            mOutBuffer[7] = 0x10;
            mOutBuffer[8] = 0x00;
            mOutBuffer[9] = 0x22;
            mOutBuffer[10] = 0x00;
            mOutBuffer[11] = 0x01;
            mOutBuffer[12] = 0x02;
            mOutBuffer[13] = 0x00;
            byte bytSelection = (byte) selection;
            mOutBuffer[14] =(byte)((direction==EM9636_IO_DIRECTION.OUT)?bytSelection:(~bytSelection)& 0x03);

            int inBufferLen;
            string returnString = AccessDAQCard(15, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen == 12 && mInBuffer[5] == 6)
                {
                    for (int i = 6; i < 6 + 6; i++)
                    {
                        if (mInBuffer[i] != mOutBuffer[i])
                        {
                            return "采集卡应答数据错误！";
                        }
                    }
                    return string.Empty;
                }
                return "采集卡应答数据错误！";
            }
            return returnString;
        }

        public string SetIOPInsOut(byte doPins, byte dioPins1_8, byte dioPins9_16)
        {
            if (!mIsConnect || mClientSocket == null)
            {
                return "EM9636B未连接";
            }
            mOutBuffer[5] = 0x0B;
            mOutBuffer[7] = 0x10;
            mOutBuffer[8] = 0x00;
            mOutBuffer[9] = 0x24;
            mOutBuffer[10] = 0x00;
            mOutBuffer[11] = 0x02;
            mOutBuffer[12] = 0x04;
            mOutBuffer[13] = doPins;
            mOutBuffer[15] = 0x00;
            mOutBuffer[14] = dioPins1_8;
            mOutBuffer[15] = dioPins9_16;

            int inBufferLen;
            string returnString = AccessDAQCard(15, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen == 12 && mInBuffer[5] == 6)
                {
                    for (int i = 6; i < 6 + 6; i++)
                    {
                        if (mInBuffer[i] != mOutBuffer[i])
                        {
                            return "采集卡应答数据错误！";
                        }
                    }
                    return string.Empty;
                }
                return "采集卡应答数据错误！";
            }
            return returnString;
        }

        //'PWM 输出频率 = 16MHz / 分频系数
        //'pwm 占空比 = 占空比系数 / 分频系数
        //'举例： 设置 PWM1 输出频率为 10K，占空比为 50%，则分频系数=1600，占空比系数=800
        //'字节序号（10 进制）： 00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18
        //'请求（16 进制）： 00 00 00 00 00 0d 01 10 00 c2 00 03 06 00 00 06 40 03 20
        public string SetPWMParameter(byte channel,long frequency,int duty)
        {
            int frequencyFactor = (int)(16*1E6/frequency);
            int dutyFactor =(int)(frequencyFactor * (duty/100.0));

            if (!mIsConnect || mClientSocket == null)
            {
                return "EM9636B未连接";
            }
            mOutBuffer[5] = 0x0D;
            mOutBuffer[7] = 0x10;
            mOutBuffer[8] = 0x00;
            mOutBuffer[9] = 0xC2;
            mOutBuffer[10] = 0x00;
            mOutBuffer[11] = 0x03;
            mOutBuffer[12] = 0x06;
            mOutBuffer[13] = 0x00;
            mOutBuffer[14] = 0x00;
            mOutBuffer[15] = (byte)((frequencyFactor >> 8));
            mOutBuffer[16] = (byte)frequencyFactor;
            mOutBuffer[17] = (byte)((dutyFactor >> 8));
            mOutBuffer[18] = (byte)dutyFactor;

            int inBufferLen;
            string returnString = AccessDAQCard(19, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen == 12 && mInBuffer[5] == 6)
                {
                    return string.Empty;
                }
                return "采集卡应答数据错误！";
            }
            return returnString;
        }

        public string SetPWMEnable(bool channelOne,bool channelTwo,bool channelThree)
        {
            byte status = (byte)(channelOne?1:0);
            status |= (byte)((channelTwo ? 1 : 0)<<1);
            status |= (byte)((channelThree ? 1 : 0) << 2);
            if (!mIsConnect || mClientSocket == null)
            {
                return "EM9636B未连接";
            }
            mOutBuffer[5] = 0x09;
            mOutBuffer[7] = 0x10;
            mOutBuffer[8] = 0x00;
            mOutBuffer[9] = 0xC5;
            mOutBuffer[10] = 0x00;
            mOutBuffer[11] = 0x01;
            mOutBuffer[12] = 0x02;
            mOutBuffer[13] = 0x00;
            mOutBuffer[14] = status;
            int inBufferLen;
            string returnString = AccessDAQCard(15, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen == mInBuffer[5] +6 && mInBuffer[5] == 6)
                {
                    for (int i = 6; i < 6 + 6; i++)
                    {
                        if (mInBuffer[i] != mOutBuffer[i])
                        {
                            return "采集卡应答数据错误！";
                        }
                    }
                    return string.Empty;
                }
                return "采集卡应答数据错误！";
            }
            return returnString;
        }

        public string GetDIO(out byte statusDI,out byte statusDO)
        {
            statusDI = 0;
            statusDO= 0;
            if (!mIsConnect || mClientSocket == null)
            {
                return "EM9636B未连接";
            }
            mOutBuffer[5] = 0x06;
            mOutBuffer[7] = 0x03;
            mOutBuffer[8] = 0x01;
            mOutBuffer[9] = 0x2D;
            mOutBuffer[10] = 0x00;
            mOutBuffer[11] = 0x01;

            int inBufferLen;
            string returnString = AccessDAQCard(12, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen == 11 && mInBuffer[5] == 5)
                {
                    statusDI = mInBuffer[9];
                    statusDO = mInBuffer[10];
                    return string.Empty;
                }
                return "采集卡应答数据错误！";
            }
            return returnString;
        }

        public string GetIO1_16(out byte statusIO1_8, out byte statusIO9_16)
        {
            statusIO1_8 = 0;
            statusIO9_16 = 0;
            if (!mIsConnect || mClientSocket == null)
            {
                return "EM9636B未连接";
            }
            mOutBuffer[5] = 0x06;
            mOutBuffer[7] = 0x03;
            mOutBuffer[8] = 0x01;
            mOutBuffer[9] = 0x2C;
            mOutBuffer[10] = 0x00;
            mOutBuffer[11] = 0x01;


            int inBufferLen;
            string returnString = AccessDAQCard(12, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen == 11 && mInBuffer[5] == 5)
                {
                    statusIO1_8 = mInBuffer[10];
                    statusIO9_16 = mInBuffer[9];
                    return string.Empty;
                }
                return "采集卡应答数据错误！";
            }
            return returnString;
        }
        public string SetIO1_16(byte statusIO1_8,byte statusIO9_16)
        {
            if (!mIsConnect || mClientSocket == null)
            {
                return "EM9636B未连接";
            }

            mOutBuffer[5] = 0x06;
            mOutBuffer[7] = 0x06;
            mOutBuffer[8] = 0x00;
            mOutBuffer[9] = 0x25;
            mOutBuffer[10] = statusIO9_16;
            mOutBuffer[11] = statusIO1_8;
            int inBufferLen;
            string returnString = AccessDAQCard(12, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen == 12 && mInBuffer[5] == 6)
                {
                    for (int i = 6; i < 6 + 6; i++)
                    {
                        if (mInBuffer[i] != mOutBuffer[i])
                        {
                            return "采集卡应答数据错误！";
                        }
                    }
                    return string.Empty;
                }
                return "采集卡应答数据错误！";
            }
            return returnString;
        }

        public string SetDO(byte status)
        {
            if (!mIsConnect || mClientSocket == null)
            {
                return "EM9636B未连接";
            }
            //mOutBuffer[5] = 0x09;
            //mOutBuffer[7] = 0x06;
            //mOutBuffer[8] = 0x00;
            //mOutBuffer[9] = 0x24;
            //mOutBuffer[10] = 0x00;
            //mOutBuffer[11] = 0x01;
            //mOutBuffer[12] = 0x02;
            //mOutBuffer[13] = status;
            //mOutBuffer[14] = 0x00;

            mOutBuffer[5] = 0x06;
            mOutBuffer[7] = 0x06;
            mOutBuffer[8] = 0x00;
            mOutBuffer[9] = 0x24;
            mOutBuffer[10] = status;
            mOutBuffer[11] = 0x00;
            int inBufferLen;
            string returnString = AccessDAQCard(15, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen ==12 && mInBuffer[5] ==6)
                {
                    for (int i = 6; i < 6+6; i++)
                    {
                        if (mInBuffer[i] != mOutBuffer[i])
                        {
                            return "采集卡应答数据错误！";
                        }
                    }
                    return string.Empty;
                }
                return "采集卡应答数据错误！";
            }
            return returnString;
        }
        public string SetDAMode(EM9636_DA_MODE mode,EM9636_DA_RANGE range)
        {
            if (!mIsConnect || mClientSocket==null)
            {
                return "EM9636B未连接";
            }
            mOutBuffer[5] = 0x09;
            mOutBuffer[7] = 0x10;
            mOutBuffer[8] = 0x00;
            mOutBuffer[9] = 0x3A;
            mOutBuffer[10] = 0x00;
            mOutBuffer[11] = 0x01;
            mOutBuffer[12] = 0x02;
            mOutBuffer[13] = (byte)mode;
            mOutBuffer[14] = (byte)range;
            int inBufferLen;
            string returnString = AccessDAQCard(15, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen==12 && mInBuffer[5]==6)
                {
                    for (int i = 6; i < 6 + 6; i++)
                    {
                        if (mInBuffer[i] != mOutBuffer[i])
                        {
                            return "采集卡应答数据错误！";
                        }
                    }
                    return string.Empty;
                }
                return "采集卡应答数据错误！";
            }
            return returnString;

        }

        public string SetDAValue(int value)
        {
            if (!mIsConnect || mClientSocket == null)
            {
                return "EM9636B未连接";
            }
            mOutBuffer[5] = 0x09;
            mOutBuffer[7] = 0x10;
            mOutBuffer[8] = 0x00;
            mOutBuffer[9] = 0x1A;
            mOutBuffer[10] = 0x00;
            mOutBuffer[11] = 0x01;
            mOutBuffer[12] = 0x02;
            mOutBuffer[13] = (byte)((value>>8) & 0x0F);
            mOutBuffer[14] = (byte)value;
            int inBufferLen;
            string returnString = AccessDAQCard(15, out inBufferLen);
            if (string.IsNullOrEmpty(returnString))
            {
                if (inBufferLen == 12 && mInBuffer[5] == 6)
                {
                    for (int i = 6; i < 6 + 6; i++)
                    {
                        if (mInBuffer[i] != mOutBuffer[i])
                        {
                            return "采集卡应答数据错误！";
                        }
                    }
                }
                return "采集卡应答数据错误！";
            }
            return returnString;

        }

        private string AccessDAQCard(int outBufferLen,out int inBufferLen)
        {
            inBufferLen = 0;
            try
            {
                mClientSocket.Send(mOutBuffer, outBufferLen, SocketFlags.None);
            }
            catch (Exception ex)
            {
               return "发送命令错误:" + ex.Message;
            }
            for (int i = 0; i < Parameter.GetInstance().Retry; i++)
            {
                int loopDelay = Parameter.GetInstance().WaitResponseDelay / Parameter.GetInstance().WaitResponseLoop;
                for (int j = 0; j < Parameter.GetInstance().WaitResponseLoop; j++)
                {
                    int currentBytesToRead = mClientSocket.Available;
                    if (currentBytesToRead > 0)
                    {
                        do
                        {
                            currentBytesToRead = mClientSocket.Available;
                            Thread.Sleep(Parameter.GetInstance().CheckDataCountDelay);
                        }
                        while (currentBytesToRead != mClientSocket.Available);
                        mClientSocket.Receive(mInBuffer, currentBytesToRead, SocketFlags.None);
                        inBufferLen = currentBytesToRead;
                        return string.Empty;
                    }
                    Thread.Sleep(loopDelay);
                }
            }
            return "数据采集卡无应答";
        }
        public void Disconnect()
        {
            if (mIsConnect)
            {
                //禁用Socket
                mClientSocket.Shutdown(SocketShutdown.Both);
                //关闭Socket 
                mClientSocket.Close();
            }
        }

        public static EM9636B GetInstance()
        {
            if (null == mSingletonEM9636B)
            {
                mSingletonEM9636B = new EM9636B();
            }
            return mSingletonEM9636B;
        }
    }
}
