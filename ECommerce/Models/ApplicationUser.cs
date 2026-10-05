using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required(ErrorMessage = "{0} can't be empty")]
		[StringLength(50, MinimumLength =3, ErrorMessage = "{0} length must be between 3 and 50")]
		[Display(Name = "First Name")]
		public string FirstName {  get; set; }



		[Required(ErrorMessage = "{0} can't be empty")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "{0} length must be between 3 and 50")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }



		// Customer fields (nullable because admin doesn't need them)

		[StringLength(100, MinimumLength = 3)]
		public string? Governorate { get; set; }



		[StringLength(100, MinimumLength = 3)]
		public string? City { get; set; }



		[Range(1, 200)]
		public int? BuildingNumber { get; set; }



		[Range(1, 200)]
		public int? AppartmentNumber { get; set; }



		[StringLength(500, MinimumLength = 10)]
		public string? AddressDetails { get; set; }



		public ICollection<Coupon>? Coupons { get; set; }


		public ICollection<Order>? Orders { get; set; }


		public Cart? Cart { get; set; }

	}
}
