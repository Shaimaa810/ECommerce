namespace ECommerce.DTO
{
	public class CartResponseDTO
	{
		public int CartId { get; set; }	

		public double TotalPrice { get; set; }

		public List<CartItemResponseDTO>? CartItems { get; set; }

	}
}
