using ECommerce.DTO;
using ECommerce.Models;

namespace ECommerce.RepositoryContracts
{
	public interface IProductVariantRepository
	{
		Task Add(ProductVariant productVariant);

		Task Update(ProductVariant productVariant);

		Task Delete(ProductVariant productVariant);

		Task<ProductVariant?> GetById(int id);

		Task<bool> Exist(int id);

		Task<List<ProductVariant>?> GetProductVariantsByProduct(int productId);


		Task<ProductVariant?> GetByIdWithDetails(int id); 

	}
}
