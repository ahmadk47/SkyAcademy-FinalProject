using BiddingManagementSystem.Domain.Common;

namespace BiddingManagementSystem.Domain.Entities;

public class EvaluationCriteria : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Weight { get; set; }  // Percentage weight in evaluation

    // Foreign key
    public Guid TenderId { get; set; }

    // Navigation property
    public virtual Tender Tender { get; set; }
}
