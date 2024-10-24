using System;
using System.Runtime.InteropServices;

namespace MCSUtil.Core
{
    public static class SetWindowsHookExHelper
    {
        private const int WH_KEYBOARD_LL = 13;

        public static IntPtr HookKeyboard(HOOKPROC lpfn)
        {
            return SetWindowsHookEx(WH_KEYBOARD_LL, lpfn, IntPtr.Zero, 0);
        }

        public static bool UnHookKeyboard(IntPtr hhk)
        {
            return UnhookWindowsHookEx(hhk);
        }

        public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
    }
}