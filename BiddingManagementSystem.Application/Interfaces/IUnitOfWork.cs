
using BiddingManagementSystem.Domain.Common;

namespace BiddingManagementSystem.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> CompleteAsync();
}
