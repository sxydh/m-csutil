using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class SetWindowPosHelperTest
    {
        [TestMethod]
        public void TestTopWindow()
        {
            var ptr = GetForegroundWindowHelper.Get();
            Assert.IsNotNull(ptr);

            var success = SetWindowPosHelper.TopWindow(ptr);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void TestUnTopWindow()
        {
            var ptr = GetForegroundWindowHelper.Get();
            Assert.IsNotNull(ptr);

            var success = SetWindowPosHelper.UnTopWindow(ptr);
            Assert.IsTrue(success);
        }
    }
}