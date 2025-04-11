using BiddingManagementSystem.Domain.Enums;
using BiddingManagementSystem.Domain.Repositories;
using MediatR;
using BiddingManagementSystem.Application.Interfaces;
namespace BiddingManagementSystem.Application.Features.Bids.Commands;

public class SubmitBidCommandHandler : IRequestHandler<SubmitBidCommand, bool>
{
    private readonly IBidRepository _bidRepository;
    private readonly ITenderRepository _tenderRepository;
    private readonly ICurrentUserService _currentUserService;

    public SubmitBidCommandHandler(
        IBidRepository bidRepository,
        ITenderRepository tenderRepository,
        ICurrentUserService currentUserService)
    {
        _bidRepository = bidRepository;
        _tenderRepository = tenderRepository;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(SubmitBidCommand request, CancellationToken cancellationToken)
    {
        var bid = await _bidRepository.GetByIdAsync(request.BidId);

        if (bid == null)
            throw new Exception($"Bid with ID {request.BidId} not found");

        // Ensure the bid belongs to the current user
        var currentUserId = _currentUserService.UserId;
        if (bid.BidderId != currentUserId)
            throw new UnauthorizedAccessException("You are not authorized to submit this bid");

        // Check if the tender is still open
        var tender = await _tenderRepository.GetByIdAsync(bid.TenderId);
        if (tender.Status != TenderStatus.Published)
            throw new Exception("Bids can only be submitted for published tenders");

        if (tender.ClosingDate < DateTime.UtcNow)
            throw new Exception("The tender bidding period has closed");

        // Update bid status and submission date
        bid.Status = BidStatus.Submitted;
        bid.SubmissionDate = DateTime.UtcNow;
        bid.LastModifiedAt = DateTime.UtcNow;
        bid.LastModifiedBy = currentUserId;

        await _bidRepository.UpdateAsync(bid);

        return true;
    }
}
