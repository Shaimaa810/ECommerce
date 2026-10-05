using ECommerce.Models;
using ECommerce.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _dbcontext;
		public CategoryRepository(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public async Task Add(Category category)
		{
			await _dbcontext.AddAsync(category);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task Delete(Category category)
		{
			_dbcontext.Remove(category);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<List<Category>?> GetAll()
		{
			List<Category>? categories = await _dbcontext.Categories.ToListAsync();
			return categories;
		}

		public async Task<Category?> GetById(int id)
		{
			Category? category = await _dbcontext.Categories.FirstOrDefaultAsync(c => c.Id == id);
			return category;	
		}

		public async Task<bool> Exist(int id)
		{
			return await _dbcontext.Categories.AnyAsync(c => c.Id == id);
		}

		public async Task Update(Category category)
		{
			_dbcontext.Update(category);
			await _dbcontext.SaveChangesAsync();
		}

	}
}
