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
    }
}