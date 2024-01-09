using AutoMapper;
using Raythos.DTOs.Private;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Repositories
{
    public class OrderItemRepository : IOrderItemInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderItemRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ICollection<OrderItemDto> GetOrderItems(long orderId)
        {
            return _mapper.Map<ICollection<OrderItemDto>>(
                _context.OrderItems.Where(oi => oi.OrderId == orderId)
            );
        }

        public bool AddOrderItem(long orderId, ICollection<CartDto> cartItems)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                foreach (var cartItem in cartItems)
                {
                    OrderItem item = _mapper.Map<OrderItem>(cartItem);
                    item.OrderId = orderId;
                    item.CreatedAt = DateTime.Now;
                    item.UpdatedAt = DateTime.Now;
                    _context.OrderItems.Add(item);
                }
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
