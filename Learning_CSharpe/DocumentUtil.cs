using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Learning_CSharpe
{
    class DocumentUtil
    {
        public static void WriteFile(string _path, string _text, Encoding _encoding)
        {
            if (!File.Exists(_path))
            {
                File.WriteAllText(_path, "", _encoding);
            }
            File.AppendAllText(_path, _text + Environment.NewLine, _encoding);
        }
        public static string ReadFile(string _path)
        {
            return File.ReadAllText(_path);
        }
    }
}
