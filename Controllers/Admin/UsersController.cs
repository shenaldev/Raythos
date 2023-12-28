using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Models;

namespace Raythos.Controllers.Admin
{
    [Route("api/dashboard/admin/user")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserInterface _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserInterface userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: api/dashboard/admin/user
        [HttpGet]
        public ActionResult<IEnumerable<UserDtoPaginated>> GetUsers([FromQuery] int page = 1)
        {
            var take = 15;
            var skip = (page - 1) * take;
            int current_page = (int)Math.Ceiling((double)_userRepository.GetTotalUsers() / take);

            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers(skip, take));
            var response = new UserDtoPaginated(
                users,
                _userRepository.GetTotalUsers(),
                page,
                current_page
            );

            return Ok(response);
        }

        // GET: api/dashboard/admin/user/5
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(long id)
        {
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(id));

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/dashboard/admin/user/5
        [HttpPut("{id}")]
        public IActionResult PutUser(long id, [FromForm] UpdateUserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest("Invalid User ID");
            }

            if (!_userRepository.IsUserExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isUpdated = _userRepository.UpdateUser(id, user);
            if (!isUpdated)
            {
                return BadRequest("User not updated");
            }

            return NoContent();
        }

        // POST: api/dashboard/admin/user
        [HttpPost]
        public ActionResult<UserDto> PostUser([FromForm] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //CHECK IF USER ALREADY EXISTS
            if (_userRepository.IsUserExists(user.Email))
            {
                return BadRequest("User email already exists");
            }

            User createdUser = _userRepository.CreateUser(user);

            if (createdUser != null)
            {
                var userData = CreatedAtAction("GetUser", new { id = user.Id }, user);
                return userData;
            }

            return BadRequest(500);
        }

        // DELETE: api/dashboard/admin/user/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(id);

            return Ok("User Has Deleted");
        }
    }
}
