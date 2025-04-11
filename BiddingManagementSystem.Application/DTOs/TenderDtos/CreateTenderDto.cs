using BiddingManagementSystem.Domain.Enums;

namespace BiddingManagementSystem.Application.DTOs.TenderDtos;

public class CreateTenderDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string IssuedBy { get; set; }
    public DateTime ClosingDate { get; set; }
    public TenderType TenderType { get; set; }
    public decimal? BudgetRange { get; set; }
    public string ContactEmail { get; set; }
    public string EligibilityCriteria { get; set; }
    public string PaymentTerms { get; set; }
    public Guid CategoryId { get; set; }
    public List<CreateEvaluationCriteriaDto> EvaluationCriteria { get; set; }
}
