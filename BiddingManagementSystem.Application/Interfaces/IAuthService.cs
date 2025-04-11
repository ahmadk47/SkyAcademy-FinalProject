
using BiddingManagementSystem.Application.DTOs.UserDTOs;
using BiddingManagementSystem.Domain.Entities;

namespace BiddingManagementSystem.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> CreateAuthResponse(ApplicationUser user);
    string GenerateJwtToken(ApplicationUser user, IList<string> roles);
}
