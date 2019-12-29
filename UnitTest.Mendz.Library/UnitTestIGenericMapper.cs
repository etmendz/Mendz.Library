using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mendz.Library;
using System;

namespace UnitTest.Mendz.Library
{
    public class PersonCSVToPerson : IGenericMapper<string, Person>
    {
        public Person Map(string input, Func<Person> instance)
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
            return person;
        }
    }

    [TestClass]
    public class UnitTestIGenericMapper
    {
        [TestMethod]
        public void TestIGenericMapper()
        {
            string personCSV = "1,Mendz";
            PersonCSVToPerson personCSVToPerson = new PersonCSVToPerson();
            Person person = personCSVToPerson.Map(personCSV, () => new Person());
            Assert.AreEqual(1, person.ID);
            Assert.AreEqual("Mendz", person.Name);
            person = personCSVToPerson.Map(personCSV);
            Assert.AreEqual(1, person.ID);
            Assert.AreEqual("Mendz", person.Name);
        }
    }
}
