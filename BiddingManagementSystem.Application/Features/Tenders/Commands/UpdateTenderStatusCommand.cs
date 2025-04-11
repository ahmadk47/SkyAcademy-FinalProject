using BiddingManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BiddingManagementSystem.Application.Features.Tenders.Commands;

public class UpdateTenderStatusCommand : IRequest<bool>
{
    public Guid TenderId { get; set; }
    public TenderStatus NewStatus { get; set; }

    // These are needed for handling file uploads
    public IFormFile? DocumentFile { get; set; }
    public string? DocumentPath { get; set; }
}
