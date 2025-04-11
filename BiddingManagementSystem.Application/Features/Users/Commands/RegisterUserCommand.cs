
using BiddingManagementSystem.Application.DTOs.UserDTOs;
using MediatR;

namespace BiddingManagementSystem.Application.Features.Users.Commands;

public class RegisterUserCommand : IRequest<AuthResponseDto>
{
    public RegisterUserDto RegisterUserDto { get; set; }
}
