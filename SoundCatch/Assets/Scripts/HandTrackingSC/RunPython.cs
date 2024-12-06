using UnityEngine;
using System.Diagnostics;

public class RunPython
{
    public void RunExe()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = Application.streamingAssetsPath + "/HandTracking/HandTracking.exe";
        startInfo.UseShellExecute = false;
        startInfo.Verb = "runas";

        Process.Start(startInfo);

    }

    public void StopPythonExe()
    {
        foreach (Process process in Process.GetProcesses())
        {
            if (process.ProcessName.StartsWith("HandTracking"))
            {
                process.Kill();
            }
        }
    }
}
