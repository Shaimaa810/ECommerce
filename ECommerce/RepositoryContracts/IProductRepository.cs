using ECommerce.DTO;
using ECommerce.Models;

namespace ECommerce.RepositoryContracts
{
	public interface IProductRepository
	{
		Task Add(Product product);

		Task Update(Product product);

		Task Delete(Product product);

		Task<Product?> GetById(int id);  // return product without navigation properties

		Task<Product?> GetByIdWithDetails(int id);  // return product with navigation properties

		Task<List<Product>?> GetAll(); 

		Task<bool> Exist(int id);

		Task<List<Product>?> GetProductsByCategory(int categoryId);


	}
}
