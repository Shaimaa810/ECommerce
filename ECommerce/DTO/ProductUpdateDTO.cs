using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTO
{
	public class ProductUpdateDTO
	{
		public int Id { get; set; }

		[StringLength(100, MinimumLength = 2)]
		public string Name { get; set; }


		[StringLength(300, MinimumLength = 2)]
		public string Description { get; set; }


		[Range(0, double.MaxValue)]
		public double Price { get; set; }


		public bool IsAvailable { get; set; }


		public int CategoryId { get; set; }

	}
}
