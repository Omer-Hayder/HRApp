using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectRepository repository) : ControllerBase
    {

        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            repository.Create(project);

            return Ok(project);
        }
    }
}
