using ECommerce.DTO;
using ECommerce.Models;
using ECommerce.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IJwtService _jwtService;
		private readonly IEmailSenderService _emailSenderService;
		private readonly UserManager<ApplicationUser> _userManager;
		public AccountController(IJwtService jwtService, UserManager<ApplicationUser> userManager, IEmailSenderService emailSenderService)
		{
			_jwtService = jwtService;
			_userManager = userManager;
			_emailSenderService = emailSenderService;
		}

		[HttpPost("admin-register")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AdminRegister(AdminRegisterDTO adminRegisterDTO)
		{
			ApplicationUser admin = new ApplicationUser()
			{
				FirstName = adminRegisterDTO.FirstName,
				LastName = adminRegisterDTO.LastName,
				Email = adminRegisterDTO.Email,
				UserName = adminRegisterDTO.UserName,
				PhoneNumber = adminRegisterDTO.PhoneNumber,
			};

			IdentityResult registerResult = await _userManager.CreateAsync(admin, adminRegisterDTO.Password);
			if (registerResult.Succeeded)
			{
				// Assign Admin role
				await _userManager.AddToRoleAsync(admin, "Admin");

				// generate email confirmation token
				string token = await _userManager.GenerateEmailConfirmationTokenAsync(admin);
				// create confirmation link
				var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = admin.Id, token }, Request.Scheme);
				var safeConfirmationLink = HtmlEncoder.Default.Encode(confirmationLink);
				string body = $@"
						<h3>Confirm Email for ECommerce</h3>
						<p>Click the link below to confirm your email:</p>
						<a href='{safeConfirmationLink}'>Confirm Email</a>
						<p>If you did not request this, ignore this email.</p>
						";
				await _emailSenderService.SendEmailAsync(admin.Email, "Confirm your email for ECommerce", body);
				return Ok(new { message = "Admin registered successfully. Please confirm email." });
			}
			string errors = string.Join("\n", registerResult.Errors.Select(e => e.Description));
			return Problem(detail: errors, statusCode: 400);
		}


		[HttpPost("customer-register")]
		public async Task<IActionResult> CustomerRegister(CustomerRegisterDTO customerRegisterDTO)
		{
			ApplicationUser customer = new ApplicationUser()
			{
				FirstName = customerRegisterDTO.FirstName,
				LastName = customerRegisterDTO.LastName,
				Email = customerRegisterDTO.Email,
				PhoneNumber = customerRegisterDTO.PhoneNumber,
				UserName = customerRegisterDTO.UserName,
				Governorate = customerRegisterDTO.Governorate,
				City = customerRegisterDTO.City,
				BuildingNumber = customerRegisterDTO.BuildingNumber,
				AppartmentNumber = customerRegisterDTO.AppartmentNumber,
				AddressDetails = customerRegisterDTO.AddressDetails,
			};

			IdentityResult result = await _userManager.CreateAsync(customer, customerRegisterDTO.Password);
			if (result.Succeeded)
			{
				// Assign Customer role
				await _userManager.AddToRoleAsync(customer, "Customer");

				// generate email confirmation token
				string token = await _userManager.GenerateEmailConfirmationTokenAsync(customer);
				// create confirmation link
				var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = customer.Id, token }, Request.Scheme);
				var safeConfirmationLink = HtmlEncoder.Default.Encode(confirmationLink);
				string body = $@"
						<h3>Confirm Email for ECommerce</h3>
						<p>Click the link below to confirm your email:</p>
						<a href='{safeConfirmationLink}'>Confirm Email</a>
						<p>If you did not request this, ignore this email.</p>
						";
				await _emailSenderService.SendEmailAsync(customer.Email, "Confirm your email for ECommerce", body);
				return Ok(new { message = "Customer registered successfully. Please confirm email." });
			}
			string errors = string.Join("/n", result.Errors.Select(e => e.Description));
			return Problem(detail: errors, statusCode: 400);
		}


		[HttpGet("confirm-email")]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			ApplicationUser? user = await _userManager.FindByIdAsync(userId);
			if (user == null)
				return Problem(detail: "user not found", statusCode: 400);
			// if token is valid => ASP.NET Core Identity automatically does: user.EmailConfirmed = true;
			IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);	
			if (result.Succeeded)
			{
				return Ok(new { message = "Email Confirmed Successfully" });
			}
			string errors = string.Join("\n", result.Errors.Select(e => e.Description));
			return Problem(detail: errors, statusCode: 400);
		}


		[HttpPost("login")]
		public async Task<ActionResult<AuthenticationResponse>> Login (LoginDTO loginDTO)
		{
			ApplicationUser? user = await _userManager.FindByNameAsync(loginDTO.UserName);
			if (user == null)
			{
				return Problem(detail: "Invalid username or password", statusCode: 400);
			}
			if (!user.EmailConfirmed)
			{
				return Problem(detail: "Confirm your email first", statusCode: 400);
			}
			bool isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
			if (isPasswordValid)
			{
				return await _jwtService.CreateJwtToken(user);
			}
			return Problem(detail: "Invalid username or password", statusCode: 400);
		}
	}
}
