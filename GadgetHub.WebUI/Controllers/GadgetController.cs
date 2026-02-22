using System.Linq;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;

namespace GadgetHub.WebUI.Controllers
{
	public class GadgetController : Controller
	{
		private IGadgetRepository repository;

		public GadgetController(IGadgetRepository repo)
		{
			repository = repo;
		}

		public ViewResult List()
		{
			return View(repository.Gadgets);
		}
	}
}