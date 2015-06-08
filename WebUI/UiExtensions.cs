using System;
using System.Web;

using Elmah;

namespace Omu.ProDinner.WebUI
{
    public static class UiExtensions
    {
        public static void Raize(this Exception ex)
        {
            if (HttpContext.Current == null)
                ErrorLog.GetDefault(null).Log(new Error(ex));
            else
                ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}