using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MiscFunctions;

namespace ChipDetection
{
    [Serializable]
    public class Configuration
    {
        [NonSerialized]
        private static Configuration mSingletonConfiguration = null;
        [NonSerialized]
        public const string mConfigurationFileName = "configuration.dat";
        [NonSerialized]
        //private MiscFunction mMiscFunctions;

        private string mConfigurationFilePath;


        private int mMaxRunningDays;
        private bool mCheckSystem;
        private bool mCheckCamera;
        private bool mCheckDAQ;
        private string mPCName;
        private string mCameraSerialNumber;
        private string mDAQSerialNumber;
        private string mVersion;
        public string Version
        {
            get { return mVersion; }
            set { mVersion = value; }
        }
        public string ConfigurationFilePath
        {
            get { return mConfigurationFilePath; }
            set { mConfigurationFilePath = value; }
        }
        public int MaxRunningDays
        {
            get { return mMaxRunningDays; }
            set { mMaxRunningDays = value; }
        }
        public bool CheckSystem
        {
            get { return mCheckSystem; }
            set { mCheckSystem = value; }
        }
        public bool CheckCamera
        {
            get { return mCheckCamera; }
            set { mCheckCamera = value; }
        }
        public bool CheckDAQ
        {
            get { return mCheckDAQ; }
            set { mCheckDAQ = value; }
        }
        public string PCName
        {
            get { return mPCName; }
            set { mPCName = value; }
        }
        public string CameraSerialNumber
        {
            get { return mCameraSerialNumber; }
            set { mCameraSerialNumber = value; }
        }
        public string DAQSerialNumber
        {
            get { return mDAQSerialNumber; }
            set { mDAQSerialNumber = value; }
        }

        public Configuration(bool isResetConfiguration)
        {
         
            mConfigurationFilePath = MiscFunction.GetInstance().GetAssemblyPath() + mConfigurationFileName;

            if (isResetConfiguration)
            {
                CreateDefualtConfigurationFile();
            }
            LoadOptionFile();
        }

        private void CreateDefualtConfigurationFile()
        {
            mVersion = "V1.1";
            mCheckSystem = false;
            mCheckCamera = false;
            mCheckDAQ = false;
            mCameraSerialNumber = "";
            mDAQSerialNumber = "";
            mPCName = "";
            mMaxRunningDays = 31 * 3;
            SaveConfigurationFile();

        }
 
        private void LoadOptionFile()
        {

            if (!File.Exists(mConfigurationFilePath))
            {
                CreateDefualtConfigurationFile();
            }
            mSingletonConfiguration = (Configuration)MiscFunction.GetInstance().DeSerializeBinaryFile(mConfigurationFilePath);
            mSingletonConfiguration.ConfigurationFilePath = mConfigurationFilePath;
        }

        public void SaveConfigurationFile()
        {

            MiscFunction.GetInstance().SerializeBinaryFile(mConfigurationFilePath, FileMode.Create, this);
        }
        public void SaveConfigurationFile(string fileName)
        {
            MiscFunction.GetInstance().SerializeBinaryFile(fileName, FileMode.Create, this);
        }
    }
}
