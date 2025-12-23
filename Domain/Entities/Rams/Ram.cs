using Domain.Common;
using Domain.Entities.Laptops;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Rams;

public class Ram : BaseAuditableEntity
{
    public int Id { get; set; }

    [ForeignKey("Laptop")]
    public int LaptopId { get; set; }
    public Laptop Laptop { get; set; }
    public string Name { get; set; }
    public decimal Storage {  get; set; }
}
