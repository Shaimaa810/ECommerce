using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
	public class OrderItem
	{
		public int Id { get; set; }

		public int Quantity { get; set; }



		[ForeignKey("ProductVariant")]
		public int ProductVariantId { get; set; }
		public ProductVariant ProductVariant { get; set; }



		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public Order Order { get; set; }


	}
}
