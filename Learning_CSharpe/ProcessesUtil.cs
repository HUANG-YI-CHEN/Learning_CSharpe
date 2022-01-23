using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Learning_CSharpe
{
    class ProcessesUtil
    {
        private Process[] processes;
        public Process[] GetProcessesList()
        {
            processes = Process.GetProcesses();
            return processes;
        }
        public bool CheckProcesses(string AppName)
        {
            bool flag = false;
            processes = Process.GetProcesses();
            foreach(Process ps in processes)
            {
                if (ps.ProcessName.Equals(AppName, StringComparison.OrdinalIgnoreCase))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        public void StartProcesses(string AppName)
        {
            processes = Process.GetProcessesByName(AppName);
            if (processes.Length > 0)
            {
                MessageBox.Show("請關閉已經啟動的程式後再進行嘗試");
                processes[0].CloseMainWindow();
                processes[0].Close();
                return;
            }
            else
                Process.Start(AppName);
        }
        public void KillProcesses(string AppName)
        {
            processes = Process.GetProcessesByName(AppName);
            foreach (Process ps in processes)
            {
                ps.Kill();
            }
        }
    }
}
