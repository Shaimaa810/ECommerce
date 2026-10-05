using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
	public class CartItem
	{
		public int Id { get; set; }



		[Range(1, int.MaxValue)]
		public int Quantity { get; set; }



		[ForeignKey("ProductVariant")]
		public int ProductVariantId { get; set; }
		public ProductVariant ProductVariant { get; set; }



		[ForeignKey("Cart")]
		public int CartId { get; set; }
		public Cart Cart { get; set; }


	}
}
