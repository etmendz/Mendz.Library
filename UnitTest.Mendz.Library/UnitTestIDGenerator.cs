using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library;
using System;

namespace UnitTest.Mendz.Library
{
    [TestClass]
    public class UnitTestIDGenerator
    {
        [TestMethod]
        public void TestIDGenerator()
        {
            IDGenerator idg = new IDGenerator();
            Assert.AreEqual(1, idg.ID);
        }

        [TestMethod]
        public void TestIDGeneratorSeed()
        {
            IDGenerator idg = new IDGenerator(101);
            Assert.AreEqual(101, idg.ID);
        }

        [TestMethod]
        public void TestIDGeneratorSeedOne()
        {
            IDGenerator idg = new IDGenerator(1);
            Assert.AreEqual(1, idg.ID);
        }

        [TestMethod]
        public void TestIDGeneratorSeedZero()
        {
            IDGenerator idg;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                idg = new IDGenerator(0);
            });
        }

        [TestMethod]
        public void TestIDGeneratorSeedNegative()
        {
            IDGenerator idg;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                idg = new IDGenerator(-1);
            });
        }

        [TestMethod]
        public void TestIDGenerator_Seed()
        {
            IDGenerator idg = new IDGenerator();
            Assert.AreEqual(1, idg.ID);
            idg.Seed(101);
            Assert.AreEqual(101, idg.ID);
        }

        [TestMethod]
        public void TestIDGenerator_SeedEqualCurrentID()
        {
            IDGenerator idg = new IDGenerator();
            Assert.AreEqual(1, idg.ID);
            idg.Seed(1);
            Assert.AreEqual(1, idg.ID);
        }

        [TestMethod]
        public void TestIDGenerator_SeedLessThanCurrentID()
        {
            IDGenerator idg = new IDGenerator(101);
            Assert.AreEqual(101, idg.ID);
            idg.Seed(100);
            Assert.AreEqual(101, idg.ID);
        }

        [TestMethod]
        public void TestIDGenerator_SeedZero()
        {
            IDGenerator idg = new IDGenerator();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                idg.Seed(0);
            });
        }

        [TestMethod]
        public void TestIDGenerator_SeedNegative()
        {
            IDGenerator idg = new IDGenerator();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                idg.Seed(-1);
            });
        }

        [TestMethod]
        public void TestIDGenerator_Generate()
        {
            IDGenerator idg = new IDGenerator();
            Assert.AreEqual(1, idg.ID);
            Assert.AreEqual(2, idg.Generate());
            Assert.AreEqual(2, idg.ID);
            idg.Generate();
            Assert.AreEqual(3, idg.ID);
        }

        [TestMethod]
        public void TestIDGenerator_GenerateAfterSeed()
        {
            IDGenerator idg = new IDGenerator();
            Assert.AreEqual(1, idg.ID);
            idg.Seed(101);
            Assert.AreEqual(101, idg.ID);
            Assert.AreEqual(102, idg.Generate());
            idg.Generate();
            Assert.AreEqual(103, idg.ID);
        }
    }
}
