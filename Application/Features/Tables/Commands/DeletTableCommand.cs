using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Tables;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Tables.Commands;

public class DeletTableCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeletTableCommand(int id)
    {
        Id = id;
    }
}
internal class DeletTableCommandHandler : IRequestHandler<DeletTableCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletTableCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeletTableCommand request, CancellationToken cancellationToken)
    {
        var Table = await _unitOfWork.Repository<Table>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!Table)
        {
            return Result<bool>.BadRequest("table not found.");
        }

        await _unitOfWork.Repository<Table>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "table deleted successfully.");
    }
}
