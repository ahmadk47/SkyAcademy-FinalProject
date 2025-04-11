
namespace BiddingManagementSystem.Application.DTOs.BidsDTOs;

 public class CreateBidDto
{
    public Guid TenderId { get; set; }
    public string TechnicalProposalSummary { get; set; }
    public decimal TotalBidAmount { get; set; }
    public string Declaration { get; set; }
    public List<CreateBidItemDto> BidItems { get; set; }
}