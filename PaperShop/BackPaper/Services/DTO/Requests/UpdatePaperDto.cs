namespace PaperShop.BackPaper.Services.DTO.Requests;

public class UpdatePaperDto
{
    public string? Name { get; set; }
    public double? Price { get; set; }
    public int? Stock { get; set; }
    public bool? Discontinued { get; set; }
}