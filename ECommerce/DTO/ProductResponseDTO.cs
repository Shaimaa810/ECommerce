namespace ECommerce.DTO
{
	public class ProductResponseDTO
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public bool IsAvailable { get; set; }

		public double Price { get; set; }

		public string CategoryName { get; set; }

		public List<ProductVariantResponseDTO> ProductVariants { get; set; }

		public List<ProductImageResponseDTO> ProductImages { get; set; }
	}
}
