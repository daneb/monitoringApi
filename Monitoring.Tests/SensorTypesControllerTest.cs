using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Mvc;
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
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IMapper _mapper;

        public SensorTypesControllerTest()
        {
            _mapperConfiguration = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            _mapper = new Mapper(_mapperConfiguration); 
        }


        [Fact]
        public async void SuccessGetAll()
        {
            SensorTypeDto expected = new SensorTypeDto() {Description = "Sample", Name = "Type1", Id = 1};

            List<SensorType> listSensorType = new List<SensorType>()
            {
                new SensorType() {Description = "Sample", Name = "Type1", Id = 1}
            };

            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();
            sensorTypesRepo.GetAll().Returns(listSensorType);

            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.GetSensorTypes() as OkObjectResult;
            List<SensorTypeDto> collection = (List<SensorTypeDto>)result?.Value;

            Assert.True(result?.StatusCode == 200);
            Assert.True((collection[0].Id.Equals(expected.Id)));
            Assert.True((collection[0].Name.Equals(expected.Name)));
            Assert.True((collection[0].Description.Equals(expected.Description)));

        }

        [Fact]
        public async void BadRequestGetAll()
        {
            SensorTypeDto expected = new SensorTypeDto();

            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();

            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.GetSensorTypes() as BadRequestResult;

            Assert.True(result?.StatusCode == 400);
        }

        [Fact]
        public async void SuccessGetSensorTypeById()
        {
            int sensorTypeId = 1;
            SensorTypeDto expected = new SensorTypeDto() { Description = "Sample", Name = "Type1", Id = 1 };
            SensorType sensorType = new SensorType() { Description = "Sample", Name = "Type1", Id = 1};

            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();
            sensorTypesRepo.GetById(sensorTypeId).Returns(sensorType);

            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.GetSensorTypeById(sensorTypeId) as OkObjectResult;
            SensorTypeDto sensorTypeResult = (SensorTypeDto)result?.Value;

            Assert.True(result?.StatusCode == 200);
            Assert.True((sensorTypeResult.Id.Equals(expected.Id)));
            Assert.True((sensorTypeResult.Name.Equals(expected.Name)));
            Assert.True((sensorTypeResult.Description.Equals(expected.Description)));
        }

        [Fact]
        public async void NotFoundGetSensorTypeById()
        {
            int sensorTypeId = 1;

            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();

            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.GetSensorTypeById(sensorTypeId) as NotFoundResult;

            Assert.True(result?.StatusCode == 404);
        }

        [Fact]
        public async void SuccessAddNewSensorType()
        {
            SensorTypeDto seed = new SensorTypeDto() { Description = "Sample", Name = "Type1", Id = 1 };
            SensorType sensorTypeToAdd = new SensorType() { Description = "Sample", Name = "Type1", Id = 1 };
            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();
            sensorTypesRepo.Create(Arg.Any<SensorType>()).Returns(1);

            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.Post(seed) as OkObjectResult;
            int sensorTypeResult = (int)result?.Value;

            Assert.True(result?.StatusCode == 200);
            Assert.True((sensorTypeResult == 1));

        }

        [Fact]
        public async void FailureAddNewSensorType()
        {
            SensorTypeDto seed = new SensorTypeDto() { Description = "Sample", Name = "Type1", Id = 1 };
            SensorType sensorTypeToAdd = new SensorType() { Description = "Sample", Name = "Type1", Id = 1 };
            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();

            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.Post(seed) as UnprocessableEntityResult;

            Assert.True(result?.StatusCode == 422);

        }

        [Fact]
        public async void SuccessEditingSensorType()
        {
            SensorTypeDto seed = new SensorTypeDto() { Description = "Sample", Name = "Type1", Id = 1 };
            SensorType sensorTypeToAdd = new SensorType() { Description = "Sample", Name = "Type1", Id = 1 };
            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();
            sensorTypesRepo.Update(Arg.Any<SensorType>()).Returns(true);

            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.Put(seed) as OkResult;

            Assert.True(result?.StatusCode == 200);
        }

        [Fact]
        public async void FailureEditingSensorType()
        {
            SensorTypeDto seed = new SensorTypeDto() { Description = "Sample", Name = "Type1", Id = 1 };
            SensorType sensorTypeToAdd = new SensorType() { Description = "Sample", Name = "Type1", Id = 1 };
            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();
            
            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.Put(seed) as UnprocessableEntityResult;

            Assert.True(result?.StatusCode == 422);
        }

        [Fact]
        public async void SuccessDeletingSensorType()
        {
            int sensorTypeId = 1;
            SensorTypeDto seed = new SensorTypeDto() { Description = "Sample", Name = "Type1", Id = 1 };
            SensorType sensorTypeToAdd = new SensorType() { Description = "Sample", Name = "Type1", Id = 1 };
            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();
            sensorTypesRepo.Delete(sensorTypeId).Returns(true);

            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.Delete(sensorTypeId) as OkResult;

            Assert.True(result?.StatusCode == 200);
        }

        [Fact]
        public async void FailureDeletingSensorType()
        {
            int sensorTypeId = 1;
            SensorTypeDto seed = new SensorTypeDto() { Description = "Sample", Name = "Type1", Id = 1 };
            SensorType sensorTypeToAdd = new SensorType() { Description = "Sample", Name = "Type1", Id = 1 };
            ISensorTypesRepository sensorTypesRepo = Substitute.For<ISensorTypesRepository>();

            var controller = new SensorTypesController(sensorTypesRepo, _mapper);
            var result = await controller.Delete(sensorTypeId) as NotFoundResult;

            Assert.True(result?.StatusCode == 404);
        }
    }
}
