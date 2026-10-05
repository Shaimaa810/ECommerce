using ECommerce.DTO;
using ECommerce.Models;

namespace ECommerce.RepositoryContracts
{
	public interface ICartRepository
	{
		Task AddCartItem(CartItem cartItem);

		Task<Cart?> GetCart(string userId);

		Task<Cart?> GetCartWithDetails(string userId);

		Task AddCart(Cart cart);

		Task<bool> ProductVariantExistInCart(string userId, int  productVariantId);

		Task<CartItem?> GetCartItemByProductVariantId(string userId, int productVariantId);

		Task UpdateCartItem(CartItem cartItem);

		Task ClearCart(string userId);


		Task<CartItem?> GetCartItemWithDetails(int id);


		Task<CartItem?> GetCartItemById(int  id);

		Task<bool> CheckCartItemBelongToUser(string userId, int cartItemId);


		Task<bool> CartItemExist(int id);


		Task DeleteCartItem(CartItem cartItem);

	}
}
