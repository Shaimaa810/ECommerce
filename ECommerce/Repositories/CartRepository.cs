using ECommerce.Models;
using ECommerce.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
	public class CartRepository : ICartRepository
	{
		private readonly ApplicationDbContext _dbcontext;
		public CartRepository(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public async Task AddCart(Cart cart)
		{
			await _dbcontext.AddAsync(cart);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task AddCartItem(CartItem cartItem)
		{
			await _dbcontext.AddAsync(cartItem);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<Cart?> GetCartWithDetails(string userId)
		{
			return await _dbcontext.Carts.Include(c => c.CartItems).ThenInclude(cartItem => cartItem.ProductVariant).ThenInclude(pv => pv.Product).Where(c => c.ApplicationUserId == userId).FirstOrDefaultAsync();
		}

		

		public async Task<CartItem?> GetCartItemByProductVariantId(string userId, int productVariantId)
		{
			return await _dbcontext.CartItems.FirstOrDefaultAsync(ci => ci.Cart.ApplicationUserId == userId && ci.ProductVariantId == productVariantId);
		}

		public async Task<bool> ProductVariantExistInCart(string userId, int productVariantId)
		{
			return await _dbcontext.CartItems.AnyAsync(ci => ci.Cart.ApplicationUserId == userId && ci.ProductVariantId == productVariantId);
		}

		public async Task UpdateCartItem(CartItem cartItem)
		{
			_dbcontext.Update(cartItem);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<Cart?> GetCart(string userId)
		{
			return await _dbcontext.Carts.FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
		}

		public async Task ClearCart(string userId)
		{
			Cart cart = await GetCartWithDetails(userId);
		    _dbcontext.RemoveRange(cart.CartItems);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<CartItem?> GetCartItemWithDetails(int id)
		{
			return await _dbcontext.CartItems.Include(ci => ci.ProductVariant).FirstOrDefaultAsync(ci => ci.Id == id);
		}

		public async Task<bool> CheckCartItemBelongToUser(string userId, int cartItemId)
		{
			return await _dbcontext.CartItems.AnyAsync(ci => ci.Id == cartItemId && ci.Cart.ApplicationUserId == userId);
		}

		public async Task<bool> CartItemExist(int id)
		{
			return await _dbcontext.CartItems.AnyAsync(ci => ci.Id == id);
		}

		public async Task DeleteCartItem(CartItem cartItem)
		{
			_dbcontext.Remove(cartItem);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<CartItem?> GetCartItemById(int id)
		{
			return await _dbcontext.CartItems.FirstOrDefaultAsync(ci => ci.Id == id);
		}
	}
}
