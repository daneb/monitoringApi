using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Models;

namespace Monitoring
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SensorTypesDto, SensorType>();
            CreateMap<ProjectDto, Project>();
            CreateMap<SensorDto, Sensor>();
            CreateMap<UserDto, User>();
            CreateMap<UserProjectPermissionDto, UserProjectPermission>();
        }
    }
}
