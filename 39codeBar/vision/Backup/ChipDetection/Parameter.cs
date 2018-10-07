using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ChipDetection
{
    [Serializable]
    public class Parameter:ICloneable
    {
        private static Parameter mSingletonParameter = null;
        private static string mAssemblyPath = string.Empty;

        private readonly string OPENFILENAME="option.ini";
        private string mOptionFile;

        private string mDAQIP;
        private int mDAQPort;
        private int mCameraID;
        private int mWaitResponseDelay;
        private int mWaitResponseLoop;
        private int mCheckDataCountDelay;
        private int mRetry;

        private double mReelDiameter ;
        private double mChipLength;
        private int mPulsesOneCycle;
        private double mScanSpeed;
        private int mMotorDirection;
        private double mMotorDefaultRate;

        private int mCaptureTop;
        private int mCaptureLeft;
        private int mCaptureWidth;
        private int mCaptureHeight;
        private int mFrameThreshold;
        private int mChipThreshold;

        private int mWidthPixels;
        private int mHeightPixels;
        private double mWidthRatio;
        private double mHeightRatio;

        private int mFrameWidthHigh;
        private int mFrameWidthLow;
        private int mFrameHeightHigh;
        private int mFrameHeightLow;
        private int mChipRatioHigh;
        private int mChipRatioLow;
        private int mChangeToNAStatusDelayCount;

        private string mExcelFileName;
        private int mStartCellRow;
        private int mStartCellCol;
        private int mRowCells;
        private string mPassLabel;

        private string mFailLabel;
        private int mSpaceInterval;
        private int mSpaceLength;

        private string mReportDirectory;

        private bool mIsControlMotor;
        private bool mIsSaveExcel;
        private bool mIsShowDebug;
        private bool mIsSavePhoto;
        private int mTriggerInterval;

        private ChipTemplate mChipTemplate;
        private string mTemplateDirectory;

        private int mFeatureCaptureLeft;
        private int mFeatureCaptureRight;
        private int mFeatureCaptureTop;
        private int mFeatureCaptureBottom;
        private int mObjectCaptureLeft;
        private int mObjectCaptureRight;
        private int mObjectCaptureTop;
        private int mObjectCaptureBottom;

        private string mDefaultTemplateName;

        private double mMotorRateThreshold;
        private double mMotorRateDecreaseValue;
        private int mMotorRateTestInterval;
        private int mMotorAutoStopInterval;

        private double mEmptyReelMotorRate;

        private string mRemoteFilePath;
        private bool mIsSaveRemoteFile;
        public bool IsSaveRemoteFile
        {
            get { return mIsSaveRemoteFile; }
            set { mIsSaveRemoteFile = value; }
        }
        public string RemoteFilePath
        {
            get { return mRemoteFilePath; }
            set { mRemoteFilePath = value; }
        }
        public double EmptyReelMotorRate
        {
            get { return mEmptyReelMotorRate; }
            set { mEmptyReelMotorRate = value; }
        }
        
        public int MotorAutoStopInterval
        {
            get { return mMotorAutoStopInterval; }
            set { mMotorAutoStopInterval = value; }
        }
        
        public double MotorRateThreshold
        {
            get { return mMotorRateThreshold; }
            set { mMotorRateThreshold = value; }
        }
        public int MotorRateTestInterval
        {
            get { return mMotorRateTestInterval; }
            set { mMotorRateTestInterval = value; }
        }
        public double MotorRateDecreaseValue
        {
            get { return mMotorRateDecreaseValue; }
            set { mMotorRateDecreaseValue = value; }
        }

        public string DefaultTemplateName
        {
            get { return mDefaultTemplateName; }
            set { mDefaultTemplateName = value; }
        }
        public int FeatureCaptureLeft
        {
            get { return mFeatureCaptureLeft; }
            set { mFeatureCaptureLeft = value; }
        }
        public int FeatureCaptureRight
        {
            get { return mFeatureCaptureRight; }
            set { mFeatureCaptureRight = value; }
        }
        public int FeatureCaptureTop
        {
            get { return mFeatureCaptureTop; }
            set { mFeatureCaptureTop = value; }
        }
        public int FeatureCaptureBottom
        {
            get { return mFeatureCaptureBottom; }
            set { mFeatureCaptureBottom = value; }
        }
        public int ObjectCaptureLeft
        {
            get { return mObjectCaptureLeft; }
            set { mObjectCaptureLeft = value; }
        }
        public int ObjectCaptureRight
        {
            get { return mObjectCaptureRight; }
            set { mObjectCaptureRight = value; }
        }
        public int ObjectCaptureTop
        {
            get { return mObjectCaptureTop; }
            set { mObjectCaptureTop = value; }
        }
        public int ObjectCaptureBottom
        {
            get { return mObjectCaptureBottom; }
            set { mObjectCaptureBottom = value; }
        }
        public int TriggerInterval
        {
            get { return mTriggerInterval; }
            set { mTriggerInterval = value; }
        }
        public string TemplateDirectory
        {
            get { return mTemplateDirectory; }
            set { mTemplateDirectory = value; }
        }
        public ChipTemplate ChipTemplate
        {
            get { return mChipTemplate; }
            set { mChipTemplate = value; }
        }

        public bool IsSavePhoto
        {
            get { return mIsSavePhoto; }
            set { mIsSavePhoto = value; }
        }

        public bool IsShowDebug
        {
            get { return mIsShowDebug; }
            set { mIsShowDebug = value; }
        }
        public bool IsControlMotor
        {
            get { return mIsControlMotor; }
            set { mIsControlMotor = value; }
        }
        public bool IsSaveExcel
        {
            get { return mIsSaveExcel; }
            set { mIsSaveExcel = value; }
        }
        public string ReportDirectory
        {
            get { return mReportDirectory; }
        }
        public int SpaceInterval
        {
            get { return mSpaceInterval; }
            set { mSpaceInterval = value; }
        }
        public int SpaceLength
        {
            get { return mSpaceLength; }
            set { mSpaceLength = value; }
        }
        public int StartCellRow
        {
            get { return mStartCellRow; }
        }
        public int StartCellCol
        {
            get { return mStartCellCol; }
        }
        public int RowCells
        {
            get { return mRowCells; }
        }
        public string PassLabel
        {
            get { return mPassLabel; }
        }
        public string FailLabel
        {
            get { return mFailLabel; }
        }
        public string ExcelFileName
        {
            get { return mExcelFileName; }
            set { mExcelFileName = value; }
        }
        
        public int ChangeToNAStatusDelayCount
        {
            get { return mChangeToNAStatusDelayCount; }
            set { mChangeToNAStatusDelayCount = value; }
        }
        public int ChipRatioHigh
        {
            get { return mChipRatioHigh; }
            set { mChipRatioHigh = value; }
        }
        public int ChipRatioLow
        {
            get { return mChipRatioLow; }
            set { mChipRatioLow = value; }
        }

        public int FrameHeightHigh
        {
            get { return mFrameHeightHigh; }
            set { mFrameHeightHigh = value; }
        }
        public int FrameHeightLow
        {
            get { return mFrameHeightLow; }
            set { mFrameHeightLow = value; }
        }
        public int FrameWidthHigh
        {
            get { return mFrameWidthHigh; }
            set { mFrameWidthHigh = value; }
        }
        public int FrameWidthLow
        {
            get { return mFrameWidthLow; }
            set { mFrameWidthLow = value; }
        }
        public int WidthPixels
        {
            get { return mWidthPixels; }
        }
        public int HeightPixels
        {
            get { return mHeightPixels; }
        }
        public double WidthRatio
        {
            get { return mWidthRatio; }
            set { mWidthRatio = value; }
        }
        public double HeightRatio
        {
            get { return mHeightRatio; }
            set { mHeightRatio = value; }
        }

        public int FrameThreshold
        {
            get { return mFrameThreshold; }
            set { mFrameThreshold = value; }
        }
        public int ChipThreshold
        {
            get { return mChipThreshold; }
            set { mChipThreshold = value; }
        }
        public int CaptureTop
        {
            get { return mCaptureTop; }
            set { mCaptureTop = value; }
        }
        public int CaptureLeft
        {
            get { return mCaptureLeft; }
            set { mCaptureLeft = value; }
        }
        public int CaptureWidth
        {
            get { return mCaptureWidth; }
            set { mCaptureWidth = value; }
        }
        public int CaptureHeight
        {
            get { return mCaptureHeight; }
            set { mCaptureHeight = value; }
        }

        public double ReelDiameter
        {
            get { return mReelDiameter; }
            set { mReelDiameter = value; }
        }
        public double ChipLength
        {
            get { return mChipLength; }
            set { mChipLength = value; }
        }
        public int PulsesOneCycle
        {
            get { return mPulsesOneCycle; }
            set { mPulsesOneCycle = value; }
        }
        public double ScanSpeed
        {
            get { return mScanSpeed; }
            set { mScanSpeed = value; }
        }
        public double MotorDefaultRate
        {
            get { return mMotorDefaultRate; }
            set { mMotorDefaultRate = value; }
        }
        public int MotorDirection
        {
            get { return mMotorDirection; }
            set { mMotorDirection = value; }
        }
        public int WaitResponseDelay
        {
            get { return mWaitResponseDelay; }
            set { mWaitResponseDelay = value; }
        }
        public int WaitResponseLoop
        {
            get { return mWaitResponseLoop; }
            set { mWaitResponseLoop = value; }
        }
        public int CheckDataCountDelay
        {
            get { return mCheckDataCountDelay; }
            set { mCheckDataCountDelay = value; }
        }
        public int Retry
        {
            get { return mRetry; }
            set { mRetry = value; }
        }
        public string DAQIP
        {
            get { return mDAQIP; }
            set { mDAQIP = value; }
        }
        public int DAQPort
        {
            get { return mDAQPort; }
            set { mDAQPort = value; }
        }
        public int CameraID
        {
            get { return mCameraID; }
            set { mCameraID = value; }
        }
        public static Parameter GetInstance()
        {
            if (null == mSingletonParameter) 
            {
                mSingletonParameter = new Parameter(); 
            }
            return mSingletonParameter;
        }


        public static String GetAssemblyPath()
        {
            if (mAssemblyPath.Length == 0)
            {
                String _FolderPath = String.Empty;
                String _CodeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                _CodeBase = _CodeBase.Substring(8, _CodeBase.Length - 8);
                String[] arrSection = _CodeBase.Split(new char[] { '/' });
                for (int i = 0; i < arrSection.Length - 1; i++)
                    _FolderPath += arrSection[i] + "\\";
                mAssemblyPath = _FolderPath;
            }
            return mAssemblyPath;

        }

        private Parameter()
        {
            
           
        }

        public void LoadOptionFile()
        {

            mOptionFile = GetAssemblyPath() + OPENFILENAME;
           // LoadOptionFile();
           // int serialPort;
            try
            {
                mDAQIP = AccessIniFile.ReadValue(mOptionFile, "DAQ", "IP");
            }
            catch (System.Exception ex)
            {
                mDAQIP = "192.168.1.126";

                System.Console.Out.WriteLine(ex.Message);
            }

            try
            {
                mDAQPort = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "DAQ", "Port"));
            }
            catch (System.Exception ex)
            {
                mDAQPort = 8000;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mWaitResponseDelay = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "DAQ", "WaitResponseDelay"));
            }
            catch (System.Exception ex)
            {
                mWaitResponseDelay = 500;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mWaitResponseLoop = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "DAQ", "WaitResponseLoop"));
            }
            catch (System.Exception ex)
            {
                mWaitResponseLoop = 25;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mRetry = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "DAQ", "ReTry"));
            }
            catch (System.Exception ex)
            {
                mRetry = 3;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mCheckDataCountDelay = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "DAQ", "CheckDataCountDelay"));
            }
            catch (System.Exception ex)
            {
                mCheckDataCountDelay = 50;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mCameraID = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Camera", "ID"));
            }
            catch (System.Exception ex)
            {
                mCameraID = 0;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mWidthPixels = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Camera", "WidthPixels"));
            }
            catch (System.Exception ex)
            {
                mWidthPixels = 1280;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mHeightPixels = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Camera", "HeightPixels"));
            }
            catch (System.Exception ex)
            {
                mHeightPixels = 960;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mTriggerInterval = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Camera", "TriggerInterval"));
            }
            catch (System.Exception ex)
            {
                mTriggerInterval = 100;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mReelDiameter = Convert.ToDouble(AccessIniFile.ReadValue(mOptionFile, "Reel", "Diameter"));
            }
            catch (System.Exception ex)
            {
                mReelDiameter =100;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mChipLength = Convert.ToDouble(AccessIniFile.ReadValue(mOptionFile, "Reel", "ChipLength"));
            }
            catch (System.Exception ex)
            {
                mChipLength =10;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mScanSpeed = Convert.ToDouble(AccessIniFile.ReadValue(mOptionFile, "Reel", "ScanSpeed"));
            }
            catch (System.Exception ex)
            {
                mScanSpeed = 2;
                System.Console.Out.WriteLine(ex.Message);
            }

            try
            {
                mPulsesOneCycle = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Motor", "PulsesOneCycle"));
            }
            catch (System.Exception ex)
            {
                mPulsesOneCycle = 10000;
                System.Console.Out.WriteLine(ex.Message);
            }

            try
            {
                mMotorDirection = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Motor", "Direction"));
            }
            catch (System.Exception ex)
            {
                mMotorDirection =1;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mMotorDefaultRate = Convert.ToDouble(AccessIniFile.ReadValue(mOptionFile, "Motor", "DefaultRate"));
            }
            catch (System.Exception ex)
            {
                mMotorDefaultRate = 2;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mMotorRateThreshold = Convert.ToDouble(AccessIniFile.ReadValue(mOptionFile, "Motor", "RateThreshold"));
            }
            catch (System.Exception ex)
            {
                mMotorRateThreshold = 0.3;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mMotorRateDecreaseValue = Convert.ToDouble(AccessIniFile.ReadValue(mOptionFile, "Motor", "RateDecreaseValue"));
            }
            catch (System.Exception ex)
            {
                mMotorRateDecreaseValue = 0.1;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mEmptyReelMotorRate = Convert.ToDouble(AccessIniFile.ReadValue(mOptionFile, "Motor", "EmptyReelRate"));
            }
            catch (System.Exception ex)
            {
                mEmptyReelMotorRate =4;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mMotorRateTestInterval = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Motor", "RateTestInterval"));
            }
            catch (System.Exception ex)
            {
                mMotorRateTestInterval =10;
                System.Console.Out.WriteLine(ex.Message);
            }  
            try
            {
                mMotorAutoStopInterval = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Motor", "AutoStopInterval"));
            }
            catch (System.Exception ex)
            {
                mMotorAutoStopInterval = 3;
                System.Console.Out.WriteLine(ex.Message);
            }  
            
            try
            {
                mCaptureTop = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "Top"));
            }
            catch (System.Exception ex)
            {
                mCaptureTop = 0;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mCaptureLeft= Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "Left"));
            }
            catch (System.Exception ex)
            {
                mCaptureLeft = 0;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mCaptureWidth = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "Width"));
            }
            catch (System.Exception ex)
            {
                mCaptureWidth = 0;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mCaptureHeight = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "Height"));
            }
            catch (System.Exception ex)
            {
                mCaptureHeight = 0;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mFrameThreshold = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "FrameThreshold"));
            }
            catch (System.Exception ex)
            {
                mFrameThreshold = 220;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mChipThreshold = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "ChipThreshold"));
            }
            catch (System.Exception ex)
            {
                mChipThreshold = 90;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mFrameWidthHigh = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "FrameWidthHigh"));
            }
            catch (System.Exception ex)
            {
                mFrameWidthHigh = 650;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mFrameWidthLow = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "FrameWidthLow"));
            }
            catch (System.Exception ex)
            {
                mFrameWidthLow = 610;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mFrameHeightHigh = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "FrameHeightHigh"));
            }
            catch (System.Exception ex)
            {
                mFrameHeightHigh = 320;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mFrameHeightLow = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "FrameHeightLow"));
            }
            catch (System.Exception ex)
            {
                mFrameHeightLow = 280;
                System.Console.Out.WriteLine(ex.Message);
            }

            try
            {
                mChipRatioHigh = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "ChipRatioHigh"));
            }
            catch (System.Exception ex)
            {
                mChipRatioHigh = 15;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mChipRatioLow = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "ChipRatioLow"));
            }
            catch (System.Exception ex)
            {
                mChipRatioLow = 5;
                System.Console.Out.WriteLine(ex.Message);
            }

            
            try
            {
                mChangeToNAStatusDelayCount = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Capture", "ChangeToNAStatusDelayCount"));
            }
            catch (System.Exception ex)
            {
                mChangeToNAStatusDelayCount = 2;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mExcelFileName = AccessIniFile.ReadValue(mOptionFile, "Output", "FileName");
            }
            catch (System.Exception ex)
            {
                mExcelFileName = "Data summary-1.xlsx";
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mStartCellRow = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Output", "StartCellRow"));
            }
            catch (System.Exception ex)
            {
                mStartCellRow = 5;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mStartCellCol = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Output", "StartCellCol"));
            }
            catch (System.Exception ex)
            {
                mStartCellCol = 8;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mIsSaveRemoteFile = (AccessIniFile.ReadValue(mOptionFile, "Output", "SaveRemoteFile")=="1");
            }
            catch (System.Exception ex)
            {
                mIsSaveRemoteFile = false;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mRemoteFilePath = AccessIniFile.ReadValue(mOptionFile, "Output", "RemoteFilePath");
            }
            catch (System.Exception ex)
            {
                mIsSaveRemoteFile = false;
                System.Console.Out.WriteLine(ex.Message);
            }

            
            try
            {
                mRowCells = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Output", "RowCells"));
            }
            catch (System.Exception ex)
            {
                mRowCells = 50;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mPassLabel = AccessIniFile.ReadValue(mOptionFile, "Output", "PassLabel");
            }
            catch (System.Exception ex)
            {
                mPassLabel = "1";
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mFailLabel = AccessIniFile.ReadValue(mOptionFile, "Output", "FailLabel");
            }
            catch (System.Exception ex)
            {
                mFailLabel = "0";
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mSpaceInterval = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Output", "SpaceInterval"));
            }
            catch (System.Exception ex)
            {
                mSpaceInterval = 10;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mSpaceLength = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Output", "SpaceLength"));
            }
            catch (System.Exception ex)
            {
                mSpaceLength = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mReportDirectory =(AccessIniFile.ReadValue(mOptionFile, "Output", "ReportDirectory"));
            }
            catch (System.Exception ex)
            {
                mReportDirectory = "Report";
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mIsControlMotor =(AccessIniFile.ReadValue(mOptionFile, "Other", "ControlMotor")=="1");
            }
            catch (System.Exception ex)
            {
                mIsControlMotor = true;
                System.Console.Out.WriteLine(ex.Message);
            }
             try
             {
                 mIsSaveExcel = (AccessIniFile.ReadValue(mOptionFile, "Other", "SaveExcel") == "1");
             }
             catch (System.Exception ex)
             {
                 mIsSaveExcel = false;
                 System.Console.Out.WriteLine(ex.Message);
             }    
            try
             {
                 mIsShowDebug = (AccessIniFile.ReadValue(mOptionFile, "Other", "ShowDebug")=="1");
             }
             catch (System.Exception ex)
             {
                 mIsShowDebug = true;
                 System.Console.Out.WriteLine(ex.Message);
             }    
            try
             {
                 mIsSavePhoto = (AccessIniFile.ReadValue(mOptionFile, "Other", "SavePhoto") == "1");
             }
             catch (System.Exception ex)
             {
                 mIsSavePhoto = false;
                 System.Console.Out.WriteLine(ex.Message);
             }    
            mChipTemplate=new ChipTemplate();
            Rectangle featureRectangle=new Rectangle();
            Rectangle objectRectangle = new Rectangle();
            try
            {
                mDefaultTemplateName = AccessIniFile.ReadValue(mOptionFile, "Template", "DefaultName");
            }
            catch (System.Exception ex)
            {
                mDefaultTemplateName = "(默认模板)";
                System.Console.Out.WriteLine(ex.Message);
            } 
            
            try
            {
                mChipTemplate.PhotoFile = mAssemblyPath + (AccessIniFile.ReadValue(mOptionFile, "Template", "DefaultPhotoFile"));
            }
            catch (System.Exception ex)
            {
                mChipTemplate.PhotoFile = mAssemblyPath + "template.bmp";
                System.Console.Out.WriteLine(ex.Message);
            } 
            try
            {
                featureRectangle.X = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureLeft"));
            }
            catch (System.Exception ex)
            {
                featureRectangle.X =1;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                featureRectangle.Y = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureTop"));
            }
            catch (System.Exception ex)
            {
                featureRectangle.Y = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                featureRectangle.Width = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureWidth"));
            }
            catch (System.Exception ex)
            {
                featureRectangle.Width = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                featureRectangle.Height = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureHeight"));
            }
            catch (System.Exception ex)
            {
                featureRectangle.Height = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            mChipTemplate.FeatureRectangle = featureRectangle;
            try
            {
                mChipTemplate.FeatureGreyDelta = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureGreyDelta"));
            }
            catch (System.Exception ex)
            {
                mChipTemplate.FeatureGreyDelta =30;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mChipTemplate.FeatureSimilarity = Convert.ToDouble(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureSimilarity"));
            }
            catch (System.Exception ex)
            {
                mChipTemplate.FeatureSimilarity = 0.6;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                objectRectangle.X = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectLeft"));
            }
            catch (System.Exception ex)
            {
                objectRectangle.X = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                objectRectangle.Y = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectTop"));
            }
            catch (System.Exception ex)
            {
                objectRectangle.Y = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                objectRectangle.Width = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectWidth"));
            }
            catch (System.Exception ex)
            {
                objectRectangle.Width = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                objectRectangle.Height = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectHeight"));
            }
            catch (System.Exception ex)
            {
                objectRectangle.Height = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            mChipTemplate.ObjectRectangle = objectRectangle;
            try
            {
                mChipTemplate.ObjectGreyDelta = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectGreyDelta"));
            }
            catch (System.Exception ex)
            {
                mChipTemplate.ObjectGreyDelta = 30;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mChipTemplate.ObjectSimilarity = Convert.ToDouble(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectSimilarity"));
            }
            catch (System.Exception ex)
            {
                mChipTemplate.ObjectSimilarity = 0.6;
                System.Console.Out.WriteLine(ex.Message);
            }
            try
            {
                mTemplateDirectory = AccessIniFile.ReadValue(mOptionFile, "Template", "Directory");
            }
            catch (System.Exception ex)
            {
                mTemplateDirectory = string.Empty;
                System.Console.Out.WriteLine(ex.Message);
            }

            Rectangle featureCaptureRectangle = new Rectangle();
            Rectangle objectCaptureRectangle = new Rectangle();
            try
            {
                mFeatureCaptureLeft = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureCaptureLeft"));
            }
            catch (System.Exception ex)
            {
                mFeatureCaptureLeft = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            featureCaptureRectangle.X = mChipTemplate.FeatureRectangle.Left - mFeatureCaptureLeft;

            try
            {
                mFeatureCaptureTop = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureCaptureTop"));
            }
            catch (System.Exception ex)
            {
                mFeatureCaptureTop = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            featureCaptureRectangle.Y = mChipTemplate.FeatureRectangle.Top - mFeatureCaptureTop;

            try
            {
                mFeatureCaptureRight =Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureCaptureRight"));
            }
            catch (System.Exception ex)
            {
                mFeatureCaptureRight = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            featureCaptureRectangle.Width = mChipTemplate.FeatureRectangle.Width + mFeatureCaptureLeft + mFeatureCaptureRight;

            try
            {
                mFeatureCaptureBottom =Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "FeatureCaptureBottom"));
            }
            catch (System.Exception ex)
            {
                mFeatureCaptureBottom= 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            featureCaptureRectangle.Height = mChipTemplate.FeatureRectangle.Height + mFeatureCaptureTop +mFeatureCaptureBottom ;
            mChipTemplate.FeatureCaptureRectangle = featureCaptureRectangle;
            try
            {
                mObjectCaptureLeft = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectCaptureLeft"));
            }
            catch (System.Exception ex)
            {
                mObjectCaptureLeft = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            objectCaptureRectangle.X = mChipTemplate.ObjectRectangle.Left - mObjectCaptureLeft;

            try
            {
                mObjectCaptureTop = Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectCaptureTop"));
            }
            catch (System.Exception ex)
            {
                mObjectCaptureTop= 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            objectCaptureRectangle.Y = mChipTemplate.ObjectRectangle.Top - mObjectCaptureTop;
            try
            {
                mObjectCaptureRight= Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectCaptureRight"));
            }
            catch (System.Exception ex)
            {
                mObjectCaptureRight = 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            objectCaptureRectangle.Width = mChipTemplate.ObjectRectangle.Width + mObjectCaptureLeft +mObjectCaptureRight;
            try
            {
                mObjectCaptureBottom=Convert.ToInt32(AccessIniFile.ReadValue(mOptionFile, "Template", "ObjectCaptureBottom"));
            }
            catch (System.Exception ex)
            {
                mObjectCaptureBottom= 1;
                System.Console.Out.WriteLine(ex.Message);
            }
            objectCaptureRectangle.Height = mChipTemplate.ObjectRectangle.Height + mObjectCaptureTop +mObjectCaptureBottom;
            mChipTemplate.ObjectCaptureRectangle = objectCaptureRectangle;
        }

        public void SaveOptionFile()
        {
            AccessIniFile.WriteValue(mOptionFile, "Capture", "Left", mCaptureLeft.ToString());
            AccessIniFile.WriteValue(mOptionFile, "Capture", "Top", mCaptureTop.ToString());
            AccessIniFile.WriteValue(mOptionFile, "Capture", "Width", mCaptureWidth.ToString());
            AccessIniFile.WriteValue(mOptionFile, "Capture", "Height", mCaptureHeight.ToString());
            AccessIniFile.WriteValue(mOptionFile, "Template", "DefaultName", mDefaultTemplateName);
        }

        public object Clone()
        {
            return this.MemberwiseClone(); //浅复制
        }
    }
}
