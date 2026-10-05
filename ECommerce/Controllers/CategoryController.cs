using ECommerce.DTO;
using ECommerce.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		private readonly IProductService _productService;
		public CategoryController(ICategoryService categoryService, IProductService productService)
		{
			_categoryService = categoryService;
			_productService = productService;
		}

		[HttpPost]
		public async Task<IActionResult> Add(CategoryDTO categoryDTO)
		{
			await _categoryService.Add(categoryDTO);
			return Ok(new {message = "Category created successfully" });
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<CategoryDTO?>> GetById(int id)
		{
			CategoryDTO? categoryDto = await _categoryService.GetById(id);
			if (categoryDto == null)
			{
				return Problem(detail: "category not found, invalid category id", statusCode: 404);
			}
			return Ok(categoryDto);
		}


		[HttpGet]
		 public async Task<ActionResult<List<CategoryDTO>>> GetAll()
		{
			var categories = await _categoryService.GetAll();
			return Ok(categories);
		}


		[HttpPatch("{id}")]
		public async Task<IActionResult> Update(int id, CategoryDTO categoryDTO)
		{
			CategoryDTO? categoryDto = await _categoryService.GetById(id);
			if (categoryDto == null)
			{
				return Problem(detail: "category not found, invalid category id", statusCode: 404);
			}
			categoryDTO.Id = id;
			await _categoryService.Update(categoryDTO);
			return Ok(new { message = "Category updated successfully" });
		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			CategoryDTO? categoryDto = await _categoryService.GetById(id);
			if (categoryDto == null)
			{
				return Problem(detail: "category not found, invalid category id", statusCode: 404);
			}
			await _categoryService.Delete(id);
			return Ok(new { message = "Category deleted successfully" });
		}


		[HttpGet("{categoryId}/products")]
		public async Task<ActionResult<List<ProductResponseDTO>?>> GetProductsByCategory(int categoryId)
		{
			CategoryDTO? categoryDto = await _categoryService.GetById(categoryId);
			if (categoryDto == null)
			{
				return Problem(detail: "category not found, invalid category id", statusCode: 404);
			}
			List<ProductResponseDTO>? products = await _productService.GetProductsByCategory(categoryId, Request);
			return Ok(products);
		}
	}
}
