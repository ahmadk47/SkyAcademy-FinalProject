
namespace BiddingManagementSystem.Application.DTOs.BidsDTOs;

public class EvaluateBidDto
{
    public Guid BidId { get; set; }
    public decimal EvaluationScore { get; set; }
    public string EvaluationComments { get; set; }
}
