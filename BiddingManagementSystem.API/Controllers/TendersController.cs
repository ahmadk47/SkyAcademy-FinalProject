using AutoMapper;
using BiddingManagementSystem.Application.DTOs.TenderDtos;
using BiddingManagementSystem.Application.Features.Tenders.Commands;
using BiddingManagementSystem.Application.Features.Tenders.Queries;
using BiddingManagementSystem.Application.Interfaces;
using BiddingManagementSystem.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiddingManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TendersController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IMediator _mediator;

    public TendersController(
        IFileStorageService fileStorageService,
        IMediator mediator)
    {
        _fileStorageService = fileStorageService;
        _mediator = mediator;
    }

    // GET: api/Tenders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TenderDto>>> GetTenders([FromQuery] GetTenderListQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET: api/Tenders/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<TenderDto>> GetTender(Guid id)
    {
        var result = await _mediator.Send(new GetTenderByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // POST: api/Tenders
    [HttpPost]
    [Authorize(Roles = "ProcurementOfficer")]
    public async Task<ActionResult<Guid>> CreateTender([FromForm] CreateTenderCommand command)
    {
        // Handle file uploads before sending the command to the handler
        if (command.Documents != null && command.Documents.Any())
        {
            List<IFormFile> uploadedFiles = new List<IFormFile>();

            foreach (IFormFile file in command.Documents)
            {
                var filePath = await _fileStorageService.SaveFileAsync(file);
                uploadedFiles.Add(file);
            }

            command.Documents = uploadedFiles;
        }

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetTender), new { id = result }, result);
    }

    // PUT: api/Tenders/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "ProcurementOfficer")]
    public async Task<IActionResult> UpdateTender(Guid id, [FromForm] UpdateTenderStatusCommand command)
    {
        if (id != command.TenderId)
            return BadRequest("Mismatched tender ID");

        if (command.DocumentFile != null)
        {
            var path = await _fileStorageService.SaveFileAsync(command.DocumentFile);
            command.DocumentPath = path;
        }

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
