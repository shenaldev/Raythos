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
    public class OrdersController(
        IOrderRepository orderRepository,
        IUserRepository userInterface,
        ICartRepository cartRepository,
        IOrderItemRepository orderItemRepository,
        IForignkeyRepository forignkeyRepository,
        ApplicationDbContext context
    ) : ControllerBase
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IUserRepository _userInterface = userInterface;
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IOrderItemRepository _orderItemRepository = orderItemRepository;
        private readonly IForignkeyRepository _forignkeyRepository = forignkeyRepository;
        private readonly ApplicationDbContext _contex = context;
        private readonly int take = 15;

        // GET: api/user/orders
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<OrderDto>>> GetOrders(
            [FromQuery] int page = 1
        )
        {
            JWTHelper jWTHelper = new(_userInterface);
            long userID = await jWTHelper.GetUserID(User);

            var skip = (page - 1) * take;
            int totalTeams = await _orderRepository.GetOrdersCountByUserId(userID);
            int lastPage = (int)Math.Ceiling((double)totalTeams / take);

            ICollection<OrderDto> orders = await _orderRepository.GetOrdersByUserId(
                userID,
                skip,
                take
            );
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

            if (!await _orderRepository.IsOrderExists(id))
            {
                return NotFound();
            }

            OrderDto? order = await _orderRepository.GetOrder(id);
            if (order != null && order.UserId != userID)
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

            if (order.AddressId == null)
            {
                ModelState.AddModelError("AddressId", "Address is required");
                return BadRequest(ModelState);
            }

            if (!await _forignkeyRepository.IsAddressExists((long)order.AddressId))
            {
                ModelState.AddModelError("AddressId", "Address does not exists");
                return BadRequest(ModelState);
            }

            //GET USER ID
            JWTHelper jWTHelper = new(_userInterface);
            long userID = await jWTHelper.GetUserID(User);

            //GET CART ITEMS
            ICollection<CartDto> cartItems = await _cartRepository.GetCartItems(userID);
            if (cartItems == null)
                return BadRequest(new { message = "No items in cart" });

            //CALCULATE TOTAL PRICE
            decimal total = 0;
            foreach (var item in cartItems)
            {
                if (item.TotalPrice == null)
                    return BadRequest(new { message = "Total price is required" });
                total += (decimal)item.TotalPrice;
            }

            //CREATE ORDER
            order.UserId = userID;
            order.Total = total;

            using var transaction = _contex.Database.BeginTransaction();
            try
            {
                var newOrder = await _orderRepository.CreateOrder(order);

                if (newOrder == null)
                {
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new { message = "Something went wrong" }
                    );
                }

                foreach (var item in cartItems)
                {
                    var newItem = await _orderItemRepository.AddOrderItem((long)newOrder.Id, item);
                    if (newItem == false)
                    {
                        await transaction.RollbackAsync();
                        return StatusCode(
                            StatusCodes.Status500InternalServerError,
                            new { message = "Something went wrong" }
                        );
                    }
                }
                //Commit Transaction
                await transaction.CommitAsync();

                //Clear Cart
                await _cartRepository.ClearCart(userID);

                return CreatedAtAction("GetOrder", new { id = newOrder.Id }, newOrder);
            }
            catch
            {
                await transaction.RollbackAsync();
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { message = "Something went wrong" }
                );
            }
        }
    }
}
