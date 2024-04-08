using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class RunPython : MonoBehaviour
{
    public void RunExe()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = Application.dataPath + "/TEST/HandTracking.exe";
        startInfo.UseShellExecute = false;

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
