using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces;
using Services.Interfaces;

namespace Monitoring.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly IUserPasswordHashProvider _userPasswordHashProvider;

        public UsersController(IUsersRepository usersRepository, IMapper mapper, IUserPasswordHashProvider userPasswordHashProvider)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _userPasswordHashProvider = userPasswordHashProvider;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserDto userDto)
        {
            var user  = await _usersRepository.Authenticate(userDto.Email, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var userWithTokenDto = _mapper.Map<User, UserDto>(user);

            return Ok(userWithTokenDto);
        }


        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<User> users = await _usersRepository.GetAll();

            if (users == null)
                return BadRequest();

            var userDtoList = _mapper.Map<List<User>, List<UserDto>>(users);

            return Ok(users);
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            User user = await _usersRepository.GetById(id);

            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<User, UserDto>(user);

            return Ok(userDto);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            // Map and create password Hash
            User user = _mapper.Map<UserDto, User>(userDto);

            if (userDto.Password == "")
                return BadRequest();

            user.PasswordHash = _userPasswordHashProvider.Hash(userDto.Password);
            int result = await _usersRepository.Create(user);

            if (result == 0)
                return UnprocessableEntity();

            return Ok();
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserDto userDto)
        {
            User user = _mapper.Map<UserDto, User>(userDto);

            if (userDto.Password == "")
                return BadRequest();

            user.PasswordHash = _userPasswordHashProvider.Hash(userDto.Password);
            bool success = await _usersRepository.Update(user);

            if (!success)
                return UnprocessableEntity();

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _usersRepository.Delete(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
