using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Locations.Commands;

public class DeleteLocationCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteLocationCommand(int id)
    {
        Id = id;
    }
}

internal class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteLocationCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<Location>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("Location not found.");
        }

        await _unitOfWork.Repository<Location>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Location deleted successfully.");
    }
}

