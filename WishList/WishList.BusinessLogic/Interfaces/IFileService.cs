using Microsoft.AspNetCore.Http;

namespace WishList.Services.Interfaces
{
    public interface IFileService
    {
        bool SaveImage(IFormFile imageFile, out string? exception, out string? fileName);
        bool DeleteImage(string imageFileName);
    }
}
