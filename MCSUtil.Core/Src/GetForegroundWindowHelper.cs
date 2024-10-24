using System;
using System.Runtime.InteropServices;

namespace MCSUtil.Core
{
    public static class GetForegroundWindowHelper
    {
        public static IntPtr Get()
        {
            return GetForegroundWindow();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
    }
}