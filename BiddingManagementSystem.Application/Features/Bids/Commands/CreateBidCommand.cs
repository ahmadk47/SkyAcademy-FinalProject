
using BiddingManagementSystem.Application.DTOs.BidsDTOs;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BiddingManagementSystem.Application.Features.Bids.Commands;

public class CreateBidCommand : IRequest<Guid>
{
    public CreateBidDto BidDto { get; set; }
    public IFormFileCollection Documents { get; set; }
}
