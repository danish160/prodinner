using System;

using FakeItEasy;
using NUnit.Framework;
using Omu.ProDinner.Core;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Service;
using Omu.ProDinner.WebUI.Mappers;
using Omu.ProDinner.WebUI.Controllers;
using Omu.ProDinner.WebUI.ViewModels.Input;

namespace Omu.ProDinner.Tests
{
    public class CruderControllerTests
    {
        CountryController c;

        [Fake]
        IProMapper mapper;
        [Fake]
        ICrudService<Country> s;

        [SetUp]
        public void SetUp()
        {
            Fake.InitializeFixture(this);
            c = new CountryController(s, mapper);
        }

        [Test]
        public void Testcs()
        {
            var s = "123.1231231239872371973832";
            var d = Convert.ToDouble(s);
            var i = (int)d;
            Assert.AreEqual(123, i);
        }

        [Test]
        public void CreateShouldBuildNewInput()
        {
            c.Create();
            A.CallTo(() => mapper.Map<Country, CountryInput>(A<Country>.Ignored, null)).MustHaveHappened();
        }

        [Test]
        public void CreateShouldReturnViewForInvalidModelstate()
        {
            c.ModelState.AddModelError("", "");
            c.Create(A.Fake<CountryInput>(), null).ShouldBeViewResult();
        }

        [Test]
        public void EditShouldReturnCreateView()
        {
            A.CallTo(() => s.Get(1)).Returns(A.Fake<Country>());
            c.Edit(1).ShouldBeViewResult().ShouldBeCreate();
            A.CallTo(() => s.Get(1)).MustHaveHappened();
        }

        [Test]
        public void EditShouldReturnViewForInvalidModelstate()
        {
            c.ModelState.AddModelError("", "");
            c.Edit(A.Fake<CountryInput>(), null).ShouldBeViewResult().ShouldBeCreate();
        }

        [Test]
        public void EditShouldReturnContentOnError()
        {
            A.CallTo(() => mapper.Map<CountryInput, Country>(A<CountryInput>.Ignored, A<Country>.Ignored)).Throws(new ProDinnerException("aa"));
            c.Edit(A.Fake<CountryInput>(), null).ShouldBeContent().Content.ShouldEqual("aa");
        }

        [Test]
        public void DeleteShouldReturnView()
        {
            c.Delete(1, "").ShouldBeViewResult();
        }
    }
}
