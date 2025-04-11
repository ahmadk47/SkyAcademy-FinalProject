
using BiddingManagementSystem.Application.DTOs.TenderDtos;
using MediatR;

namespace BiddingManagementSystem.Application.Features.Tenders.Queries;

public class GetTenderByIdQuery(Guid id) : IRequest<TenderDto>
{
    public Guid Id { get; set; } = id;
}
