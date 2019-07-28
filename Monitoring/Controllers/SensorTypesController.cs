﻿using System;
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

            var sensorTypesDtoList = _mapper.Map<List<SensorType>, List<SensorTypeDto>>(sensorTypes);

            return Ok(sensorTypesDtoList);
        }

        // GET: api/SensorType/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorTypeById(int id)
        {
            var sensorType = await _sensorsTypeRepository.GetById(id);

            if (sensorType == null)
                return NotFound();

            var sensorTypeDto = _mapper.Map<SensorType, SensorTypeDto>(sensorType);

            return Ok(sensorTypeDto);

        }

        [Authorize(Roles = "Administrator")]
        // POST: api/SensorType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SensorTypeDto sensorTypeDto)
        {
            SensorType sensorType = _mapper.Map<SensorTypeDto,SensorType>(sensorTypeDto);
            int result = await _sensorsTypeRepository.Create(sensorType);

            if (result == 0)
                return UnprocessableEntity();

            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        // PUT: api/SensorType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SensorTypeDto sensorTypeDto)
        {
            SensorType sensorType = _mapper.Map<SensorTypeDto, SensorType>(sensorTypeDto);
            bool success = await _sensorsTypeRepository.Update(sensorType);

            if (!success)
                return UnprocessableEntity();

            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _sensorsTypeRepository.Delete(id);

            if (!success)
                return NotFound();

            return Ok();
        }
    }

}
