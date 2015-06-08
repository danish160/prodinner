using System.Linq;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;

namespace Omu.ProDinner.WebUI.Controllers.Awesome.MultiLookup
{
    public class RolesMultiLookupController : Controller
    {
        private readonly IRepo<Role> repo;

        public RolesMultiLookupController(IRepo<Role> repo)
        {
            this.repo = repo;
        }

        public ActionResult SearchForm()
        {
            return Content("");
        }

        [HttpPost]
        public ActionResult Search(int[] selected)
        {
            var r = repo.GetAll();
            if (selected != null) r = r.Where(o => !selected.Contains(o.Id));

            return Json(new AjaxListResult { Items = r.ToArray().Select(o => new KeyContent(o.Id, o.Name)) });
        }

        public ActionResult Selected(int[] selected)
        {
            selected = selected ?? new int[] {};
            var r = repo.GetAll().Where(o => selected.Contains(o.Id)).ToArray();

            return Json(new AjaxListResult { Items = r.Select(o => new KeyContent(o.Id, o.Name)) });
        }

        public ActionResult GetItems(int[] v)
        {
            return Json(repo.GetAll()
                .Where(o => v.Contains(o.Id))
                .ToArray()
                .Select(o => new KeyContent(o.Id, o.Name)));
        }
    }

}