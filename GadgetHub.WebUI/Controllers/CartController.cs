using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Models;
using GadgetHub.WebUI.Models;
using GadgetHubs.Domain.Abstract;
using System.Linq;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
	public class CartController : Controller
	{
		private IGadgetRepository repository;
		private IOrderProcessor orderProcessor;

		public CartController(IGadgetRepository repo, IOrderProcessor processor)
		{
			repository = repo;
			orderProcessor = processor;
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

		public PartialViewResult Summary(Cart cart)
		{
			return PartialView(cart);
		}

		public ViewResult Checkout()
		{
			return View(new ShippingDetails());
		}

		[HttpPost]
		public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
		{
			if (!cart.Lines.Any())
			{
				ModelState.AddModelError("", "Sorry, your cart is empty!");
			}

			if (ModelState.IsValid)
			{
				orderProcessor.ProcessOrder(cart, shippingDetails);

				cart.Clear();

				return View("Completed", shippingDetails);
			}

			return View(shippingDetails);
		}
	}
}