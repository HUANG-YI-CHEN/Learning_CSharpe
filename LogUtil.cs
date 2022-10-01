using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MachineLog.Lib
{
    public static class LogUtil
    {
        #region LogTrace
        private static readonly object LockFile = new object();
        /// <summary>
        /// [一般寫入檔案] e.g. .\Logs\2022\09\20220930.txt
        /// </summary>
        /// <param name="Message">檔案內容</param>
        public static void LogTrace(string Message)
        {
            LogTrace(string.Empty, Message);
        }
        /// <summary>
        /// [特殊寫入檔案] e.g. .\Logs\{RMS}\2022\09\20220930.txt
        /// </summary>
        /// <param name="Dir">目錄名稱</param>
        /// <param name="Message">檔案內容</param>
        public static void LogTrace(string Dir, string Message)
        {
            LogTrace(string.Empty, Dir, Message, string.Empty);
        }
        /// <summary>
        /// [特殊寫入檔案] e.g. .\Logs\{RMS}\2022\09\20220930.txt
        /// </summary>
        /// <param name="Dir">目錄名稱</param>
        /// <param name="Message">檔案內容</param>
        /// <param name="MethodName">程式方法名稱</param>
        public static void LogTrace(string Dir, string Message, string MethodName)
        {
            LogTrace(string.Empty, Dir, Message, MethodName);
        }
        /// <summary>
        /// [特殊寫入檔案] e.g. .\Logs\{ASE99-999-9999}\{RMS}\2022\09\20220930.txt
        /// </summary>
        /// <param name="AseMachID">機台名稱</param>
        /// <param name="Dir">目錄名稱</param>
        /// <param name="Message">檔案內容</param>
        /// <param name="MethodName">程式方法名稱</param>
        public static void LogTrace(string AseMachID, string Dir, string Message, string MethodName)
        {
            // DirBase:當前程式執行根目錄
            // Year: 2022
            // Month: 09
            // CurrentDate: 20220930
            // FileType: .txt
            // CurrentDir: .\Logs\{ASE99-999-9999}\{RMS}\2022\09
            // FileName: .\Logs\{ASE99-999-9999}\{RMS}\2022\09\20220930.txt
            // NowTime: 2022-09-30 01:36:14.972
            try
            {
                DateTime GetDateTime = DateTime.Now;
                string DirBase = System.AppDomain.CurrentDomain.BaseDirectory + @"Logs\" + (string.IsNullOrEmpty(AseMachID) ? "" : AseMachID + @"\" + (string.IsNullOrEmpty(Dir) ? "" : Dir + @"\"));
                string Year = GetDateTime.ToString("yyyy"), Month = GetDateTime.ToString("MM"), CurrentDate = GetDateTime.ToString("yyyyMMdd"), FileType = ".txt", NowTime = GetDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string CurrentDir = DirBase + Year + @"\" + Month + @"\";
                string FileName = CurrentDir + CurrentDate + FileType;
                if (!Directory.Exists(CurrentDir))
                    Directory.CreateDirectory(CurrentDir);
                if (!File.Exists(FileName))
                    using (var f = File.Create(FileName)) { f.Close(); }

                using (var fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (var log = new StreamWriter(fs, Encoding.Default))
                    {
                        lock (LockFile)
                        {
                            // 取得當前方法類別命名空間名稱
                            //string Namespace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                            //log.WriteLine(NowTime + " [" + Namespace + "] → " + Message);
                            // 取得當前類別名稱
                            //string ClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
                            //log.WriteLine(NowTime + " [" + ClassName + "] → " + Message);
                            // 取得當前所使用的方法
                            //string Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
                            //log.WriteLine(NowTime + " [" + MethodName + "] → " + Message);
                            if (string.IsNullOrEmpty(MethodName))
                                log.WriteLine(NowTime + " → " + Message);
                            else
                                log.WriteLine(NowTime + " [" + MethodName + "] → " + Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogTrace(ex.Message);
            }
        }
        #endregion
        #region ExtraLogTrace
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SrcDir">目標目錄</param>
        /// <param name="Message">檔案內容</param>
        public static void ExtraLogTrace(string SrcDir, string Message)
        {
            ExtraLogTrace(SrcDir, Message, string.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SrcDir">目標目錄</param>
        /// <param name="Message">檔案內容</param>
        /// <param name="MethodName">程式方法名稱</param>
        public static void ExtraLogTrace(string SrcDir, string Message, string MethodName)
        {
            ExtraLogTrace(SrcDir, string.Empty, Message, MethodName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AseMachID">目標目錄</param>
        /// <param name="Dir">目錄名稱</param>
        /// <param name="Message">檔案內容</param>
        /// <param name="MethodName">程式方法名稱</param>
        public static void ExtraLogTrace(string SrcDir, string Dir, string Message, string MethodName)
        {
            // DirBase:當前程式執行根目錄
            // Year: 2022
            // Month: 09
            // CurrentDate: 20220930
            // FileType: .txt
            // CurrentDir: .\Logs\{ASE99-999-9999}\{RMS}\2022\09
            // FileName: .\Logs\{ASE99-999-9999}\{RMS}\2022\09\20220930.txt
            // NowTime: 2022-09-30 01:36:14.972
            try
            {
                DateTime GetDateTime = DateTime.Now;
                string DirBase = (SrcDir.Substring(SrcDir.Length).Equals(@"\") ? SrcDir : SrcDir + @"\") + @"Logs\" + (string.IsNullOrEmpty(Dir) ? "" : Dir + @"\");
                string Year = GetDateTime.ToString("yyyy"), Month = GetDateTime.ToString("MM"), CurrentDate = GetDateTime.ToString("yyyyMMdd"), FileType = ".txt", NowTime = GetDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string CurrentDir = DirBase + Year + @"\" + Month + @"\";
                string FileName = CurrentDir + CurrentDate + FileType;
                if (!Directory.Exists(CurrentDir))
                    Directory.CreateDirectory(CurrentDir);
                if (!File.Exists(FileName))
                    using (var f = File.Create(FileName)) { f.Close(); }

                using (var fs = new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (var log = new StreamWriter(fs, Encoding.Default))
                    {
                        lock (LockFile)
                        {
                            // 取得當前方法類別命名空間名稱
                            //string Namespace = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace;
                            //log.WriteLine(NowTime + " [" + Namespace + "] → " + Message);
                            // 取得當前類別名稱
                            //string ClassName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName;
                            //log.WriteLine(NowTime + " [" + ClassName + "] → " + Message);
                            // 取得當前所使用的方法
                            //string Method = System.Reflection.MethodBase.GetCurrentMethod().Name;
                            //log.WriteLine(NowTime + " [" + Method + "] → " + Message);
                            if (string.IsNullOrEmpty(MethodName))
                                log.WriteLine(NowTime + " → " + Message);
                            else
                                log.WriteLine(NowTime + " [" + MethodName + "] → " + Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogTrace(ex.Message);
            }
        }
        #endregion
    }
}
