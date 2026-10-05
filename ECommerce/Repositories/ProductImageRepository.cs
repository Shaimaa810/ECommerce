using ECommerce.Models;
using ECommerce.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ECommerce.Repositories
{
	public class ProductImageRepository : IProductImageRepository
	{
		private readonly ApplicationDbContext _dbcontext;
		public ProductImageRepository(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public async Task Add(List<ProductImage> productImages)
		{
			await _dbcontext.AddRangeAsync(productImages);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task Delete(ProductImage productImage)
		{
			_dbcontext.Remove(productImage);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<bool> Exist(int id)
		{
			return await _dbcontext.ProductImages.AnyAsync(p =>  p.Id == id);
		}

		public async Task<ProductImage?> GetById(int id)
		{
			return await _dbcontext.ProductImages.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
