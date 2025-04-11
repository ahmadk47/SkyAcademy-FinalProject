using BiddingManagementSystem.Application.DTOs.TenderDtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BiddingManagementSystem.Application.Features.Tenders.Commands;

public class CreateTenderCommand : IRequest<Guid>
{
    public CreateTenderDto TenderDto { get; set; }
    public List<IFormFile> Documents { get; set; }
    
    public IFormFile DocumentsFile { get; set; }
}
