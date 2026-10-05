namespace ECommerce.DTO
{
	public class AdminOrderResponseDTO
	{
		public int OrderId { get; set; }
		public string UserId { get; set; }

		public string CustomerName { get; set; }

		public string CustomerEmail { get; set; }

		public string CustomerPhoneNumber { get; set; }

		public string CustomerShippingAddress { get; set; }

		public List<OrderItemResponseDTO> OrderItems { get; set; }

		public string? CouponCode { get; set; }

		public double Discount { get; set; }

		public double TotalBeforeDiscount { get; set; }

		public double TotalAfterDiscount { get; set ; }

		public DateTime CreatedAt { get; set; }
	}
}
