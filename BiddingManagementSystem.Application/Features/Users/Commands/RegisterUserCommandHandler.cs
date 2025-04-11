
using BiddingManagementSystem.Application.DTOs.UserDTOs;
using BiddingManagementSystem.Domain.Entities;
using BiddingManagementSystem.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BiddingManagementSystem.Application.Features.Users.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponseDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Email = request.RegisterUserDto.Email,
            UserName = request.RegisterUserDto.Email,
            FirstName = request.RegisterUserDto.FirstName,
            LastName = request.RegisterUserDto.LastName,
            CompanyName = request.RegisterUserDto.CompanyName,
            CompanyRegistrationNumber = request.RegisterUserDto.CompanyRegistrationNumber,
            Address = request.RegisterUserDto.Address,
            PhoneNumber = request.RegisterUserDto.PhoneNumber,
            RegistrationDate = DateTime.UtcNow,
            IsActive = true
        };

        var result = await _userManager.CreateAsync(user, request.RegisterUserDto.Password);

        if (!result.Succeeded)
        {
            throw new Exception($"User registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }

        // Add default role (bidder)
        await _userManager.AddToRoleAsync(user, "Bidder");

        // Generate token and return auth response
        return await _authService.CreateAuthResponse(user);
    }
}
