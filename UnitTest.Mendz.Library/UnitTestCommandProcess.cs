using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library;
using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Text;

namespace UnitTest.Mendz.Library
{
    [TestClass]
    public class UnitTestCommandProcess
    {
        private StringBuilder _output = new StringBuilder();
        private StringBuilder _error = new StringBuilder();

        [TestMethod]
        public void TestCommandProcess()
        {
            int exitCode;
            exitCode = CommandProcess.Start(new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = "/c dir"
            });
            Assert.AreEqual(0, exitCode);
            Assert.AreEqual(0, _output.Length);
            Assert.AreEqual(0, _error.Length);
            exitCode = CommandProcess.Start(new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = "/c dir",
                RedirectStandardOutput = true
            }, Process_Output);
            Assert.AreEqual(0, exitCode);
            Assert.AreNotEqual(0, _output.Length);
            Assert.AreEqual(0, _error.Length);
            _output.Clear();
            _error.Clear();
            exitCode = CommandProcess.Start(new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = @"/c dir A:\",
                RedirectStandardOutput = true,
                RedirectStandardError = true
            }, Process_Output, Process_Error);
            Assert.AreNotEqual(0, exitCode);
            Assert.AreNotEqual(0, _output.Length);
            Assert.AreNotEqual(0, _error.Length);
            _output.Clear();
            _error.Clear();
            Assert.ThrowsException<WarningException>(() =>
            {
                exitCode = CommandProcess.Start(new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = "/c dir"
                }, null, null, Process_Exited);
            });
            Assert.AreNotEqual(0, exitCode);
            exitCode = CommandProcess.Start(new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = "/c dir"
            }, null, null, Process_Exited, true);
            Assert.AreEqual(0, exitCode);
        }

        [TestMethod]
        public void TestCommandProcessNoHandler()
        {
            int exitCode;
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                exitCode = CommandProcess.Start(new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = "/c dir",
                    RedirectStandardOutput = true
                });
            });
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                exitCode = CommandProcess.Start(new ProcessStartInfo()
                {
                    FileName = "cmd.exe",
                    Arguments = "/c dir",
                    RedirectStandardError = true
                });
            });
        }

        private void Process_Output(object sender, DataReceivedEventArgs e)
        {
            _output.AppendLine("Process_Output just happened.");
            _output.Append(e.Data);
        }

        private void Process_Error(object sender, DataReceivedEventArgs e)
        {
            _error.AppendLine("Process_Error just happened.");
            _error.Append(e.Data);
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            throw new WarningException("Exited.");
        }
    }
}
