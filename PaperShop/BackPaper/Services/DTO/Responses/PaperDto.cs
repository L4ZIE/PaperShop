using PaperShop.BackPaper.DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace PaperShop.BackPaper.Services.DTO.Responses;

public class PaperDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public bool Discontinued { get; set; }
    public int Stock { get; set; }
    public List<PropertyDto> Properties { get; set; } = new List<PropertyDto>();
    
    public static PaperDto FromEntity(Paper paper)
    {
        if (paper == null) throw new ArgumentNullException(nameof(paper)); 

        return new PaperDto
        {
            Id = paper.Id,
            Name = paper.Name,
            Price = paper.Price,
            Stock = paper.Stock,
            Discontinued = paper.Discontinued,
            Properties = paper.PaperProperties.Select(pp => new PropertyDto 
            {
                Id = pp.Property.Id, 
                PropertyName = pp.Property.PropertyName 
            }).ToList()
        };
    }
}