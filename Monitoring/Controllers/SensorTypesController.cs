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
    public class SensorTypesController : ControllerBase
    {
        private readonly ISensorTypesRepository _sensorsTypeRepository;

        public SensorTypesController(ISensorTypesRepository iSensorsRepository)
        {
            _sensorsTypeRepository = iSensorsRepository;
        }

        // GET: api/SensorTypes
        [HttpGet]
        public async Task<List<SensorTypes>> GetSensorTypes()
        {
            return await _sensorsTypeRepository.GetAll();
        }

        // GET: api/SensorTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorTypes>> GetSensorTypeById(int id)
        {
            return await _sensorsTypeRepository.GetById(id);
        }

        // POST: api/SensorTypes
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/SensorTypes/5
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
