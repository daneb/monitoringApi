using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Interfaces;
using Models.Repository;

namespace Monitoring.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorsRepository _sensorsRepository;
        private readonly IMapper _mapper;

        public SensorsController(ISensorsRepository iSensorsRepository, IMapper mapper)
        {
            _sensorsRepository = iSensorsRepository;
            _mapper = mapper;
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
            bool success = await _sensorsRepository.Delete(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }
}
