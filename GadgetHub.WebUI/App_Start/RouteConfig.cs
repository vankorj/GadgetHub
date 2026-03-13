using System.Web.Mvc;
using System.Web.Routing;

namespace GadgetHub.WebUI
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: null,
				url: "",
				defaults: new { controller = "Gadget", action = "List", category = (string)null, page = 1 }
			);

			routes.MapRoute(
				name: null,
				url: "Page{page}",
				defaults: new { controller = "Gadget", action = "List", category = (string)null }
			);

			routes.MapRoute(
				name: null,
				url: "{category}",
				defaults: new { controller = "Gadget", action = "List", page = 1 }
			);

			routes.MapRoute(
				name: null,
				url: "{category}/Page{page}",
				defaults: new { controller = "Gadget", action = "List" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Gadget", action = "List", id = UrlParameter.Optional }
			);
		}
	}
}