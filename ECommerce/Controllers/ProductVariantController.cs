using ECommerce.DTO;
using ECommerce.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductVariantController : ControllerBase
	{
		private readonly IProductVariantService _productVariantService;
		private readonly IProductService _productService;
		
		public ProductVariantController(IProductVariantService productVariantService, IProductService productService)
		{
			_productVariantService = productVariantService;
			_productService = productService;
		}


		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Add(ProductVariantRequestDTO productVariantRequestDTO)
		{
			bool exist = await _productService.Exist(productVariantRequestDTO.ProductId);
			if (!exist)
			{
				return Problem(detail: "product not found, invalid product id", statusCode: 404);
			}
			await _productVariantService.Add(productVariantRequestDTO);

			return Ok(new {message = "product variant created successfully"});
		}


		[HttpGet("{id}")]
		[Authorize(Roles = "Admin,Customer")]
		public async Task<ActionResult<ProductVariantResponseDTO?>> GetById(int id)
		{
			ProductVariantResponseDTO? productVariant = await _productVariantService.GetById(id);
			if (productVariant == null)
			{
				return Problem(detail: "product variant not found, invalid product variant id", statusCode: 404);
			}
			return Ok(productVariant);
		}


		[HttpPatch("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateProductVariant(int id, ProductVariantUpdateDTO productVariantUpdateDTO)
		{
			bool exist = await _productVariantService.Exist(id);
			if (!exist)
			{
				return Problem(detail: "product variant not found, invalid product variant id", statusCode: 404);
			}
			productVariantUpdateDTO.Id = id;
			await _productVariantService.Update(productVariantUpdateDTO);
			return Ok(new { message = "product variant updated successfully" });
		}


		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteProductVariant(int id)
		{
			bool exist = await _productVariantService.Exist(id);
			if (!exist)
			{
				return Problem(detail: "product variant not found, invalid product variant id", statusCode: 404);
			}
			await _productVariantService.Delete(id);
			return Ok(new { message = "product variant deleted successfully" });
		}

	}
}
