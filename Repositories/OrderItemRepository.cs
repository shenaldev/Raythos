using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs.Private;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderItemRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<OrderItemDto>> GetOrderItems(long orderId)
        {
            return _mapper.Map<ICollection<OrderItemDto>>(
                await _context.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync()
            );
        }

        public async Task<bool> AddOrderItem(long orderId, ICollection<CartDto> cartItems)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                foreach (var cartItem in cartItems)
                {
                    OrderItem item = _mapper.Map<OrderItem>(cartItem);
                    item.OrderId = orderId;
                    item.CreatedAt = DateTime.Now;
                    item.UpdatedAt = DateTime.Now;
                    await _context.OrderItems.AddAsync(item);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
