using Domain.Common;
using Domain.Common.Enums.ChairLegTypes;
using Domain.Entities.Houses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Chairs;

public class Chair : BaseAuditableEntity
{
    [ForeignKey("House")]
    public int? HouseId { get; set; }
    public House House { get; set; }
    public string Name { get; set; }
    public ChairLegType ChairLegType { get; set; }
}
