using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Rooms.Commands;

public class UpdateRoomCommand : IRequest<Result<Room>>
{

    public int Id { get; set; }
    public CreateRoomCommand CreateCommand { get; set; } = new();

    public UpdateRoomCommand(int id, CreateRoomCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Result<Room>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateRoomCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Room>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {

        var Room = await _unitOfWork.Repository<Room>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Room == null)
        {
            return Result<Room>.BadRequest("Room id not found");
        }

        _mapper.Map(request.CreateCommand, Room);

        await _unitOfWork.Repository<Room>().UpdateAsync(Room);
        await _unitOfWork.Save(cancellationToken);

        return Result<Room>.Success("Update Room...");
    }
}