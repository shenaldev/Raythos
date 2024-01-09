using Raythos.DTOs.Private;

namespace Raythos.Interfaces
{
    public interface IOrderItemInterface
    {
        ICollection<OrderItemDto> GetOrderItems(long orderId);

        bool AddOrderItem(long orderId, ICollection<CartDto> cartItems);
    }
}
