using ECommerce.DTO;
using ECommerce.Models;
using ECommerce.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartService _cartService;
		private readonly IProductVariantService _productVariantService;
		public CartController(ICartService cartService, IProductVariantService productVariantService)
		{
			_cartService = cartService;
			_productVariantService = productVariantService;
		}

		// add cart item
		[HttpPost("add-cart-item")]    // api/cart/add-cart-item
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> AddCartItem(CartItemRequestDTO cartItemRequestDTO)
		{
			bool existPV = await _productVariantService.Exist(cartItemRequestDTO.ProductVariantId);
			if (!existPV)
			{
				return NotFound("Product variant not exist, invalid product variant id");
			}
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			bool isAdded = await _cartService.AddCartItem(cartItemRequestDTO, userId);
			return (isAdded) ? Ok("Cart Item Added Successfully") : BadRequest("Product Variant Not Exist");
		}


		[HttpGet]
		[Authorize (Roles = "Customer")]
		public async Task<ActionResult<CartResponseDTO>> GetCart()
		{
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			CartResponseDTO cartResponseDTO = await _cartService.GetCart(userId);
			return Ok(cartResponseDTO);
		}


		[HttpDelete]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> ClearCart()
		{
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			await _cartService.ClearCart(userId);
			return Ok(new {message = "Cart deleted successfully"});
		}


		[HttpPatch("cart-item/{id}")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> UpdateCartItem(int id,[FromQuery] int newQuantity)
		{
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			bool exist = await _cartService.CartItemExist(id);
			if (!exist)
				return NotFound("cart item not exist");
			CartItem? cartItem = await _cartService.GetCartItemWithDetails(id);
			if ( newQuantity > cartItem.ProductVariant.Quantity)
			{
				return BadRequest("invalid quantity");
			}
			bool update = await _cartService.UpdateCartItem(userId, id, newQuantity);
			if (update)
				return Ok(new { message = "cart item updated successfully" });
			return NotFound(new { message = "cart item not exist" });
		}


		[HttpDelete("cart-item/{id}")]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> DeleteCartItem(int id)
		{
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			bool exist = await _cartService.CartItemExist(id);
			if (!exist)
				return NotFound("cart item not exist");
			bool delete = await _cartService.DeleteCartItem(userId, id);
			if (!delete)
				return NotFound("Cart item not exist");
			return Ok("cart item deleted successfully");

		}

	}
}
