using System;
using NUnit.Framework;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Data;
using Omu.ValueInjecter;

namespace Omu.ProDinner.Tests
{
    public class MappingTest : IntegrationTestsBase
    {
        private UniRepo urepo;

        [SetUp]
        public void Start()
        {
            urepo = new UniRepo(new DbContextFactory());
        }

        [Test]
        public void AutoTest()
        {
            var types = new[]
                            {
                                typeof (Dinner), 
                                typeof (Meal), 
                                typeof (Chef), 
                                typeof (Country), 
                                typeof (User), 
                                typeof (Role), 
                            };

            foreach (var type in types)
            {
                Console.WriteLine("testing " + type.Name);
                dynamic o = Activator.CreateInstance(type).InjectFrom(new Fill(urepo));
                o = urepo.Insert(o);
                urepo.Save();
                Assert.IsTrue(o.Id != 0);
                Console.WriteLine(type.Name + " ok");
                Console.WriteLine();
            }
        }
    }
}