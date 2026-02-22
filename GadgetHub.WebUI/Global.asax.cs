using GadgetHub.WebUI.Infrastructure;
using Ninject;
using System.Web.Mvc;
using System.Web.Routing;

namespace GadgetHub.WebUI
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			IKernel kernel = new StandardKernel();
			DependencyResolver.SetResolver(
				new NinjectDependencyResolver(kernel));
		}
	}
}