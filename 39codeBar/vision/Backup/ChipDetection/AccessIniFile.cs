using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ChipDetection
{
    /// <summary>
    /// IniFile class implements IDeployFile interface
    /// access ini file by API functions
    /// </summary>
    public class AccessIniFile
    {
        #region Read Ini File API Declaration

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);

        #endregion

        #region Read Ini File

        /// <summary>
        /// Reads a key value in a certain section.
        /// If mIniFilePath does not exist, return a empty string or return the key's string value.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string ReadValue(string iniFilePath, string section, string key)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder keyValue = new StringBuilder(255);
                if (GetPrivateProfileString(section, key, "", keyValue, 255, iniFilePath) != 0)
                    return keyValue.ToString();
                else
                    throw new Exception();
            }
            else
                throw new Exception("没有找到文件" + iniFilePath + "。打开文件失败！"); ;
        }

        #endregion

        #region Write Ini File

        /// <summary>
        /// Writes a key value in a certain section.
        /// If mIniFilePath does not exist or writing value fails, return false, or return true. 
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static bool WriteValue(string iniFilePath, string section, string key, string value)
        {
            if (File.Exists(iniFilePath))
            {
                if (WritePrivateProfileString(section, key, value, iniFilePath) == 0)
                    return false;
                else
                    return true;
            }
            else
                throw new Exception("没有找到文件" + iniFilePath + "。打开文件失败！"); ;
        }

        #endregion
    }
}
