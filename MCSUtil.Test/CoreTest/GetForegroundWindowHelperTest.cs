using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class GetForegroundWindowHelperTest
    {
        [TestMethod]
        public void TestGet()
        {
            var ptr = GetForegroundWindowHelper.Get();
            Assert.IsNotNull(ptr);
        }
    }
}