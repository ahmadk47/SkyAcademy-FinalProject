
using MediatR;

namespace BiddingManagementSystem.Application.Features.Bids.Commands;

public class SubmitBidCommand : IRequest<bool>
{
    public Guid BidId { get; set; }
}
