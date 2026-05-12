namespace API.DTOs
{
    public class DownloadFileDto
    {
        public byte[] FileBytes { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }
    }
}
