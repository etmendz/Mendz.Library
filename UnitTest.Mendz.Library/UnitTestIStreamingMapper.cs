using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library;
using System;
using System.Collections.Generic;

namespace UnitTest.Mendz.Library
{
    public class PersonCSVsToPersons : IStreamingMapper<string, Person>
    {
        public IEnumerable<Person> Map(IEnumerable<string> input, Func<Person> instance)
        {
            PersonCSVToPerson personCSVToPerson = new PersonCSVToPerson();
            foreach (var item in input)
            {
                yield return personCSVToPerson.Map(item, instance);
            }
        }
    }

    [TestClass]
    public class UnitTestIStreamingMapper
    {
        [TestMethod]
        public void TestIStreamingMapper()
        {
            string[] persons = new string[] { "1,Mendz", "2,AsI", "3,SeeTech" };
            int counter = 0;
            PersonCSVsToPersons personCSVsToPersons = new PersonCSVsToPersons();
            foreach (var person in personCSVsToPersons.Map(persons, () => new Person()))
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
            foreach (var person in personCSVsToPersons.Map(persons))
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
