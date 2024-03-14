using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace RetroCloudSaving.Processes
{
    internal class ProcessHandler
    {
        public static void StartProcess(string processName, string path, Action onProcessExit, string arguments = "")
        {
            Process process = new Process();
            process.StartInfo.FileName = path + processName;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = path;

            if (arguments != "")
            {
                process.StartInfo.Arguments = arguments;
            }

            process.Start();
            process.WaitForExit();
            onProcessExit?.Invoke();
        }

    }
}
