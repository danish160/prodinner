using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Omu.ProDinner.WebUI.Controllers
{
    public class ChangeThemeController : Controller
    {
        private const string DefaultTheme = "gui2";

        private const string CookieName = "prodinner83theme";

        //theme, jqueryUiTheme
        static readonly IDictionary<string, string> Themes = new Dictionary<string, string>
            {
                {"bui", "smoothness"}, 
                {"met", "smoothness"}, 
                {"gui","smoothness"},
                {"gui2", "smoothness"}, 
                {"gui3", "start"},
                {"compact", "smoothness"}, 
                {"start","start"}, 
                {"black-tie","black-tie"}, 
            };

        public ActionResult Index()
        {
            var currentTheme = DefaultTheme;

            if (Request.Cookies[CookieName] != null)
                currentTheme = Request.Cookies[CookieName].Value;

            return View(Themes.Select(theme => new SelectListItem
            {
                Text = theme.Key,
                Value = theme.Key + "|" + theme.Value,
                Selected = theme.Key == currentTheme
            }));
        }

        [HttpPost]
        public ActionResult Change(string s)
        {
            Response.Cookies.Add(new HttpCookie(CookieName, s) { Expires = DateTime.Now.AddDays(30) });
            return new EmptyResult();
        }

        public static string GetTheme()
        {
            var request = System.Web.HttpContext.Current.Request;
            var s = DefaultTheme;
            if (request.Cookies[CookieName] != null && Themes.ContainsKey(request.Cookies[CookieName].Value))
            {
                s = request.Cookies[CookieName].Value;
            }

            return s;
        }

        public static string GetJqTheme()
        {
            var request = System.Web.HttpContext.Current.Request;
            var s = DefaultTheme;
            if (request.Cookies[CookieName] != null && Themes.ContainsKey(request.Cookies[CookieName].Value))
            {
                s = request.Cookies[CookieName].Value;
            }

            return Themes[s];
        }
    }
}