using ECommerce.DTO;
using ECommerce.Models;
using ECommerce.RepositoryContracts;
using ECommerce.ServiceContracts;

namespace ECommerce.Services
{
	public class ProductVariantService : IProductVariantService
	{
		private readonly IProductVariantRepository _productVariantRepository;
		public ProductVariantService(IProductVariantRepository productVariantRepository)
		{
			_productVariantRepository = productVariantRepository;
		}

		public async Task Add(ProductVariantRequestDTO productVariantRequestDTO)
		{
			ProductVariant productVariant = new ProductVariant()
			{
				Color = productVariantRequestDTO.Color,
				Size = productVariantRequestDTO.Size,
				Quantity = productVariantRequestDTO.Quantity,
				ProductId = productVariantRequestDTO.ProductId,
			};
			await _productVariantRepository.Add(productVariant);
		}

		public async Task Delete(int id)
		{
			ProductVariant? productVariant = await _productVariantRepository.GetById(id);
			await _productVariantRepository.Delete(productVariant);
		}

		public async Task<List<ProductVariantResponseDTO>?> GetProductVariantsByProduct(int productId)
		{
			List<ProductVariant>? productVariants = await _productVariantRepository.GetProductVariantsByProduct(productId);
			if (productVariants == null) 
				return new List<ProductVariantResponseDTO>(); // return empty list
			return productVariants.Select(pv => new ProductVariantResponseDTO()
			{
				Id = pv.Id,
				Color = pv.Color,
				Size = pv.Size,
				Quantity = pv.Quantity,
			}).ToList();
		}

		public async Task<ProductVariantResponseDTO?> GetById(int id)
		{
			ProductVariant? productVariant = await _productVariantRepository.GetById(id);
			if (productVariant == null) return null;
			return new ProductVariantResponseDTO()
			{
				Id = productVariant.Id,
				Color = productVariant.Color,
				Size = productVariant.Size,
				Quantity = productVariant.Quantity,
			};
		}

		public async Task<bool> Exist(int id)
		{
			return await _productVariantRepository.Exist(id);
		}

		public async Task Update(ProductVariantUpdateDTO productVariantUpdateDTO)
		{
			ProductVariant? productVariant = await _productVariantRepository.GetById(productVariantUpdateDTO.Id);
			productVariant.Color = productVariantUpdateDTO.Color;
			productVariant.Size = productVariantUpdateDTO?.Size;
			productVariant.Quantity = productVariantUpdateDTO.Quantity;
			productVariant.ProductId = productVariantUpdateDTO.ProductId;
			await _productVariantRepository.Update(productVariant);
		}
	}
}
