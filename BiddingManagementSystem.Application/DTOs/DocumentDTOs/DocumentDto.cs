
using BiddingManagementSystem.Domain.Enums;

namespace BiddingManagementSystem.Application.DTOs.DocumentDTOs;

public class DocumentDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string OriginalFileName { get; set; }
    public string ContentType { get; set; }
    public long FileSize { get; set; }
    public DocumentType DocumentType { get; set; }
    public DateTime UploadDate { get; set; }
}
