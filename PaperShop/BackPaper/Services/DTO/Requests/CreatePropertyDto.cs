using System.ComponentModel.DataAnnotations;

namespace PaperShop.BackPaper.Services.DTO.Requests;

public class CreatePropertyDto
{
    [Required(ErrorMessage = "Property name is required.")]
    [StringLength(255, ErrorMessage = "Property name cannot exceed 255 characters.")]
    public string PropertyName { get; set; } = null!;
}