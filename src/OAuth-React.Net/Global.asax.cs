using System.Web.Mvc;
using System.Web.Routing;

namespace OAuth_React.Net
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Initialize IOC Container and add registrations
            Bootstrapper.Initialise();
            //Register custom controller factory
            ControllerBuilder.Current.SetControllerFactory(typeof(ControllerFactory));
        }
    }
}
