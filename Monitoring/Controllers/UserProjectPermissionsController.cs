using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Interfaces;

namespace Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProjectPermissionsController : ControllerBase
    {
        private readonly IUserProjectPermissionsRepository _iUsersProjectPermissionsRepository;

        public UserProjectPermissionsController(IUserProjectPermissionsRepository userProjectPermissionsRepository)
        {
            _iUsersProjectPermissionsRepository = userProjectPermissionsRepository;
        }

        // GET: api/UserProjectPermissions
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/UserProjectPermissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProjectPermissions>> GetUserProjectPermissions(int id)
        {
            return await _iUsersProjectPermissionsRepository.GetById(id);
        }

        // POST: api/UserProjectPermissions
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/UserProjectPermissions/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
