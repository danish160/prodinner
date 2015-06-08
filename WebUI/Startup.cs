using Microsoft.Owin;

using Omu.ProDinner.WebUI;
using Omu.ProDinner.WebUI.App_Start;

using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Omu.ProDinner.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            OwinConfig.ConfigureAuth(app);
        }
    }
}