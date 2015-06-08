using System.Web.Mvc;

using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.WebUI.Utils;
using Omu.ProDinner.WebUI.ViewModels.Input;
using Omu.ValueInjecter;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IRepo<Feedback> repo;

        public FeedbackController(IRepo<Feedback> repo)
        {
            this.repo = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View(new FeedbackInput());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FeedbackInput input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            input.Comments = HtmlUtil.SanitizeHtml(input.Comments);
            
            var feedback = new Feedback { Comments = input.Comments };
            feedback = repo.Insert(feedback);
            repo.Save();

            Session["lastFeedback"] = feedback.Id;
            return Json(new { });
        }

        public ActionResult Edit(int id)
        {
            return View("Create", new FeedbackInput().InjectFrom(repo.Get(id)));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FeedbackInput input)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", input);
            }

            var feedback = repo.Get(input.Id);
            feedback.Comments = HtmlUtil.SanitizeHtml(input.Comments);
            repo.Save();

            return Json(new { });
        }
    }
}