using System;
using System.Linq;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Service;
using Omu.ProDinner.WebUI.Dto;
using Omu.ProDinner.WebUI.Mappers;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class CountryController : Cruder<Country, CountryInput>
    {
        public CountryController(ICrudService<Country> service, IMapper<Country, CountryInput> v)
            : base(service, v)
        {
        }

        public ActionResult GridGetItems(GridParams g, string parent, bool? restore)
        {
            var isAdmin = User.IsInRole("admin");

            var data = service.Where(o => o.Name.StartsWith(parent), isAdmin);

            if (restore.HasValue && isAdmin)
            {
                service.Restore(Convert.ToInt32(g.Key));
            }

            var model = new GridModelBuilder<Country>(data.AsQueryable(), g)
            {
                Key = "Id",
                Map = MapEntityToGridModel,
                GetItem = () => service.Get(Convert.ToInt32(g.Key))
            }.Build();

            return Json(model);
        }

        /// <summary>
        /// view name for CountryLookup to render items
        /// </summary>
        protected override string RowViewName
        {
            get
            {
                return "ListItems/Country";
            }
        }
    }
}