﻿namespace BiddingManagementSystem.Application.DTOs.UserDTOs;

public class AuthResponseDto
{
    public string Id { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public List<string> Roles { get; set; }
}
