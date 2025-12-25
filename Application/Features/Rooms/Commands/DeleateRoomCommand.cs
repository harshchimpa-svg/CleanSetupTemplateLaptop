using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Rooms;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Rooms.Commands;

public class DeleateRoomCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateRoomCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateRoomCommandHandler : IRequestHandler<DeleateRoomCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateRoomCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateRoomCommand request, CancellationToken cancellationToken)
    {
        var RoomExists = await _unitOfWork.Repository<Room>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!RoomExists)
        {
            return Result<bool>.BadRequest("Room not found.");
        }

        await _unitOfWork.Repository<Room>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Room deleted successfully.");
    }
}