
using BiddingManagementSystem.Domain.Entities;

namespace BiddingManagementSystem.Domain.Repositories;

public interface IBidRepository : IAsyncRepository<Bid>
{
    Task<IReadOnlyList<Bid>> GetBidsByTenderIdAsync(Guid tenderId);
    Task<Bid> GetBidWithDetailsAsync(Guid id);
    Task<IReadOnlyList<Bid>> GetBidsByBidderIdAsync(string bidderId);
}
