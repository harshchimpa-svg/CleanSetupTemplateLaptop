using Application.Dto.Locations;
using Application.Dto.Rooms;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Rooms;
using MediatR;
using Shared;

namespace Application.Features.Rooms.Queryes;

public class GetRoomQuery : IRequest<Result<List<GetRoomDto>>>
{
}
internal class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, Result<List<GetRoomDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetRoomQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetRoomDto>>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<Room>().GetAll();

        var map = _mapper.Map<List<GetRoomDto>>(locations);

        return Result<List<GetRoomDto>>.Success(map, "Location list");
    }
}