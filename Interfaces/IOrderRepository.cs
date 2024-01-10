using Raythos.DTOs.Private;

namespace Raythos.Interfaces
{
    public interface IOrderRepository
    {
        Task<ICollection<OrderDto>> GetOrders(int skip, int take = 15);

        Task<ICollection<OrderDto>> GetOrdersByUserId(long userId, int skip, int take = 15);

        Task<OrderDto?> GetOrder(long id);

        Task<OrderDto?> CreateOrder(CreateOrderDto dto);

        Task<OrderDto?> UpdateOrder(long id, CreateCartDto dto);

        Task<bool> UpdateOrderStatus(long id, string status);

        Task<bool> DeleteOrder(long id);

        Task<bool> OrderExists(long id);

        Task<int> GetOrdersCount();

        Task<int> GetOrdersCountByUserId(long userId);
    }
}
