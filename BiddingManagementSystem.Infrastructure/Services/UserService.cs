using BiddingManagementSystem.Application.Interfaces;
using BiddingManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BiddingManagementSystem.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        async Task<bool> IUserService.IsInRoleAsync(string userId, string role)
        {
            // Convert int userId to string if ApplicationUser uses string IDs
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return false;

            return await _userManager.IsInRoleAsync(user, role);
        }


        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

    }
}
