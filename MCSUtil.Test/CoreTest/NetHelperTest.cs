﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MCSUtil.Core;

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
}