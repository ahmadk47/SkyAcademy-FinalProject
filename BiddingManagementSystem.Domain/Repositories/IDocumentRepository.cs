
using BiddingManagementSystem.Domain.Entities;
namespace BiddingManagementSystem.Domain.Repositories;
public interface IDocumentRepository : IAsyncRepository<Document>
{
    Task<IReadOnlyList<Document>> GetTenderDocumentsAsync(Guid tenderId);
    Task<IReadOnlyList<Document>> GetBidDocumentsAsync(Guid bidId);
}
