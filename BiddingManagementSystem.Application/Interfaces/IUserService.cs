
using BiddingManagementSystem.Domain.Entities;

namespace BiddingManagementSystem.Application.Interfaces;

public interface IUserService
{
    Task<ApplicationUser> GetUserByIdAsync(string userId);
    Task<bool> IsInRoleAsync(string userId, string role);
}
