using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiddingManagementSystem.Domain.Enums;
using BiddingManagementSystem.Application.DTOs.DocumentDTOs;
namespace BiddingManagementSystem.Application.DTOs.BidsDTOs;

public class BidDto
{
    public Guid Id { get; set; }
    public string BidderCompanyName { get; set; }
    public string RegistrationNumber { get; set; }
    public string BidderAddress { get; set; }
    public string BidderEmail { get; set; }
    public string BidderPhone { get; set; }
    public string TechnicalProposalSummary { get; set; }
    public decimal TotalBidAmount { get; set; }
    public BidStatus Status { get; set; }
    public DateTime SubmissionDate { get; set; }
    public decimal? EvaluationScore { get; set; }
    public string EvaluationComments { get; set; }
    public string Declaration { get; set; }
    public Guid TenderId { get; set; }
    public string TenderTitle { get; set; }
    public string TenderReferenceNumber { get; set; }
    public string BidderId { get; set; }
    public List<BidItemDto> BidItems { get; set; }
    public List<DocumentDto> Documents { get; set; }
}