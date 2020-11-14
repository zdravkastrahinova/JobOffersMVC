using Microsoft.AspNetCore.Http;
using System.IO;

namespace JobOffersMVC.Services.Helpers
{
    public class FileHelperService : IFileHelperService
    {
        public string BuildFilePath(string directory, string filePath)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return Path.Combine(directory, filePath);
        }

        public void CreateFile(IFormFile imageFile, string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
        }
    }
}
