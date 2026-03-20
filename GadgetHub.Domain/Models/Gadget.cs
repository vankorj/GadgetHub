using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Domain.Models
{
	public class Gadget
	{
		public int? Id { get; set; }  // <-- nullable now
		public string Name { get; set; }
		public string Brand { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
	}
}