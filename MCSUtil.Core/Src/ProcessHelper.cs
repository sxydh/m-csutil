using System;
using System.Diagnostics;

namespace MCSUtil.Core
{
    public static class ProcessHelper
    {
        public static Process Get(string processName)
        {
            try
            {
                var processes = Process.GetProcessesByName(processName);
                return processes.Length == 1 ? processes[0] : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static int? GetProcessId(string processName)
        {
            return Get(processName)?.Id;
        }

        public static int? Start(string fileName, string arguments = "")
        {
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    UseShellExecute = true
                };
                return Process.Start(processStartInfo)?.Id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool Kill(int processId)
        {
            try
            {
                Process.GetProcessById(processId).Kill();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Kill(string processName)
        {
            var processId = GetProcessId(processName);
            return processId != null && Kill(processId.Value);
        }
    }
}