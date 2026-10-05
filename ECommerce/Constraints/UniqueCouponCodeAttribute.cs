using ECommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Constraints
{
	public class UniqueCouponCodeAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
				return new ValidationResult("Coupon code is required");

			ApplicationDbContext? dbcontext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));	
			if (dbcontext == null)
				throw new Exception("DbContext not found");

			string couponCode = (string)value;
			int currentId = 0;
			var dto = validationContext.ObjectInstance;
			var IdProperty = dto.GetType().GetProperty("Id");
			if (IdProperty != null)
			{
				currentId = (int)(IdProperty.GetValue(dto) ?? 0);
			}
			Coupon? coupon = dbcontext.Coupons.FirstOrDefault(c => c.Code == couponCode && c.Id != currentId);

			if (coupon == null)
				return ValidationResult.Success;
			return new ValidationResult("Coupon code must be unique");

		}
	}
}
