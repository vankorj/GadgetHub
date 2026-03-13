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

		public ViewResult List(string category, int page = 1)
		{
			var gadgets = repository.Gadgets
				.Where(g => category == null || g.Category == category)
				.OrderBy(g => g.Id);

			GadgetsListViewModel model = new GadgetsListViewModel
			{
				Gadgets = gadgets
					.Skip((page - 1) * pageSize)
					.Take(pageSize),

				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = pageSize,
					TotalItems = category == null
						? repository.Gadgets.Count()
						: repository.Gadgets.Where(g => g.Category == category).Count()
				},

				CurrentCategory = category
			};

			return View(model);
		}
	}
}