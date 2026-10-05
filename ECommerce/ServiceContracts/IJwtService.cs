using ECommerce.DTO;
using ECommerce.Models;

namespace ECommerce.ServiceContracts
{
	public interface IJwtService
	{
		Task<AuthenticationResponse> CreateJwtToken(ApplicationUser applicationUser);
	}
}
