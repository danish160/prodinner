using System.Linq;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.WebUI.Utils;

namespace Omu.ProDinner.WebUI.Controllers.Awesome.AjaxList
{
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