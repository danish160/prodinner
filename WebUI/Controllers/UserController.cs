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
    [Authorize(Roles = "admin")]
    public class UserController : Crudere<User, UserCreateInput, UserEditInput>
    {
        private new readonly IUserService service;

        public UserController(IMapper<User, UserCreateInput> v, IMapper<User, UserEditInput> ve, IUserService service)
            : base(service, v, ve)
        {
            this.service = service;
        }

        protected override object MapEntityToGridModel(User user)
        {
            return new
                    {
                        user.Id,
                        user.IsDeleted,
                        user.Login,
                        Roles = string.Join(", ", user.Roles.Select(o => o.Name))
                    };
        }

        public ActionResult GridGetItems(GridParams g, string parent, bool? restore)
        {
            var isAdmin = User.IsInRole("admin");

            var data = service.Where(o => o.Login.StartsWith(parent), isAdmin);

            if (restore.HasValue && isAdmin)
            {
                service.Restore(Convert.ToInt32(g.Key));
            }

            var model = new GridModelBuilder<User>(data.AsQueryable(), g)
            {
                Key = "Id",
                Map = MapEntityToGridModel,
                GetItem = () => service.Get(Convert.ToInt32(g.Key))
            }.Build();

            return Json(model);
        }

        public ActionResult ChangePassword(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordInput input)
        {
            if (!ModelState.IsValid) return View(input);
            service.ChangePassword(input.Id, input.Password);
            return Json(new { Login = service.Get(input.Id).Login });
        }
    }
}