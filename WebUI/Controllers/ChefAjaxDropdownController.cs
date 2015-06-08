using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.Resources;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class ChefIdAjaxDropdownController : Controller
    {
        private readonly IRepo<Chef> repo;

        public ChefIdAjaxDropdownController(IRepo<Chef> repo)
        {
            this.repo = repo;
        }

        public ActionResult GetItems(int? v)
        {
            var list = new List<SelectableItem> { new SelectableItem ("", Mui.not_selected) };


            list.AddRange(repo.GetAll().ToArray().Select(o => new SelectableItem
                                                     {
                                                         Text = string.Format("{0} {1}",o.FirstName,o.LastName),
                                                         Value = o.Id.ToString(),
                                                         Selected = o.Id == v
                                                     }));
            return Json(list);
        }
    }
}