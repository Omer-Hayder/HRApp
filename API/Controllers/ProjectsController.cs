using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IGenericRepository<Project> repository) : ControllerBase
    {

        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            repository.Create(project);

            return Ok(project);
        }
    }
}
