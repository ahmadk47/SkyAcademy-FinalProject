
using BiddingManagementSystem.Application.DTOs.UserDTOs;
using BiddingManagementSystem.Application.Interfaces;
using BiddingManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BiddingManagementSystem.Application.Features.Users.Commands;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IAuthService _authService;

    public LoginUserCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IAuthService authService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authService = authService;
    }

    public async Task<AuthResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.LoginUserDto.Email);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("User account is deactivated");

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName,
            request.LoginUserDto.Password,
            false,
            lockoutOnFailure: false);

        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Invalid credentials");

        return await _authService.CreateAuthResponse(user);
    }
}
