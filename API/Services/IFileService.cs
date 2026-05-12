using API.DTOs;

namespace API.Services
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file);
        Task<DownloadFileDto>DownloadAsync(string fileName);
    }
}
