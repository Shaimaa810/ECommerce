using ECommerce.DTO;

namespace ECommerce.ServiceContracts
{
	public interface IProductVariantService
	{
		Task Add(ProductVariantRequestDTO productVariantRequestDTO);

		Task Update (ProductVariantUpdateDTO productVariantUpdateDTO);

		Task Delete(int id);

		Task<ProductVariantResponseDTO?> GetById(int id); 


		Task<bool> Exist(int id);

		Task<List<ProductVariantResponseDTO>?> GetProductVariantsByProduct(int productId);
	}
}
