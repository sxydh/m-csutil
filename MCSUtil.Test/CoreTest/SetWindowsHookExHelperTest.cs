using System;
using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class SetWindowsHookExHelperTest
    {
        [TestMethod]
        public void TestHookKeyboard()
        {
            var hhk = SetWindowsHookExHelper.HookKeyboard(OnKeyboard);
            Assert.IsNotNull(hhk);
        }

        private static IntPtr OnKeyboard(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return CallNextHookExHelper.Call(IntPtr.Zero, nCode, wParam, lParam);
        }
    }
}