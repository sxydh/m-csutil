using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class ProcessHelperTest
    {
        [TestMethod]
        public void TestStart()
        {
            var processId = ProcessHelper.Start("cmd");
            Assert.IsNotNull(processId);
        }

        [TestMethod]
        public void TestStop()
        {
            var stop = ProcessHelper.Stop(-100);
            Assert.IsTrue(!stop);
        }
    }
}