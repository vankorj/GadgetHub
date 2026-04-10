using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Models;
using GadgetHub.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
	public class CartController : Controller
	{
		private IGadgetRepository repository;

		public CartController(IGadgetRepository repo)
		{
			repository = repo;
		}

		public RedirectToRouteResult AddToCart(Cart cart, int id, string returnUrl)
		{
			Gadget gadget = repository.Gadgets
				.FirstOrDefault(g => g.Id == id);

			if (gadget != null)
			{
				cart.AddItem(gadget, 1);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		public RedirectToRouteResult RemoveFromCart(Cart cart, int id, string returnUrl)
		{
			Gadget gadget = repository.Gadgets
				.FirstOrDefault(g => g.Id == id);

			if (gadget != null)
			{
				cart.RemoveLine(gadget);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		public ViewResult Index(Cart cart, string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = cart,
				ReturnUrl = returnUrl
			});
		}

		// CART SUMMARY WIDGET
		public PartialViewResult Summary(Cart cart)
		{
			return PartialView(cart);
		}

		// CHECKOUT (GET)
		public ViewResult Checkout()
		{
			return View(new ShippingDetails());
		}

		// CHECKOUT (POST)
		[HttpPost]
		public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
		{
			if (cart.Lines.Count() == 0)
			{
				ModelState.AddModelError("", "Sorry, your cart is empty!");
			}

			if (ModelState.IsValid)
			{
				// TODO: Add order processing logic here
				cart.Clear();
				return View("Completed");
			}

			return View(shippingDetails);
		}
	}
}
