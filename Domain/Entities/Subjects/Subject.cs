using Domain.Common;
using Domain.Entities.Classes;
using Domain.Entities.Lessones;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Subjects;

public class Subject : BaseAuditableEntity
{
    public string Name { get; set; }

    [ForeignKey("Class")]
    public int ClassId { get; set; }
    public Class Class { get; set; }
    public List<Lesson>  Lesson { get; set; }
}
