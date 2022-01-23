using System;
using System.IO;

namespace Learning_CSharpe
{
    public class FileUtil
    {
        public static void Create(string _src)
        {
            try
            {
                if (!Directory.Exists(_src))
                    Directory.CreateDirectory(_src);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void Delete(string _src)
        {
            try
            {
                if (Directory.Exists(_src))
                {
                    foreach (string file in Directory.GetFiles(_src))
                    {
                        File.Delete(file);
                    }
                    foreach (string folder in Directory.GetDirectories(_src))
                    {
                        FileUtil.Delete(folder);
                    }
                }
                else if (File.Exists(_src))
                {
                    File.Delete(_src);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Directory.Delete(_src, true);
            }
        }
        public static void Copy(string _src, string _dest)
        {
            try
            {
                if (Directory.Exists(_src))
                {
                    FileUtil.Create(_dest);
                    foreach (string file in Directory.GetFiles(_src))
                    {
                        string fileName = (file.Replace(_src, "")).Substring(1);
                        string destFullPath = Path.Combine(_dest, fileName);
                        File.Copy(file, destFullPath, true);                        
                    }
                    foreach (string folder in Directory.GetDirectories(_src))
                    {
                        string folderName = (folder.Replace(_src, "")).Substring(1);
                        string destFolderName = Path.Combine(_dest, folderName);
                        FileUtil.Copy(folder, destFolderName);
                    }
                }
                else if (File.Exists(_src))
                {
                    File.Copy(_src, _dest, true);                    
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void Move(string _src, string _dest)
        {
            try
            {
                FileUtil.Copy(_src, _dest);
                FileUtil.Delete(_src);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
