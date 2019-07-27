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
            CreateMap<SensorTypeDto, SensorType>();
            CreateMap<SensorType, SensorTypeDto>();
            CreateMap<ProjectDto, Project>();
            CreateMap<Project, ProjectDto>();
            CreateMap<SensorDto, Sensor>();
            CreateMap<Sensor, SensorDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserProjectPermissionDto, UserProjectPermission>();
            CreateMap<UserProjectPermission, UserProjectPermissionDto>();
        }
    }
}
