using GadgetHub.Domain.Models;
using GadgetHub.WebUI.Models;

namespace GadgetHub.Domain.Abstract
{
	public interface IOrderProcessor
	{
		void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
	}
}
