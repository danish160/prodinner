using System.Linq;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.Resources;

namespace Omu.ProDinner.WebUI.Controllers.Awesome
{
    public class DataController : Controller
    {
        private readonly IUniRepo repo;

        public DataController(IUniRepo repo)
        {
            this.repo = repo;
        }

        public ActionResult GetChefs(bool? any)
        {
            var items = repo.GetAll<Chef>().ToArray()
                .Select(o => new KeyContent(o.Id, string.Format("{0} {1}", o.FirstName, o.LastName))).ToList();

            if (any.HasValue)
            {
                items.Insert(0, new KeyContent("", Mui.any));
            }

            return Json(items);
        }

        public ActionResult GetCountries()
        {
            var items = repo.GetAll<Country>().ToArray()
                .Select(o => new KeyContent(o.Id, o.Name));
            return Json(items);
        }
    }
}