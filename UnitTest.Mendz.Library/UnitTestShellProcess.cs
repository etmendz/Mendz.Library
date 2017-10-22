using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library;
using System;
using System.Diagnostics;
using System.ComponentModel;

namespace UnitTest.Mendz.Library
{
    [TestClass]
    public class UnitTestShellProcess
    {
        [TestMethod]
        public void TestShellProcess()
        {
            ShellProcess.Start(new ProcessStartInfo() { FileName = "cmd" });
            ShellProcess.Start(new ProcessStartInfo() { FileName = "notepad.exe" });
            ShellProcess.Start(new ProcessStartInfo() { FileName = "https://www.bing.com" });
            ShellProcess.Start(new ProcessStartInfo()
            {
                FileName = @"D:\New Microsoft Word Document.docx"
            });
            Assert.ThrowsException<WarningException>(() =>
            {
                ShellProcess.Start(new ProcessStartInfo()
                {
                    FileName = @"D:\New Microsoft Word Document.docx"
                }, Process_Exited);
            });
            ShellProcess.Start(new ProcessStartInfo()
            {
                FileName = @"D:\New Microsoft Word Document.docx"
            }, Process_Exited, true);
            ShellProcess.Start(new ProcessStartInfo()
            {
                FileName = "negativetest.docx",
            }, null, true);
            Assert.ThrowsException<Win32Exception>(() =>
            {
                ShellProcess.Start(new ProcessStartInfo() { FileName = "negativetest.docx" });
            });
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                ShellProcess.Start(new ProcessStartInfo()
                {
                    FileName = "negativetest.docx",
                    ErrorDialog = true
                });
            });
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            throw new WarningException("Exited.");
        }
    }
}
