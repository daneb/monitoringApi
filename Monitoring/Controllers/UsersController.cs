using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces;

namespace Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository iUsersRepository, IMapper mapper)
        {
            _usersRepository = iUsersRepository;
            _mapper = mapper;
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
            User user = _mapper.Map<UserDto, User>(userDto);
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
