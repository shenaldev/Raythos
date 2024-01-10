using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.Private.CartDtos;
using Raythos.Interfaces;
using Raythos.Utils;
using System.Net;

namespace Raythos.Controllers.Private
{
    [Route("api/user/carts")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IForignkeyRepository _forignkeyRepository;
        private readonly IUserRepository _userRepository;

        public CartsController(
            ICartRepository cartRepository,
            IForignkeyRepository forignkeyRepository,
            IUserRepository userRepository
        )
        {
            _cartRepository = cartRepository;
            _forignkeyRepository = forignkeyRepository;
            _userRepository = userRepository;
        }

        // GET: api/user/carts
        [HttpGet]
        public async Task<ActionResult<ICollection<CartDto>>> GetCarts()
        {
            JWTHelper jWTHelper = new(_userRepository);
            long userID = await jWTHelper.GetUserID(User);

            ICollection<CartDto> result = await _cartRepository.GetCartItems(userID);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/user/carts
        [HttpPost]
        public async Task<IActionResult> PostCart([FromForm] CreateCartDto cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cart.AircraftId == null)
                return BadRequest(new { message = "Aircraft ID is required" });

            if (!await _forignkeyRepository.IsAircraftExists((long)cart.AircraftId))
                return BadRequest(new { message = "Aircraft does not exist" });

            JWTHelper jWTHelper = new(_userRepository);
            long userID = await jWTHelper.GetUserID(User);
            cart.UserId = userID;

            var result = await _cartRepository.AddToCart(cart);

            if (result == null)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }

            return Ok(result);
        }

        // PUT: api/user/carts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(long id, [FromForm] CreateCartDto cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cart.AircraftId == null)
                return BadRequest(new { message = "Aircraft ID is required" });

            if (!await _forignkeyRepository.IsAircraftExists((long)cart.AircraftId))
                return BadRequest(new { message = "Aircraft does not exist" });

            JWTHelper jWTHelper = new(_userRepository);
            long userID = await jWTHelper.GetUserID(User);
            cart.UserId = userID;

            var result = await _cartRepository.UpdateCart(id, cart);

            if (result == null)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }

            return Ok(result);
        }

        // DELETE: api/user/carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(long id)
        {
            if (!await _cartRepository.IsCartExists(id))
            {
                return NotFound();
            }

            bool isDeleted = await _cartRepository.DeleteCart(id);
            if (isDeleted)
            {
                return Ok("Item has been deleted");
            }
            else
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
        }

        // DELETE: api/user/carts
        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            JWTHelper jWTHelper = new(_userRepository);
            long userID = await jWTHelper.GetUserID(User);

            bool isDeleted = await _cartRepository.ClearCart(userID);
            if (!isDeleted)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }

            return Ok("Cart has been cleared");
        }
    }
}
