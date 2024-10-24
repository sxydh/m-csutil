using System;
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
            var fileServer = new FileServer();
            fileServer.Start().Wait(TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void TestNew1()
        {
            var fileServer = new FileServer(51);
            fileServer.Start().Wait(TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void TestNew5()
        {
            var fileServer = new FileServer("0.0.0.0", 55, "", "admin", "123");
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
            var subFileServer = new SubFileServer(60, "");
            subFileServer.Start().Wait(TimeSpan.FromSeconds(1));
        }
    }

    internal class SubFileServer : FileServer
    {
        internal SubFileServer(int port, string root) : base(port, root)
        {
        }
    }
}