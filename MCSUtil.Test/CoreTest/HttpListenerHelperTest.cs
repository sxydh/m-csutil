using System;
using System.Threading.Tasks;
using MCSUtil.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MCSUtil.Test.CoreTest
{
    [TestClass]
    public class FileServerTest
    {
        [TestMethod]
        public async Task TestNew()
        {
            var fileServer = new FileServer();
            _ = fileServer.Start();
            await Task.Delay(1000);
        }

        [TestMethod]
        public async Task TestNew1()
        {
            var fileServer = new FileServer(100);
            _ = fileServer.Start();
            await Task.Delay(1000);
        }

        [TestMethod]
        public async Task TestNew5()
        {
            var fileServer = new FileServer("+", 500, "ROOT", "admin", "123");
            _ = fileServer.Start();
            await Task.Delay(1000);
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