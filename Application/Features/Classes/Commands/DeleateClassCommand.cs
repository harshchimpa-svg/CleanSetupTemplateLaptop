using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Classes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Classes.Commands;

public class DeleateClassCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateClassCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateClassCommandHandler : IRequestHandler<DeleateClassCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateClassCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateClassCommand request, CancellationToken cancellationToken)
    {
        var ClassExists = await _unitOfWork.Repository<Class>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!ClassExists)
        {
            return Result<bool>.BadRequest("Class not found.");
        }

        await _unitOfWork.Repository<Class>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Class deleted successfully.");
    }
}