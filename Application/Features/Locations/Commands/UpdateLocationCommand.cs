using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Locations.Commands;

public class UpdateLocationCommand : IRequest<Result<Location>>
{

    public int Id { get; set; } 
    public CreateLocationCommand CreateCommand { get; set; } = new();

    public UpdateLocationCommand(int id, CreateLocationCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Result<Location>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLocationCommandHandler(IMapper mapper, IUnitOfWork LocationRepository)
    {
        _mapper = mapper;
        _unitOfWork = LocationRepository;
    }

    public async Task<Result<Location>> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        if (request.CreateCommand.ParentId.HasValue)
        {
            var parent = await _unitOfWork.Repository<Location>().GetByID(request.CreateCommand.ParentId.Value);

            if (parent == null)
            {
                return Result<Location>.BadRequest("Parent Id is not exist.");
            }
        }

        var location = await _unitOfWork.Repository<Location>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (location == null)
        {
            return Result<Location>.BadRequest("Sorry id not found");
        }

        _mapper.Map(request.CreateCommand, location);

        await _unitOfWork.Repository<Location>().UpdateAsync(location);
        await _unitOfWork.Save(cancellationToken);

        return Result<Location>.Success("Update location...");
    }

}

