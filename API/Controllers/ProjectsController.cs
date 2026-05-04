using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IUnitOfWork unitOfWork) : ControllerBase
    {

        [HttpPost]
        public IActionResult CreateProject(Project project)
        {
            unitOfWork.Projects.Create(project);
            unitOfWork.Complete();

            return Ok(project);
        }
    }
}
