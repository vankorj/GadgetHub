using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Models;
using System.Data.Entity;

namespace GadgetHub.Domain.Concrete
{
	public class EFDbContext : DbContext
	{
		public DbSet<Gadget> Gadgets { get; set; }
	}
}
