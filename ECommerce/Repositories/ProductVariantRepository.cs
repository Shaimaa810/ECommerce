using ECommerce.Models;
using ECommerce.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
	public class ProductVariantRepository : IProductVariantRepository
	{
		private readonly ApplicationDbContext _dbcontext;
		public ProductVariantRepository(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}


		public async Task Add(ProductVariant productVariant)
		{
			await _dbcontext.AddAsync(productVariant);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task Delete(ProductVariant productVariant)
		{
			_dbcontext.Remove(productVariant);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<ProductVariant?> GetById(int id)
		{
			return await _dbcontext.ProductVariants.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<ProductVariant?> GetByIdWithDetails(int id)
		{
			return await _dbcontext.ProductVariants.Include(pv => pv.Product).
													FirstOrDefaultAsync(pv => pv.Id == id);
		}

		public async Task<List<ProductVariant>?> GetProductVariantsByProduct(int productId)
		{
			return await _dbcontext.ProductVariants.Where(pv => pv.ProductId == productId).ToListAsync();	
		}

		public async Task<bool> Exist(int id)
		{
			return await _dbcontext.ProductVariants.AnyAsync(p => p.Id == id);
		}

		public async Task Update(ProductVariant productVariant)
		{
			_dbcontext.Update(productVariant);
			await _dbcontext.SaveChangesAsync();
		}

	}
}
