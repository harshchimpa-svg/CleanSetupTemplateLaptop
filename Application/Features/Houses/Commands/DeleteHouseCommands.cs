using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Houses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Houses.Commands;

public class DeleteHouseCommands : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleteHouseCommands(int id)
    {
        Id = id;
    }
}
internal class DeleteHouseCommandsHandler : IRequestHandler<DeleteHouseCommands, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHouseCommandsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteHouseCommands request, CancellationToken cancellationToken)
    {
        var locationExists = await _unitOfWork.Repository<House>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!locationExists)
        {
            return Result<bool>.BadRequest("House not found.");
        }

        await _unitOfWork.Repository<House>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "House deleted successfully.");
    }
}