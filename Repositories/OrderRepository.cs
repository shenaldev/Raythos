using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs.Private.CartDtos;
using Raythos.DTOs.Private.OrderDtos;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<OrderDto>> GetOrders(int skip, int take = 15)
        {
            return _mapper.Map<ICollection<OrderDto>>(
                await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Address)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync()
            );
        }

        public async Task<ICollection<OrderDto>> GetOrdersByUserId(
            long userId,
            int skip,
            int take = 15
        )
        {
            return _mapper.Map<ICollection<OrderDto>>(
                await _context.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.Address)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync()
            );
        }

        // Get Single Order Details For User
        public async Task<SingleUserOrderDto?> GetOrder(long id)
        {
            return _mapper.Map<SingleUserOrderDto>(
                await _context.Orders
                    .Include(o => o.Address)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Aircraft)
                    .Where(o => o.Id == id)
                    .FirstOrDefaultAsync()
            );
        }

        // Get Single Order Details For Admin
        public async Task<SingleOrderDto?> GetOrderAdmin(long id)
        {
            return _mapper.Map<SingleOrderDto>(
                await _context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Address)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Aircraft)
                    .FirstOrDefaultAsync(o => o.Id == id)
            );
        }

        public async Task<OrderDto?> CreateOrder(CreateOrderDto order)
        {
            try
            {
                Order newOrder = _mapper.Map<Order>(order);
                newOrder.CreatedAt = DateTime.Now;
                newOrder.UpdatedAt = DateTime.Now;

                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                return _mapper.Map<OrderDto>(newOrder);
            }
            catch
            {
                return null;
            }
        }

        public Task<OrderDto?> UpdateOrder(long id, CreateCartDto dto)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateOrderStatus(long id, string status)
        {
            try
            {
                Order? order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return false;
                }

                order.Status = status;
                order.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteOrder(long id)
        {
            try
            {
                Order? order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                    return false;
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsOrderExists(long id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }

        public async Task<int> GetOrdersCount()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<int> GetOrdersCountByUserId(long userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).CountAsync();
        }
    }
}
