using System.Collections.Generic;
using System.Linq;
using GadgetHub.Domain.Models;

namespace GadgetHub.WebUI.Models
{
	public class Cart
	{
		public List<CartLine> Lines { get; set; } = new List<CartLine>();

		public void AddItem(Gadget gadget, int quantity)
		{
			var line = Lines
				.Where(g => g.Gadget.Id == gadget.Id)
				.FirstOrDefault();

			if (line == null)
			{
				Lines.Add(new CartLine
				{
					Gadget = gadget,
					Quantity = quantity
				});
			}
			else
			{
				line.Quantity += quantity;
			}
		}

		public void RemoveLine(Gadget gadget)
		{
			Lines.RemoveAll(l => l.Gadget.Id == gadget.Id);
		}

		public void Clear()
		{
			Lines.Clear();
		}

		public decimal ComputeTotalValue()
		{
			return Lines.Sum(e => e.Gadget.Price * e.Quantity);
		}

		public int TotalItems()
		{
			return Lines.Sum(e => e.Quantity);
		}
	}

	public class CartLine
	{
		public Gadget Gadget { get; set; }
		public int Quantity { get; set; }
	}
}