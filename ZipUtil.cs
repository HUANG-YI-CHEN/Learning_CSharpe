using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MachineLog.Lib
{
    public class ZipUtil
    {
        private static string FilePath = @"C:\Program Files\7-Zip\7z.exe";
        private static readonly bool bEnableLog = false;


        ZipUtil(string filePath)
        {
            FilePath = filePath;
        }
        #region 7Z 壓縮
        /// <summary>
        /// 7Z 壓縮檔案
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <returns></returns>
        public static KeyValuePair<bool, String> Zip(string SrcPath, string DstPath)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            result = Zip(SrcPath, DstPath, string.Empty);
            return result;
        }
        /// <summary>
        /// 7Z 壓縮檔案
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <param name="Password">密碼</param>
        /// <returns></returns>
        public static KeyValuePair<bool, String> Zip(string SrcPath, string DstPath, string Password)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            result = Zip(SrcPath, DstPath, string.Empty, true);
            return result;
        }
        /// <summary>
        /// 7Z 壓縮檔案
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <param name="Password">密碼</param>
        /// <param name="IsCover">是否覆蓋</param>
        /// <returns></returns>
        public static KeyValuePair<bool, String> Zip(string SrcPath, string DstPath, string Password, bool IsCover)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            string Argments = string.Empty;
            if (!string.IsNullOrEmpty(Password) && IsCover) //壓縮加密檔案且覆蓋已存在壓縮檔案
                Argments = string.Format("a -tzip -p{0} -o+ {1} {2} -r", Password, DstPath, SrcPath);
            else if (!string.IsNullOrEmpty(Password) && !IsCover)  //壓縮加密檔案且不覆蓋已存在壓縮檔案
                Argments = string.Format("a -tzip -p{0} -o- {1} {2} -r", Password, DstPath, SrcPath);
            else if (string.IsNullOrEmpty(Password) && IsCover) //壓縮且覆蓋已存在壓縮檔案( -o+覆蓋 )
                Argments = string.Format("a -tzip -o+ {0} {1} -r", DstPath, SrcPath);
            else //壓縮且不覆蓋已存在壓縮檔案( -o-不覆蓋 )
                Argments = string.Format("a -tzip -o- {0} {1} -r", DstPath, SrcPath);
            //Argments = string.Format("a -tzip -ep1 -o- {0} {1} -r", DstPath, SrcPath);
            try
            {
                using (Process ps = new Process())
                {
                    ps.StartInfo.FileName = FilePath;
                    ps.StartInfo.Arguments = Argments;
                    ps.Start();
                    ps.WaitForExit();
                    if (ps.ExitCode.Equals(0))
                    {
                        ps.Close();
                        result = new KeyValuePair<bool, string>(true, "[" + SrcPath + "] → [" + DstPath + "] 檔案壓縮成功.");
                    }
                    else
                    {
                        ps.Close();
                        result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] → [" + DstPath + "] 檔案壓縮失敗.");
                    }
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] → [" + DstPath + "] 檔案壓縮失敗. 原因:" + ex.Message);
            }
            if (bEnableLog)
                LogUtil.LogTrace(result.Key.ToString() + " | " + result.Value);
            return result;
        }
        #endregion
        #region 7Z 解壓縮
        /// <summary>
        ///  7Z 解壓縮檔案
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <returns></returns>
        public static KeyValuePair<bool, String> UnZip(string SrcPath, string DstPath)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            result = UnZip(SrcPath, DstPath, string.Empty);
            return result;
        }
        /// <summary>
        ///  7Z 解壓縮檔案
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <param name="Password">密碼</param>
        /// <param name="IsCover">是否覆蓋</param>
        /// <returns></returns>
        public static KeyValuePair<bool, String> UnZip(string SrcPath, string DstPath, string Password)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            string Argments = string.Empty;
            if (!string.IsNullOrEmpty(Password)) //解壓加密檔案且覆蓋已存在檔案( -p密碼 )
                Argments = string.Format("x -tzip -p{0} {1} -o{2} -y", Password, SrcPath, DstPath);
            else if (string.IsNullOrEmpty(Password)) //覆蓋命令( x -o+ 代表覆蓋已存在的檔案)
                Argments = string.Format("x -tzip {0} -o{1} -y", SrcPath, DstPath);
            try
            {
                if (!File.Exists(SrcPath))
                    result = new KeyValuePair<bool, string>(false, "[" + FilePath + "] 檔案路徑不存在!!");
                else
                {
                    using (Process ps = new Process())
                    {
                        ps.StartInfo.FileName = FilePath;
                        ps.StartInfo.Arguments = Argments;
                        ps.Start();
                        ps.WaitForExit();
                        if (ps.ExitCode.Equals(0))
                        {
                            ps.Close();
                            result = new KeyValuePair<bool, string>(true, "[" + SrcPath + "] → [" + DstPath + "] 檔案解壓縮成功.");
                        }
                        else
                        {
                            ps.Close();
                            result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] → [" + DstPath + "] 檔案解壓縮失敗.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] → [" + DstPath + "]  檔案解壓縮失敗. 原因:" + ex.Message);
            }
            if (bEnableLog)
                LogUtil.LogTrace(result.Key.ToString() + " | " + result.Value);
            return result;
        }
        #endregion
    }
}
