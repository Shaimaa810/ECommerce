using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTO
{
	public class AdminRegisterDTO
	{
		public string UserName { get; set;}


		[Phone]
		public string PhoneNumber { get; set;}


		[EmailAddress]
		public string Email { get; set;}

		public string Password { get; set; }


		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "{0} can't be empty")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "{0} length must be between 3 and 50")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }



		[Required(ErrorMessage = "{0} can't be empty")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "{0} length must be between 3 and 50")]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
	}
}
