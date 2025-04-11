
using AutoMapper;
using BiddingManagementSystem.Domain.Entities;
using BiddingManagementSystem.Domain.Enums;
using BiddingManagementSystem.Domain.Repositories;
using MediatR;
using BiddingManagementSystem.Application.Interfaces;
namespace BiddingManagementSystem.Application.Features.Bids.Commands
{
    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, Guid>
    {
        private readonly IBidRepository _bidRepository;
        private readonly ITenderRepository _tenderRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;

        public CreateBidCommandHandler(
            IBidRepository bidRepository,
            ITenderRepository tenderRepository,
            IDocumentRepository documentRepository,
            IFileStorageService fileStorageService,
            ICurrentUserService currentUserService,
            IUserService userService)
        {
            _bidRepository = bidRepository;
            _tenderRepository = tenderRepository;
            _documentRepository = documentRepository;
            _fileStorageService = fileStorageService;
            _currentUserService = currentUserService;
            _userService = userService;
        }

        public async Task<Guid> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {
            var tender = await _tenderRepository.GetByIdAsync(request.BidDto.TenderId);

            if (tender == null)
                throw new Exception($"Tender with ID {request.BidDto.TenderId} not found");

            if (tender.Status != TenderStatus.Published)
                throw new Exception("Bids can only be submitted for published tenders");

            if (tender.ClosingDate < DateTime.UtcNow)
                throw new Exception("The tender bidding period has closed");

            var currentUserId = _currentUserService.UserId;
            var currentUser = await _userService.GetUserByIdAsync(currentUserId);

            var bid = new Bid
            {
                BidderCompanyName = currentUser.CompanyName,
                RegistrationNumber = currentUser.CompanyRegistrationNumber,
                BidderAddress = currentUser.Address,
                BidderEmail = currentUser.Email,
                BidderPhone = currentUser.PhoneNumber,
                TechnicalProposalSummary = request.BidDto.TechnicalProposalSummary,
                TotalBidAmount = request.BidDto.TotalBidAmount,
                Status = BidStatus.Draft,
                Declaration = request.BidDto.Declaration,
                TenderId = request.BidDto.TenderId,
                BidderId = currentUserId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = currentUserId,
                BidItems = new List<BidItem>()
            };

            // Add bid items
            if (request.BidDto.BidItems != null && request.BidDto.BidItems.Any())
            {
                foreach (var itemDto in request.BidDto.BidItems)
                {
                    bid.BidItems.Add(new BidItem
                    {
                        Description = itemDto.Description,
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.UnitPrice,
                        TotalPrice = itemDto.Quantity * itemDto.UnitPrice,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = currentUserId
                    });
                }
            }

            // Save bid to get ID
            bid = await _bidRepository.AddAsync(bid);

            // Process and save documents if any
            if (request.Documents != null && request.Documents.Count > 0)
            {
                foreach (var file in request.Documents)
                {
                    if (file.Length > 0)
                    {
                        // Determine document type
                        DocumentType documentType = DocumentType.BidDocument;
                        if (file.FileName.Contains("technical", StringComparison.OrdinalIgnoreCase))
                            documentType = DocumentType.TechnicalProposal;
                        else if (file.FileName.Contains("financial", StringComparison.OrdinalIgnoreCase))
                            documentType = DocumentType.FinancialProposal;
                        else if (file.FileName.Contains("registration", StringComparison.OrdinalIgnoreCase))
                            documentType = DocumentType.CompanyRegistration;
                        else if (file.FileName.Contains("tax", StringComparison.OrdinalIgnoreCase))
                            documentType = DocumentType.TaxComplianceCertificate;
                        else if (file.FileName.Contains("financial statement", StringComparison.OrdinalIgnoreCase))
                            documentType = DocumentType.FinancialStatement;

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
                            DocumentType = documentType,
                            BidId = bid.Id,
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = currentUserId
                        };

                        await _documentRepository.AddAsync(document);
                    }
                }
            }

            return bid.Id;
        }
    }
}
