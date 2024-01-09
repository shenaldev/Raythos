using Raythos.DTOs.Private;

namespace Raythos.Interfaces
{
    public interface IOrderInterface
    {
        ICollection<OrderDto> GetOrders(int skip, int take = 15);

        ICollection<OrderDto> GetOrdersByUserId(long userId, int skip, int take = 15);

        OrderDto GetOrder(long id);

        OrderDto CreateOrder(CreateOrderDto dto);

        OrderDto UpdateOrder(long id, CreateCartDto dto);

        bool UpdateOrderStatus(long id, string status);

        bool DeleteOrder(long id);

        bool OrderExists(long id);

        int GetOrdersCount();

        int GetOrdersCountByUserId(long userId);
    }
}
