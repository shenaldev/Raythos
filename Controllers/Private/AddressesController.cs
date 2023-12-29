using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;
using Raythos.Utils;

namespace Raythos.Controllers.Private
{
    [Route("api/user/addresses")]
    [ApiController]
    [Authorize]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressInterface _addressInterface;
        private readonly IUserInterface _userInterface;

        public AddressesController(IAddressInterface addressInterface, IUserInterface userInterface)
        {
            _addressInterface = addressInterface;
            _userInterface = userInterface;
        }

        // GET: api/user/addresses
        [HttpGet]
        public ICollection<AddressDto>? GetAddresses()
        {
            JWTHelper jWTHelper = new(_userInterface);
            long userID = jWTHelper.GetUserID(User);

            if (userID == -1)
            {
                return null;
            }

            return _addressInterface.GetAddresses(userID);
        }

        // GET: api/user/addresses/5
        [HttpGet("{id}")]
        public ActionResult<Address>? GetAddress(long id)
        {
            Address address = _addressInterface.GetAddress(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // POST: api/user/addresses
        [HttpPost]
        public ActionResult<AddressDto>? PostAddress([FromForm] AddressDto address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (address.CountryId == -1)
            {
                ModelState.AddModelError("CountryId", "CountryId is required");
                return BadRequest(ModelState);
            }

            JWTHelper jWTHelper = new(_userInterface);
            long userID = jWTHelper.GetUserID(User);
            address.UserId = userID;
            AddressDto newAddress = _addressInterface.CreateAddress(address);

            if (newAddress == null)
            {
                return StatusCode(500);
            }

            return newAddress;
        }

        // PUT: api/user/addresses/5
        [HttpPut("{id}")]
        public IActionResult PutAddress(long id, [FromForm] AddressDto address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (address.CountryId == -1)
            {
                ModelState.AddModelError("CountryId", "CountryId is required");
                return BadRequest(ModelState);
            }

            if (!_addressInterface.IsAddressExists(id))
            {
                return NotFound();
            }

            JWTHelper jWTHelper = new(_userInterface);
            long userID = jWTHelper.GetUserID(User);

            if (!_addressInterface.IsAddressBelongsToUser(id, userID))
            {
                return Unauthorized();
            }

            address.UserId = userID;

            if (!_addressInterface.UpdateAddress(id, address))
            {
                return StatusCode(500);
            }

            return NoContent();
        }

        // DELETE: api/user/addresses/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(long id)
        {
            if (!_addressInterface.IsAddressExists(id))
            {
                return NotFound();
            }

            JWTHelper jWTHelper = new(_userInterface);
            long userID = jWTHelper.GetUserID(User);

            if (!_addressInterface.IsAddressBelongsToUser(id, userID))
            {
                return Unauthorized();
            }

            if (!_addressInterface.DeleteAddress(id))
            {
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
