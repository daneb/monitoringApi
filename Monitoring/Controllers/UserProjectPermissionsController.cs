using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces;

namespace Monitoring.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectPermissionsController : ControllerBase
    {
        private readonly IUserProjectPermissionsRepository _userProjectPermissionsRepository;
        private readonly IMapper _mapper;

        public UserProjectPermissionsController(IUserProjectPermissionsRepository userProjectPermissionsRepository, IMapper mapper)
        {
            _userProjectPermissionsRepository = userProjectPermissionsRepository;
            _mapper = mapper;
        }


        // GET: api/UserProjectPermission/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProjectPermissions(int id)
        {
            UserProjectPermission userProjectPermissions = await _userProjectPermissionsRepository.GetById(id);

            if (userProjectPermissions == null)
                return NotFound();

            var userProjectPermissionDto = _mapper.Map<UserProjectPermission, UserProjectPermissionDto>(userProjectPermissions);

            return Ok(userProjectPermissionDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProjectPermissions()
        {
            List<UserProjectPermission> userProjectPermissions = await _userProjectPermissionsRepository.GetAll();

            if (userProjectPermissions == null)
                return BadRequest();

            var userProjectPermissionDtoList = _mapper.Map<List<UserProjectPermission>, List<UserProjectPermissionDto>>(userProjectPermissions);

            return Ok(userProjectPermissionDtoList);
        }

        // POST: api/UserProjectPermission
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserProjectPermissionDto userProjectPermissionDto)
        {
            UserProjectPermission userProjectPermission = _mapper.Map<UserProjectPermissionDto, UserProjectPermission>(userProjectPermissionDto);
            int result = await _userProjectPermissionsRepository.Create(userProjectPermission);

            if (result == 0)
                return UnprocessableEntity();

            return Ok();
        }

        // PUT: api/UserProjectPermission/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserProjectPermissionDto userProjectPermissionDto)
        {
            UserProjectPermission userProjectPermission = _mapper.Map<UserProjectPermissionDto, UserProjectPermission>(userProjectPermissionDto);
            bool success = await _userProjectPermissionsRepository.Update(userProjectPermission);

            if (!success)
                return UnprocessableEntity();

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _userProjectPermissionsRepository.Delete(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
