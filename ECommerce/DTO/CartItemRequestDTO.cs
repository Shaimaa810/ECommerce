using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTO
{
	public class CartItemRequestDTO
	{
		public int ProductVariantId {  get; set; }


		[Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 1")]
		public int Quantity { get; set; }
	}
}
