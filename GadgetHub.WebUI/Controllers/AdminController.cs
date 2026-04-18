using System.Linq;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Models;

namespace GadgetHub.WebUI.Controllers
{
	public class AdminController : Controller
	{
		private IGadgetRepository repository;

		public AdminController(IGadgetRepository repo)
		{
			repository = repo;
		}

		public ViewResult Index()
		{
			return View(repository.Gadgets);
		}

		public ViewResult Edit(int Id)
		{
			Gadget gadget = repository.Gadgets
				.FirstOrDefault(p => p.Id == Id);

			return View(gadget);
		}

		public ViewResult Create()
		{
			return View("Edit", new Gadget());
		}

		[HttpPost]
		public ActionResult Edit(Gadget gadget)
		{
			if (ModelState.IsValid)
			{
				repository.SaveGadget(gadget);
				TempData["message"] = "Gadget saved";
				return RedirectToAction("Index");
			}

			return View(gadget);
		}

		public ActionResult Delete(int Id)
		{
			Gadget gadget = repository.Gadgets
				.FirstOrDefault(p => p.Id == Id);

			if (gadget != null)
			{
				repository.DeleteGadget(Id);
				TempData["message"] = "Gadget deleted";
			}

			return RedirectToAction("Index");
		}
	}
}