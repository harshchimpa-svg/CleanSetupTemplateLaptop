using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.Houses;
using Application.Dto.Memberes;
using Domain.Entities.HouseMembers;

namespace Application.Dto.HouseMembers;

public class GetHouseMemberDto : BaseDto, IMapFrom<HouseMember>
{
    public int HouseId { get; set; }
    public GetHouseDto House { get; set; }
    public int MemberId { get; set; }
    public GetMemberDto Member { get; set; }
}
