using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper) : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadDocumentDto dto)
        {
            var fileName = await fileService.UploadAsync(dto.File);

            var employee = unitOfWork.Employees.GetById(dto.EmployeeId);
            if (employee == null) {
                return BadRequest("Invalid Employee Id");
            }

            var document = mapper.Map<EmployeeDocument>(dto);

            unitOfWork.EmployeeDocuments.Create(document);
            unitOfWork.Complete();

            return Ok(document);
        }
    }
}
