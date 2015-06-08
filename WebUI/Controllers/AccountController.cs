using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

using Omu.ProDinner.Core.Model;
using Omu.ProDinner.Core.Service;
using Omu.ProDinner.WebUI.ViewModels.Input;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        private IAuthenticationManager Authentication
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void SignInOwin(string name, bool rememberMe, IEnumerable<string> roles)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, name) },
                DefaultAuthenticationTypes.ApplicationCookie,
                    ClaimTypes.Name, ClaimTypes.Role);

            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }


            Authentication.SignIn(new AuthenticationProperties
            {
                IsPersistent = rememberMe
            }, identity);
        }

        public ActionResult SignIn()
        {
            return View(new SignInInput { Login = "o", Password = "1" });
        }

        [HttpPost]
        public ActionResult SignIn(SignInInput input)
        {
            if (!ModelState.IsValid)
            {
                input.Password = null;
                input.Login = null;
                return View(input);
            }

            var user = userService.Get(input.Login, input.Password);

            //ACHTUNG: remove this in a real app
            if (input.Login == "o" && input.Password == "1")
            {
                user = new User { Login = "o", Roles = new[] { new Role { Name = "admin" } } };
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Try Login: o and Password: 1");
                return View();
            }

            SignInOwin(user.Login, input.Remember, user.Roles.Select(o => o.Name));


            return RedirectToAction("index", "home");
        }

        public ActionResult SignOff()
        {
            Authentication.SignOut();
            return RedirectToAction("SignIn", "Account");
        }
    }
}