using ECommerce.DTO;
using ECommerce.Models;
using ECommerce.RepositoryContracts;
using ECommerce.ServiceContracts;

namespace ECommerce.Services
{
	public class CartService : ICartService
	{
		private readonly ICartRepository _cartRepository;
		private readonly IProductVariantRepository _productVariantRepository;
		public CartService(ICartRepository cartRepository, IProductVariantRepository productVariantRepository)
		{
			_cartRepository = cartRepository;
			_productVariantRepository = productVariantRepository;
		}

		public async Task<bool> AddCartItem(CartItemRequestDTO cartItemRequestDTO, string userId)
		{
			Cart? cart = await _cartRepository.GetCart(userId);
			if (cart == null)
			{
				cart = new Cart()
				{
					ApplicationUserId = userId,
					CartItems = new List<CartItem>()
				};
				await _cartRepository.AddCart(cart);
			}
			ProductVariant? productVariant = await _productVariantRepository.GetById(cartItemRequestDTO.ProductVariantId);

			// check if product variant exist in cart or not
			bool pvExistInCart = await _cartRepository.ProductVariantExistInCart(userId, cartItemRequestDTO.ProductVariantId);
			if (pvExistInCart)
			{
				// product variant exist int cart
				CartItem? savedCartItem = await _cartRepository.GetCartItemByProductVariantId(userId, cartItemRequestDTO.ProductVariantId);
				if (productVariant?.Quantity < cartItemRequestDTO.Quantity + savedCartItem?.Quantity)
				{
					return false;
				}
				// update cart item
				savedCartItem.Quantity += cartItemRequestDTO.Quantity;
				await _cartRepository.UpdateCartItem(savedCartItem);
				return true;
			}

			// product variant not exist in cart
			if (cartItemRequestDTO.Quantity > productVariant?.Quantity)
				return false;
			CartItem cartItem= new CartItem()
			{
				ProductVariantId = cartItemRequestDTO.ProductVariantId,
				Quantity = cartItemRequestDTO.Quantity,
				CartId = cart.Id
			};
			await _cartRepository.AddCartItem(cartItem);
			return true;
		}

		public async Task<bool> CartItemExist(int id)
		{
			return await _cartRepository.CartItemExist(id);
		}

		public async Task ClearCart(string userId)
		{
			await _cartRepository.ClearCart(userId);
		}

		public async Task<bool> DeleteCartItem(string userId, int cartItemId)
		{
			// check if cart item id belong to current logged in user
			bool belong = await _cartRepository.CheckCartItemBelongToUser(userId, cartItemId);
			if (!belong)
				return false;
			CartItem? cartItem = await _cartRepository.GetCartItemById(cartItemId);
			await _cartRepository.DeleteCartItem(cartItem);
			return true;
		}

		public async Task<CartResponseDTO> GetCart(string userId)
		{
			Cart? cart = await _cartRepository.GetCartWithDetails(userId);
			if (cart == null)
			{
				cart = new Cart()
				{
					ApplicationUserId = userId,
				};
				await _cartRepository.AddCart(cart);
				return new CartResponseDTO()
				{
					CartId = cart.Id,
					CartItems = new List<CartItemResponseDTO>(),  // empty list
					TotalPrice = 0
				};
			}

			List<CartItemResponseDTO> cartItemResponseDTOs = new List<CartItemResponseDTO>();
			double cartTotalPrice = cart.CartItems.Sum(ci => ci.ProductVariant.Product.Price * ci.Quantity);
			foreach (CartItem cartItem in cart.CartItems)
			{
				cartItemResponseDTOs.Add(new CartItemResponseDTO()
				{
					CartItemId = cartItem.Id,
					ProductVariantId= cartItem.ProductVariantId,
					ProductId = cartItem.ProductVariant.ProductId,
					ProductName = cartItem.ProductVariant.Product.Name,
					ProductDescription = cartItem.ProductVariant.Product.Description,
					UnitPrice = cartItem.ProductVariant.Product.Price,
					TotalPrice = cartItem.ProductVariant.Product.Price * cartItem.Quantity,
					Color = cartItem.ProductVariant.Color,
					Size = cartItem.ProductVariant.Size,
					Quantity = cartItem.Quantity,
				});
			}
			return new CartResponseDTO()
			{
				CartId= cart.Id,
				TotalPrice =  cartTotalPrice,
				CartItems = cartItemResponseDTOs
			};
		}

		public async Task<CartItem?> GetCartItemWithDetails(int id)
		{
			return await _cartRepository.GetCartItemWithDetails(id);
		}

		public async Task<bool> UpdateCartItem(string userId, int cartItemId, int newQuantity)
		{
			// check if cart item id belong to current logged in user
			bool belong = await _cartRepository.CheckCartItemBelongToUser(userId, cartItemId);
			if (!belong)
				return false;
			CartItem? cartItem = await _cartRepository.GetCartItemWithDetails(cartItemId);
			if (newQuantity > cartItem.ProductVariant.Quantity)
				return false;
			cartItem.Quantity = newQuantity;
			await _cartRepository.UpdateCartItem(cartItem);
			return true;
		}


	}
}
