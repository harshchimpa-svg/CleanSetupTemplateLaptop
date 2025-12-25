using Domain.Common;
using Domain.Common.Enums.BedTypes;
using Domain.Entities.Rooms;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Beds;

public class Bed : BaseAuditableEntity
{
    [ForeignKey("Room")]
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public BedType BedType { get; set; }
}
