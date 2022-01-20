using System;
using System.Configuration;
using System.Collections.Specialized;

namespace Learning_CSharpe
{
    class ConfigureUtil
    {
        // [ConfigureUtil.config]
        public string GetAppConfig(string Attr) 
        {
            return ConfigurationManager.AppSettings.Get(Attr);
        }
        public String GetConnectConfig(string Attr)
        {
            return ConfigurationManager.ConnectionStrings[Attr].ToString();
        }
        public NameValueCollection GetAllAppConfig()
        {
            return ConfigurationManager.AppSettings;
        }

    }
}
