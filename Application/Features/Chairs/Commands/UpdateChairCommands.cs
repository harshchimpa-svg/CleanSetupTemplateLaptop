using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Chairs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Chairs.Commands;

public class UpdateChairCommands : IRequest<Result<Chair>>
{

    public int Id { get; set; }
    public CreateChairCommands CreateCommand { get; set; } = new();

    public UpdateChairCommands(int id, CreateChairCommands createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateChairCommandsHandler : IRequestHandler<UpdateChairCommands, Result<Chair>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateChairCommandsHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Chair>> Handle(UpdateChairCommands request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.HouseId.HasValue)
        {
            var parent = await _unitOfWork.Repository<Chair>().GetByID(request.CreateCommand.HouseId.Value);

            if (parent == null)
            {
                return Result<Chair>.BadRequest("HouseId is not exist.");
            }
        }

        var Chair = await _unitOfWork.Repository<Chair>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Chair == null)
        {
            return Result<Chair>.BadRequest("Chair id not found");
        }

        _mapper.Map(request.CreateCommand, Chair);

        await _unitOfWork.Repository<Chair>().UpdateAsync(Chair);
        await _unitOfWork.Save(cancellationToken);

        return Result<Chair>.Success("Update Chair...");
    }
}