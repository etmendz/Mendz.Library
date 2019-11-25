using Mendz.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace UnitTest.Mendz.Library
{
    public class AsyncPersonCSVToPerson : IAsyncGenericMapper<string, Person>
    {
        public async Task<Person> MapAsync(string input, Func<Person> instance)
        {
            string[] p = input.Split(',');
            Person person;
            if (instance == null)
            {
                person = new Person();
            }
            else
            {
                person = instance();
            }
            person.ID = Convert.ToInt32(p[0]);
            person.Name = p[1];
            await Task.Delay(1);
            return person;
        }
    }

    [TestClass]
    public class UnitTestIAsyncGenericMapper
    {
        [TestMethod]
        public void TestIAsyncGenericMapper() => TestIAsyncGenericMapperAsync();

        private async void TestIAsyncGenericMapperAsync()
        {
            string personCSV = "1,Mendz";
            AsyncPersonCSVToPerson personCSVToPerson = new AsyncPersonCSVToPerson();
            Person person = await personCSVToPerson.MapAsync(personCSV, () => new Person());
            Assert.AreEqual(1, person.ID);
            Assert.AreEqual("Mendz", person.Name);
            person = await personCSVToPerson.MapAsync(personCSV);
            Assert.AreEqual(1, person.ID);
            Assert.AreEqual("Mendz", person.Name);
        }
    }
}
