using ECommerce.DTO;
using ECommerce.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		private readonly IProductVariantService _productVariantService;
		private readonly IProductImageService _productImageService;
		public ProductController(IProductService productService, ICategoryService categoryService, IProductVariantService productVariantService, IProductImageService productImageService)
		{
			_productService = productService;
			_categoryService = categoryService;
			_productVariantService = productVariantService;
			_productImageService = productImageService;
		}


		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddProduct(ProductRequestDTO productRequestDTO)
		{
			bool categoryExists = await _categoryService.Exist(productRequestDTO.CategoryId);
			if (!categoryExists)
			{
				return Problem(detail: "category not found, invalid category id", statusCode: 404);
			}
			await _productService.Add(productRequestDTO);
			return Ok(new { message = "Product created successfully" });
		}


		[HttpPatch("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDTO productUpdateDTO)
		{
			bool productExist= await _productService.Exist(id);
			if (!productExist)
			{
				return Problem(detail: "product not found, invalid product id", statusCode: 404);
			}

			bool categoryExist = await _categoryService.Exist(id);
			if (!categoryExist)
			{
				return Problem(detail: "category not found, invalid category id", statusCode: 404);
			}
			productUpdateDTO.Id = id;
			await _productService.Update(productUpdateDTO);
			return Ok(new { message = "product updated successfully" });
		}


		[HttpGet("{id}")]
		[Authorize(Roles ="Admin,Customer")]
		public async Task<ActionResult<ProductResponseDTO?>> GetProductWithDetails(int id)
		{
			bool productExist = await _productService.Exist(id); 
			if (!productExist)
			{
				return Problem(detail: "product not found, invalid product id", statusCode: 404);
			}
			ProductResponseDTO? productResponseDTO = await _productService.GetByIdWithDetails(id, Request);
			return Ok(productResponseDTO);
		}


		[HttpGet]
		[Authorize(Roles = "Admin,Customer")]
		public async Task<ActionResult<List<ProductResponseDTO>?>> GetAll()
		{
			List<ProductResponseDTO>? products = await _productService.GetAll(Request);
			return Ok(products);
		}


		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			bool productExist = await _productService.Exist(id);
			if (!productExist)
			{
				return Problem(detail: "product not found, invalid product id", statusCode: 404);
			}
			await _productService.Delete(id);
			return Ok(new { message = "product deleted successfully" });
		}


		[HttpGet("{productId}/variants")]
		[Authorize(Roles = "Admin,Customer")]
		public async Task<ActionResult<List<ProductVariantResponseDTO>?>> GetProductVariantsByProduct(int productId)
		{
			bool productExist = await _productService.Exist(productId);
			if (!productExist)
			{
				return Problem(detail: "product not found, invalid product id", statusCode: 404);
			}
			List<ProductVariantResponseDTO>? variants = await _productVariantService.GetProductVariantsByProduct(productId);
			return Ok(variants);
		}


		[HttpPost("{productId}/images")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AddImages(int productId, List<IFormFile> productImages)
		{
			bool exist = await _productService.Exist(productId);
			if (!exist)
			{
				return Problem(detail: "product not found, invalid product id", statusCode: 404);
			}
			await _productImageService.Add(productId, productImages);
			return Ok(new {message = "product images created successfully"});
		}


	}
}
