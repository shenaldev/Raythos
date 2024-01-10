using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Raythos.DTOs.Private;
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
            return _mapper.Map<ICollection<OrderDto>>(await _context.Orders.Skip(skip).Take(take).ToListAsync());
        }

        public async Task<ICollection<OrderDto>> GetOrdersByUserId(long userId, int skip, int take = 15)
        {
            return _mapper.Map<ICollection<OrderDto>>(
                await _context.Orders.Where(o => o.UserId == userId).Skip(skip).Take(take).ToListAsync()
            );
        }

        public async Task<OrderDto?> GetOrder(long id)
        {
            return _mapper.Map<OrderDto>(await _context.Orders.FindAsync(id));
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

        public Task<bool> UpdateOrderStatus(long id, string status)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteOrder(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> OrderExists(long id)
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
