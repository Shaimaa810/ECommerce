using ECommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Constraints
{
	public class UniqueCategoryNameAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
				return new ValidationResult("Category name is required");

			ApplicationDbContext? dbcontext = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));
			if (dbcontext == null)
				throw new Exception("DbContext not found");

			string categoryName = (string)value;
			int currentId = 0;
			var dto = validationContext.ObjectInstance;
			var idProperty = dto.GetType().GetProperty("Id");
			if(idProperty != null)
			{
				currentId = (int)(idProperty.GetValue(dto) ?? 0);
			}

			Category? category = dbcontext.Categories.FirstOrDefault(c => c.Name == categoryName && c.Id != currentId);

			if (category == null)
				return ValidationResult.Success;
			return new ValidationResult("Category name must be unique");

		}
	}
}
