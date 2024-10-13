namespace PaperShop.BackPaper.Services.DTO.Requests;

public class UpdateOrderDto
{
    public DateOnly? DeliveryDate { get; set; }
    public double? TotalAmount { get; set; }
    public int? CustomerId { get; set; }
    public string? Status { get; set; }
    public List<UpdateOrderEntryDto> OrderEntries { get; set; } = new List<UpdateOrderEntryDto>();
}