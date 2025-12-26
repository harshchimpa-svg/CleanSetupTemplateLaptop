using Domain.Common;
using Domain.Entities.Memberes;

namespace Domain.Entities.Houses;

public class House : BaseAuditableEntity
{
    public string Name { get; set; }
    public Member Member { get; set; }
}
