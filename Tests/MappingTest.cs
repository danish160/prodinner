using System;
using NUnit.Framework;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.Data;
using System.Collections.Generic;
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

    public class Fill : NoSourceValueInjection
    {
        private readonly IUniRepo urepo;

        private static long s;
        private static int i;
        private readonly bool isChild;

        public Fill(IUniRepo urepo, bool isChild = false)
        {
            this.urepo = urepo;
            this.isChild = isChild;
        }

        protected override void Inject(object target)
        {
            var props = target.GetProps();
            for (var j = 0; j < props.Count; j++)
            {
                var p = props[j];
                if (p.PropertyType == typeof(string)) p.SetValue(target, "a" + ++s);
                else if (p.PropertyType == typeof(int) && !p.Name.EndsWith("Id")) p.SetValue(target, ++i);
                else if (p.PropertyType == typeof(DateTime)) p.SetValue(target, DateTime.Now);
                else if (p.PropertyType.IsSubclassOf(typeof(Entity)))
                {
                    Console.WriteLine("   create a " + p.PropertyType.Name);
                    dynamic o = Activator.CreateInstance(p.PropertyType).InjectFrom(new Fill(urepo, true));
                    o = urepo.Insert(o);
                    urepo.Save();
                    p.SetValue(target, o);
                }
                else if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>) && !isChild)
                {
                    var t = p.PropertyType.GetGenericArguments()[0];
                    if (!t.IsSubclassOf(typeof(DelEntity))) continue;

                    var tlist = typeof(List<>).MakeGenericType(t);
                    dynamic list = Activator.CreateInstance(tlist);
                    Console.WriteLine("   creating a list of " + t.Name);
                    for (var k = 0; k < 3; k++)
                    {
                        Console.WriteLine("      create a " + t.Name);
                        dynamic o = Activator.CreateInstance(t).InjectFrom(new Fill(urepo, true));
                        o = urepo.Insert(o);
                        urepo.Save();
                        Console.WriteLine("      add " + t.Name  + " to list");
                        list.Add(o);
                    }
                    p.SetValue(target, list);
                }
            }
        }
    }
}