using Application.Common.Mappings.Commons;
using Application.Dto.CommonDtos;
using Domain.Entities.Rooms;
using Domain.Subjects;

namespace Application.Dto.Rooms;

public class GetRoomDto : BaseDto, IMapFrom<Room>
{
    public string Name { get; set; }
    public int Size { get; set; }
}
