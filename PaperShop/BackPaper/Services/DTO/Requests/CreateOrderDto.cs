using PaperShop.BackPaper.DataAccess.Models;

namespace PaperShop.BackPaper.Services.DTO.Requests;

public class CreateOrderDto
{
    public DateOnly? DeliveryDate { get; set; }
    public double TotalAmount { get; set; }
    public int? CustomerId { get; set; }
    public List<CreateOrderEntryDto> OrderEntries { get; set; } = new List<CreateOrderEntryDto>();
    
    public Order ToEntity()
    {
        return new Order
        {
            DeliveryDate = this.DeliveryDate,
            TotalAmount = this.TotalAmount,
            CustomerId = this.CustomerId,
            OrderDate = DateTime.UtcNow,
            OrderEntries = this.OrderEntries.Select(entry => new OrderEntry
            {
                Quantity = entry.Quantity,
                ProductId = entry.ProductId
                
            }).ToList()
        };
    }
}