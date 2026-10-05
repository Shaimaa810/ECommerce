using ECommerce.Constraints;

namespace ECommerce.DTO
{
	public class CustomerOrderResponseDTO
	{
		public int OrderId { get; set; }

		public List<OrderItemResponseDTO> OrderItems { get; set; }

		public string? CouponCode { get; set; }

		public double Discount { get; set; }

		public double TotalBeforeDiscount { get; set; }

		public double TotalAfterDiscount { get; set; }

		public string Status { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
