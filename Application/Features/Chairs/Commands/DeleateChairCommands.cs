using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Chairs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Chairs.Commands;

public class DeleateChairCommands : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateChairCommands(int id)
    {
        Id = id;
    }
}
internal class DeleateChairCommandsHandler : IRequestHandler<DeleateChairCommands, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateChairCommandsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateChairCommands request, CancellationToken cancellationToken)
    {
        var ChairExists = await _unitOfWork.Repository<Chair>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!ChairExists)
        {
            return Result<bool>.BadRequest("Chair not found.");
        }

        await _unitOfWork.Repository<Chair>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Chair deleted successfully.");
    }
}


