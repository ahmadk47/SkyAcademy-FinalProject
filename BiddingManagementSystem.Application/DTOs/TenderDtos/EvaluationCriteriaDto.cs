
namespace BiddingManagementSystem.Application.DTOs.TenderDtos;
public class EvaluationCriteriaDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Weight { get; set; }
}