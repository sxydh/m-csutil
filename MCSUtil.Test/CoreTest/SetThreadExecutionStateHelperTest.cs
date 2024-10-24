using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class SetThreadExecutionStateHelperTest
    {
        [TestMethod]
        public void TestKeepAwake()
        {
            var state = SetThreadExecutionStateHelper.KeepAwake();
            Assert.IsNotNull(state);
        }

        [TestMethod]
        public void TestResetAwake()
        {
            var state = SetThreadExecutionStateHelper.ResetAwake();
            Assert.IsNotNull(state);
        }
    }
}