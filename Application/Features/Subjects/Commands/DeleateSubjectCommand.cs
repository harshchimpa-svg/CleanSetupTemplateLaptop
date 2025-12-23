using Application.Features.Locations.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Subjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Subjects.Commands;

public class DeleateSubjectCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateSubjectCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateSubjectCommandHandler : IRequestHandler<DeleateSubjectCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateSubjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateSubjectCommand request, CancellationToken cancellationToken)
    {
        var SubjectExists = await _unitOfWork.Repository<Subject>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!SubjectExists)
        {
            return Result<bool>.BadRequest("Subject not found.");
        }

        await _unitOfWork.Repository<Subject>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Subject deleted successfully.");
    }
}