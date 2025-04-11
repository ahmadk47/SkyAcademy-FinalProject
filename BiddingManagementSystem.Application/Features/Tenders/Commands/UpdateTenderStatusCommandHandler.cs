
using BiddingManagementSystem.Domain.Enums;
using BiddingManagementSystem.Domain.Repositories;
using MediatR;
using BiddingManagementSystem.Application.Interfaces;
namespace BiddingManagementSystem.Application.Features.Tenders.Commands;

public class UpdateTenderStatusCommandHandler : IRequestHandler<UpdateTenderStatusCommand, bool>
{
    private readonly ITenderRepository _tenderRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTenderStatusCommandHandler(
        ITenderRepository tenderRepository,
        ICurrentUserService currentUserService)
    {
        _tenderRepository = tenderRepository;
        _currentUserService = currentUserService;
    }

    public async Task<bool> Handle(UpdateTenderStatusCommand request, CancellationToken cancellationToken)
    {
        var tender = await _tenderRepository.GetByIdAsync(request.TenderId);

        if (tender == null)
            throw new Exception($"Tender with ID {request.TenderId} not found");

        // Check if the status transition is valid
        ValidateStatusTransition(tender.Status, request.NewStatus);

        tender.Status = request.NewStatus;
        tender.LastModifiedAt = DateTime.UtcNow;
        tender.LastModifiedBy = _currentUserService.UserId;

        await _tenderRepository.UpdateAsync(tender);

        return true;
    }

    private void ValidateStatusTransition(TenderStatus currentStatus, TenderStatus newStatus)
    {
        // Define valid status transitions
        bool isValidTransition = (currentStatus, newStatus) switch
        {
            (TenderStatus.Draft, TenderStatus.Published) => true,
            (TenderStatus.Published, TenderStatus.UnderEvaluation) => true,
            (TenderStatus.UnderEvaluation, TenderStatus.Awarded) => true,
            (TenderStatus.UnderEvaluation, TenderStatus.Closed) => true,
            (TenderStatus.Published, TenderStatus.Canceled) => true,
            (TenderStatus.Published, TenderStatus.Closed) => true,
            (TenderStatus.Awarded, TenderStatus.Closed) => true,
            _ => false
        };

        if (!isValidTransition)
        {
            throw new InvalidOperationException($"Invalid status transition from {currentStatus} to {newStatus}");
        }
    }
}
