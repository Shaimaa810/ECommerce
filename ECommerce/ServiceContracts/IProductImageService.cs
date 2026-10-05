namespace ECommerce.ServiceContracts
{
	public interface IProductImageService
	{
		Task Add(int productId, List<IFormFile> images);

		Task Delete(int id);

		Task<bool> Exist(int id);
	}
}
