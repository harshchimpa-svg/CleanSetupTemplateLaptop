using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Application.Dto.Rooms;
using Domain.Common.Enums.BedTypes;
using Domain.Entities.Beds;
using Domain.Entities.Rooms;

namespace Application.Dto.Beds;

public class GetBedDto : BaseDto, IMapFrom<Bed>
{
    public int RoomId { get; set; }
    public GetRoomDto Room { get; set; }
    public BedType BedType { get; set; }
}
