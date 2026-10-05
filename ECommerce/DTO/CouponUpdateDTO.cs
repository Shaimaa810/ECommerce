using ECommerce.Constraints;
using ECommerce.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTO
{
	public class CouponUpdateDTO
	{
		public int Id { get; set; }


		[UniqueCouponCode]
		[Required]
		public string Code { get; set; }


		[Required]
		public bool IsActive { get; set; }


		[Range(1, int.MaxValue)]
		[Required]
		public int MaxUses { get; set; }


		[Range(0, int.MaxValue)]
		[Required]
		public int UsedCount { get; set; }


		[Range(0, int.MaxValue)]
		[Required]
		public int MinOrderAmount { get; set; }


		[Required]
		public DiscountType DiscountType { get; set; }  // fixed or percentage



		// if type is fixed --->> ex. value = 10 pounds
		// if type is percentage --->> ex. value = 10%
		[Range(0, int.MaxValue)]
		[Required]
		public double Value { get; set; }


		[Required]
		public DateTime StartDate { get; set; }


		[Required]
		public DateTime ExpirationDate { get; set; }
	}
}
