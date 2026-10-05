using ECommerce.Models;
using ECommerce.RepositoryContracts;
using ECommerce.ServiceContracts;

namespace ECommerce.Services
{
	public class ProductImageService : IProductImageService
	{
		private readonly IProductImageRepository _productImageRepository;
		public ProductImageService(IProductImageRepository productImageRepository)
		{
			_productImageRepository = productImageRepository;
		}

		public async Task Add(int productId, List<IFormFile> images)
		{
			// save images in wwwroot/productImages
			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages");
			if (!Directory.Exists(folderPath))
			{
				Directory.CreateDirectory(folderPath);
			}
			List<ProductImage> productImages = new List<ProductImage>();
			foreach (IFormFile image in images)
			{
				string uniqueImageName = Guid.NewGuid().ToString() + image.FileName;
				string relativePath = $"/ProductImages/{uniqueImageName}";
				string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages", uniqueImageName);
				// save file 
				using (var stream = new FileStream(absolutePath, FileMode.Create))
				{
					image.CopyTo(stream);
				}
				ProductImage productImage = new ProductImage()
				{
					RelativePath = relativePath,
					ProductId = productId,
				};
				productImages.Add(productImage);
			}
			await _productImageRepository.Add(productImages);
		}

		public async Task Delete(int id)
		{
			ProductImage? productImage = await _productImageRepository.GetById(id);

			// delete image from wwwroot
			string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", productImage.RelativePath);
			if (File.Exists(absolutePath))
			{
				File.Delete(absolutePath);
			}
			await _productImageRepository.Delete(productImage);
		}

		public async Task<bool> Exist(int id)
		{
			return await _productImageRepository.Exist(id);
		}
	}
}
