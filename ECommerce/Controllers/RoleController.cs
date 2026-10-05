using ECommerce.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		public RoleController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[HttpPost]
		//[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateRole(RoleDTO roleDTO) 
		{
			IdentityRole role = new IdentityRole();
			role.Name = roleDTO.RoleName;
			IdentityResult result = await _roleManager.CreateAsync(role);
			if (result.Succeeded)
			{
				return Ok(new { message = $"role {roleDTO.RoleName} created successfully" });
			}
			string errors = string.Join("\n", result.Errors.Select(e => e.Description));
			return Problem(detail: errors, statusCode: 400);
		}

	}
}
