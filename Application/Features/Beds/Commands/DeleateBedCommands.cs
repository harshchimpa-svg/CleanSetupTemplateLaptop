using Application.Features.Chairs.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Beds;
using Domain.Entities.Chairs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Beds.Commands;

public class DeleateBedCommands : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateBedCommands(int id)
    {
        Id = id;
    }
}
internal class DeleateBedCommandsHandler : IRequestHandler<DeleateBedCommands, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateBedCommandsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateBedCommands request, CancellationToken cancellationToken)
    {
        var ChairExists = await _unitOfWork.Repository<Bed>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!ChairExists)
        {
            return Result<bool>.BadRequest("Chair not found.");
        }

        await _unitOfWork.Repository<Bed>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Chair deleted successfully.");
    }
}