using Raythos.DTOs.Private;

namespace Raythos.Interfaces
{
    public interface ICartRepository
    {
        public ICollection<CartDto> GetCarts(long userID);

        public CartDto GetCart(long id);

        public CartDto AddToCart(CreateCartDto cart);

        public CartDto UpdateCart(long id, CreateCartDto cart);

        public bool DeleteCart(long id);

        public bool IsCartExists(long userId, long aircraftId);

        public bool IsCartExists(long id);

        public int GetTotalCarts();
    }
}
