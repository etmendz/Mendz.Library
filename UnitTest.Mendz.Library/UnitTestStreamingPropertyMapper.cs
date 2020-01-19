using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library.Conventions;
using System;

namespace UnitTest.Mendz.Library
{
    [TestClass]
    public class UnitTestStreamingPropertyMapper
    {
        [TestMethod]
        public void TestStreamingPropertyMapper()
        {
            StreamingPropertyMapper<Source, Target> spm = new StreamingPropertyMapper<Source, Target>();
            Source source = new Source() { ID = 1, Name = "Test", DOB = DateTime.Today, Value = 100d, Worth = 100m, Percentile = 1f };
            Target target = spm.Map(source, () => new Target());
            Assert.AreEqual(source.ID, target.ID);
            Assert.AreEqual(source.Name, target.Name);
            Assert.AreEqual(source.DOB, target.DOB);
            Assert.AreEqual(source.Value, target.Value);
            Assert.AreEqual(source.Worth, target.Worth);
            Assert.AreEqual(source.Percentile, target.Percentile);
        }
    }
}
