using System.Linq;
using System.Web;
using System.Web.Mvc;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Models;

namespace GadgetHub.WebUI.Controllers
{
	[Authorize]
	public class AdminController : Controller
	{
		private readonly IGadgetRepository repository;

		public AdminController(IGadgetRepository repo)
		{
			repository = repo;
		}

		// =========================
		// LIST
		// =========================
		public ViewResult Index()
		{
			return View(repository.Gadgets);
		}

		// =========================
		// EDIT (GET)
		// =========================
		public ViewResult Edit(int id)
		{
			var gadget = repository.Gadgets
				.FirstOrDefault(p => p.Id == id);

			if (gadget == null)
			{
				return View("Error"); // or RedirectToAction("Index")
			}

			return View(gadget);
		}

		// =========================
		// CREATE
		// =========================
		public ViewResult Create()
		{
			return View("Edit", new Gadget());
		}

		// =========================
		// SAVE (CREATE + EDIT)
		// =========================
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Gadget gadget, HttpPostedFileBase image)
		{
			if (!ModelState.IsValid)
			{
				return View(gadget);
			}

			// GET existing record (important for updates)
			var existing = repository.Gadgets
				.FirstOrDefault(p => p.Id == gadget.Id);

			if (existing != null)
			{
				// UPDATE existing fields safely
				existing.Name = gadget.Name;
				existing.Price = gadget.Price;
				existing.Description = gadget.Description;
				existing.Category = gadget.Category;

				// IMAGE LOGIC (ONLY overwrite if new image uploaded)
				if (image != null && image.ContentLength > 0)
				{
					existing.ImageMimeType = image.ContentType;

					existing.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(existing.ImageData, 0, image.ContentLength);
				}

				repository.SaveGadget(existing);
			}
			else
			{
				// CREATE NEW
				if (image != null && image.ContentLength > 0)
				{
					gadget.ImageMimeType = image.ContentType;

					gadget.ImageData = new byte[image.ContentLength];
					image.InputStream.Read(gadget.ImageData, 0, image.ContentLength);
				}

				repository.SaveGadget(gadget);
			}

			TempData["message"] = "Gadget saved";
			return RedirectToAction("Index");
		}

		// =========================
		// DELETE
		// =========================
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			var gadget = repository.Gadgets
				.FirstOrDefault(p => p.Id == id);

			if (gadget != null)
			{
				repository.DeleteGadget(id);
				TempData["message"] = "Gadget deleted";
			}

			return RedirectToAction("Index");
		}
	}
}