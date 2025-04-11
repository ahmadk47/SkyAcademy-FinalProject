
using AutoMapper;
using BiddingManagementSystem.Application.DTOs.TenderDtos;
using BiddingManagementSystem.Application.Interfaces;
using BiddingManagementSystem.Domain.Enums;
using BiddingManagementSystem.Domain.Repositories;
using MediatR;

namespace BiddingManagementSystem.Application.Features.Tenders.Queries;

public class GetTenderListQueryHandler : IRequestHandler<GetTenderListQuery, List<TenderDto>>
{
    private readonly ITenderRepository _tenderRepository;
    private readonly IMapper _mapper;

    public GetTenderListQueryHandler(ITenderRepository tenderRepository, IMapper mapper)
    {
        _tenderRepository = tenderRepository;
        _mapper = mapper;
    }

    public async Task<List<TenderDto>> Handle(GetTenderListQuery request, CancellationToken cancellationToken)
    {
        var tenders = await _tenderRepository.GetTendersWithCategoryAsync();

        // Apply filters
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            tenders = tenders.Where(t =>
                t.Title.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                t.ReferenceNumber.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (request.CategoryId.HasValue)
        {
            tenders = tenders.Where(t => t.CategoryId == request.CategoryId.Value).ToList();
        }

        if (request.OnlyOpenTenders.HasValue && request.OnlyOpenTenders.Value)
        {
            tenders = tenders.Where(t =>
                t.Status == TenderStatus.Published &&
                t.ClosingDate > DateTime.UtcNow).ToList();
        }

        return _mapper.Map<List<TenderDto>>(tenders);
    }
}
