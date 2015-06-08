using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;

namespace Omu.ProDinner.WebUI.Controllers.Awesome
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

    public class ChefsAjaxListController : Controller
    {
        private readonly IRepo<Chef> repo;

        public ChefsAjaxListController(IRepo<Chef> repo)
        {
            this.repo = repo;
        }

        public ActionResult Search(string search, int page, bool isTheadEmpty)
        {
            var list = repo.Where(o => (o.FirstName + " " + o.LastName).Contains(search), User.IsInRole("admin"))
                .OrderByDescending(o => o.Id);

            var result = new AjaxListResult
                             {
                                 Content = this.RenderView("ListItems/Chef", list.Page(page, 10).ToList()),
                                 More = list.Count() > page * 10
                             };
            if (isTheadEmpty) result.Thead = this.RenderView("ListItems/ChefThead");

            return Json(result);
        }
    }

    public class CountriesAjaxListController : Controller
    {
        private readonly IRepo<Country> repo;

        public CountriesAjaxListController(IRepo<Country> repo)
        {
            this.repo = repo;
        }

        public ActionResult Search(string search, int page)
        {
            var list = repo.Where(o => o.Name.StartsWith(search), User.IsInRole("admin"))
                .OrderByDescending(o => o.Id);

            var result = new AjaxListResult
                             {
                                 Content = this.RenderView("ListItems/Country", list.Page(page, 10).ToList()),
                                 More = list.Count() > page * 10
                             };

            return Json(result);
        }
    }

    public class UsersAjaxListController : Controller
    {
        private readonly IRepo<User> repo;

        public UsersAjaxListController(IRepo<User> repo)
        {
            this.repo = repo;
        }

        public ActionResult Search(string search, int page, bool isTheadEmpty)
        {
            var list = repo.Where(o => o.Login.StartsWith(search), User.IsInRole("admin")).OrderByDescending(o => o.Id);
            var result = new AjaxListResult
            {
                Content = this.RenderView("ListItems/User", list.Page(page, 10).ToList()),
                More = list.Count() > page * 10
            };
            if (isTheadEmpty) result.Thead = this.RenderView("ListItems/UserThead");

            return Json(result);
        }
    }

    public class FeedbackAjaxListController : Controller
    {
        private readonly IRepo<Feedback> repo;

        public FeedbackAjaxListController(IRepo<Feedback> repo)
        {
            this.repo = repo;
        }

        public ActionResult Search(string search, int page)
        {
            var list = repo.GetAll().OrderByDescending(o => o.Id);

            var result = new AjaxListResult
            {
                Content = this.RenderView("ListItems/Feedback", list.Page(page, 10).ToList()),
                More = list.Count() > page * 10
            };

            return Json(result);
        }
    }
}