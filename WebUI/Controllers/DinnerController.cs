using System;
using System.Linq;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Service;
using Omu.ProDinner.WebUI.Mappers;
using Omu.ProDinner.WebUI.ViewModels.Input;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class DinnerController : Cruder<Dinner, DinnerInput>
    {
        public DinnerController(ICrudService<Dinner> service, IProMapper mapper)
            : base(service, mapper)
        {
        }

        protected override object MapEntityToGridModel(Dinner o)
        {
            return new
                {
                    o.Id, 
                    o.IsDeleted, 
                    o.Name,
                    CountryName = o.Country.Name,
                    ChefName = o.Chef.FirstName + " " + o.Chef.LastName,
                    o.Address,
                    MealsCount = o.Meals.Count
                };
        }

        [HttpPost]
        public ActionResult GridGetItems(GridParams g, string parent, bool? restore)
        {
            var isAdmin = User.IsInRole("admin");

            var data = service.Where(o => o.Name.Contains(parent), isAdmin);

            if (restore.HasValue && isAdmin)
            {
                service.Restore(Convert.ToInt32(g.Key));
            }

            var model = new GridModelBuilder<Dinner>(data.AsQueryable(), g)
                {
                    Key = "Id",
                    Map = MapEntityToGridModel,
                    GetItem = () => service.Get(Convert.ToInt32(g.Key))
                }.Build();

            return Json(model);
        }

        public ActionResult Details(int key)
        {
            var dinner = service.Get(key);
            return View(dinner);
        }
    }
}