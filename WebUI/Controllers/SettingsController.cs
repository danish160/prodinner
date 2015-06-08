using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Omu.AwesomeMvc;
using Omu.ProDinner.WebUI.Utils;
using Omu.ProDinner.WebUI.ViewModels.Display;
using Omu.ProDinner.WebUI.ViewModels.Input;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class SettingsController : Controller
    {
        private const string DefaultTheme = "start";

        private const string MobileDefaultTheme = "start";

        private const string CookieName = "prodinner";

        private static readonly string[] Themes = new[]
            {
                "wui", 
                "bui",
                "met",
                "gui",
                "gui2",
                "gui3",
                "start",
                "black-tie"
            };

        readonly IDictionary<string, string> langs = new Dictionary<string, string>
                                                    {
                                                        {"en","english"},
                                                        {"fr","francais"},
                                                        {"es","español"},
                                                        {"ro","română"},
                                                        {"de","deutsch"},
                                                        {"ru","русский"},
                                                        {"it","italiano"},
                                                        {"auto","auto"},//browser default
                                                    };

        public ActionResult Index()
        {
            var settings = ReadSettings(HttpContext.ApplicationInstance.Request);

            var langCookie = Request.Cookies["lang"];
            var lang = langCookie == null ? "auto" : langCookie.Value;

            var input = new SettingsInput
            {
                Themes = Themes.Select(theme => new KeyContent(theme, theme)),
                SelectedTheme = settings.Theme,
                Langs = langs.Select(o => new KeyContent(o.Key, o.Value)),
                SelectedLang = lang
            };

            return View(input);
        }

        [HttpPost]
        public ActionResult Change(string theme)
        {
            Response.Cookies.Add(new HttpCookie(CookieName, theme) { Expires = DateTime.Now.AddDays(30) });
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult ChangeLang(string l)
        {
            var cookie = new HttpCookie("lang") { Value = l, Expires = DateTime.Now.AddYears(1) };

            Response.Cookies.Add(cookie);

            return Content("");
        }

        public static SettingsVal ReadSettings(HttpRequest request)
        {
            var settings = new SettingsVal();

            if (ClientUtils.IsMobile())
            {
                settings.Theme = MobileDefaultTheme;
            }

            if (request.Cookies[CookieName] != null)
            {
                settings.Theme = request.Cookies[CookieName].Value;
            }

            if (string.IsNullOrWhiteSpace(settings.Theme))
            {
                settings.Theme = DefaultTheme;
            }

            return settings;
        }
    }
}