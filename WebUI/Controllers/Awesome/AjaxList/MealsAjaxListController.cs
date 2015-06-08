using System.Linq;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.WebUI.Utils;

namespace Omu.ProDinner.WebUI.Controllers.Awesome.AjaxList
{
    public class MealsAjaxListController : Controller
    {
        private readonly IRepo<Meal> repo;

        public MealsAjaxListController(IRepo<Meal> repo)
        {
            this.repo = repo;
        }

        public ActionResult Search(string search, int page, int? pageSize)
        {
            pageSize = pageSize ?? 10;
            var list = repo.Where(o => o.Name.Contains(search), User.IsInRole("admin")).OrderByDescending(o => o.Id);

            return Json(new AjaxListResult
                {
                    Content = this.RenderView("ListItems/Meal", list.Page(page, pageSize.Value).ToList()),
                    More = list.Count() > page * pageSize
                });
        }
    }
}