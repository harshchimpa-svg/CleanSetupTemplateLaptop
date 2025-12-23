using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Houses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Houses.Commands;

public class UpdateHouseCommands : IRequest<Result<House>>
{

    public int Id { get; set; }
    public CreateHouseCommands CreateCommand { get; set; } = new();

    public UpdateHouseCommands(int id, CreateHouseCommands createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateHouseCommandsHandler : IRequestHandler<UpdateHouseCommands, Result<House>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHouseCommandsHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<House>> Handle(UpdateHouseCommands request, CancellationToken cancellationToken)
    {

        var House = await _unitOfWork.Repository<House>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (House == null)
        {
            return Result<House>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, House);

        await _unitOfWork.Repository<House>().UpdateAsync(House);
        await _unitOfWork.Save(cancellationToken);

        return Result<House>.Success("Update House...");
    }
}