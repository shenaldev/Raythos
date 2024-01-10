using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs.AddressDtos;
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
        private readonly IAddressRepository _addressRepo;
        private readonly IUserRepository _userInterface;

        public AddressesController(IAddressRepository addressRepo, IUserRepository userInterface)
        {
            _addressRepo = addressRepo;
            _userInterface = userInterface;
        }

        // GET: api/user/addresses
        [HttpGet]
        public async Task<ICollection<AddressDto>?> GetAddresses()
        {
            JWTHelper jWTHelper = new(_userInterface);
            long userID = await jWTHelper.GetUserID(User);

            if (userID == -1)
            {
                return null;
            }

            return await _addressRepo.GetAddresses(userID);
        }

        // GET: api/user/addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>?> GetAddress(long id)
        {
            var address = await _addressRepo.GetAddress(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // POST: api/user/addresses
        [HttpPost]
        public async Task<ActionResult<AddressDto?>> PostAddress([FromForm] AddressDto address)
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
            long userID = await jWTHelper.GetUserID(User);
            address.UserId = userID;
            AddressDto? newAddress = await _addressRepo.CreateAddress(address);

            if (newAddress == null)
            {
                return StatusCode(500);
            }

            return newAddress;
        }

        // PUT: api/user/addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(long id, [FromForm] UpdateAddressDto address)
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

            if (!await _addressRepo.IsAddressExists(id))
            {
                return NotFound();
            }

            JWTHelper jWTHelper = new(_userInterface);
            long userID = await jWTHelper.GetUserID(User);

            if (!await _addressRepo.IsAddressBelongsToUser(id, userID))
            {
                return Unauthorized();
            }

            address.UserId = userID;

            var updatedAddress = await _addressRepo.UpdateAddress(id, address);
            if (updatedAddress == null)
            {
                return StatusCode(500);
            }

            return Ok(updatedAddress);
        }

        // DELETE: api/user/addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(long id)
        {
            if (!await _addressRepo.IsAddressExists(id))
            {
                return NotFound();
            }

            JWTHelper jWTHelper = new(_userInterface);
            long userID = await jWTHelper.GetUserID(User);

            if (!await _addressRepo.IsAddressBelongsToUser(id, userID))
            {
                return Unauthorized();
            }

            if (!await _addressRepo.DeleteAddress(id))
            {
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
