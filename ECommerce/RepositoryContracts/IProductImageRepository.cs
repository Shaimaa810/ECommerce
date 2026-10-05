using ECommerce.Models;

namespace ECommerce.RepositoryContracts
{
	public interface IProductImageRepository
	{
		Task Add(List<ProductImage> productImages);

		Task Delete(ProductImage productImage);

		Task<ProductImage?> GetById(int id);

		Task<bool> Exist(int id);
	}
}
