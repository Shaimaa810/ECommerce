using ECommerce.DTO;

namespace ECommerce.ServiceContracts
{
	public interface IProductService
	{
		Task Add(ProductRequestDTO productRequestDTO);


		Task Update(ProductUpdateDTO productUpdateDTO);


		Task<ProductResponseDTO?> GetById(int id);

		Task<ProductResponseDTO?> GetByIdWithDetails(int id, HttpRequest request);

		Task<List<ProductResponseDTO>?> GetAll(HttpRequest request);

		Task<bool> Exist(int id);

		Task Delete(int id);

		Task<List<ProductResponseDTO>?> GetProductsByCategory(int categoryId, HttpRequest request);

	}
}
