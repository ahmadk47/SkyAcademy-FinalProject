
using BiddingManagementSystem.Application.DTOs.BidsDTOs;
using MediatR;

namespace BiddingManagementSystem.Application.Features.Bids.Commands
{
    public class EvaluateBidCommand : IRequest<bool>
    {
        public EvaluateBidDto EvaluateBidDto { get; set; }
    }
}
