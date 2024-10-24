using System;
using System.Runtime.InteropServices;

namespace MCSUtil.Core
{
    public static class SetWindowPosHelper
    {
        public static void TopWindow()
        {
        }

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    }
}