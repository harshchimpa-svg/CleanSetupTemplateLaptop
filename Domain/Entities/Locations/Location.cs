using Domain.Common;
using Domain.Locations.LocationTypes;

namespace Domain.Locations;

public class Location : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int? ParentId { get; set; }
    public Location Parent { get; set; }
    public LocationType? LocationType { get; set; }
}
