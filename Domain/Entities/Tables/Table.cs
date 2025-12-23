using Domain.Common;

namespace Domain.Entities.Tables;

public class Table : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public int? ParentId { get; set; }
    public Table Parent { get; set; }
}
