using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BiddingManagementSystem.Domain.Entities;
using BiddingManagementSystem.Domain.Repositories;

namespace BiddingManagementSystem.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    Task<Document> IAsyncRepository<Document>.AddAsync(Document entity)
    {
        throw new NotImplementedException();
    }

    Task<int> IAsyncRepository<Document>.CountAsync(Expression<Func<Document, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    Task IAsyncRepository<Document>.DeleteAsync(Document entity)
    {
        throw new NotImplementedException();
    }

    Task<IReadOnlyList<Document>> IDocumentRepository.GetBidDocumentsAsync(Guid bidId)
    {
        throw new NotImplementedException();
    }

    Task<Document> IAsyncRepository<Document>.GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<IReadOnlyList<Document>> IDocumentRepository.GetTenderDocumentsAsync(Guid tenderId)
    {
        throw new NotImplementedException();
    }

    Task<IReadOnlyList<Document>> IAsyncRepository<Document>.ListAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<IReadOnlyList<Document>> IAsyncRepository<Document>.ListAsync(Expression<Func<Document, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    Task IAsyncRepository<Document>.UpdateAsync(Document entity)
    {
        throw new NotImplementedException();
    }
}
