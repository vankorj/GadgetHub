using System.ComponentModel.DataAnnotations;

namespace GadgetHub.Domain.Models
{
	public class Gadget
	{
		public int? Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public string Brand { get; set; }

		[Required]
		[Range(0.01, 10000)]
		public decimal Price { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		public string Category { get; set; }
	}
}