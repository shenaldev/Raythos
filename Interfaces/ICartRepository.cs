using Raythos.DTOs.Private;

namespace Raythos.Interfaces
{
    public interface ICartRepository
    {
        Task<ICollection<CartDto>> GetCartItems(long userID);

        Task<CartDto?> GetCartItem(long id);

        Task<CartDto?> AddToCart(CreateCartDto cart);

        Task<CartDto?> UpdateCart(long id, CreateCartDto cart);

        Task<bool> DeleteCart(long id);

        Task<bool> IsCartExists(long userId, long aircraftId);

        Task<bool> IsCartExists(long id);

        Task<int> GetTotalCarts();
    }
}
