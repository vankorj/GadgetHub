using GadgetHub.Domain.Abstract;
using GadgetHub.WebUI.Models;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly IAuthProvider authProvider;

		public AccountController(IAuthProvider authProvider)
		{
			this.authProvider = authProvider;
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			if (authProvider.Authenticate(model.UserName, model.Password))
			{
				authProvider.SetAuthCookie(model.UserName);
				return RedirectToAction("Index", "Admin");
			}

			ModelState.AddModelError("", "Invalid username or password");
			return View(model);
		}

		public ActionResult Logout()
		{
			authProvider.SignOut();
			return RedirectToAction("Login");
		}
	}
}