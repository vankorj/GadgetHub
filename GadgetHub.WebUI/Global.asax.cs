using GadgetHub.WebUI.Infrastructure;
using GadgetHub.WebUI.Models;
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

			// Register Cart Model Binder
			ModelBinders.Binders.Add(
				typeof(Cart),
				new CartModelBinder()
			);
		}
	}
}