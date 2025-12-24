using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.Houses;
using Domain.Common.Enums.ChairLegTypes;
using Domain.Entities.Chairs;
using Domain.Entities.Houses;
using Domain.Entities.Laptops;

namespace Application.Dto.Chairs;

public class GetChairDto : BaseDto, IMapFrom<Chair>
{
    public int HouseId { get; set; }
    public GetHouseDto House { get; set; }
    public string Name { get; set; }
    public ChairLegType ChairLegType { get; set; }
}
