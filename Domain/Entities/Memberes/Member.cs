using Domain.Common;
using Domain.Entities.Houses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Memberes;

public class Member : BaseAuditableEntity
{
    public string Name { get; set; }
    public int PhoneNumber { get; set; }

    [ForeignKey("House")]
    public int HouseId { get; set; }
    public House House { get; set; }
}
