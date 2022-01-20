using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace Learning_CSharpe
{
    class ServiceUtil
    {
        private ServiceController tController;
        public void ServiceSwitch(string MachineName, string ServiceName)
        {
            tController = new ServiceController();
            //指到本機
            tController.MachineName = MachineName;
            //服務的名稱
            tController.ServiceName = ServiceName;
            if (tController.Status == ServiceControllerStatus.Stopped)
            {
                tController.Start();
            }
            else if (tController.Status == ServiceControllerStatus.Running)
            {
                tController.Stop();
            }
        }
    }
}
