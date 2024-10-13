using PaperShop.BackPaper.DataAccess.Models;

namespace PaperShop.BackPaper.Services.DTO.Requests;

public class CreatePaperDto
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int Stock { get; set; }
    public bool Discontinued { get; set; }
    
    public Paper ToEntity()
    {
        return new Paper
        {
            Name = this.Name,
            Price = this.Price,
            Stock = this.Stock,
            Discontinued = this.Discontinued
        };
    }
}