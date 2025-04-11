
using BiddingManagementSystem.Domain.Entities;

namespace BiddingManagementSystem.Domain.Repositories;

public interface ITenderRepository : IAsyncRepository<Tender>
{
    Task<IReadOnlyList<Tender>> GetTendersWithCategoryAsync();

    Task<Tender> GetTenderWithDetailsAsync(Guid id);
    Task<IReadOnlyList<Tender>> GetOpenTendersAsync();
}
