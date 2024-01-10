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

        public async Task<ICollection<CartDto>> GetCartItems(long userID)
        {
            return _mapper.Map<ICollection<CartDto>>(
                await _context.Carts
                    .Include(c => c.Aircraft)
                    .Where(c => c.UserId == userID)
                    .ToListAsync()
            );
        }

        public async Task<CartDto?> GetCartItem(long id)
        {
            return _mapper.Map<CartDto>(
                await _context.Carts
                    .Include(c => c.Aircraft)
                    .Where(c => c.Id == id)
                    .FirstOrDefaultAsync()
            );
        }

        public async Task<CartDto?> AddToCart(CreateCartDto cart)
        {
            try
            {
                cart.CreatedAt = DateTime.Now;
                cart.UpdatedAt = DateTime.Now;

                Cart newItem = _mapper.Map<Cart>(cart);
                await _context.Carts.AddAsync(newItem);
                await _context.SaveChangesAsync();
                return _mapper.Map<CartDto>(newItem);
            }
            catch
            {
                return null;
            }
        }

        public async Task<CartDto?> UpdateCart(long id, CreateCartDto cart)
        {
            try
            {
                cart.Id = id;
                Cart updatedItem = _mapper.Map<Cart>(cart);
                updatedItem.UpdatedAt = System.DateTime.Now;

                _context.Entry(updatedItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return _mapper.Map<CartDto>(updatedItem);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteCart(long id)
        {
            try
            {
                Cart? cartItem = await _context.Carts.FindAsync(id);
                if (cartItem == null)
                    return false;

                _context.Carts.Remove(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ClearCart(long userId)
        {
            try
            {
                ICollection<Cart> cartItems = await _context.Carts
                    .Where(c => c.UserId == userId)
                    .ToListAsync();
                if (cartItems.Count == 0)
                    return false;

                _context.Carts.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsCartExists(long userId, long aircraftId)
        {
            return await _context.Carts.AnyAsync(
                c => c.UserId == userId && c.AircraftId == aircraftId
            );
        }

        public async Task<bool> IsCartExists(long id)
        {
            return await _context.Carts.AnyAsync(c => c.Id == id);
        }

        public async Task<int> GetTotalCarts()
        {
            return await _context.Carts.CountAsync();
        }
    }
}
