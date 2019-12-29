using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library.Conventions;
using System;

namespace UnitTest.Mendz.Library
{
    [TestClass]
    public class UnitTestDateTimeStampedFileName
    {
        [TestMethod]
        public void TestDateTimeStampedFileName()
        {
            DateTimeStampedFileName dtsf;
            dtsf = new DateTimeStampedFileName("FileName.ext", new DateTime(2020, 1, 1, 1, 15, 30));
            Assert.AreEqual(false, dtsf.StampAsExtension);
            Assert.AreEqual("20200101", dtsf.DateStamp);
            Assert.AreEqual("011530", dtsf.TimeStamp);
            Assert.AreEqual("FileName_20200101_011530.ext", dtsf.ToString());
            dtsf = new DateTimeStampedFileName("FileName.ext", new DateTime(2020, 1, 1, 1, 15, 30), '_', true);
            Assert.AreEqual(true, dtsf.StampAsExtension);
            Assert.AreEqual("20200101", dtsf.DateStamp);
            Assert.AreEqual("011530", dtsf.TimeStamp);
            Assert.AreEqual("FileName.ext.20200101_011530", dtsf.ToString());
        }

        [TestMethod]
        public void TestDateTimeStampedFileNameParse()
        {
            DateTimeStampedFileName dtsf;
            dtsf = DateTimeStampedFileName.Parse("FileName_20200101_011530.ext");
            Assert.AreEqual(false, dtsf.StampAsExtension);
            Assert.AreEqual("FileName.ext", dtsf.FileName);
            Assert.AreEqual("20200101", dtsf.DateStamp);
            Assert.AreEqual("011530", dtsf.TimeStamp);
            dtsf = DateTimeStampedFileName.Parse("FileName.ext.20200101_011530");
            Assert.AreEqual(true, dtsf.StampAsExtension);
            Assert.AreEqual("FileName.ext", dtsf.FileName);
            Assert.AreEqual("20200101", dtsf.DateStamp);
            Assert.AreEqual("011530", dtsf.TimeStamp);
        }
    }
}
