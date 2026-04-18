using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Concrete;
using GadgetHub.Domain.Infrastructure.Concrete;
using GadgetHub.Domain.Models;
using GadgetHub.Domain.Abstract;
using Moq;
using Ninject;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace GadgetHub.WebUI.Infrastructure
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;

		public NinjectDependencyResolver(IKernel kernelParam)
		{
			kernel = kernelParam;
			AddBindings();
		}

		public object GetService(System.Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(System.Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}

		private void AddBindings()
		{
			// Existing binding
			kernel.Bind<IGadgetRepository>()
				  .To<EFGadgetRepository>();

			// Email settings
			kernel.Bind<EmailSettings>()
				.ToSelf()
				.InSingletonScope()
				.WithConstructorArgument("settings", new EmailSettings
				{
					WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"]),
					FileLocation = ConfigurationManager.AppSettings["Email.FileLocation"]
				});

			// Order processor
			kernel.Bind<IOrderProcessor>()
				  .To<EmailOrderProcessor>();

			// =========================
			// AUTH PROVIDER (ADD THIS)
			// =========================
			kernel.Bind<IAuthProvider>()
				  .To<FormsAuthProvider>();
		}
	}
}