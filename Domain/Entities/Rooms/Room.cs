using Domain.Common;

namespace Domain.Entities.Rooms;

public class Room : BaseAuditableEntity
{
    public string Name { get; set; }
    public int Size { get; set; }
} 
