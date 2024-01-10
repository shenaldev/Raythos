using Raythos.DTOs.Private;

namespace Raythos.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<ICollection<OrderItemDto>> GetOrderItems(long orderId);

        Task<bool> AddOrderItem(long orderId, CartDto cartItem);
    }
}
