using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Models;
using Models.Interfaces;
using Project = Models.Project;

namespace Monitoring.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectsRepository projectsRepository, IMapper mapper)
        {
            _projectsRepository = projectsRepository;
            _mapper = mapper;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            List<Project> projects = await _projectsRepository.GetAll();

            if (projects == null)
                return BadRequest();

            var projectDtoList = _mapper.Map<List<Project>, List<ProjectDto>>(projects);

            return Ok(projectDtoList);
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            Project project = await _projectsRepository.GetById(id);

            if (project == null)
                return NotFound();

            var projectDto = _mapper.Map<Project, ProjectDto>(project);

            return Ok(project);
        }

        // POST: api/Project
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectDto projectDto)
        {
            Project project = _mapper.Map<ProjectDto, Project>(projectDto);
            int result = await _projectsRepository.Create(project);

            if (result == 0)
                return UnprocessableEntity();

            return Ok();
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectDto projectDto)
        {
            Project project = _mapper.Map<ProjectDto, Project>(projectDto);
            bool success = await _projectsRepository.Update(project);

            if (!success)
                return UnprocessableEntity();

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _projectsRepository.Delete(id);

            if (!success)
                return NotFound();

            return Ok();

        }
    }
}
