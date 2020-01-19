using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library.Conventions;
using System;

namespace UnitTest.Mendz.Library
{
    [TestClass]
    public class UnitTestStreamingToKeyValueMapper
    {
        [TestMethod]
        public void TestStreamingToKeyValueMapper()
        {
            StreamingToKeyValueMapper<Source, IndexedTarget> spm = new StreamingToKeyValueMapper<Source, IndexedTarget>((t, k, v) => t[k] = v);
            Source source = new Source() { ID = 1, Name = "Test", DOB = DateTime.Today, Value = 100, Worth = 100, Percentile = 1 };
            IndexedTarget indexedTarget = spm.Map(source, () => new IndexedTarget());
            Assert.AreEqual(indexedTarget["ID"], source.ID);
            Assert.AreEqual(indexedTarget["Name"], source.Name);
            Assert.AreEqual(indexedTarget["DOB"], source.DOB);
            Assert.AreEqual(indexedTarget["Value"], source.Value);
            Assert.AreEqual(indexedTarget["Worth"], source.Worth);
            Assert.AreEqual(indexedTarget["Percentile"], source.Percentile);
        }
    }
}
