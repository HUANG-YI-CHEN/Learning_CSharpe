using System.ServiceProcess;

namespace Learning_CSharpe
{
    class ServiceUtil
    {
        public static void ServiceSwitch(string MachineName, string ServiceName)
        {
            ServiceController tController = new ServiceController();
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
            tController.Dispose();
        }
    }
}
