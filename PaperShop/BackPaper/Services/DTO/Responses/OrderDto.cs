using PaperShop.BackPaper.DataAccess.Models;

namespace PaperShop.BackPaper.Services.DTO.Responses;

public class OrderDto
{
    public int Id { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public double TotalAmount { get; set; }
    public int? CustomerId { get; set; }
    public string Status { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    public List<OrderEntryDto> OrderEntries { get; set; } = new List<OrderEntryDto>();
    
    public static OrderDto FromEntity(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,  
            DeliveryDate = order.DeliveryDate,
            TotalAmount = order.TotalAmount,
            CustomerId = order.CustomerId,
            Status = order.Status,  
            OrderDate = order.OrderDate,
            OrderEntries = order.OrderEntries.Select(entry => new OrderEntryDto()
            {
                Id = entry.Id,
                Quantity = entry.Quantity,
                ProductId = entry.ProductId,
                OrderId = entry.OrderId,
                Product = PaperDto.FromEntity(entry.Product) 
            }).ToList()
        };
    }
}