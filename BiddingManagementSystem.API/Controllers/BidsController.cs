using BiddingManagementSystem.Application.DTOs.BidsDTOs;
using BiddingManagementSystem.Application.Features.Bids.Commands;
using BiddingManagementSystem.Application.Interfaces;
using BiddingManagementSystem.Domain.Repositories;
using BiddingManagementSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BiddingManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BidsController : ControllerBase
{
    private readonly IBidRepository _bidRepository;
    private readonly ITenderRepository _tenderRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDocumentRepository _documentRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IUserService _userService;

    public BidsController(
        IBidRepository bidRepository,
        ITenderRepository tenderRepository,
        ICurrentUserService currentUserService,
        IDocumentRepository documentRepository,
        IFileStorageService fileStorageService,
        IUserService userService)
    {
        _bidRepository = bidRepository;
        _tenderRepository = tenderRepository;
        _currentUserService = currentUserService;
        _documentRepository = documentRepository;
        _fileStorageService= fileStorageService;
        _userService = userService;
    }

   

    // POST: api/Bids
    [HttpPost]
    [Authorize(Roles = "Bidder")]
    public async Task<ActionResult<BidDto>> CreateBid([FromForm] CreateBidCommand command)
    {
        // If there's a document being uploaded
        if (command.Documents != null && command.Documents.Count > 0)
        {
            foreach (var file in command.Documents)
            {
                var filePath = await _fileStorageService.SaveFileAsync(file);
                // You can handle file and store the file paths here, if needed.
            }
        }

        var handler = new CreateBidCommandHandler (_bidRepository, _tenderRepository, _documentRepository, _fileStorageService, _currentUserService, _userService);
        var result = await handler.Handle(command, default);

        return CreatedAtAction(nameof(EvaluateBid), new { id = result }, result);

    }

    // POST: api/Bids/Award
    [HttpPost("Award")]
    [Authorize(Roles = "ProcurementOfficer")]
    public async Task<IActionResult> AwardBid([FromBody] AwardBidCommand command)
    {
        var handler = new AwardBidCommandHandler(_bidRepository, _tenderRepository, _currentUserService);
        var result = await handler.Handle(command, default);

        if (!result)
        {
            return BadRequest("Failed to award the bid");
        }

        return NoContent();
    }

    // POST: api/Bids/Submit
    [HttpPost("Submit")]
    [Authorize(Roles = "Bidder")]
    public async Task<IActionResult> SubmitBid([FromBody] SubmitBidCommand command)
    {
        var handler = new SubmitBidCommandHandler(_bidRepository, _tenderRepository, _currentUserService);
        var result = await handler.Handle(command, default);

        if (!result)
        {
            return BadRequest("Failed to submit the bid");
        }

        return NoContent();
    }

    // POST: api/Bids/Evaluate
    [HttpPost("Evaluate")]
    [Authorize(Roles = "ProcurementOfficer")]
    public async Task<IActionResult> EvaluateBid([FromBody] EvaluateBidCommand command)
    {
        var handler = new EvaluateBidCommandHandler(_bidRepository, _tenderRepository, _currentUserService);
        var result = await handler.Handle(command, default);

        if (!result)
        {
            return BadRequest("Failed to evaluate the bid");
        }

        return NoContent();
    }
}
