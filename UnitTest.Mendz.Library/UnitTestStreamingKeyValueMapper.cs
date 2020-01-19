using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library.Conventions;
using System.Collections.Generic;

namespace UnitTest.Mendz.Library
{
    [TestClass]
    public class UnitTestStreamingKeyValueMapper
    {
        [TestMethod]
        public void TestStreamingKeyValueMapper()
        {
            StreamingKeyValueMapper<IndexedSource, IndexedTarget> spm = new StreamingKeyValueMapper<IndexedSource, 
                IndexedTarget>((t, k, s) => t[k] = s[k], 
                new List<string>() { "ID", "Name", "DOB", "Value", "Worth", "Percentile" });
            IndexedSource indexedSource = new IndexedSource();
            IndexedTarget indexedTarget = spm.Map(indexedSource, () => new IndexedTarget());
            Assert.AreEqual(indexedTarget["ID"], indexedSource["ID"]);
            Assert.AreEqual(indexedTarget["Name"], indexedSource["Name"]);
            Assert.AreEqual(indexedTarget["DOB"], indexedSource["DOB"]);
            Assert.AreEqual(indexedTarget["Value"], indexedSource["Value"]);
            Assert.AreEqual(indexedTarget["Worth"], indexedSource["Worth"]);
            Assert.AreEqual(indexedTarget["Percentile"], indexedSource["Percentile"]);
        }
    }
}
