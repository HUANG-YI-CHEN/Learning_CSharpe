using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MachineLog.Lib
{
    public static class FileUtil
    {
        private static readonly bool bEnableLog = false;
        #region Delete File | Directory
        /// <summary>
        /// 檔案刪除
        /// </summary>
        /// <param name="FilePath">檔案路徑</param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> FileDelete(string FilePath)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            try
            {
                if (!File.Exists(FilePath))
                    result = new KeyValuePair<bool, string>(false, "[" + FilePath + "] 檔案路徑不存在!!");
                else
                {
                    File.Delete(FilePath);
                    result = new KeyValuePair<bool, string>(true, "[" + FilePath + "] 檔案刪除成功.");
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, "[" + FilePath + "] 檔案刪除失敗. 原因:" + ex.Message);
            }
            if (bEnableLog)
                LogUtil.LogTrace(result.Key.ToString() + " | " + result.Value);
            return result;
        }
        /// <summary>
        /// 目錄刪除
        /// </summary>
        /// <param name="DirPath">目錄路徑</param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> DirDelete(string DirPath)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            result = DirDelete(DirPath, true);
            return result;
        }
        /// <summary>
        /// 目錄刪除
        /// </summary>
        /// <param name="DirPath">目錄路徑</param>
        /// <param name="recursive">是否遞迴刪除目錄下所有檔案</param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> DirDelete(string DirPath, bool recursive)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            try
            {
                if (!Directory.Exists(DirPath))
                    result = new KeyValuePair<bool, string>(false, "[" + DirPath + "] 目錄路徑不存在!!");
                else
                {
                    Directory.Delete(DirPath, recursive);
                    result = new KeyValuePair<bool, string>(true, "[" + DirPath + "] 目錄刪除成功.");
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, "[" + DirPath + "] 目錄刪除失敗. 原因:" + ex.Message);
            }
            if (bEnableLog)
                LogUtil.LogTrace(result.Key.ToString() + " | " + result.Value);
            return result;
        }
        #endregion
        #region Copy File | Directory
        /// <summary>
        /// 檔案複製
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> FileCopy(string SrcPath, string DstPath)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            result = FileCopy(SrcPath, DstPath, true);
            return result;
        }
        /// <summary>
        /// 檔案複製
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <param name="overwrite">是否覆寫</param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> FileCopy(string SrcPath, string DstPath, bool overwrite)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            try
            {
                if (!File.Exists(SrcPath))
                {
                    result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] 檔案來源路徑不存在!!");
                }
                else
                {
                    File.Copy(SrcPath, DstPath, overwrite);
                    result = new KeyValuePair<bool, string>(true, "[" + SrcPath + "] → [" + DstPath + "] 檔案複製成功.");
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] → [" + DstPath + "] 檔案複製失敗. 原因:" + ex.Message);
            }
            if (bEnableLog)
                LogUtil.LogTrace(result.Key.ToString() + " | " + result.Value);
            return result;
        }
        /// <summary>
        /// 資料夾複製
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> DirCopy(string SrcPath, string DstPath)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            try
            {
                if (!Directory.Exists(SrcPath))
                {
                    result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] 目錄來源路徑不存在!!");
                }
                else
                {
                    //Now Create all of the directories
                    foreach (string dirPath in Directory.GetDirectories(SrcPath, "*", SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(SrcPath, DstPath));

                    //Copy all the files & Replaces any files with the same name
                    foreach (string newPath in Directory.GetFiles(SrcPath, "*.*", SearchOption.AllDirectories))
                        FileCopy(newPath, newPath.Replace(SrcPath, DstPath));
                    result = new KeyValuePair<bool, string>(true, "[" + SrcPath + "] → [" + DstPath + "] 目錄複製成功.");
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] → [" + DstPath + "] 目錄複製失敗. 原因:" + ex.Message);
            }
            if (bEnableLog)
                LogUtil.LogTrace(result.Key.ToString() + " | " + result.Value);
            return result;
        }
        /// <summary>
        /// 資料夾複製_遞迴
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> DirCopyCTE(string SrcPath, string DstPath)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            try
            {
                if (!Directory.Exists(SrcPath))
                    result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] 目錄來源路徑不存在!!");
                else
                {
                    if (!Directory.Exists(DstPath))
                        Directory.CreateDirectory(DstPath);
                    foreach (string filePath in Directory.GetFiles(SrcPath))
                        FileCopy(filePath, filePath.Replace(SrcPath, DstPath));

                    foreach (string DirPath in Directory.GetDirectories(SrcPath))
                    {
                        DirCopyCTE(DirPath, DirPath.Replace(SrcPath, DstPath));
                    }
                    result = new KeyValuePair<bool, string>(true, "[" + SrcPath + "] → [" + DstPath + "] 目錄複製成功.");
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] → [" + DstPath + "] 目錄複製失敗. 原因:" + ex.Message);
            }
            if (bEnableLog)
                LogUtil.LogTrace(result.Key.ToString() + " | " + result.Value);
            return result;
        }
        #endregion
        #region Move File / Directory
        /// <summary>
        /// 檔案搬移
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> FileMove(string SrcPath, string DstPath)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            try
            {
                if (!File.Exists(SrcPath))
                {
                    result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] 檔案來源路徑不存在!!");
                }
                else
                {
                    if (!File.Exists(DstPath))
                    {
                        File.Move(SrcPath, DstPath);
                        FileDelete(SrcPath);
                    }
                    else
                    {
                        FileCopy(SrcPath, DstPath);
                        FileDelete(SrcPath);
                    }
                    result = new KeyValuePair<bool, string>(true, "[" + SrcPath + "] → [" + DstPath + "] 檔案搬移成功.");
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] → [" + DstPath + "] 檔案搬移失敗. 原因:" + ex.Message);
            }
            if (bEnableLog)
                LogUtil.LogTrace(result.Key.ToString() + " | " + result.Value);
            return result;
        }
        /// <summary>
        /// 資料夾搬移
        /// </summary>
        /// <param name="SrcPath">來源檔案路徑</param>
        /// <param name="DstPath">目的檔案路徑</param>
        /// <returns></returns>
        public static KeyValuePair<bool, string> DirMove(string SrcPath, string DstPath)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, string.Empty);
            try
            {
                if (!Directory.Exists(SrcPath))
                {
                    result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] 資料夾來源路徑不存在!!");
                }
                else
                {
                    if (!Directory.Exists(DstPath))
                    {
                        Directory.Move(SrcPath, DstPath);
                        DirDelete(SrcPath);
                    }
                    else
                    {
                        DirCopy(SrcPath, DstPath);
                        DirDelete(SrcPath);
                    }
                    result = new KeyValuePair<bool, string>(true, "[" + SrcPath + "] → [" + DstPath + "] 資料夾搬移成功.");
                }
            }
            catch (Exception ex)
            {
                result = new KeyValuePair<bool, string>(false, "[" + SrcPath + "] → [" + DstPath + "] 資料夾搬移失敗. 原因:" + ex.Message);
            }
            if (bEnableLog)
                LogUtil.LogTrace(result.Key.ToString() + " | " + result.Value);
            return result;
        }
        #endregion
    }
}
