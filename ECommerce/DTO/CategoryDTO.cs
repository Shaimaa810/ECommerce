using ECommerce.Constraints;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTO
{
	public class CategoryDTO
	{
		public int Id { get; set; }

		[UniqueCategoryName]
		[StringLength(200, MinimumLength = 2)]
		public string Name { get; set; }
	}
}
