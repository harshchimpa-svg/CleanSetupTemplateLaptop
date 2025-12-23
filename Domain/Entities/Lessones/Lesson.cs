using Domain.Common;
using Domain.Entities.Classes;
using Domain.Subjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Lessones;

public class Lesson : BaseAuditableEntity
{
    public string Name { get; set; }

    [ForeignKey("Subject")]
    public int SubjectId { get; set; }
    public Subject Subject { get; set; }
}
