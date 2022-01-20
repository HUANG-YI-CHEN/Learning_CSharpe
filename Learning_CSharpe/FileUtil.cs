using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Learning_CSharpe
{
    public class FileUtil
    {
        
        public void DirectoryDelete(string targetDirectory)
        {
            if (Directory.Exists(targetDirectory))
            {
                try
                {
                    Directory.Delete(targetDirectory);
                }
                catch(Exception ex)
                {
                    throw (ex);
                }
            }
        }
        public void DirectoryDeleteRecursive(string targetDirectory)
        {
            try
            {
                Directory.Delete(targetDirectory,true);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void DirectoryMove(string sourcePath, string targetDirectory)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(targetDirectory);
            if (dirInfo.Exists == false)
                Directory.CreateDirectory(targetDirectory);

            List<String> MyMusicFiles = Directory
                               .GetFiles(sourcePath, "*.*", SearchOption.AllDirectories).ToList();

            foreach (string file in MyMusicFiles)
            {
                FileInfo mFile = new FileInfo(file);
                // to remove name collisions
                if (new FileInfo(dirInfo + "\\" + mFile.Name).Exists == false)
                {
                    mFile.MoveTo(dirInfo + "\\" + mFile.Name);
                }
            }
        }

        public void DirectoryCopy(string sourcePath, string targetDirectory)
        {
            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);
            string[] files = Directory.GetFiles(sourcePath);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(targetDirectory, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourcePath);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(targetDirectory, name);
                DirectoryCopy(folder, dest);
            }
        }
    }
}
