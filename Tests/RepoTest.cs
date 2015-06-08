using NUnit.Framework;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Data;

namespace Omu.ProDinner.Tests
{
    public class RepoTest : IntegrationTestsBase
    {
        [Test]
        public void TestInsert()
        {
            var repo = new Repo<Country>(new DbContextFactory());
            var country = new Country { Name = "testCountry" };
            country = repo.Insert(country);
            repo.Save();
            var country1 = repo.Get(country.Id);
            Assert.AreEqual(country.Name, country1.Name);
        }

        [Test]
        public static void TestRemove()
        {
            var repo = new Repo<Country>(new DbContextFactory());
            var country = new Country { Name = "testCountry" };
            country = repo.Insert(country);
            repo.Save();

            repo.Delete(country);
            repo.Save();

            var country1 = repo.Get(country.Id);
            Assert.IsTrue(country1.IsDeleted);
        }

        [Test]
        public static void TestUpdate()
        {
            var repo = new Repo<Country>(new DbContextFactory());
            var country = new Country { Name = "testCountry" };
            country = repo.Insert(country);
            repo.Save();

            country.Name = "changedName";
            repo.Save();

            var country1 = repo.Get(country.Id);
            Assert.AreEqual(country.Name, country1.Name);
        }
    }
}