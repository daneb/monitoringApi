using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Interfaces;
using Models.Repository;

namespace Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorsRepository _sensorsRepository;

        public SensorsController(ISensorsRepository iSensorsRepository)
        {
            _sensorsRepository = iSensorsRepository;
        }

        //GET api/sensors
        [HttpGet]
        public async Task<IActionResult> GetSensors()
        {
            List<Sensor> sensors = await _sensorsRepository.GetAll();

            if (sensors == null)
                return BadRequest();

            return Ok(sensors);
        }

        // GET api/sensors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> GetSensor(int id)
        {
            Sensor sensor = await _sensorsRepository.GetById(id);

            if (sensor == null)
                return NotFound();

            return Ok(sensor);
        }

        // POST api/sensors
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/sensors/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/sensors/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
