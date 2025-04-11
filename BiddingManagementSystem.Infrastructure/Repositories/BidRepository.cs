using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BiddingManagementSystem.Domain.Entities;
using BiddingManagementSystem.Domain.Repositories;
using BiddingManagementSystem.Infrastructure.Persistence;

namespace BiddingManagementSystem.Infrastructure.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly ApplicationDbContext _context;

        public BidRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bid?> GetByIdAsync(Guid id)
        {
            return await _context.Bids.FindAsync(id);
        }

        public async Task UpdateAsync(Bid bid)
        {
            _context.Bids.Update(bid);
            await _context.SaveChangesAsync();
        }

        Task<Bid> IAsyncRepository<Bid>.AddAsync(Bid entity)
        {
            throw new NotImplementedException();
        }

        Task<int> IAsyncRepository<Bid>.CountAsync(Expression<Func<Bid, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task IAsyncRepository<Bid>.DeleteAsync(Bid entity)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Bid>> IBidRepository.GetBidsByBidderIdAsync(string bidderId)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Bid>> IBidRepository.GetBidsByTenderIdAsync(Guid tenderId)
        {
            throw new NotImplementedException();
        }

        Task<Bid> IBidRepository.GetBidWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Bid>> IAsyncRepository<Bid>.ListAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<Bid>> IAsyncRepository<Bid>.ListAsync(Expression<Func<Bid, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
