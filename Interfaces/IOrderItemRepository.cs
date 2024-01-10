using Raythos.DTOs.Private.CartDtos;
using Raythos.DTOs.Private.OrderItemDtos;

namespace Raythos.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<ICollection<OrderItemDto>> GetOrderItems(long orderId);

        Task<bool> AddOrderItem(long orderId, CartDto cartItem);

        Task<bool> DeleteOrderItem(long id);
    }
}
