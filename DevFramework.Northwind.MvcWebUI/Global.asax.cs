using DevFramework.Core.Utilities.Mvc.Infastructure;
using DevFramework.Northwind.Business.DepedencyResolvers.Ninject;
using System.Web.Mvc;
using System.Web.Routing;

namespace DevFramework.Northwind.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule()));
        }
    }
}
