using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Lessones;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Subjects;

public class UpdateLessonCommand : IRequest<Result<Lesson>>
{
    public int Id { get; set; }
    public CreateLessonCommand CreateCommand { get; set; } = new();

    public UpdateLessonCommand(int id, CreateLessonCommand createCommand)
    {
        Id = id;
        CreateCommand = createCommand;
    }
}
internal class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, Result<Lesson>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLessonCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Lesson>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {

        var Lesson = await _unitOfWork.Repository<Lesson>().Entities.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Lesson == null)
        {
            return Result<Lesson>.BadRequest("Lesson id not found");
        }

        _mapper.Map(request.CreateCommand, Lesson);

        await _unitOfWork.Repository<Lesson>().UpdateAsync(Lesson);
        await _unitOfWork.Save(cancellationToken);

        return Result<Lesson>.Success("Update Lesson...");
    }
}