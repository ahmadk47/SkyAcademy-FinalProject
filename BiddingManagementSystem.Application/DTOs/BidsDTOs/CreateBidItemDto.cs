
namespace BiddingManagementSystem.Application.DTOs.BidsDTOs;

public class CreateBidItemDto
{
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
