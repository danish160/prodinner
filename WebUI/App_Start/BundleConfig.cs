using System.Web.Optimization;

namespace Omu.ProDinner.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //    "~/Content/site.css",
            //    "~/Content/awe.css")
            //    );

            bundles.Add(new ScriptBundle("~/bundle/Scripts/js").Include(
                "~/Scripts/AwesomeMvc.js",
                "~/Scripts/awem.js",
                "~/Scripts/utils.js",
                "~/Scripts/proutils.js",
                "~/Scripts/Site.js")
                );
        }
    }
}