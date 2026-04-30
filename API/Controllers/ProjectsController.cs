using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(AppDbContext context) : ControllerBase
    {

        [HttpPost]
        public IActionResult CreateProject(CreateProjectDto dto)
        {
            var project = new Project() { Name = dto.Name };

            context.Projects.Add(project);
            context.SaveChanges();

            return Ok(project);
        }
    }
}
