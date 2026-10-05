namespace ECommerce.DTO
{
	public class OrderItemResponseDTO
	{
		public int ProductId { get; set; }

		public string ProductName { get; set; }

		public string ProductDescription { get; set; }

		public string Size { get; set; }

		public string Color { get; set; }

		public int Quantity { get; set; }

		public double UnitPrice { get; set; }

		public double TotalPrice { get; set; }
	}
}
