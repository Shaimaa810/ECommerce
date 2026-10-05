namespace ECommerce.DTO
{
	public class ApplyCouponResponseDTO
	{
		public string CouponCode { get; set; }

		public double Discount { get; set; }

		public double TotalBeforeDiscount { get; set; }

		 public double TotalAfterDiscount { get; set; }
	}
}
