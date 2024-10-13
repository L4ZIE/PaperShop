
using Property = PaperShop.BackPaper.DataAccess.Models.Property;

namespace PaperShop.BackPaper.Services.DTO.Responses;

public class PropertyDto
{
    public int Id { get; set; }
    public string PropertyName { get; set; } = null!;

    public static PropertyDto FromEntity(Property property)
    {
        return new PropertyDto
        {
            Id = property.Id,
            PropertyName = property.PropertyName
        };
    }
}