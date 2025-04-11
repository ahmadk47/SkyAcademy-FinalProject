using System.Reflection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
namespace BiddingManagementSystem.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string CompanyName { get; set; } = null!;
    public string ?CompanyRegistrationNumber { get; set; }
    public string ?Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public virtual ICollection<Bid> Bids { get; set; }
    public virtual ICollection<Tender> CreatedTenders { get; set; }
}