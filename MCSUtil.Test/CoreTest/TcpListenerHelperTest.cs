using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class TcpListenerHelperTest
    {
        [TestMethod]
        public void TestIsPortInUse()
        {
            var isPortInUse = TcpListenerHelper.IsPortInUse(8080);
            Assert.IsNotNull(isPortInUse);
        }
    }
}