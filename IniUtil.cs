using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MachineLog.Lib
{
    public class IniUtil
    {
        private string filePath;
        private StringBuilder lpReturnedString;
        private int bufferSize;
        private static readonly bool bEnableLog = false;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defaultVal, StringBuilder resVal, int size, string filePath);

        public IniUtil(string iniPath)
        {
            this.bufferSize = 512;
            this.lpReturnedString = new StringBuilder(bufferSize);
            try
            {
                this.filePath = iniPath;
            }
            catch
            {
                if (!File.Exists(iniPath))
                {
                    string FileName = Path.GetFileName(iniPath);
                    string DirPath = System.AppDomain.CurrentDomain.BaseDirectory;
                    iniPath = DirPath + FileName;
                    using (var f = File.Create(iniPath)) { f.Close(); }
                }
                this.filePath = iniPath;
            }
        }
        /// <summary>
        /// 設定 Section Key 對應 Value
        /// </summary>
        /// <param name="section">Section</param>
        /// <param name="key">Key</param>
        /// <param name="val">Value</param>
        public void WriteIni(string section, string key, string val)
        {
            try
            {
                WritePrivateProfileString(section, (string.IsNullOrEmpty(key) ? string.Empty : key), (string.IsNullOrEmpty(val) ? string.Empty : val), filePath);
            }
            catch (Exception ex)
            {
                string Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
                if (bEnableLog)
                    LogUtil.LogTrace(string.Empty, ex.Message, Method);
            }
        }
        /// <summary>
        /// 取得 Section Key 對應 Value
        /// </summary>
        /// <param name="section">Section</param>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public string ReadIni(string section, string key)
        {
            return ReadIni(section, key, string.Empty);
        }
        /// <summary>
        /// 取得 Section Key 對應 Value
        /// </summary>
        /// <param name="section">Section</param>
        /// <param name="key">Key</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns></returns>
        public string ReadIni(string section, string key, string defaultValue)
        {
            string GetValue;
            try
            {
                lpReturnedString.Clear();
                GetPrivateProfileString(section, key, defaultValue, lpReturnedString, bufferSize, filePath);
                GetValue = lpReturnedString.ToString();
            }
            catch (Exception ex)
            {
                GetValue = string.Empty;
                string Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
                if (bEnableLog)
                    LogUtil.LogTrace(string.Empty, ex.Message, Method);
            }
            return GetValue;
        }
    }
}
