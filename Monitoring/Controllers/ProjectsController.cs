using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Models;
using Models.Interfaces;
using Project = Models.Project;

namespace Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsController(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }

        // GET: api/Project
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            List<Project> projects = await _projectsRepository.GetAll();

            if (projects == null)
                return BadRequest();

            return Ok(projects);
        }

        // GET: api/Project/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            Project project = await _projectsRepository.GetById(id);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        // POST: api/Project
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Project/5
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
