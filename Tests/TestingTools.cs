using System.Web.Mvc;
using NUnit.Framework;

namespace Omu.ProDinner.Tests
{
    public static class TestingTools
    {
        public static void ShouldEqual(this object o, object to)
        {
            Assert.AreEqual(to, o);
        }

        public static void ShouldBeTrue(this bool o)
        {
            Assert.IsTrue(o);
        }

        public static void IsNotNull(this object o)
        {
            Assert.IsNotNull(o);
        }

        public static void IsNull(this object o)
        {
            Assert.IsNull(o);
        }

        public static ViewResult ShouldBeViewResult(this ActionResult o)
        {
            Assert.IsNotNull(o);
            Assert.IsTrue(o.GetType() == typeof(ViewResult));
            return o as ViewResult;
        }

        public static ViewResult ShouldBeCreate(this ViewResult o)
        {
            Assert.AreEqual("create", o.ViewName);
            return o;
        }

        public static void ShouldBeJson(this ActionResult o)
        {
            Assert.IsNotNull(o);
            Assert.IsTrue(o.GetType() == typeof(JsonResult));
        }

        public static ContentResult ShouldBeContent(this ActionResult o)
        {
            Assert.IsNotNull(o);
            Assert.IsTrue(o.GetType() == typeof(ContentResult));
            return o as ContentResult;
        }

        public static void ShouldRedirectToAction(this ActionResult o, string action)
        {
            Assert.IsNotNull(o);
            Assert.IsTrue(o.GetType() == typeof(RedirectToRouteResult));
            Assert.AreEqual(action, (o as RedirectToRouteResult).RouteValues["action"].ToString());
        }
    }
}