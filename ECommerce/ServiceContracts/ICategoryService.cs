using ECommerce.DTO;
using ECommerce.Models;

namespace ECommerce.ServiceContracts
{
	public interface ICategoryService
	{
		Task Add(CategoryDTO categoryDTO);

		Task<List<CategoryDTO>> GetAll();

		Task<CategoryDTO?> GetById(int id);

		Task Update(CategoryDTO categoryDTO);

		Task Delete(int id);

		Task<bool> Exist(int id);
	}
}
