using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTO
{
	public class ProductRequestDTO
	{
		[StringLength(100, MinimumLength = 2)]
		[Required]
		public string Name { get; set; }


		[StringLength(300, MinimumLength = 2)]
		[Required]
		public string Description { get; set; }


		[Range(0, double.MaxValue)]
		[Required]
		public double Price { get; set; }


		[Required]
		public bool IsAvailable { get; set; }

		[Required]
		public int CategoryId { get; set; }


		public List<ProductVariantRequestDTO> ProductVariants { get; set; }

	}
}
