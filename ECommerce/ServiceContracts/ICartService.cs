using ECommerce.DTO;
using ECommerce.Models;

namespace ECommerce.ServiceContracts
{
	public interface ICartService
	{
		Task<bool> AddCartItem(CartItemRequestDTO cartItemRequestDTO, string userId);

		Task<CartResponseDTO> GetCart(string userId);

		Task ClearCart(string userId);


		Task<bool> UpdateCartItem(string userId, int cartItemId, int newQuantity);


		Task<CartItem?> GetCartItemWithDetails(int id);



		Task<bool> CartItemExist(int id);

		Task<bool> DeleteCartItem(string userId, int cartItemId);
		
	}
}
