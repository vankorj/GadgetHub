using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Models;
using Moq;
using System.Linq;
using GadgetHub.Domain.Concrete;

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
			// Create mock repository
			Mock<IGadgetRepository> mock = new Mock<IGadgetRepository>();

			/*
			mock.Setup(m => m.Gadgets).Returns(new List<Gadget>
			{
				new Gadget { GadgetID = 1, Name = "iPhone 15", Brand = "Apple", Price = 999, Description = "Latest Apple smartphone", Category = "Smartphones"},
				new Gadget { GadgetID = 2, Name = "Galaxy S23", Brand = "Samsung", Price = 899, Description = "Flagship Samsung phone", Category = "Smartphones"},
				new Gadget { GadgetID = 3, Name = "MacBook Pro", Brand = "Apple", Price = 1999, Description = "High-performance laptop", Category = "Laptops"},
				new Gadget { GadgetID = 4, Name = "Apple Watch", Brand = "Apple", Price = 399, Description = "Smart wearable device", Category = "Wearables"},
				new Gadget { GadgetID = 5, Name = "AirPods Pro", Brand = "Apple", Price = 249, Description = "Wireless earbuds", Category = "Accessories"}
			}.AsQueryable());
			*/
			kernel.Bind<IGadgetRepository>().To<EFGadgetRepository>();
		}
	}
}