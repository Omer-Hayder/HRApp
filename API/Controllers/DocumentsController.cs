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
    public class DocumentsController(IUnitOfWork unitOfWork, IFileService fileService, IMapper mapper, IPhotoService photoService) : ControllerBase
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
            document.FileName = fileName;
            document.UploadDate = DateTime.UtcNow;

            unitOfWork.EmployeeDocuments.Create(document);
            unitOfWork.Complete();

            return Ok(document);
        }

        [HttpPost("multiple")]
        public async Task<IActionResult> UploadMultiple([FromForm] UploadMultiDocumentDto dto)
        {
            foreach (var file in dto.Files)
            {

            }
            return Ok();
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            var result = await fileService.DownloadAsync(fileName);

            return File(result.FileBytes, result.ContentType, result.FileName);
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<Photo>> AddPhoto([FromForm] UploadDocumentDto dto)
        {
            var employee = unitOfWork.Employees.GetById(dto.EmployeeId);
            if (employee == null)
            {
                return BadRequest("Invalid Employee Id");
            }

            var result = await photoService.UploadPhotoAsync(dto.File);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                EmployeeId = employee.Id,
            };

            employee.Photos.Add(photo);

            if (unitOfWork.Complete() > 0) return photo;

            return BadRequest("Problem Adding photo");
        }

    }
}
