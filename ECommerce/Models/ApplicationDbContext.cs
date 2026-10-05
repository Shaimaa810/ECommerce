using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Models
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options):base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Category>()
				.HasMany(c => c.Products)
				.WithOne(p => p.Category)
				.OnDelete(DeleteBehavior.Restrict);
		}

		public DbSet<Cart> Carts { get; set; }

		public DbSet<CartItem> CartItems { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<Coupon> Coupons { get; set; }

		public DbSet<Order>  Orders { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<ProductImage> ProductImages { get; set; }

		public DbSet<ProductVariant> ProductVariants { get; set; }
	}
}
