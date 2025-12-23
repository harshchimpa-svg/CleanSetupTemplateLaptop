using Application.Features.Subjects.Commands;
using Application.Interfaces.UnitOfWorkRepositories;
using Domain.Entities.Lessones;
using Domain.Subjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Subjects;

public class DeleateLessonCommand : IRequest<Result<bool>>
{
    public int Id { get; set; }
    public DeleateLessonCommand(int id)
    {
        Id = id;
    }
}
internal class DeleateLessonCommandHandler : IRequestHandler<DeleateLessonCommand, Result<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleateLessonCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleateLessonCommand request, CancellationToken cancellationToken)
    {
        var LessonExists = await _unitOfWork.Repository<Subject>().Entities
                              .AnyAsync(x => x.Id == request.Id);

        if (!LessonExists)
        {
            return Result<bool>.BadRequest("Lesson not found.");
        }

        await _unitOfWork.Repository<Lesson>().DeleteAsync(request.Id);
        await _unitOfWork.Save(cancellationToken);

        return Result<bool>.Success(true, "Lesson deleted successfully.");
    }
}