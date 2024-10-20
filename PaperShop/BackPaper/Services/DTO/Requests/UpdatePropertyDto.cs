using System.ComponentModel.DataAnnotations;

namespace PaperShop.BackPaper.Services.DTO.Requests;

public class UpdatePropertyDto
{
    [StringLength(255, ErrorMessage = "Property name cannot exceed 255 characters.")]
    public string? PropertyName { get; set; }
}