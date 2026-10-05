using ECommerce.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductImageController : ControllerBase
	{
		private readonly IProductImageService _productImageService;
		public ProductImageController(IProductImageService productImageService)
		{
			_productImageService = productImageService;
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{
			bool exists = await _productImageService.Exist(id);
			if (!exists)
			{
				return Problem(detail: "Image not found, invalid image id", statusCode: 404);
			}
			await _productImageService.Delete(id);
			return Ok(new {message = "image deleted Successfully"});
		}

	}
}
