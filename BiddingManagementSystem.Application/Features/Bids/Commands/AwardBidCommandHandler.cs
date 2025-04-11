using BiddingManagementSystem.Domain.Enums;
using BiddingManagementSystem.Domain.Repositories;
using MediatR;
using BiddingManagementSystem.Application.Interfaces;

namespace BiddingManagementSystem.Application.Features.Bids.Commands;

public class AwardBidCommandHandler : IRequestHandler<AwardBidCommand, bool>
{
    private readonly IBidRepository _bidRepository;
    private readonly ITenderRepository _tenderRepository;
    private readonly ICurrentUserService _currentUserService;

    public AwardBidCommandHandler(
        IBidRepository bidRepository,
        ITenderRepository tenderRepository,
        ICurrentUserService currentUserService)
    {
        _bidRepository = bidRepository;
        _tenderRepository = tenderRepository;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(AwardBidCommand request, CancellationToken cancellationToken)
    {
        var bid = await _bidRepository.GetByIdAsync(request.BidId);

        if (bid == null)
            throw new Exception($"Bid with ID {request.BidId} not found");

        var tender = await _tenderRepository.GetByIdAsync(bid.TenderId);

        // Check if tender is in evaluation stage
        if (tender.Status != TenderStatus.UnderEvaluation)
            throw new Exception("Bids can only be awarded when tender is in evaluation stage");

        // Update bid status
        bid.Status = BidStatus.Awarded;
        bid.LastModifiedAt = DateTime.UtcNow;
        bid.LastModifiedBy = _currentUserService.UserId;

        await _bidRepository.UpdateAsync(bid);

        // Update tender status and awarded bid
        tender.Status = TenderStatus.Awarded;
        tender.AwardedBidId = bid.Id;
        tender.LastModifiedAt = DateTime.UtcNow;
        tender.LastModifiedBy = _currentUserService.UserId;

        await _tenderRepository.UpdateAsync(tender);

        // Update other bids for this tender
        var otherBids = await _bidRepository.GetBidsByTenderIdAsync(tender.Id);
        foreach (var otherBid in otherBids)
        {
            if (otherBid.Id != bid.Id && otherBid.Status == BidStatus.UnderEvaluation)
            {
                otherBid.Status = BidStatus.Rejected;
                otherBid.LastModifiedAt = DateTime.UtcNow;
                otherBid.LastModifiedBy = _currentUserService.UserId;

                await _bidRepository.UpdateAsync(otherBid);
            }
        }

        return true;
    }
}
