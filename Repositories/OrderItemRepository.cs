using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs.Private.CartDtos;
using Raythos.DTOs.Private.OrderItemDtos;
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

        public async Task<bool> AddOrderItem(long orderId, CartDto cartItem)
        {
            try
            {
                if (
                    cartItem.AircraftId == null
                    || cartItem.Quantity == null
                    || cartItem.TotalPrice == null
                )
                    return false;

                OrderItem item =
                    new()
                    {
                        OrderId = orderId,
                        AircraftId = (long)cartItem.AircraftId,
                        Quantity = (int)cartItem.Quantity,
                        TotalPrice = (decimal)cartItem.TotalPrice,
                        Customizations = cartItem.Customizations,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                await _context.OrderItems.AddAsync(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteOrderItem(long id)
        {
            try
            {
                OrderItem? orderItem = await _context.OrderItems.FindAsync(id);
                if (orderItem == null)
                {
                    return false;
                }

                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
