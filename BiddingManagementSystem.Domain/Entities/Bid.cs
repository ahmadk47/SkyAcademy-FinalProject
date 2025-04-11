
using BiddingManagementSystem.Domain.Common;
using BiddingManagementSystem.Domain.Enums;

namespace BiddingManagementSystem.Domain.Entities;

public class Bid : BaseEntity
{
    public string BidderCompanyName { get; set; }
    public string RegistrationNumber { get; set; }
    public string BidderAddress { get; set; }
    public string BidderEmail { get; set; }
    public string BidderPhone { get; set; }
    public string TechnicalProposalSummary { get; set; }
    public decimal TotalBidAmount { get; set; }
    public BidStatus Status { get; set; } = BidStatus.Draft;
    public DateTime SubmissionDate { get; set; }
    public decimal? EvaluationScore { get; set; }
    public string EvaluationComments { get; set; }
    public string Declaration { get; set; }

    // Foreign keys
    public Guid TenderId { get; set; }
    public string BidderId { get; set; }

    // Navigation properties
    public virtual Tender Tender { get; set; }
    public virtual ApplicationUser Bidder { get; set; }
    public virtual ICollection<Document> Documents { get; set; }
    public virtual ICollection<BidItem> BidItems { get; set; }
}
