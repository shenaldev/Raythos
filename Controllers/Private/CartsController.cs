﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.Private;
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
        private readonly ICartInterface _cartRepository;
        private readonly IUserInterface _userInterface;
        private readonly IAircraftInterface _aircraftInterface;

        public CartsController(
            ICartInterface cartRepository,
            IUserInterface userInterface,
            IAircraftInterface aircraftInterface
        )
        {
            _cartRepository = cartRepository;
            _userInterface = userInterface;
            _aircraftInterface = aircraftInterface;
        }

        // GET: api/user/carts
        [HttpGet]
        public ActionResult<ICollection<CartDto>> GetCarts()
        {
            JWTHelper jWTHelper = new(_userInterface);
            long userID = jWTHelper.GetUserID(User);

            ICollection<CartDto> result = _cartRepository.GetCarts(userID);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/user/carts
        [HttpPost]
        public IActionResult PostCart([FromForm] CreateCartDto cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cart.AircraftId == null)
                return BadRequest(new { message = "Aircraft ID is required" });

            if (!_aircraftInterface.IsAircraftExists((long)cart.AircraftId))
                return BadRequest(new { message = "Aircraft does not exist" });

            JWTHelper jWTHelper = new(_userInterface);
            long userID = jWTHelper.GetUserID(User);
            cart.UserId = userID;

            var result = _cartRepository.AddToCart(cart);

            if (result == null)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }

            return Ok(result);
        }

        // PUT: api/user/carts/5
        [HttpPut("{id}")]
        public IActionResult PutCart(long id, [FromForm] CreateCartDto cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cart.AircraftId == null)
                return BadRequest(new { message = "Aircraft ID is required" });

            if (!_aircraftInterface.IsAircraftExists((long)cart.AircraftId))
                return BadRequest(new { message = "Aircraft does not exist" });

            JWTHelper jWTHelper = new(_userInterface);
            long userID = jWTHelper.GetUserID(User);
            cart.UserId = userID;

            var result = _cartRepository.UpdateCart(id, cart);

            if (result == null)
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }

            return Ok(result);
        }

        // DELETE: api/user/carts/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCart(long id)
        {
            if (!_cartRepository.IsCartExists(id))
            {
                return NotFound();
            }

            bool isDeleted = _cartRepository.DeleteCart(id);
            if (isDeleted)
            {
                return Ok("Item has been deleted");
            }
            else
            {
                return StatusCode(HttpStatusCode.InternalServerError.GetHashCode());
            }
        }
    }
}
