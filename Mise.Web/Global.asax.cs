using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mise.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            try
            {
                if (Session["Korisnik"] == null)
                    return;
                HttpContext.Current.User = Session["Korisnik"] as IPrincipal;
                Thread.CurrentPrincipal = Session["Korisnik"] as IPrincipal;
            }
            catch (Exception)
            {

            }
        }
    }
}
