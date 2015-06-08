using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;

using Omu.ProDinner.WebUI.App_Start;
using Omu.ProDinner.WebUI.Controllers;

namespace Omu.ProDinner.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Bootstrapper.Bootstrap();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var c = Request.Cookies["lang"];
            if (c == null || c.Value == "auto") return;

            var l = c.Value;

            // Uses WebForms code to apply "auto" culture to current thread and deal with
            // invalid culture requests automatically. Defaults to en-US when not specified.
            using (var fakePage = new Page())
            {
                var ignored = fakePage.Server; // Work around a WebForms quirk
                fakePage.Culture = l; // Apply local formatting to this thread
                fakePage.UICulture = l; // Apply local language to this thread
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(l);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(l);
                HttpContext.Current.Items.Add("lang", l);
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //return;
            var exception = Server.GetLastError();
            // Log the exception.
            Response.Clear();

            var httpException = exception as HttpException;

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            if (httpException == null)
            {
                routeData.Values.Add("action", "Index");
            }
            else //It's an Http Exception, Let's handle it.
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // Page not found.
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 505:
                        // Server error.
                        routeData.Values.Add("action", "HttpError505");
                        break;

                    // Here you can handle Views to other error codes.
                    // I choose a General error template  
                    default:
                        routeData.Values.Add("action", "Index");
                        break;
                }
            }

            // Pass exception details to the target error View.
            routeData.Values.Add("error", exception);

            // Clear the error on server.
            Server.ClearError();

            // Call target Controller and pass the routeData.
            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(
                 new HttpContextWrapper(Context), routeData));
        }
    }

}