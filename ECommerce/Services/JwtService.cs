using ECommerce.DTO;
using ECommerce.Models;
using ECommerce.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services
{
	public class JwtService : IJwtService
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<ApplicationUser> _userManager;
		public JwtService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
		{
			_configuration = configuration;
			_userManager = userManager;
		}


		public async Task<AuthenticationResponse> CreateJwtToken(ApplicationUser user)
		{
			DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:Expiration_Minutes"]));
			var roles = await _userManager.GetRolesAsync(user);

			//Claim[] claims = new Claim[]
			//{
			//	new Claim(JwtRegisteredClaimNames.Sub, user.Id),
			//	new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT unique ID
			//	//new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()), // Issued at (date and time of token generation)
			//	new Claim(JwtRegisteredClaimNames.Iat,new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64),
			//	new Claim(ClaimTypes.NameIdentifier, user.UserName) // optional
			//};

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // JWT unique ID
				//new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()), // Issued at (date and time of token generation)
				new Claim(JwtRegisteredClaimNames.Iat,new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64),
				new Claim(ClaimTypes.NameIdentifier, user.UserName) // optional
			};

			// add user's roles to jwt
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret_Key"]));
			SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256); // HmacSha256 == HS256
			JwtSecurityToken tokenGenerator = new JwtSecurityToken(
				_configuration["JWT:Issuer"],
				_configuration["JWT:Audience"],
				claims,
				expires: expiration,
				signingCredentials: signingCredentials
			);

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			string token = tokenHandler.WriteToken(tokenGenerator);
			return new AuthenticationResponse()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				UserName = user.UserName,
				PhoneNumber = user.PhoneNumber,
				Token = token,
				Expiration = expiration,

				Governorate = user?.Governorate,
				City = user?.City,
				BuildingNumber = user?.BuildingNumber,
				AppartmentNumber = user?.AppartmentNumber,
				AddressDetails = user?.AddressDetails,
			};
		}
	}
}
