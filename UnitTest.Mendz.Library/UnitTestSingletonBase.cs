using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library;
using System;

namespace UnitTest.Mendz.Library
{
    public class TestSingleton : SingletonBase<TestSingleton>
    {
        private DateTime _created;

        public string Info
        {
            get => Instance.ToString() + " as of " + _created.ToString();
        }

        private TestSingleton() => _created = DateTime.Now;
    }

    [TestClass]
    public class UnitTestSingletonBase
    {
        [TestMethod]
        public void TestSingletonBase()
        {
            TestSingleton ts = TestSingleton.Instance;
            Assert.IsInstanceOfType(ts, typeof(TestSingleton));
            Assert.AreSame(ts, TestSingleton.Instance);
            Assert.AreEqual(ts, TestSingleton.Instance);
            ReferenceEquals(ts, TestSingleton.Instance);
            Equals(ts.Info, TestSingleton.Instance.Info);
            TestSingleton tsx = TestSingleton.Instance;
            Assert.IsInstanceOfType(tsx, typeof(TestSingleton));
            Assert.AreSame(tsx, ts);
            Assert.AreEqual(tsx, ts);
            ReferenceEquals(tsx, ts);
            Equals(tsx.Info, ts.Info);
            Assert.IsInstanceOfType(TestSingleton.Instance, typeof(TestSingleton));
        }
    }
}
