using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiddingManagementSystem.Domain.Enums;

namespace BiddingManagementSystem.Application.DTOs.TenderDtos;

public class UpdateTenderDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ClosingDate { get; set; }
    public TenderType TenderType { get; set; }
    public decimal? BudgetRange { get; set; }
    public string ContactEmail { get; set; }
    public Guid CategoryId { get; set; }
}
