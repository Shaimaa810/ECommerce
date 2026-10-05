using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTO
{
	public class AuthenticationResponse
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string UserName { get; set; }

		public string Token { get; set; }

		public DateTime Expiration {  get; set; }

		// ----------- Customer properties ----------
		public string? Governorate { get; set; }

		public string? City { get; set; }

		public int? BuildingNumber { get; set; }

		public int? AppartmentNumber { get; set; }

		public string? AddressDetails { get; set; }

	}
}
