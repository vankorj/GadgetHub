using System.Collections.Generic;
using GadgetHub.Domain.Models;

namespace GadgetHub.WebUI.Models
{
	public class GadgetsListViewModel
	{
		public IEnumerable<Gadget> Gadgets { get; set; }

		public PagingInfo PagingInfo { get; set; }
	}
}