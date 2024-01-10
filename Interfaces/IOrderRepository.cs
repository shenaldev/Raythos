using Raythos.DTOs.Private.CartDtos;
using Raythos.DTOs.Private.OrderDtos;

namespace Raythos.Interfaces
{
    public interface IOrderRepository
    {
        Task<ICollection<OrderDto>> GetOrders(int skip, int take = 15);

        Task<ICollection<OrderDto>> GetOrdersByUserId(long userId, int skip, int take = 15);

        Task<SingleUserOrderDto?> GetOrder(long id);

        Task<SingleOrderDto?> GetOrderAdmin(long id);

        Task<OrderDto?> CreateOrder(CreateOrderDto dto);

        Task<OrderDto?> UpdateOrder(long id, CreateCartDto dto);

        Task<bool> UpdateOrderStatus(long id, string status);

        Task<bool> DeleteOrder(long id);

        Task<bool> IsOrderExists(long id);

        Task<int> GetOrdersCount();

        Task<int> GetOrdersCountByUserId(long userId);
    }
}
