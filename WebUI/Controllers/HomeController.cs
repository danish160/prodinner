using System.Web.Mvc;

using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Service;
using Omu.ProDinner.WebUI.Mappers;
using Omu.ProDinner.WebUI.ViewModels.Input;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class HomeController: Cruder<Dinner,DinnerInput>
    {
        public HomeController(ICrudService<Dinner> service, IProMapper v) : base(service, v)
        {
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ShowGrid()
        {
            return View();
        }

        protected override string RowViewName
        {
            get { return "ListItems/Dinner"; }
        }
    }
}