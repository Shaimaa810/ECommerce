using ECommerce.DTO;
using ECommerce.Enums;
using ECommerce.Models;
using ECommerce.RepositoryContracts;
using ECommerce.ServiceContracts;

namespace ECommerce.Services
{
	public class OrderService : IOrderService
	{
		private readonly ICouponRepository _couponRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly ICartRepository _cartRepository;
		private readonly IProductVariantRepository _productVariantRepository;

		public OrderService(ICouponRepository couponRepository, IOrderRepository orderRepository, ICartRepository cartRepository, IProductVariantRepository productVariantRepository)
		{
			_couponRepository = couponRepository;
			_orderRepository = orderRepository;
			_cartRepository = cartRepository;
			_productVariantRepository = productVariantRepository;
		}

		public async Task<string> Add(string userId, string couponCode)
		{
			Cart? cart = await _cartRepository.GetCartWithDetails(userId);
			double totalBeforeDiscount = cart.CartItems.Sum(ci => ci.ProductVariant.Product.Price * ci.Quantity);
			List<OrderItem> orderItems = new List<OrderItem>();
			foreach (CartItem cartItem in cart.CartItems)
			{
				orderItems.Add(new OrderItem()
				{
					Quantity = cartItem.Quantity,
					ProductVariantId = cartItem.ProductVariantId
				});
				ProductVariant productVariant = cartItem.ProductVariant;
				productVariant.Quantity -= cartItem.Quantity;
				await _productVariantRepository.Update(productVariant);
			}
			// check if coupon valid or not
			Coupon? coupon = null;
			double discount = 0;
			bool couponValid = true;
			string errorMessage = "";
			if (couponCode != null)
			{
				coupon = await _couponRepository.GetByCode(couponCode);
				if (coupon != null)
				{
					// check if coupon valid or not
					// check if coupon not expired
					if (!(coupon.StartDate < DateTime.Now && coupon.ExpirationDate > DateTime.Now))
					{
						errorMessage = "Coupon is not available yet.";
						couponValid = false;
					}
					// check if coupon active or not
					if (!coupon.IsActive)
					{
						errorMessage = "Coupon is not active.";
						couponValid = false;
					}
					// check if coupon reach max uses or not
					if (coupon.UsedCount == coupon.MaxUses)
					{
						errorMessage = "Coupon usage limit has been reached.";
						couponValid = false;
					}
					// check if order price > min order amount
					if (totalBeforeDiscount < coupon.MinOrderAmount)
					{
						errorMessage = $"Minimum order amount is {coupon.MinOrderAmount}.";
						couponValid = false;
					}
				}
				else
				{
					errorMessage = "Coupon not exist, invalid coupon code";
					return errorMessage;
				}
				if (couponValid)
				{
					if (coupon.DiscountType == DiscountType.Fixed)
						discount = coupon.Value;
					else  // discount type -> percentage
						discount = totalBeforeDiscount * (coupon.Value / 100);
					coupon.UsedCount += 1;
					await _couponRepository.Update(coupon);
				}
				else
					return errorMessage;
			}
			Order order = new Order()
			{
				OrderItems = orderItems,
				TotalBeforeDiscount = totalBeforeDiscount,
				Discount = discount,
				TotalAfterDiscount = totalBeforeDiscount - discount,
				IsDelivered = false,
				CreatedAt = DateTime.Now,
				ApplicationUserId = userId,
				CouponId = coupon?.Id
			};

			await _orderRepository.Add(order);
			await _cartRepository.ClearCart(userId);
			return errorMessage;      // errorMessage = ""
		}

		public async Task DeliverOrder(int orderId)
		{
			await _orderRepository.DeliverOrder(orderId);
		}

		public async Task<List<CustomerOrderResponseDTO>?> GetCustomerOrders(string userId)
		{
			List<Order>? orders = await _orderRepository.GetCustomerOrders(userId);
			if (orders == null)
				return new List<CustomerOrderResponseDTO>() { };  // return empty list
			List<CustomerOrderResponseDTO> customerOrderResponseDTOs = new List<CustomerOrderResponseDTO>();
			foreach (Order order in orders)
			{
				customerOrderResponseDTOs.Add(new CustomerOrderResponseDTO()
				{
					OrderId = order.Id,
					OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDTO()
					{
						ProductId = oi.ProductVariant.ProductId,
						ProductName = oi.ProductVariant.Product.Name,
						ProductDescription = oi.ProductVariant.Product.Description,
						Size = oi.ProductVariant.Size,
						Color = oi.ProductVariant.Color,
						Quantity = oi.Quantity,
						UnitPrice = oi.ProductVariant.Product.Price,
						TotalPrice = oi.ProductVariant.Product.Price * oi.Quantity
					}).ToList(),
					CouponCode = (order.Coupon == null) ? null : order.Coupon.Code,
					Discount = order.Discount,
					TotalBeforeDiscount = order.TotalBeforeDiscount,
					TotalAfterDiscount = order.TotalAfterDiscount,
					Status = (order.IsDelivered) ? "Delivered" : "Not Delivered",
					CreatedAt = order.CreatedAt,
				});
			}
			return customerOrderResponseDTOs;
		}

		public async Task<List<AdminOrderResponseDTO>> GetNotDeliveredOrders()
		{
			List<Order>? orders = await _orderRepository.GetNotDeliveredOrders();
			if (orders == null)
				return new List<AdminOrderResponseDTO>();  // return empty list
			List<AdminOrderResponseDTO> orderResponseDTOs = new List<AdminOrderResponseDTO>();
			foreach(Order order in orders)
			{
				orderResponseDTOs.Add(new AdminOrderResponseDTO()
				{
					OrderId = order.Id,
					UserId = order.ApplicationUserId,
					CustomerName = order.ApplicationUser.FirstName + " " + order.ApplicationUser.LastName,
					CustomerEmail = order.ApplicationUser.Email,
					CustomerPhoneNumber = order.ApplicationUser.PhoneNumber,
					CustomerShippingAddress = order.ApplicationUser.AddressDetails,
					OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDTO()
					{
						ProductId = oi.ProductVariant.ProductId,
						ProductName = oi.ProductVariant.Product.Name,
						ProductDescription = oi.ProductVariant.Product.Description,
						Color = oi.ProductVariant.Color,
						Size = oi.ProductVariant.Size,
						Quantity = oi.Quantity,
						UnitPrice = oi.ProductVariant.Product.Price,
						TotalPrice = oi.ProductVariant.Product.Price * oi.Quantity
					}).ToList(),
					CouponCode = (order.Coupon == null) ? null : order.Coupon.Code,
					Discount = order.Discount,
					TotalBeforeDiscount = order.TotalBeforeDiscount,
					TotalAfterDiscount = order.TotalAfterDiscount,
					CreatedAt = order.CreatedAt,
				
				});
			}
			return orderResponseDTOs;

		}
	}
}
