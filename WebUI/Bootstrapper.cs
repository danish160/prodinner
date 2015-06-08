using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Omu.ProDinner.Infra;

namespace Omu.ProDinner.WebUI
{
    public class Bootstrapper
    {
        public static void Bootstrap()
        {
            RouteConfigurator.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.Container));
            WindsorConfigurator.Configure();
            AwesomeConfigurator.Configure();

            Globals.PicturesPath = HttpContext.Current.Server.MapPath("~/pictures");
            new Worker().Start();
        }
    }
}