using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GadgetHub.Domain.Models;

namespace GadgetHub.Domain.Abstract
{
	public interface IGadgetRepository
	{
		IQueryable<Gadget> Gadgets { get; }
	}
}
