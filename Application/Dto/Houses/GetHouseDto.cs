using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Houses;

namespace Application.Dto.Houses;

public class GetHouseDto : BaseDto, IMapFrom<House>
{
    public string Name { get; set; }

}
