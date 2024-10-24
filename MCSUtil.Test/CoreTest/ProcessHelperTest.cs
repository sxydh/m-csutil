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
        public void TestGet()
        {
            var process = ProcessHelper.Get("explorer");
            Assert.IsNotNull(process);
        }

        [TestMethod]
        public void TestGetProcessId()
        {
            var processId = ProcessHelper.GetProcessId("explorer");
            Assert.IsNotNull(processId);
        }

        [TestMethod]
        public void TestStart()
        {
            var processId = ProcessHelper.Start("cmd");
            Assert.IsNotNull(processId);
        }

        [TestMethod]
        public void TestKill()
        {
            const string processName = "notepad";

            var processId = Process.Start(processName)?.Id;
            var isKilled = ProcessHelper.Kill(processId ?? throw new ArgumentException());
            Assert.IsTrue(isKilled);

            Process.Start(processName);
            isKilled = ProcessHelper.Kill(processName);
            Assert.IsTrue(isKilled);
        }
    }
}