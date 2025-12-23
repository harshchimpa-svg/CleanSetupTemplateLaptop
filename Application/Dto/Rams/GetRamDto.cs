using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Laptops;
using Domain.Entities.Rams;

namespace Application.Dto.Rams;

public class GetRamDto : BaseDto, IMapFrom<Ram>
{
    public decimal Storage { get; set; }

}
