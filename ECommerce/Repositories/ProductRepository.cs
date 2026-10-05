using ECommerce.Models;
using ECommerce.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _dbcontext;
		public ProductRepository(ApplicationDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}

		public async Task Add(Product product)
		{
			await _dbcontext.AddAsync(product);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task Delete(Product product)
		{
			_dbcontext.Remove(product);
			await _dbcontext.SaveChangesAsync();
		}

		public async Task<List<Product>?> GetAll()
		{
			return await _dbcontext.Products.
									Include(p => p.ProductVariants).
									Include(p => p.Category).
									Include(p => p.ProductImages).
									ToListAsync();
											//Select(p => new ProductResponseDTO()
											//{
											//	Id = p.Id,
											//	Name = p.Name,
											//	Description = p.Description,
											//	Price = p.Price,
											//	IsAvailable = p.IsAvailable,
											//	CategoryName = p.Category.Name,
											//	ProductVariants = p.ProductVariants.Select(pv => new ProductVariantResponseDTO()
											//	{
											//		Id = pv.Id,
											//		Color = pv.Color,
											//		Size = pv.Size,
											//		Quantity = pv.Quantity,
											//	}).ToList(),
											//	ProductImages = p.ProductImages.Select(image => new ProductImageResponseDTO()
											//	{
											//		Id= image.Id,
											//		// request.Scheme -> Protocol being used -> "https" or "http"
											//		// request.Host -> The domain + port of your server:
											//		ImageUrl = $"{request.Scheme}://{request.Host}{image.RelativePath}"
											//	}).ToList()

											//}).ToListAsync();
		}

		public async Task<Product?> GetById(int id)
		{
			return await _dbcontext.Products.FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<List<Product>?> GetProductsByCategory(int categoryId)
		{
			return await _dbcontext.Products.
									Include(p => p.ProductVariants).
									Include(p => p.ProductImages).
									Include(p => p.Category).
									Where(p => p.CategoryId == categoryId).
									ToListAsync();


											//Select(p => new ProductResponseDTO()
											//{
											//	Id = p.Id,
											//	Name = p.Name,
											//	Description = p.Description,
											//	Price = p.Price,
											//	IsAvailable = p.IsAvailable,
											//	CategoryName = p.Category.Name,
											//	ProductVariants = p.ProductVariants.Select(pv => new ProductVariantResponseDTO()
											//	{
											//		Id = pv.Id,
											//		Size = pv.Size,
											//		Color = pv.Color,
											//		Quantity = pv.Quantity,
											//	}).ToList(),
											//	ProductImages = p.ProductImages.Select(image => new ProductImageResponseDTO()
											//	{
											//		Id = image.Id,
											//		// request.Scheme -> Protocol being used -> "https" or "http"
											//		// request.Host -> The domain + port of your server:
											//		ImageUrl = $"{request.Scheme}://{request.Host}{image.RelativePath}"
											//	}).ToList()
											//}).ToListAsync();
		}

		public async Task<Product?> GetByIdWithDetails(int id)
		{
			return await _dbcontext.Products.Include(p => p.ProductVariants).
											Include(p => p.Category).	
											Include(p => p.ProductImages).
											FirstOrDefaultAsync(p => p.Id == id);

			//Select(p => new ProductResponseDTO()
			//{
			//	Id = p.Id,
			//	Name = p.Name,
			//	Description = p.Description,
			//	Price = p.Price,
			//	IsAvailable = p.IsAvailable,
			//	CategoryName = p.Category.Name,
			//	ProductVariants = p.ProductVariants.Select(pv => new ProductVariantResponseDTO()
			//	{
			//		Id = pv.Id,
			//		Color = pv.Color,
			//		Size = pv.Size,
			//		Quantity = pv.Quantity,
			//	}).ToList(),
			//	ProductImages = p.ProductImages.Select(image => new ProductImageResponseDTO()
			//	{
			//		Id = image.Id,
			//		// request.Scheme -> Protocol being used -> "https" or "http"
			//		// request.Host -> The domain + port of your server:
			//		ImageUrl = $"{request.Scheme}://{request.Host}{image.RelativePath}"
			//	}).ToList()
			//})


		}

		public async Task<bool> Exist(int id)
		{
			return await _dbcontext.Products.AnyAsync(p => p.Id == id);
		}

		public async Task Update(Product product)
		{
			_dbcontext.Update(product);
			await _dbcontext.SaveChangesAsync();
		}
	}
}
