using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTO
{
	public class ProductVariantUpdateDTO
	{
		[Required]
		public int Id { get; set; }


		[Required]
		public string Color { get; set; }


		[Required]
		public string Size { get; set; }


		[Range(1, int.MaxValue)]
		[Required]
		public int Quantity { get; set; }


		[Required]
		public int ProductId { get; set; }
	}
}
