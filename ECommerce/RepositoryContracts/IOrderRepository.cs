using ECommerce.Models;

namespace ECommerce.RepositoryContracts
{
	public interface IOrderRepository
	{
		Task Add(Order order);

		Task<List<Order>?> GetNotDeliveredOrders();

		Task DeliverOrder(int orderId);


		Task<List<Order>?> GetCustomerOrders(string userId);


		//Task Add(Order order);

		//Task Update(Order order);

		//Task<OrderResponseDTO?> GetByIdWithDetails(int id);

		//Task<Order?> GetById(int id);

		//Task<List<OrderResponseDTO>?> GetNotDeliveredOrders();

		//Task<List<OrderResponseDTO>?> GetUserOrders(string userId);

		//Task<bool> Exists(int id);


	}
}
