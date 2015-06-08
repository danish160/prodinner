using System;
using System.Linq;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Repository;
using Omu.ProDinner.Core.Service;
using Omu.ProDinner.WebUI.Mappers;
using Omu.ProDinner.WebUI.ViewModels.Input;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class ChefController : Cruder<Chef, ChefInput>
    {
        private readonly IRepo<Chef> chefRepo;

        public ChefController(ICrudService<Chef> service, IProMapper mapper, IRepo<Chef> chefRepo)
            : base(service, mapper)
        {
            this.chefRepo = chefRepo;
        }

        protected override object MapEntityToGridModel(Chef chef)
        {
            return new { chef.Id, chef.FirstName, chef.LastName, Country = chef.Country.Name, chef.IsDeleted };
        }

        [HttpPost]
        public ActionResult GridGetItems(GridParams g, string parent, bool? restore)
        {
            var isAdmin = User.IsInRole("admin");

            var data = chefRepo.Where(o => o.FirstName.StartsWith(parent) || o.LastName.StartsWith(parent), isAdmin);

            if (restore.HasValue && isAdmin)
            {
                service.Restore(Convert.ToInt32(g.Key));
            }

            var model = new GridModelBuilder<Chef>(data.AsQueryable(), g)
                    {
                        Key = "Id",
                        Map = MapEntityToGridModel,
                        GetItem = () => chefRepo.Get(Convert.ToInt32(g.Key))
                    }.Build();

            return Json(model);
        }
    }
}