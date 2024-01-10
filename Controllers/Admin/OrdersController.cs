using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.Private.OrderDtos;
using Raythos.DTOs.Private.OrderItemDtos;
using Raythos.Interfaces;
using Raythos.Responses;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/orders")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly int take = 15;

        public OrdersController(
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository
        )
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }

        // GET: api/dashboard/admin/orders
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<OrderDto>>> GetOrders(
            [FromQuery] int page = 1
        )
        {
            var skip = (page - 1) * take;
            int totalOrders = await _orderRepository.GetOrdersCount();
            int lastPage = (int)Math.Ceiling((double)totalOrders / take);

            ICollection<OrderDto> orders = await _orderRepository.GetOrders(skip, take);
            if (orders == null)
            {
                return NotFound();
            }

            return PaginatedResponse<OrderDto>.Paginate(orders, totalOrders, page, lastPage);
        }

        // GET: api/dashboard/admin/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SingleOrderDto>> GetOrderAsync(long id)
        {
            if (!await _orderRepository.IsOrderExists(id))
            {
                return NotFound();
            }

            SingleOrderDto? order = await _orderRepository.GetOrderAdmin(id);
            return Ok(order);
        }

        // PUT : api/dashboard/admin/orders/status/5
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(
            [FromRoute] long id,
            [FromForm] string status
        )
        {
            if (status == null || status == "")
            {
                return BadRequest("Status is required");
            }

            if (!await _orderRepository.IsOrderExists(id))
            {
                return NotFound();
            }

            var statusUpdate = await _orderRepository.UpdateOrderStatus(id, status);
            if (statusUpdate == false)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = "Something went wrong" }
                );
            }

            return Ok();
        }

        // DELETE: api/dashboard/admin/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(long id)
        {
            if (!await _orderRepository.IsOrderExists(id))
            {
                return NotFound();
            }

            ICollection<OrderItemDto> OrderItems = await _orderItemRepository.GetOrderItems(id);
            foreach (OrderItemDto item in OrderItems)
            {
                var isDeleted = await _orderItemRepository.DeleteOrderItem(item.Id);
                if (isDeleted == false)
                {
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        new { Message = "Something went wrong" }
                    );
                }
            }

            var deleted = await _orderRepository.DeleteOrder(id);
            if (deleted == false)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new { Message = "Something went wrong" }
                );
            }

            return Ok();
        }
    }
}
