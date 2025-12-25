using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Memberes;

namespace Application.Dto.Memberes;

public class GetMemberDto : BaseDto, IMapFrom<Member>
{
    public string Name { get; set; }
    public int PhoneNumber { get; set; }
}
