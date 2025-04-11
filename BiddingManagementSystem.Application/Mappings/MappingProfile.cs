
using AutoMapper;
using BiddingManagementSystem.Application.DTOs.BidsDTOs;
using BiddingManagementSystem.Application.DTOs.DocumentDTOs;
using BiddingManagementSystem.Application.DTOs.TenderDtos;
using BiddingManagementSystem.Domain.Entities;

namespace BiddingManagementSystem.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Tender mappings
        CreateMap<Tender, TenderDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => $"{src.Creator.FirstName} {src.Creator.LastName}"))
            .ForMember(dest => dest.BidsCount, opt => opt.MapFrom(src => src.Bids != null ? src.Bids.Count : 0));

        // Document mappings
        CreateMap<Document, DocumentDto>()
            .ForMember(dest => dest.UploadDate, opt => opt.MapFrom(src => src.CreatedAt));

        // Category mappings
        CreateMap<TenderCategory, TenderCategoryDto>();

        // Evaluation Criteria mappings
        CreateMap<EvaluationCriteria, EvaluationCriteriaDto>();

        // Bid mappings
        CreateMap<Bid, BidDto>()
            .ForMember(dest => dest.TenderTitle, opt => opt.MapFrom(src => src.Tender.Title))
            .ForMember(dest => dest.TenderReferenceNumber, opt => opt.MapFrom(src => src.Tender.ReferenceNumber));

        // Bid Item mappings
        CreateMap<BidItem, BidItemDto>();
    }
}
