namespace ECommerce.DTO
{
	public class CouponResponseDTO
	{
		public int Id { get; set; }

		public string Code { get; set; }


		public bool IsActive { get; set; }

		public int MaxUses { get; set; }

		public int UsedCount { get; set; }

		public int MinOrderAmount { get; set; }

		public string DiscountType { get; set; }  // fixed or percentage


		// if type is fixed --->> ex. value = 10 pounds
		// if type is percentage --->> ex. value = 10%
		public double Value { get; set; }


		public DateTime StartDate { get; set; }

		public DateTime ExpirationDate { get; set; }
	}
}
