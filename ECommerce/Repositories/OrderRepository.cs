using ECommerce.Models;
using ECommerce.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _dbContext;
		public OrderRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Add(Order order)
		{
			await _dbContext.AddAsync(order);
			await _dbContext.SaveChangesAsync();
		}

		public async Task DeliverOrder(int orderId)
		{
			Order? order = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
			order.IsDelivered = true;
			await _dbContext.SaveChangesAsync();
		}

		public async Task<List<Order>?> GetCustomerOrders(string userId)
		{
			List<Order>? orders = await _dbContext.Orders.Include(o => o.OrderItems).ThenInclude(orderItem => orderItem.ProductVariant).ThenInclude(pv => pv.Product).
														  Include(o => o.Coupon).	
												          Where(o => o.ApplicationUserId == userId).ToListAsync();	
			return orders;
		}

		public async Task<List<Order>?> GetNotDeliveredOrders()
		{
			return await _dbContext.Orders.Include(o => o.ApplicationUser).
										   Include(o => o.OrderItems).ThenInclude(orderItem => orderItem.ProductVariant).ThenInclude(pv => pv.Product).
										   Include(o => o.Coupon).
										   Where(o => !o.IsDelivered).ToListAsync();

		}





















		//public async Task Add(Order order)
		//{
		//	await _dbContext.AddAsync(order);
		//	await _dbContext.SaveChangesAsync();
		//}

		//public async Task<bool> Exists(int id)
		//{
		//	return await _dbContext.Orders.AllAsync(o => o.Id == id);
		//}

		//public Task<Order?> GetById(int id)
		//{
		//	return _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
		//}

		//public async Task<OrderResponseDTO?> GetByIdWithDetails(int id)
		//{
		//	return await _dbContext.Orders.Include(o => o.Coupon).
		//								   Include(o => o.OrderItems).	
		//										ThenInclude(orderItem => orderItem.ProductVariant).
		//								   Where(o => o.Id == id).
		//								   Select(o => new OrderResponseDTO()
		//								   {
		//									   Id = o.Id,
		//									   IsDelivered = o.IsDelivered,
		//									   TotalBeforeDiscount = o.TotalBeforeDiscount,
		//									   TotalAfterDiscount = o.TotalAfterDiscount,
		//									   Discount = o.Discount,
		//									   CreatedAt = o.CreatedAt,
		//									   CouponCode = (o.Coupon == null) ? null : o.Coupon.Code,
		//									   OrderItems = o.OrderItems.Select(orderItem => new OrderItemResponseDTO()
		//									   {
		//										   Id = orderItem.Id,
		//										   Quantity = orderItem.Quantity,
		//										   ProductVariant = new ProductVariantResponseDTO()
		//										   {
		//											   Id = orderItem.ProductVariant.Id,
		//											   Color = orderItem.ProductVariant.Color,
		//											   Size = orderItem.ProductVariant.Size,
		//											   Quantity = orderItem.ProductVariant.Quantity
		//										   }
		//									   }).ToList()

		//								   }).FirstOrDefaultAsync();
		//}

		//public async Task<List<OrderResponseDTO>?> GetNotDeliveredOrders()
		//{
		//	return await _dbContext.Orders.Include(o => o.Coupon).
		//								   Include(o => o.OrderItems).
		//										ThenInclude(orderItem => orderItem.ProductVariant).
		//								   Where(o => !o.IsDelivered).
		//								   Select(o => new OrderResponseDTO()
		//								   {
		//									   Id = o.Id,
		//									   IsDelivered = o.IsDelivered,
		//									   TotalBeforeDiscount = o.TotalBeforeDiscount,
		//									   TotalAfterDiscount = o.TotalAfterDiscount,
		//									   Discount = o.Discount,
		//									   CreatedAt = o.CreatedAt,
		//									   CouponCode = (o.Coupon == null) ? null : o.Coupon.Code,
		//									   OrderItems = o.OrderItems.Select(orderItem => new OrderItemResponseDTO()
		//									   {
		//										   Id = orderItem.Id,
		//										   Quantity = orderItem.Quantity,
		//										   ProductVariant = new ProductVariantResponseDTO()
		//										   {
		//											   Id = orderItem.ProductVariant.Id,
		//											   Color = orderItem.ProductVariant.Color,
		//											   Size = orderItem.ProductVariant.Size,
		//											   Quantity = orderItem.ProductVariant.Quantity
		//										   }
		//									   }).ToList()

		//								   }).ToListAsync();
		//}

		//public async Task<List<OrderResponseDTO>?> GetUserOrders(string userId)
		//{
		//	return await _dbContext.Orders.Include(o => o.Coupon).
		//								   Include(o => o.OrderItems).
		//										ThenInclude(orderItem => orderItem.ProductVariant).
		//								   Where(o => o.ApplicationUserId == userId).
		//								   Select(o => new OrderResponseDTO()
		//								   {
		//									   Id = o.Id,
		//									   IsDelivered = o.IsDelivered,
		//									   TotalBeforeDiscount = o.TotalBeforeDiscount,
		//									   TotalAfterDiscount = o.TotalAfterDiscount,
		//									   Discount = o.Discount,
		//									   CreatedAt = o.CreatedAt,
		//									   CouponCode = (o.Coupon == null) ? null : o.Coupon.Code,
		//									   OrderItems = o.OrderItems.Select(orderItem => new OrderItemResponseDTO()
		//									   {
		//										   Id = orderItem.Id,
		//										   Quantity = orderItem.Quantity,
		//										   ProductVariant = new ProductVariantResponseDTO()
		//										   {
		//											   Id = orderItem.ProductVariant.Id,
		//											   Color = orderItem.ProductVariant.Color,
		//											   Size = orderItem.ProductVariant.Size,
		//											   Quantity = orderItem.ProductVariant.Quantity
		//										   }
		//									   }).ToList()

		//								   }).ToListAsync();
		//}

		//public async Task Update(Order order)
		//{
		//	_dbContext.Update(order);
		//	await _dbContext.SaveChangesAsync();
		//}
	}
}
