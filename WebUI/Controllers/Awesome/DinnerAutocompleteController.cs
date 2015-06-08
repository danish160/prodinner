using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;

namespace Omu.ProDinner.WebUI.Controllers.Awesome
{
    public class DinnerAutocompleteController : Controller
    {
        private readonly IRepo<Dinner> repo;

        public DinnerAutocompleteController(IRepo<Dinner> repo)
        {
            this.repo = repo;
        }

        public ActionResult GetItems(string v, int? chef, IEnumerable<int> meals)
        {
            var query = repo.Where(o => o.Name.Contains(v));
            if (chef.HasValue) query = query.Where(o => o.ChefId == chef);
            if (meals != null) query = query.Where(o => meals.All(m => o.Meals.Select(g => g.Id).Contains(m)));
            
            var list = query.ToList();

            return Json(list.Select(i => new KeyContent { Content = i.Name, Key = i.Id, Encode = false}).Take(5));
        }
    }
}