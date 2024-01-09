using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.Private;
using Raythos.Interfaces;
using Raythos.Responses;
using Raythos.Utils;

namespace Raythos.Controllers.Private
{
    [Route("api/user/orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userInterface;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderItemInterface _orderItemRepository;
        private readonly int take = 15;

        public OrdersController(
            IOrderRepository orderRepository,
            IUserRepository userInterface,
            ICartRepository cartRepository,
            IOrderItemInterface orderItemRepository
        )
        {
            _orderRepository = orderRepository;
            _userInterface = userInterface;
            _cartRepository = cartRepository;
            _orderItemRepository = orderItemRepository;
        }

        // GET: api/user/orders
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<OrderDto>>> GetOrders(
            [FromQuery] int page = 1
        )
        {
            JWTHelper jWTHelper = new(_userInterface);
            long userID = await jWTHelper.GetUserID(User);

            var skip = (page - 1) * take;
            int totalTeams = _orderRepository.GetOrdersCountByUserId(userID);
            int lastPage = (int)Math.Ceiling((double)totalTeams / take);

            ICollection<OrderDto> orders = _orderRepository.GetOrdersByUserId(userID, skip, take);
            if (orders == null)
            {
                return NotFound();
            }

            return PaginatedResponse<OrderDto>.Paginate(orders, totalTeams, page, lastPage);
        }

        // GET: api/user/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(long id)
        {
            JWTHelper jWTHelper = new(_userInterface);
            long userID = await jWTHelper.GetUserID(User);

            if (!_orderRepository.OrderExists(id))
            {
                return NotFound();
            }

            OrderDto order = _orderRepository.GetOrder(id);
            if (order.UserId != userID)
            {
                return Unauthorized();
            }

            return Ok(order);
        }

        // POST: api/user/orders
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder([FromForm] CreateOrderDto order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (order.AddressId)

            //GET USER ID
            JWTHelper jWTHelper = new(_userInterface);
            long userID = await jWTHelper.GetUserID(User);

            //GET CART ITEMS
            ICollection<CartDto> carts = _cartRepository.GetCarts(userID);
            if (carts == null)
                return BadRequest(new { message = "No items in cart" });

            //CALCULATE TOTAL PRICE
            decimal total = 0;
            foreach (var cart in carts)
            {
                if (cart.TotalPrice == null)
                    return BadRequest(new { message = "Total price is required" });
                total += (decimal)cart.TotalPrice;
            }

            //CREATE ORDER
            order.UserId = userID;
            order.Total = total;
            var result = _orderRepository.CreateOrder(order);

            if (result == null)
            {
                return BadRequest(new { message = "Fail to create order" });
            }

            //Create Order Items
            bool IsItemSaved = _orderItemRepository.AddOrderItem(result.Id, carts);
            if (!IsItemSaved)
            {
                _orderRepository.DeleteOrder(result.Id);
                return StatusCode(500, new { message = "Something went wrong" });
            }

            return Ok(result);
        }
    }
}
