using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaperShop.BackPaper.DataAccess.Models;

public class PaperProperty
{
    [Key]
    [Column(Order = 0)]
    public int PaperId { get; set; }
    [ForeignKey("PaperId")]
    public Paper Paper { get; set; } = null!;
    
    [Key]
    [Column(Order = 1)]
    public int PropertyId { get; set; }
    [ForeignKey("PropertyId")]
    public Property Property { get; set; } = null!;
}