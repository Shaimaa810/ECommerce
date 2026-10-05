using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
	public class ProductVariant
	{
		public int Id { get; set; }


		public string Color { get; set; }



		public string Size { get; set; }



		[Range(1, int.MaxValue)]	
		public int Quantity { get; set; }



		public ICollection<CartItem> CartItems { get; set; }



		[ForeignKey("Product")]
		public int ProductId { get; set; }
		public Product Product { get; set; }


		public ICollection<OrderItem> OrderItems { get; set; }

	}
}
