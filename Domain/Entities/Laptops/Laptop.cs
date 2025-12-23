using Domain.Common;
using Domain.Entities.Rams;

namespace Domain.Entities.Laptops;

public class Laptop : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public Ram Ram { get; set; }
}
