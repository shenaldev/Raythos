using AutoMapper;
using Raythos.DTOs.Private;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class OrderRepository : IOrderInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<OrderDto> GetOrders(int skip, int take = 15)
        {
            return _mapper.Map<ICollection<OrderDto>>(_context.Orders.Skip(skip).Take(take));
        }

        public ICollection<OrderDto> GetOrdersByUserId(long userId, int skip, int take = 15)
        {
            return _mapper.Map<ICollection<OrderDto>>(
                _context.Orders.Where(o => o.UserId == userId).Skip(skip).Take(take)
            );
        }

        public OrderDto GetOrder(long id)
        {
            return _mapper.Map<OrderDto>(_context.Orders.Find(id));
        }

        public OrderDto CreateOrder(CreateOrderDto order)
        {
            try
            {
                Order newOrder = _mapper.Map<Order>(order);
                newOrder.CreatedAt = DateTime.Now;
                newOrder.UpdatedAt = DateTime.Now;
                _context.Orders.Add(newOrder);
                int IsSaved = _context.SaveChanges();
                if (IsSaved > 0)
                {
                    return _mapper.Map<OrderDto>(newOrder);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public OrderDto UpdateOrder(long id, CreateCartDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateOrderStatus(long id, string status)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteOrder(long id)
        {
            throw new System.NotImplementedException();
        }

        public bool OrderExists(long id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }

        public int GetOrdersCount()
        {
            return _context.Orders.Count();
        }

        public int GetOrdersCountByUserId(long userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).Count();
        }
    }
}
