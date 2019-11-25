using Mendz.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.Mendz.Library
{
    public class AsyncPersonCSVsToPersons : IAsyncStreamingMapper<string, Person>
    {
        public async IAsyncEnumerable<Person> MapAsync(IAsyncEnumerable<string> input, Func<Person> instance)
        {
            PersonCSVToPerson personCSVToPerson = new PersonCSVToPerson();
            await foreach (var item in input)
            {
                yield return personCSVToPerson.Map(item, instance);
            }
        }
    }

    [TestClass]
    public class UnitTestIAsyncStreamingMapper
    {
        [TestMethod]
        public void TestIStreamingMapper() => TestIStreamingMapperAsync();

        private async void TestIStreamingMapperAsync()
        {
            var persons = (new string[] { "1,Mendz", "2,AsI", "3,SeeTech" }).AsQueryable().AsAsyncEnumerable();
            int counter = 0;
            AsyncPersonCSVsToPersons personCSVsToPersons = new AsyncPersonCSVsToPersons();
            await foreach (var person in personCSVsToPersons.MapAsync(persons, () => new Person()))
            {
                counter++;
                switch (counter)
                {
                    case 1:
                        Assert.AreEqual(1, person.ID);
                        Assert.AreEqual("Mendz", person.Name);
                        break;
                    case 2:
                        Assert.AreEqual(2, person.ID);
                        Assert.AreEqual("AsI", person.Name);
                        break;
                    case 3:
                        Assert.AreEqual(3, person.ID);
                        Assert.AreEqual("SeeTech", person.Name);
                        break;
                    default:
                        break;
                }
            }
            counter = 0;
            await foreach (var person in personCSVsToPersons.MapAsync(persons))
            {
                counter++;
                switch (counter)
                {
                    case 1:
                        Assert.AreEqual(1, person.ID);
                        Assert.AreEqual("Mendz", person.Name);
                        break;
                    case 2:
                        Assert.AreEqual(2, person.ID);
                        Assert.AreEqual("AsI", person.Name);
                        break;
                    case 3:
                        Assert.AreEqual(3, person.ID);
                        Assert.AreEqual("SeeTech", person.Name);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
