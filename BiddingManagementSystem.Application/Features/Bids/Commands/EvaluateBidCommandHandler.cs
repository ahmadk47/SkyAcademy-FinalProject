using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiddingManagementSystem.Domain.Enums;
using BiddingManagementSystem.Domain.Repositories;
using MediatR;
using BiddingManagementSystem.Application.Interfaces;
namespace BiddingManagementSystem.Application.Features.Bids.Commands;
public class EvaluateBidCommandHandler : IRequestHandler<EvaluateBidCommand, bool>
{
    private readonly IBidRepository _bidRepository;
    private readonly ITenderRepository _tenderRepository;
    private readonly ICurrentUserService _currentUserService;

    public EvaluateBidCommandHandler(
        IBidRepository bidRepository,
        ITenderRepository tenderRepository,
        ICurrentUserService currentUserService)
    {
        _bidRepository = bidRepository;
        _tenderRepository = tenderRepository;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(EvaluateBidCommand request, CancellationToken cancellationToken)
    {
        var bid = await _bidRepository.GetByIdAsync(request.EvaluateBidDto.BidId);

        if (bid == null)
            throw new Exception($"Bid with ID {request.EvaluateBidDto.BidId} not found");

        var tender = await _tenderRepository.GetByIdAsync(bid.TenderId);

        // Check if tender is in evaluation stage
        if (tender.Status != TenderStatus.UnderEvaluation)
            throw new Exception("Bids can only be evaluated when tender is in evaluation stage");

        // Update bid with evaluation information
        bid.Status = BidStatus.UnderEvaluation;
        bid.EvaluationScore = request.EvaluateBidDto.EvaluationScore;
        bid.EvaluationComments = request.EvaluateBidDto.EvaluationComments;
        bid.LastModifiedAt = DateTime.UtcNow;
        bid.LastModifiedBy = _currentUserService.UserId;

        await _bidRepository.UpdateAsync(bid);

        return true;
    }
}