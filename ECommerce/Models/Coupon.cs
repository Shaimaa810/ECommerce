using ECommerce.Constraints;
using ECommerce.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
	public class Coupon
	{
		public int Id { get; set; }


		[UniqueCouponCode]
		public string Code { get; set; }


		public bool IsActive { get; set; }


		[Range(1, int.MaxValue)]
		public int MaxUses { get; set; }


		[Range(0, int.MaxValue)]
		public int UsedCount { get; set; }


		[Range(0, int.MaxValue)]
		public int MinOrderAmount { get; set; }


		public DiscountType DiscountType { get; set; }  // fixed or percentage



		// if type is fixed --->> ex. value = 10 pounds
		// if type is percentage --->> ex. value = 10%
		[Range(0, int.MaxValue)]
		public double Value { get; set; }


		public DateTime StartDate { get; set; }

		public DateTime ExpirationDate { get; set; }



		public ICollection<Order>? Orders { get; set; }


		public ICollection<ApplicationUser>? ApplicationUsers { get; set; }
	}
}
