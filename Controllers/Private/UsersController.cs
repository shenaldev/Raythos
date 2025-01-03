﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raythos.DTOs;
using Raythos.Interfaces;
using Raythos.Utils;

namespace Raythos.Controllers.Private
{
    [Route("api/private/user")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //Get User Details
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            String? UserEmail = JWTHelper.GetEmailFromJWT(User);

            if (UserEmail == null)
            {
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(await _userRepository.GetUser(UserEmail));
            return user;
        }

        //Update User Details
        [HttpPut]
        public async Task<IActionResult> PutUser([FromForm] UpdateUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            String? UserEmail = JWTHelper.GetEmailFromJWT(User);

            if (UserEmail == null)
            {
                return NotFound();
            }

            if (!await _userRepository.IsUserExists(UserEmail))
            {
                return NotFound();
            }

            var updatedUser = await _userRepository.UpdateUser(user.Id, user);

            if (updatedUser == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<UserDto>(updatedUser));
        }
    }
}
