using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Omu.ProDinner.Infra;
using Omu.ProDinner.WebUI.Mappers;
using Omu.ProDinner.WebUI.Utils;

namespace Omu.ProDinner.WebUI.App_Start
{
    public class Bootstrapper
    {
        public static void Bootstrap()
        {
            RouteConfigurator.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(IoC.Container));
            WindsorConfig.Configure();
            AwesomeConfig.Configure();
            MapperConfig.Configure();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Globals.PicturesPath = HttpContext.Current.Server.MapPath("~/pictures");
            new Worker().Start();
        }
    }
}