using System;
using System.Collections.Generic;
using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class FileServerTest
    {
        [TestMethod]
        public void TestNew()
        {
            var fileServer = new FileServer(8080, "");
            fileServer.Start().Wait(TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void TestGetContentType()
        {
            var contentType = FileServer.GetContentType(".js");
            Assert.IsNotNull(contentType);
        }

        [TestMethod]
        public void TestSubFileServer()
        {
            var subFileServer = new SubFileServer(8080, "");
            subFileServer.Start().Wait(TimeSpan.FromSeconds(1));
        }
    }

    internal class SubFileServer : FileServer
    {
        internal SubFileServer(int port, string root) : base(port, root)
        {
        }
    }

    [TestClass]
    public class IPHelperTest
    {
        [TestMethod]
        public void TestGetAliveIPList()
        {
            var aliveIPList = IPHelper.GetAliveIPList();
            Assert.IsNotNull(aliveIPList);

            aliveIPList = IPHelper.GetAliveIPList(new List<string> { "1.1.1.1" });
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