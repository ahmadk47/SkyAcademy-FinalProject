
using BiddingManagementSystem.Application.DTOs.TenderDtos;
using MediatR;

namespace BiddingManagementSystem.Application.Features.Tenders.Queries;

public class GetTenderListQuery : IRequest<List<TenderDto>>
{
    // Optional filter parameters
    public string SearchTerm { get; set; }
    public Guid? CategoryId { get; set; }
    public bool? OnlyOpenTenders { get; set; }
}
