using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs.Private;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CartRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }

        public ICollection<CartDto> GetCarts(long userID)
        {
            return _mapper.Map<ICollection<CartDto>>(
                _context.Carts.Include(c => c.Aircraft).Where(c => c.UserId == userID).ToList()
            );
        }

        public CartDto GetCart(long id)
        {
            return _mapper.Map<CartDto>(
                _context.Carts.Include(c => c.Aircraft).Where(c => c.Id == id).FirstOrDefault()
            );
        }

        public CartDto AddToCart(CreateCartDto cart)
        {
            try
            {
                cart.CreatedAt = DateTime.Now;
                cart.UpdatedAt = DateTime.Now;

                Cart newItem = _mapper.Map<Cart>(cart);
                _context.Carts.Add(newItem);
                int IsSaved = _context.SaveChanges();
                if (IsSaved == 0)
                    return null;

                // Get the newly added item with the aircraft details
                CartDto newlyAddedItem = GetCart(newItem.Id);

                return newlyAddedItem;
            }
            catch
            {
                return null;
            }
        }

        public CartDto UpdateCart(long id, CreateCartDto cart)
        {
            try
            {
                cart.Id = id;
                Cart updatedItem = _mapper.Map<Cart>(cart);
                updatedItem.UpdatedAt = System.DateTime.Now;
                _context.Carts.Update(updatedItem);
                int IsSaved = _context.SaveChanges();
                if (IsSaved == 0)
                    return null;

                return GetCart(id);
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteCart(long id)
        {
            try
            {
                Cart? cartItem = _context.Carts.Find(id);
                if (cartItem == null)
                    return false;

                _context.Carts.Remove(cartItem);
                int IsSaved = _context.SaveChanges();
                return IsSaved > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool IsCartExists(long userId, long aircraftId)
        {
            return _context.Carts.Any(c => c.UserId == userId && c.AircraftId == aircraftId);
        }

        public bool IsCartExists(long id)
        {
            return _context.Carts.Any(c => c.Id == id);
        }

        public int GetTotalCarts()
        {
            return _context.Carts.Count();
        }
    }
}
