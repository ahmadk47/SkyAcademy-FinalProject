
using BiddingManagementSystem.Domain.Common;

namespace BiddingManagementSystem.Domain.Entities;

public class BidItem : BaseEntity
{
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    // Foreign key
    public Guid BidId { get; set; }

    // Navigation property
    public virtual Bid Bid { get; set; }
}
