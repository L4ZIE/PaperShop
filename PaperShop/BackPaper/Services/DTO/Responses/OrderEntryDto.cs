using PaperShop.BackPaper.DataAccess.Models;

namespace PaperShop.BackPaper.Services.DTO.Responses;

public class OrderEntryDto
{
    public int Id { get; set; } 
    public int Quantity { get; set; }  
    public int ProductId { get; set; }  
    public int? OrderId { get; set; } 
    public PaperDto? Product { get; set; }
    
    public static OrderEntryDto FromEntity(OrderEntry entry)
    {
        return new OrderEntryDto
        {
            Id = entry.Id,
            Quantity = entry.Quantity,
            ProductId = entry.ProductId,
            OrderId = entry.OrderId,
            Product = PaperDto.FromEntity(entry.Product) 
        };
    }
}