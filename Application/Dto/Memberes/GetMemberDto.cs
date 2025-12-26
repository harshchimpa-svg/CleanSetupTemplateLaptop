using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.Houses;
using Domain.Entities.Houses;
using Domain.Entities.Memberes;

namespace Application.Dto.Memberes;

public class GetMemberDto : BaseDto, IMapFrom<Member>
{
    public string Name { get; set; }
    public int PhoneNumber { get; set; }
    public int HouseId { get; set; }
    public GetHouseDto House { get; set; }
}
