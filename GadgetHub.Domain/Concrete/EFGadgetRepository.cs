using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace GadgetHub.Domain.Concrete
{
	public class EFGadgetRepository : IGadgetRepository
	{
		private EFDbContext context = new EFDbContext();

		public IEnumerable<Gadget> Gadgets => context.Gadgets;

		public void SaveGadget(Gadget gadget)
		{
			if (gadget.Id == 0)
			{
				context.Gadgets.Add(gadget);
			}
			else
			{
				Gadget dbEntry = context.Gadgets.Find(gadget.Id);

				if (dbEntry != null)
				{
					dbEntry.Name = gadget.Name;
					dbEntry.Description = gadget.Description;
					dbEntry.Price = gadget.Price;
					dbEntry.Category = gadget.Category;

					// =========================
					// IMAGE SUPPORT (NEW)
					// =========================
					if (gadget.ImageData != null)
					{
						dbEntry.ImageData = gadget.ImageData;
						dbEntry.ImageMimeType = gadget.ImageMimeType;
					}
				}
			}

			context.SaveChanges();
		}

		public Gadget DeleteGadget(int id)
		{
			Gadget dbEntry = context.Gadgets.Find(id);

			if (dbEntry != null)
			{
				context.Gadgets.Remove(dbEntry);
				context.SaveChanges();
			}

			return dbEntry;
		}
	}
}