using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IGenericRepository<Department> repository) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(repository.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            repository.Create(department);
            return Ok(department);
        }

        [HttpPut]
        public IActionResult Update(Department department)
        {
            repository.Update(department);
            return Ok(department);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            repository.Delete(id);
            return Ok();
        }
    }
}
