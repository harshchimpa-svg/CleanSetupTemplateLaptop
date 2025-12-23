using Application.Common.Mappings.Commons;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Lessones;
using MediatR;
using Shared;

namespace Application.Features.Subjects;

public class CreateLessonCommand : IRequest<Result<string>>, ICreateMapFrom<Lesson>
{
    public string Name { get; set; }
    public int SubjectId { get; set; }
}
internal class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateLessonCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = _mapper.Map<Lesson>(request);

        await _unitOfWork.Repository<Lesson>().AddAsync(lesson);
        await _unitOfWork.Save(cancellationToken);

        return Result<string>.Success("Lesson created successfully.");
    }
}