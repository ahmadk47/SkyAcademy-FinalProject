
using Microsoft.AspNetCore.Http;

namespace BiddingManagementSystem.Application.Interfaces;

public interface IFileStorageService
{
    Task<string?> SaveFileAsync(IFormFile documentFile);

   
        bool DeleteFile(string filePath);
}
