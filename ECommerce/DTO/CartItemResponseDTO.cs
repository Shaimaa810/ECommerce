namespace ECommerce.DTO
{
	public class CartItemResponseDTO
	{
		public int CartItemId { get; set; }

		public int ProductId { get; set; }

		public int ProductVariantId { get; set; }

		public string ProductName { get; set; }

		public string ProductDescription { get; set; }

		public double UnitPrice { get; set; }

		public double TotalPrice { get; set; }

		public string Color { get; set; }

		public string Size { get; set; }

		public int Quantity { get; set; }
	}
}
