using ECommerce.DTO;
using ECommerce.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ECommerce.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly ICouponService _couponService;
		public OrderController(IOrderService orderService, ICouponService couponService)
		{
			_orderService = orderService;
			_couponService = couponService;
		}

		[HttpPost]
		[Authorize(Roles = "Customer")]
		public async Task<IActionResult> AddOrder([FromQuery]string? couponCode) // path -> api/order?couponCode=dhhd
		{
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			string errorMessage = await _orderService.Add(userId, couponCode);
			if (errorMessage.IsNullOrEmpty())
				return Ok("Order created successfully");
			return BadRequest(errorMessage);
		}


		[HttpGet("not-delivered")]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<List<AdminOrderResponseDTO>>> GetNotDeliveredOrder()
		{
			List<AdminOrderResponseDTO>? adminOrderResponseDTOs = await _orderService.GetNotDeliveredOrders();
			return Ok(adminOrderResponseDTOs);
		}


		[HttpPut("{orderId}/deliver")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeliverOrder(int orderId)
		{
			await _orderService.DeliverOrder(orderId);
			return Ok("order marked as delivered successfully");
		}


		[HttpGet]
		[Authorize(Roles = "Customer")]
		public async Task<ActionResult<List<CustomerOrderResponseDTO>?>> GetCustomerOrders()
		{
			string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			List<CustomerOrderResponseDTO>? orderes = await _orderService.GetCustomerOrders(userId);
			return Ok(orderes);
		}






		//[HttpPost]
		//[Authorize]
		//public async Task<IActionResult> AddOrder(OrderRequestDTO orderRequestDTO)
		//{
		//	string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		//	await _orderService.Add(orderRequestDTO, userId);
		//	return Ok(new { message = "order created successfully" });
		//}


		//[HttpGet("not-delivered")]
		//[Authorize(Roles = "Admin")]
		//public async Task<ActionResult<List<OrderResponseDTO>?>> GetNotDeliveredOrders()
		//{
		//	List<OrderResponseDTO>? notDeliveredOrders = await _orderService.GetNotDeliveredOrders();
		//	return Ok(notDeliveredOrders);
		//}


		//[HttpGet("{id}")]
		//[Authorize]
		//public async Task<ActionResult<OrderResponseDTO?>> GetById(int id)
		//{
		//	OrderResponseDTO? order = await _orderService.GetByIdWithDetails(id);
		//	if (order == null)
		//	{
		//		return Problem(detail: "Order not found, invalid order id", statusCode: 404);
		//	}
		//	return Ok(order);
		//}


		//[HttpGet("user-orders")]
		//[Authorize]
		//public async Task<ActionResult<List<OrderResponseDTO>?>> GserOrders()
		//{
		//	string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
		//	List<OrderResponseDTO>? orders = await _orderService.GetUserOrders(userId);
		//	return Ok(orders);
		//}


		//[HttpPatch("{id}")]
		//[Authorize(Roles = "Admin")]
		//public async Task<IActionResult> DeliverOrder(int id)
		//{
		//	bool exist = await _orderService.Exists(id);
		//	if (!exist)
		//	{
		//		return Problem(detail: "Order not found, invalid order id", statusCode: 404);
		//	}
		//	await _orderService.DeliverOrder(id);
		//	return Ok(new { message = "order updated successfully" });
		//}
	}
}
