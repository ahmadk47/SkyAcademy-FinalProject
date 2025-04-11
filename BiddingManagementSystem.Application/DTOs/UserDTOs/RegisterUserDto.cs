namespace BiddingManagementSystem.Application.DTOs.UserDTOs;
public class RegisterUserDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string ?CompanyName { get; set; }
    public string ? CompanyRegistrationNumber { get; set; }
    public string? Address { get; set; }
    public string PhoneNumber { get; set; } = null!;
}