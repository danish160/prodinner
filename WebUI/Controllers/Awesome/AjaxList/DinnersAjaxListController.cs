using System.Linq;
using System.Threading;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.WebUI.Utils;

namespace Omu.ProDinner.WebUI.Controllers.Awesome.AjaxList
{
    public class DinnersAjaxListController : Controller
    {
        private readonly IRepo<Dinner> repo;

        public DinnersAjaxListController(IRepo<Dinner> repo)
        {
            this.repo = repo;
        }

        public ActionResult Search(string search, int? chef, int[] meals, int page)
        {
            var list = repo.Where(o => o.Name.Contains(search), User.IsInRole("admin"));

            if (chef.HasValue) list = list.Where(o => o.ChefId == chef.Value);
            if (meals != null) list = list.Where(o => meals.All(m => o.Meals.Select(g => g.Id).Contains(m)));
            list = list.OrderByDescending(o => o.Id);

            return Json(new AjaxListResult
            {
                Content = this.RenderView("ListItems/Dinner", list.Page(page, 7).ToList()),
                More = list.Count() > page * 7
            });
        }
    }
}