using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Models;
using GadgetHub.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
	public class GadgetController : Controller
	{
		private readonly IGadgetRepository repository;
		private const int PageSize = 4;

		public GadgetController(IGadgetRepository repo)
		{
			repository = repo;
		}

		public ViewResult List(string category, int page = 1)
		{
			if (repository == null)
				throw new System.Exception("IGadgetRepository was not injected correctly.");

			var query = repository.Gadgets
				.Where(g => string.IsNullOrEmpty(category) || g.Category == category)
				.OrderBy(g => g.Id);

			var totalItems = query.Count();

			var pagedGadgets = query
				.Skip((page - 1) * PageSize)
				.Take(PageSize)
				.ToList();

			var model = new GadgetsListViewModel
			{
				Gadgets = pagedGadgets,
				CurrentCategory = category,
				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = totalItems
				}
			};

			return View(model);
		}

		[AllowAnonymous]
		public FileContentResult GetImage(int id)
		{
			var gadget = repository.Gadgets
				.FirstOrDefault(g => g.Id == id);

			if (gadget != null && gadget.ImageData != null)
			{
				return File(gadget.ImageData, gadget.ImageMimeType);
			}

			return null;
		}
	}
}