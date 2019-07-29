using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Interfaces;
using Models.Repository;
using Monitoring.Controllers;
using NSubstitute;
using Xunit;

namespace Monitoring.Tests
{
    public class SensorTypesControllerTest
    {
        private MapperConfiguration mapperConfiguration;

        public SensorTypesControllerTest()
        {
            var mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
        }


        [Fact]
        public void SuccessGetAll()
        { 
            List<SensorType> listSensorType = new List<SensorType>()
            {
                new SensorType() {Description = "Sample", Name = "Type1", Id = 1}
            };

            SensorTypesRepository sensorTypesRepo = Substitute.For<SensorTypesRepository>();
            sensorTypesRepo.GetAll().Returns(listSensorType);

            IMapper mapper = new Mapper(mapperConfiguration);

            var controller = new SensorTypesController(sensorTypesRepo, mapper);
        }
    }
}
