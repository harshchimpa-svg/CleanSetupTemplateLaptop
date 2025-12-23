using Application.Common.Mappings.Commons;
using Domain.Entities.UserAddresses;

namespace Application.Dto.Users;

public class GetUserAddressDto : IMapFrom<UserAddress>
{
    public string Address1 { get; set; }
    public string? Address2 { get; set; }
    public int? CityId { get; set; }
    public int? StateId { get; set; }
    public int? CountryId { get; set; }
    public int? PinCode { get; set; }
}
