using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library.Conventions;
using System;

namespace UnitTest.Mendz.Library
{
    [TestClass]
    public class UnitTestStreamingFromKeyValueMapper
    {
        [TestMethod]
        public void TestStreamingFromKeyValueMapper()
        {
            StreamingFromKeyValueMapper<IndexedSource, Target> spm = new StreamingFromKeyValueMapper<IndexedSource, Target>((s, k) => s[k]);
            IndexedSource indexedSource = new IndexedSource();
            Target target = spm.Map(indexedSource, () => new Target());
            Assert.AreEqual(indexedSource["ID"], target.ID);
            Assert.AreEqual(indexedSource["Name"], target.Name);
            Assert.AreEqual(indexedSource["DOB"], target.DOB);
            Assert.AreEqual(indexedSource["Value"], target.Value);
            Assert.AreEqual(indexedSource["Worth"], target.Worth);
            Assert.AreEqual(indexedSource["Percentile"], target.Percentile);
        }
    }
}
