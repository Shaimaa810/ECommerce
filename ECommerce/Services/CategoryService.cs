using ECommerce.DTO;
using ECommerce.Models;
using ECommerce.RepositoryContracts;
using ECommerce.ServiceContracts;

namespace ECommerce.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task Add(CategoryDTO categoryDTO)
		{
			Category category = new Category()
			{
				Name = categoryDTO.Name,
			};
			await _categoryRepository.Add(category);
		}

		public async Task Delete(int id)
		{
			Category? category = await _categoryRepository.GetById(id);
			await _categoryRepository.Delete(category);
		}

		public async Task<List<CategoryDTO>> GetAll()
		{
			List<Category>? categories = await _categoryRepository.GetAll();

			return categories?.Select(c => new CategoryDTO()
			{
				Name = c.Name,
				Id = c.Id
			}).ToList() ?? new List<CategoryDTO>();
		}

		public async Task<CategoryDTO?> GetById(int id)
		{
			Category? category = await _categoryRepository.GetById(id);
			if (category == null) return null;
			return new CategoryDTO()
			{
				Name = category.Name,
				Id = category.Id
			};
		}

		public async Task<bool> Exist(int id)
		{
			return await _categoryRepository.Exist(id);
		}

		public async Task Update(CategoryDTO categoryDTO)
		{
			Category? category = await _categoryRepository.GetById(categoryDTO.Id);
			category.Name = categoryDTO.Name;
			await _categoryRepository.Update(category);
		}
	}
}
