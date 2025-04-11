
using BiddingManagementSystem.Application.DTOs.UserDTOs;
using MediatR;

namespace BiddingManagementSystem.Application.Features.Users.Commands;

public class LoginUserCommand : IRequest<AuthResponseDto>
{
    public LoginUserDto LoginUserDto { get; set; }
}
