using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class DateTimeHelperTest
    {
        [TestMethod]
        public void TestTimestamp()
        {
            var timestamp = DateTimeHelper.Timestamp();
            Assert.IsNotNull(timestamp);
        }
    }
}