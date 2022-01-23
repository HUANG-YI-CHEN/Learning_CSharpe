using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Learning_CSharpe
{
    class ConfigureUtil
    {
        // [ConfigureUtil.config]
        public static string GetAppConfig(string Attr) 
        {
            return ConfigurationManager.AppSettings.Get(Attr);
        }
        public static String GetConnectConfig(string Attr)
        {
            return ConfigurationManager.ConnectionStrings[Attr].ToString();
        }
        public static NameValueCollection GetAllAppConfig()
        {
            return ConfigurationManager.AppSettings;
        }

    }
}
