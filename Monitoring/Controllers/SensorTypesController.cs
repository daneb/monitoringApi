using System;
using System.Collections.Generic;
using System.Linq;
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
    public class SensorTypesController : ControllerBase
    {
        private readonly ISensorTypesRepository _sensorsTypeRepository;
        private readonly IMapper _mapper;

        public SensorTypesController(ISensorTypesRepository iSensorsRepository, IMapper mapper)
        {
            _sensorsTypeRepository = iSensorsRepository;
            _mapper = mapper;
        }

        // GET: api/SensorType
        [HttpGet]
        public async Task<IActionResult> GetSensorTypes()
        {
            var sensorTypes = await _sensorsTypeRepository.GetAll();

            if (sensorTypes == null)
                return BadRequest();

            return Ok(sensorTypes);
        }

        // GET: api/SensorType/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorTypeById(int id)
        {
            var sensorType = await _sensorsTypeRepository.GetById(id);

            if (sensorType == null)
                return NotFound();

            return Ok(sensorType);
        }

        // POST: api/SensorType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SensorTypesDto sensorTypeDTO)
        {
            SensorType sensorType = _mapper.Map<SensorTypesDto,SensorType>(sensorTypeDTO);
            int result = await _sensorsTypeRepository.Create(sensorType);

            if (result == 0)
                return UnprocessableEntity();

            return Ok();
        }

        // PUT: api/SensorType/5
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
