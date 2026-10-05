using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
	public class Cart
	{
		public int Id { get; set; }

		public ICollection<CartItem>? CartItems { get; set; }


		[ForeignKey("ApplicationUser")]
		public string ApplicationUserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
	}
}
