using System;
using System.Diagnostics;

namespace MCSUtil.Core
{
    public static class ProcessHelper
    {
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

        public static bool Stop(int processId)
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
    }
}