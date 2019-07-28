using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Interfaces;
using Models.Repository;
using Services.Interfaces;

namespace Monitoring.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorsRepository _sensorsRepository;
        private readonly IMapper _mapper;
        private readonly ISensorAuthorizationService _authorizationService;

        public SensorsController(ISensorsRepository iSensorsRepository, IMapper mapper, ISensorAuthorizationService authorizationService)
        {
            _sensorsRepository = iSensorsRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        //GET api/sensors
        [HttpGet]
        public async Task<IActionResult> GetSensors()
        {
            List<Sensor> sensors = await _sensorsRepository.GetAll();

            if (sensors == null)
                return BadRequest();

            var sensorDtoList = _mapper.Map<List<Sensor>, List<SensorDto>>(sensors);

            return Ok(sensorDtoList);
        }

        // GET api/sensors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensor(int id)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return BadRequest();

            bool found = await _authorizationService.IsAuthorized(int.Parse(userId), id, Permissions.View);
            if (!found)
                return Unauthorized();

            Sensor sensor = await _sensorsRepository.GetById(id);
            if (sensor == null)
                return NotFound();

            var sensorDto = _mapper.Map<Sensor, SensorDto>(sensor);
            return Ok(sensorDto);
        }

        // POST api/sensors
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SensorDto sensorDto)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return BadRequest();

            bool found = await _authorizationService.IsAuthorized(int.Parse(userId), sensorDto.Id, Permissions.View);
            if (!found)
                return Unauthorized();

            Sensor sensor = _mapper.Map<SensorDto, Sensor>(sensorDto);
            int result = await _sensorsRepository.Create(sensor);

            if (result == 0)
                return UnprocessableEntity();

            return Ok();
        }

        // PUT api/sensors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SensorDto sensorDto)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return BadRequest();
            bool found = await _authorizationService.IsAuthorized(int.Parse(userId), sensorDto.Id, Permissions.View);

            if (!found)
                return Unauthorized();
            Sensor sensor = _mapper.Map<SensorDto, Sensor>(sensorDto);

            bool success = await _sensorsRepository.Update(sensor);
            if (!success)
                return UnprocessableEntity();

            return Ok();
        }

        // DELETE api/sensors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return BadRequest();
            bool found = await _authorizationService.IsAuthorized(int.Parse(userId), id, Permissions.View);

            if (!found)
                return Unauthorized();
            bool success = await _sensorsRepository.Delete(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
