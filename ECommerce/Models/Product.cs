using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
	public class Product
	{
		public int Id { get; set; }


		[StringLength(100, MinimumLength = 2)]
		public string Name { get; set; }



		[StringLength(300, MinimumLength = 2)]
		public string Description { get; set; }


		[Range(0, double.MaxValue)]
		public double Price { get; set; }


		public bool IsAvailable { get; set; }



		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }



		public ICollection<ProductImage> ProductImages { get; set; }

		public ICollection<ProductVariant> ProductVariants { get; set; }
	}
}
