using System.Linq;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;

namespace GadgetHub.WebUI.Controllers
{
	public class GadgetController : Controller
	{
		private IGadgetRepository repository;

		public int pageSize = 4;

		public GadgetController(IGadgetRepository repo)
		{
			repository = repo;
		}

		public ViewResult List(int page = 1)
		{
			GadgetsListViewModel model = new GadgetsListViewModel
			{
				Gadgets = repository.Gadgets
					.OrderBy(g => g.Id)
					.Skip((page - 1) * pageSize)
					.Take(pageSize),

				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = pageSize,
					TotalItems = repository.Gadgets.Count()
				}
			};

			return View(model);
		}
	}
}