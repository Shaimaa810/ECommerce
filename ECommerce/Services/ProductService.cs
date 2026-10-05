using Azure.Core;
using ECommerce.DTO;
using ECommerce.Models;
using ECommerce.RepositoryContracts;
using ECommerce.ServiceContracts;

namespace ECommerce.Services
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		public async Task Add(ProductRequestDTO productRequestDTO)
		{
			Product product = new Product()
			{
				Name = productRequestDTO.Name,
				Description = productRequestDTO.Description,
				Price = productRequestDTO.Price,
				IsAvailable = productRequestDTO.IsAvailable,
				CategoryId = productRequestDTO.CategoryId,
				ProductVariants = productRequestDTO.ProductVariants.
									Select(pv => new ProductVariant() 
									{
										Color = pv.Color, 
										Size = pv.Size, 
										Quantity = pv.Quantity 
									}).ToList()
			};
			await _productRepository.Add(product);
		}

		public async Task Delete(int id)
		{
			Product? product = await _productRepository.GetById(id);
			await _productRepository.Delete(product);
		}

		public async Task<List<ProductResponseDTO>?> GetAll(HttpRequest request)
		{
			List<Product>? products = await _productRepository.GetAll();
			if (products == null) return new List<ProductResponseDTO>();  // return empty list
			List<ProductResponseDTO> productResponseDTOs = new List<ProductResponseDTO>();
			foreach (Product product in products)
			{
				productResponseDTOs.Add(ConvertProductToProductResponseDto(product, request));
			}
			return productResponseDTOs;
		}

		public async Task<ProductResponseDTO?> GetById(int id)
		{
			Product? product = await _productRepository.GetById(id);
			if (product == null) return null;
			return new ProductResponseDTO()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				IsAvailable = product.IsAvailable,
			};
		}

		public async Task<List<ProductResponseDTO>?> GetProductsByCategory(int categoryId, HttpRequest request)
		{
			List<Product>? products = await _productRepository.GetProductsByCategory(categoryId);
			if (products == null) return new List<ProductResponseDTO>();  // return empty list
			List<ProductResponseDTO> productResponseDTOs = new List<ProductResponseDTO>();
			foreach (Product product in products)
			{
				productResponseDTOs.Add(ConvertProductToProductResponseDto(product, request));
			}
			return productResponseDTOs;
		}

		public async Task<ProductResponseDTO?> GetByIdWithDetails(int id, HttpRequest request)
		{
			Product? product = await _productRepository.GetByIdWithDetails(id);
			if (product == null) return null;
			return ConvertProductToProductResponseDto(product, request);
		}

		public async Task<bool> Exist(int id)
		{
			return await _productRepository.Exist(id);
		}

		public async Task Update(ProductUpdateDTO productUpdateDTO)
		{
			Product? product = await _productRepository.GetById(productUpdateDTO.Id);
			product.Name = productUpdateDTO.Name;
			product.Description = productUpdateDTO.Description;
			product.Price = productUpdateDTO.Price;
			product.IsAvailable = productUpdateDTO.IsAvailable;
			product.CategoryId = productUpdateDTO.CategoryId;

			await _productRepository.Update(product);
		}


		private ProductResponseDTO ConvertProductToProductResponseDto(Product product, HttpRequest request)
		{
			return new ProductResponseDTO()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				IsAvailable = product.IsAvailable,
				CategoryName = product.Category.Name,
				ProductVariants = product.ProductVariants.Select(pv => new ProductVariantResponseDTO()
				{
					Id = pv.Id,
					Color = pv.Color,
					Size = pv.Size,	
					Quantity = pv.Quantity
				}).ToList(),
				ProductImages = product.ProductImages.Select(image => new ProductImageResponseDTO()
				{
					Id = image.Id,
					// request.Scheme -> Protocol being used -> "https" or "http"
					// request.Host -> The domain + port of your server:
					ImageUrl = $"{request.Scheme}://{request.Host}{image.RelativePath}"
				}).ToList(),
			};
		}
	}
}
