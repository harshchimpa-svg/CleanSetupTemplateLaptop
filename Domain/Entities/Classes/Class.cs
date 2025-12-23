using Domain.Common;
using Domain.Entities.Lessones;
using Domain.Subjects;

namespace Domain.Entities.Classes;

public class Class : BaseAuditableEntity
{
    public string Name { get; set; }
    public List<Subject> Subject { get; set; }

}
