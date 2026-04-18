using System.Collections.Generic;
using GadgetHub.Domain.Models;

namespace GadgetHub.Domain.Abstract
{
	public interface IGadgetRepository
	{
		IEnumerable<Gadget> Gadgets { get; }

		void SaveGadget(Gadget gadget);

		Gadget DeleteGadget(int id);
	}
}