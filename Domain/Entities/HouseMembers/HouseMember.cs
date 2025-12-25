using Domain.Common;
using Domain.Entities.Houses;
using Domain.Entities.Memberes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.HouseMembers;

public class HouseMember : BaseAuditableEntity
{
    [ForeignKey("House")]
    public int HouseId { get; set; }
    public House House { get; set; }

    [ForeignKey("Member")]
    public int MemberId { get; set; }
    public Member Member { get; set; }
}
