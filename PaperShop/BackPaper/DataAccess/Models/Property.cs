using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaperShop.BackPaper.DataAccess.Models;

[Table("properties")]
public partial class Property
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("property_name")]
    [StringLength(255)]
    public string PropertyName { get; set; } = null!;

    
    [InverseProperty("Property")]
    public virtual ICollection<PaperProperty> PaperProperties { get; set; } = new List<PaperProperty>();
}