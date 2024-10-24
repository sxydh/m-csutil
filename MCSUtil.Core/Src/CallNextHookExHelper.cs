using System;
using System.Runtime.InteropServices;

namespace MCSUtil.Core
{
    public static class CallNextHookExHelper
    {
        public static IntPtr Call(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam)
        {
            return CallNextHookEx(hhk, nCode, wParam, lParam);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    }
}