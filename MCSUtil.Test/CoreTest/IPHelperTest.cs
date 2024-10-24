using System.Collections.Generic;
using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class IPHelperTest
    {
        [TestMethod]
        public void TestGetAliveIPList()
        {
            var aliveIPList = IPHelper.GetAliveIPList();
            Assert.IsNotNull(aliveIPList);

            aliveIPList = IPHelper.GetAliveIPList(new List<string> { "8.8.8.8" });
            Assert.IsTrue(aliveIPList.Count == 1);
        }

        [TestMethod]
        public void TestGetSubnetIPList()
        {
            var subnetIpList = IPHelper.GetSubnetIPList();
            Assert.IsNotNull(subnetIpList);
        }
    }
}