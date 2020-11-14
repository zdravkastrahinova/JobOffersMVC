using Microsoft.AspNetCore.Http;

namespace JobOffersMVC.Services.Helpers
{
    public interface IFileHelperService
    {
        string BuildFilePath(string directory, string filePath);

        void CreateFile(IFormFile imageFile, string filePath);
    }
}
