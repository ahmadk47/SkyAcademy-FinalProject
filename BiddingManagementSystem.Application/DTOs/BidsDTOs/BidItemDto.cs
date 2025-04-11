

namespace BiddingManagementSystem.Application.DTOs.BidsDTOs;
public class BidItemDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
