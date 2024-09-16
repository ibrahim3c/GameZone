
using Microsoft.AspNetCore.StaticFiles;

namespace GameZone.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async  Task DeleteFileAsync(string FilePath)
        {
            // => /wwwroot/
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" +"/"+FilePath);
            if (File.Exists(directoryPath))
            {
                File.Delete(directoryPath);
               
            }
           
        }




        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            try
            {
                if (file == null || file.Length == 0) return string.Empty;


                var path = Path.Combine(webHostEnvironment.WebRootPath, folder);
                var extension = Path.GetExtension(file.FileName);
                var fileName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty)}{extension}";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fullPath = Path.Combine(path, fileName);

                using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Flush();
                }

                //return Path.Combine(folder, fileName).Replace("\\", "/"); // Return relative path
                return Path.Combine(folder, fileName); // Return relative path
            }
            catch
            {
                return string.Empty;
            }
        }

    }
}
