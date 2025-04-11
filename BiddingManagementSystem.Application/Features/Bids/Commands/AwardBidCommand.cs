
using MediatR;

namespace BiddingManagementSystem.Application.Features.Bids.Commands
{
    public class AwardBidCommand : IRequest<bool>
    {
        public Guid BidId { get; set; }
    }
}
