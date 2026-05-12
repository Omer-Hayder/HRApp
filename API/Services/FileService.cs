using API.DTOs;
using API.Exceptions;

namespace API.Services
{
    public class FileService : IFileService
    {
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".pdf" };
        private const long MaxFileSize = 2_000_000;
        private readonly string uploadsFolder;

        public FileService()
        {
            uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new BadRequestException("No file uploaded");

            var extension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(extension.ToLower()))
            {
                throw new BadRequestException("Invalid file type");
            }

            if (file.Length > MaxFileSize)
            {
                throw new BadRequestException("File too large");
            }

            var newFileName = $"{Guid.NewGuid()}{extension}";

            var path = Path.Combine(uploadsFolder, newFileName);

            using var stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream);

            return newFileName;
        }

        public async Task<DownloadFileDto> DownloadAsync(string fileName)
        {
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found");

            var fileBytes = await File.ReadAllBytesAsync(filePath);

            var extension = Path.GetExtension(fileName).ToLower();

            var contentType = extension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream"
            };

            return new DownloadFileDto
            {
                FileBytes = fileBytes,
                ContentType = contentType,
                FileName = fileName,
            };
        }
    }
}
