using System.Reflection.Metadata;
using BiddingManagementSystem.Domain.Common;
using BiddingManagementSystem.Domain.Enums;

namespace BiddingManagementSystem.Domain.Entities;

public class Tender : BaseEntity
{
    public string Title { get; set; }
    public string ReferenceNumber { get; set; }
    public string Description { get; set; }
    public string IssuedBy { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public TenderType TenderType { get; set; }
    public decimal? BudgetRange { get; set; }
    public string ContactEmail { get; set; }
    public string EligibilityCriteria { get; set; }
    public TenderStatus Status { get; set; } = TenderStatus.Draft;

    // Foreign keys
    public Guid CategoryId { get; set; }
    public string CreatorId { get; set; }

    // Navigation properties
    public virtual TenderCategory Category { get; set; }
    public virtual ApplicationUser Creator { get; set; }
    public virtual ICollection<Bid> Bids { get; set; }
    public virtual ICollection<Document> Documents { get; set; }
    public virtual ICollection<EvaluationCriteria> EvaluationCriteria { get; set; }
    public virtual Bid AwardedBid { get; set; }
    public Guid? AwardedBidId { get; set; }
}
