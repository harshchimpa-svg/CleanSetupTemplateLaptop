using Application.Features.Chairs.Commands;
using Application.Features.Rooms.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Beds;
using Domain.Entities.Chairs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Beds.Commands;

public class UpdateBedCommands : IRequest<Result<Bed>>
{
        
    public int Id { get; set; }
    public CreateBedCommands CreateCommand { get; set; } = new();

    public UpdateBedCommands(int id, CreateBedCommands createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateBedCommandsHandler : IRequestHandler<UpdateBedCommands, Result<Bed>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBedCommandsHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Bed>> Handle(UpdateBedCommands request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.RoomId.HasValue)
        {
            var parent = await _unitOfWork.Repository<Chair>().GetByID(request.CreateCommand.RoomId.Value);

            if (parent == null)
            {
                return Result<Bed>.BadRequest("HouseId is not exist.");
            }
        }

        var Bed = await _unitOfWork.Repository<Bed>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Bed == null)
        {
            return Result<Bed>.BadRequest("Chair id not found");
        }

        _mapper.Map(request.CreateCommand, Bed);

        await _unitOfWork.Repository<Bed>().UpdateAsync(Bed);
        await _unitOfWork.Save(cancellationToken);

        return Result<Bed>.Success("Update Bed...");
    }
}