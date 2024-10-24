using System;
using System.Runtime.InteropServices;

namespace MCSUtil.Core
{
    public static class SetWindowsHookExHelper
    {
        private const int WH_KEYBOARD_LL = 13;

        public static void HookKeyboard(HOOKPROC lpfn)
        {
            SetWindowsHookEx(WH_KEYBOARD_LL, lpfn, IntPtr.Zero, 0);
        }

        public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hMod, uint dwThreadId);
    }
}