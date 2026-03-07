namespace GadgetHub.WebUI.Models
{
	public class PagingInfo
	{
		public int TotalItems { get; set; }

		public int ItemsPerPage { get; set; }

		public int CurrentPage { get; set; }

		public int TotalPages
		{
			get
			{
				return (int)System.Math.Ceiling
					((decimal)TotalItems / ItemsPerPage);
			}
		}
	}
}