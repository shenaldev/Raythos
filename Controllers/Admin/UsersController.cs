using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;
using Raythos.Responses;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/user")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/dashboard/admin/user
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<UserDto>>> GetUsers(
            [FromQuery] int page = 1
        )
        {
            var take = 15;
            var skip = (page - 1) * take;
            int totalUsers = await _userRepository.GetTotalUsers();
            int lastPage = (int)Math.Ceiling((double)totalUsers / take);

            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetUsers(skip, take));
            return PaginatedResponse<UserDto>.Paginate(users, totalUsers, page, lastPage);
        }

        // GET: api/dashboard/admin/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(long id)
        {
            var user = _mapper.Map<UserDto>(await _userRepository.GetUser(id));

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/dashboard/admin/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, [FromForm] UpdateUserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest("Invalid User ID");
            }

            if (!await _userRepository.IsUserExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = await _userRepository.UpdateUser(id, user);
            if (updatedUser == null)
            {
                return BadRequest("User not updated");
            }

            return Ok(_mapper.Map<UserDto>(updatedUser));
        }

        // POST: api/dashboard/admin/user
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser([FromForm] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //CHECK IF USER ALREADY EXISTS
            if (await _userRepository.IsUserExists(user.Email))
            {
                return BadRequest("User email already exists");
            }

            User createdUser = await _userRepository.CreateUser(user);

            if (createdUser != null)
            {
                var userData = CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
                return userData;
            }

            return BadRequest(500);
        }

        // DELETE: api/dashboard/admin/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUser(id);

            return NoContent();
        }
    }
}
