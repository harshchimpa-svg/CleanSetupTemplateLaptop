using Application.Dto.Lessones;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Lessones;
using MediatR;
using Shared;

namespace Application.Features.Subjects.Queryes;

public class GetByIdLessonQuery : IRequest<Result<GetLessonDto>>
{
    public int Id { get; set; }

    public GetByIdLessonQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdLessonQueryHandler : IRequestHandler<GetByIdLessonQuery, Result<GetLessonDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdLessonQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetLessonDto>> Handle(GetByIdLessonQuery request, CancellationToken cancellationToken)
    {
        var lesson = await _unitOfWork.Repository<Lesson>().GetByID(request.Id);

        if (lesson == null)
        {
            return Result<GetLessonDto>.BadRequest("Lesson not found.");
        }

        var mapData = _mapper.Map<GetLessonDto>(lesson);

        return Result<GetLessonDto>.Success(mapData, "Lesson");
    }
}