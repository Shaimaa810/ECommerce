using ECommerce.Constraints;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
	public class Category
	{
		public int Id { get; set; }


		[UniqueCategoryName]
		[StringLength(200, MinimumLength = 2)]
		public string Name { get; set; }



		public ICollection<Product> Products { get; set; }
	}
}
