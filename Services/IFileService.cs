namespace GameZone.Services
{
    public interface IFileService
    {
        public Task<string> UploadFileAsync(IFormFile file, string folder);
        public Task DeleteFileAsync(string FilePath);
    }
}
