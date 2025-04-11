using AutoMapper;
using BiddingManagementSystem.Application.DTOs.TenderDtos;
using BiddingManagementSystem.Domain.Repositories;
using MediatR;

namespace BiddingManagementSystem.Application.Features.Tenders.Queries;

public class GetTenderByIdQueryHandler : IRequestHandler<GetTenderByIdQuery, TenderDto?>
{
    private readonly ITenderRepository _tenderRepository;
    private readonly IMapper _mapper;

    public GetTenderByIdQueryHandler(ITenderRepository tenderRepository, IMapper mapper)
    {
        _tenderRepository = tenderRepository;
        _mapper = mapper;
    }

    public async Task<TenderDto?> Handle(GetTenderByIdQuery request, CancellationToken cancellationToken)
    {
        var tender = await _tenderRepository.GetTenderWithDetailsAsync(request.Id);

        if (tender == null)
            return null;

        return _mapper.Map<TenderDto>(tender);
    }
}
