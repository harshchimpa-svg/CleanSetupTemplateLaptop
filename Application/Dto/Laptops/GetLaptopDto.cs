using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.Rams;
using Domain.Entities.Laptops;

namespace Application.Dto.Laptops;

public class GetLaptopDto : BaseDto, IMapFrom<Laptop>
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public decimal Storage { get; set; }
    public GetRamDto Ram { get; set; }

}
