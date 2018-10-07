using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MiscFunctions;

namespace ChipDetection
{
    public enum UserType
    {
        Administrator,
        Operator,
        Manager
    }
    [Serializable]
    public class UserAuthority
    {
        private string mName;
        private string mPassword;
        private UserType mUserType;
        private string mRemark;
        public string Remark
        {
            get { return mRemark; }
            set { mRemark = value; }
        }
        public string Name
        {
            get { return mName; }
        }
        public string Password
        {
            get { return mPassword; }
        }
        public UserType UserType
        {
            get { return mUserType; }
        }

        public void Set(string name, string password, UserType userType)
        {
            mName = name;
            mPassword = password;
            mUserType = userType;
        }
        public UserAuthority(string name,string password,UserType userType)
        {
            mName = name;
            mPassword = password;
            mUserType = userType;
        }
    }

    [Serializable]
    public class UserConfiguration
    {
        private Dictionary<string, UserAuthority> mUser;

        [NonSerialized]
        private static UserConfiguration mSingletonConfiguration = null;
        [NonSerialized]
        public const string mUserConfigurationFileName = "user.dat";
        [NonSerialized]
        private string mConfigurationFilePath;


        public string ConfigurationFilePath
        {
            get { return mConfigurationFilePath; }
            set { mConfigurationFilePath = value; }
        }
        public Dictionary<string, UserAuthority> User
        {
            get { return mUser; }
            set { mUser = value; }
        }
        private UserConfiguration()
        {            
            mConfigurationFilePath = mUserConfigurationFileName;
         }


   
        public static UserConfiguration GetInstance()
        {

            if (null == mSingletonConfiguration)
            {
                mSingletonConfiguration = new UserConfiguration();
            }
            return mSingletonConfiguration;
        }

        public void CreateDefualtUserConfigurationFile()
        {
            UserAuthority administratorUser = new UserAuthority("admin", "111111", UserType.Administrator);
            //UserAuthority managerUser = new UserAuthority("manager", "02468", UserType.Manager);
            UserAuthority operatorUser = new UserAuthority("operator", "123456", UserType.Operator);
            mUser = new Dictionary<string, UserAuthority>();
            mUser.Add(administratorUser.Name,administratorUser);
            //mUser.Add(managerUser.Name, managerUser);
            mUser.Add(operatorUser.Name, operatorUser);
            SaveConfigurationFile();

        }
        public bool LoadUserConfigurationFile()
        {
            try
            {
                mSingletonConfiguration = (UserConfiguration)MiscFunction.GetInstance().DeSerializeBinaryFile(MiscFunction.GetInstance().GetAssemblyPath() + mConfigurationFilePath);
	            mSingletonConfiguration.ConfigurationFilePath = mConfigurationFilePath;
                return true;
            }
            catch (System.Exception ex)
            {
                CreateDefualtUserConfigurationFile();
                System.Console.Out.WriteLine(ex.Message);
                return false;
            }
        }
        public void SaveConfigurationFile()
        {
            MiscFunction.GetInstance().SerializeBinaryFile(mConfigurationFilePath, FileMode.Create, this);
        }
    }
}
