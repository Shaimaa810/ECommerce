using ECommerce.Models;

namespace ECommerce.RepositoryContracts
{
	public interface ICategoryRepository
	{
		Task Add(Category category);

		Task Update(Category category);

		Task Delete(Category category);

		Task<Category?> GetById (int id);

		Task<List<Category>?> GetAll();

		Task<bool> Exist(int id);
	}
}
