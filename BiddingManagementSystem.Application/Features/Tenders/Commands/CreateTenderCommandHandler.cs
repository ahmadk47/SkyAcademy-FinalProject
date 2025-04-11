
using AutoMapper;
using BiddingManagementSystem.Domain.Entities;
using BiddingManagementSystem.Domain.Enums;
using BiddingManagementSystem.Domain.Repositories;
using MediatR;
using BiddingManagementSystem.Application.Interfaces;
namespace BiddingManagementSystem.Application.Features.Tenders.Commands;

public class CreateTenderCommandHandler : IRequestHandler<CreateTenderCommand, Guid>
{
    private readonly ITenderRepository _tenderRepository;
    private readonly IDocumentRepository _documentRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly ICurrentUserService _currentUserService;

    public CreateTenderCommandHandler(
        ITenderRepository tenderRepository,
        IDocumentRepository documentRepository,
        IFileStorageService fileStorageService,
        ICurrentUserService currentUserService)
    {
        _tenderRepository = tenderRepository;
        _documentRepository = documentRepository;
        _fileStorageService = fileStorageService;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateTenderCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId;

        // Generate a unique reference number
        var referenceNumber = $"TDR-{DateTime.UtcNow.ToString("yyyyMMdd")}-{Guid.NewGuid().ToString().Substring(0, 8)}";

        var tender = new Tender
        {
            Title = request.TenderDto.Title,
            ReferenceNumber = referenceNumber,
            Description = request.TenderDto.Description,
            IssuedBy = request.TenderDto.IssuedBy,
            IssueDate = DateTime.UtcNow,
            ClosingDate = request.TenderDto.ClosingDate,
            TenderType = request.TenderDto.TenderType,
            BudgetRange = request.TenderDto.BudgetRange,
            ContactEmail = request.TenderDto.ContactEmail,
            EligibilityCriteria = request.TenderDto.EligibilityCriteria,
            Status = TenderStatus.Draft,
            CategoryId = request.TenderDto.CategoryId,
            CreatorId = currentUserId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = currentUserId,
            EvaluationCriteria = new List<EvaluationCriteria>()
        };

        // Add evaluation criteria
        if (request.TenderDto.EvaluationCriteria != null && request.TenderDto.EvaluationCriteria.Any())
        {
            foreach (var criteriaDto in request.TenderDto.EvaluationCriteria)
            {
                tender.EvaluationCriteria.Add(new EvaluationCriteria
                {
                    Name = criteriaDto.Name,
                    Description = criteriaDto.Description,
                    Weight = criteriaDto.Weight,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = currentUserId
                });
            }
        }

        // Save tender to get ID
        tender = await _tenderRepository.AddAsync(tender);

        // Process and save documents if any
        if (request.Documents != null && request.Documents.Count > 0)
        {
            foreach (var file in request.Documents)
            {
                if (file.Length > 0)
                {
                    // Save file to storage
                    var fileName = await _fileStorageService.SaveFileAsync(file);

                    // Create document record
                    var document = new Document
                    {
                        FileName = fileName,
                        OriginalFileName = file.FileName,
                        ContentType = file.ContentType,
                        FileSize = file.Length,
                        FilePath = fileName,
                        DocumentType = DocumentType.TenderDocument,
                        TenderId = tender.Id,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = currentUserId
                    };

                    await _documentRepository.AddAsync(document);
                }
            }
        }

        return tender.Id;
    }
}
