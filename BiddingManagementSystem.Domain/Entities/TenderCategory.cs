
using BiddingManagementSystem.Domain.Common;

namespace BiddingManagementSystem.Domain.Entities;

public class TenderCategory : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    // Navigation properties
    public virtual ICollection<Tender> Tenders { get; set; }
}
