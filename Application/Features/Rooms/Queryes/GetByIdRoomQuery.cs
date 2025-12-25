using Application.Dto.Rooms;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Rooms;
using MediatR;
using Shared;

namespace Application.Features.Rooms.Queryes;

public class GetByIdRoomQuery : IRequest<Result<GetRoomDto>>
{
    public int Id { get; set; }

    public GetByIdRoomQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdRoomQueryHandler : IRequestHandler<GetByIdRoomQuery, Result<GetRoomDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdRoomQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetRoomDto>> Handle(GetByIdRoomQuery request, CancellationToken cancellationToken)
    {
        var subject = await _unitOfWork.Repository<Room>().GetByID(request.Id);

        if (subject == null)
        {
            return Result<GetRoomDto>.BadRequest("Room not found.");
        }

        var mapData = _mapper.Map<GetRoomDto>(subject);

        return Result<GetRoomDto>.Success(mapData, "Room");
    }
}