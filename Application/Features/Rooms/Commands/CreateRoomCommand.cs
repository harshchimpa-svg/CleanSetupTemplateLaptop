using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Rooms;
using MediatR;
using Shared;

namespace Application.Features.Rooms.Commands;

public class CreateRoomCommand : IRequest<Result<string>>, ICreateMapFrom<Room>
{
    public string Name { get; set; }
    public int Size { get; set; }

}
internal class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var Room = _mapper.Map<Room>(request);

        await _unitOfWork.Repository<Room>().AddAsync(Room);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Room created successfully.");
    }
}