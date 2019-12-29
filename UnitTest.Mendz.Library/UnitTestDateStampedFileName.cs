using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library.Conventions;
using System;

namespace UnitTest.Mendz.Library
{
    [TestClass]
    public class UnitTestDateStampedFileName
    {
        [TestMethod]
        public void TestDateStampedFileName()
        {
            DateStampedFileName dsf;
            dsf = new DateStampedFileName("FileName.ext", new DateTime(2020, 1, 1));
            Assert.AreEqual(false, dsf.StampAsExtension);
            Assert.AreEqual("20200101", dsf.DateStamp);
            Assert.AreEqual("FileName_20200101.ext", dsf.ToString());
            dsf = new DateStampedFileName("FileName.ext", new DateTime(2020, 1, 1), '_', true);
            Assert.AreEqual(true, dsf.StampAsExtension);
            Assert.AreEqual("20200101", dsf.DateStamp);
            Assert.AreEqual("FileName.ext.20200101", dsf.ToString());
        }

        [TestMethod]
        public void TestDateStampedFileNameParse()
        {
            DateStampedFileName dsf;
            dsf = DateStampedFileName.Parse("FileName_20200101.ext");
            Assert.AreEqual(false, dsf.StampAsExtension);
            Assert.AreEqual("FileName.ext", dsf.FileName);
            Assert.AreEqual("20200101", dsf.DateStamp);
            dsf = DateStampedFileName.Parse("FileName.ext.20200101");
            Assert.AreEqual(true, dsf.StampAsExtension);
            Assert.AreEqual("FileName.ext", dsf.FileName);
            Assert.AreEqual("20200101", dsf.DateStamp);
        }
    }
}
