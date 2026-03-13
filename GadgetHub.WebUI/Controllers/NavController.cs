using System.Web.Mvc;
using System.Collections.Generic;
using GadgetHub.Domain.Abstract;
using System.Linq;

namespace GadgetHub.WebUI.Controllers
{
	public class NavController : Controller
	{
		private IGadgetRepository repository;

		public NavController(IGadgetRepository repo)
		{
			repository = repo;
		}

	public PartialViewResult Menu(string category = null)
		{
			ViewBag.SelectedCategory = category;

			IEnumerable<string> categories = repository.Gadgets
				.Select(x => x.Category)
				.Distinct()
				.OrderBy(x => x);

			return PartialView(categories);
		}
	}
}