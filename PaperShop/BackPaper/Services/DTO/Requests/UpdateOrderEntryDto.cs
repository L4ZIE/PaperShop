namespace PaperShop.BackPaper.Services.DTO.Requests;

public class UpdateOrderEntryDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
}