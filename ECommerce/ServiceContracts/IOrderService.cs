using ECommerce.DTO;

namespace ECommerce.ServiceContracts
{
	public interface IOrderService
	{
		Task<string> Add(string userId, string couponCode);

		Task<List<AdminOrderResponseDTO>> GetNotDeliveredOrders();

		Task DeliverOrder(int  orderId);

		Task<List<CustomerOrderResponseDTO>?> GetCustomerOrders(string userId);














		//Task Add(OrderRequestDTO orderRequestDTO, string userId);

		//Task<OrderResponseDTO?> GetByIdWithDetails(int id);

		//Task<List<OrderResponseDTO>?> GetUserOrders(string userId);

		//Task<List<OrderResponseDTO>?> GetNotDeliveredOrders();

		//Task DeliverOrder(int orderId);

		//Task<bool> Exists(int id);
	}
}
