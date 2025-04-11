
using BiddingManagementSystem.Domain.Common;
using BiddingManagementSystem.Domain.Enums;

namespace BiddingManagementSystem.Domain.Entities;

public class Document : BaseEntity
{
    public string FileName { get; set; }
    public string OriginalFileName { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    public string FilePath { get; set; }
    public DocumentType DocumentType { get; set; }

    // Foreign keys
    public Guid? TenderId { get; set; }
    public Guid? BidId { get; set; }

    // Navigation properties
    public virtual Tender Tender { get; set; }
    public virtual Bid Bid { get; set; }
}
