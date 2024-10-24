using System;
using System.Diagnostics;
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
            var processId = Process.Start("cmd")?.Id;
            var stopped = ProcessHelper.Stop(processId ?? throw new ArgumentException());
            Assert.IsTrue(stopped);
        }
    }
}