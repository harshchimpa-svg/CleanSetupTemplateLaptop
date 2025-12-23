using Domain.Common;
using Domain.Entities.ApplicationUsers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserAddresses;

public class UserAddress : BaseAuditableEntity
{
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }

    [ForeignKey("City")]
    public int? CityId { get; set; }

    [ForeignKey("State")]
    public int? StateId { get; set; }

    [ForeignKey("Country")]
    public int? CountryId { get; set; }

    public int? PinCode { get; set; }
}
