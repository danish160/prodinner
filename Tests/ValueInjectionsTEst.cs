using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using NUnit.Framework;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.Data;
using Omu.ProDinner.Infra;
using Omu.ProDinner.WebUI.Mappers.Injections;
using Omu.ProDinner.WebUI.ViewModels.Input;
using Omu.ValueInjecter;

namespace Omu.ProDinner.Tests
{
    public class ValueInjectionsTest
    {
        [Test]
        public void EntitiesToIntsTest()
        {
            var dinner = new Dinner { Meals = new List<Meal> { new Meal { Id = 3 }, new Meal { Id = 7 } } };

            var dinnerInput = new DinnerInput();

            dinnerInput.InjectFrom<EntitiesToInts>(dinner);

            Assert.IsNotNull(dinnerInput.Meals);
            Assert.AreEqual(2, dinnerInput.Meals.Count());
            Assert.AreEqual(3, dinnerInput.Meals.First());
        }

        [Test]
        public void IntsToEntities()
        {
            WindsorRegistrar.RegisterSingleton(typeof(IRepo<>), typeof(Repo<>));
            WindsorRegistrar.RegisterSingleton(typeof(IDbContextFactory), typeof(DbContextFactory));
            using (var scope = new TransactionScope())
            {
                var repo = new Repo<Meal>(new DbContextFactory());
                var meal1 = new Meal { Name = "a" };
                var meal2 = new Meal { Name = "b" };

                meal1 = repo.Insert(meal1);
                meal2 = repo.Insert(meal2);
                repo.Save();

                var dinnerInput = new DinnerInput { Meals = new int[] { meal1.Id, meal2.Id } };
                var dinner = new Dinner();

                dinner.InjectFrom<IntsToEntities>(dinnerInput);

                Assert.IsNotNull(dinner.Meals);
                Assert.AreEqual(2, dinner.Meals.Count);
                Assert.AreEqual(meal1.Id, dinner.Meals.First().Id);
            }
        }

        [Test]
        public void NormalToNullables()
        {
            var dinner = new Dinner { ChefId = 3 };
            var dinnerInput = new DinnerInput();

            dinnerInput.InjectFrom<NormalToNullables>(dinner);

            Assert.AreEqual(3, dinnerInput.ChefId);
            Assert.AreEqual(null, dinnerInput.Start);
            Assert.AreEqual(null, dinnerInput.CountryId);
        }

        [Test]
        public void NullablesToNormal()
        {
            var dinnerInput = new DinnerInput { ChefId = 3 };
            var dinner = new Dinner();

            dinner.InjectFrom<NullablesToNormal>(dinnerInput);

            Assert.AreEqual(3, dinner.ChefId);
            Assert.AreEqual(0, dinner.CountryId);
            Assert.AreEqual(default(DateTime), dinner.Start);
        }
    }
}