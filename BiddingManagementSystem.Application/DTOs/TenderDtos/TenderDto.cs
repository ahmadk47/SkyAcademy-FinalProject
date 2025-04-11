
using BiddingManagementSystem.Domain.Enums;
using BiddingManagementSystem.Application.DTOs.DocumentDTOs;
namespace BiddingManagementSystem.Application.DTOs.TenderDtos;

public class TenderDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ReferenceNumber { get; set; }
    public string Description { get; set; }
    public string IssuedBy { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ClosingDate { get; set; }
    public TenderType TenderType { get; set; }
    public decimal? BudgetRange { get; set; }
    public string ContactEmail { get; set; }
    public TenderStatus Status { get; set; }
    public string CategoryName { get; set; }
    public Guid CategoryId { get; set; }
    public string CreatorId { get; set; }
    public string CreatorName { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<DocumentDto> Documents { get; set; }
    public List<EvaluationCriteriaDto> EvaluationCriteria { get; set; }
    public int BidsCount { get; set; }
    public Guid? AwardedBidId { get; set; }
}
