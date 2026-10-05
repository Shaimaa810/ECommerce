using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
	public class Order
	{
		public int Id { get; set; }

		public bool IsDelivered { get; set; }

		public double TotalBeforeDiscount { get; set; }

		public double TotalAfterDiscount { get; set; }


		public double Discount { get; set; }


		public DateTime CreatedAt { get; set; }



		public ICollection<OrderItem> OrderItems { get; set; }



		[ForeignKey("ApplicationUser")]
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }



		[ForeignKey("Coupon")]
		public int? CouponId { get; set; }
		public Coupon? Coupon { get; set; }
	}
}
