using Application.Dto.Lessones;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Lessones;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Subjects.Queryes;

public class GetLessonQuery : IRequest<Result<List<GetLessonDto>>>
{
}
internal class GetLessonQueryHandler : IRequestHandler<GetLessonQuery, Result<List<GetLessonDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetLessonQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetLessonDto>>> Handle(GetLessonQuery request, CancellationToken cancellationToken)

    {
        var query = _unitOfWork.Repository<Lesson>().Entities.Include(s => s.Subject).ThenInclude(x => x.Class)
             .ToList();

        var map = _mapper.Map<List<GetLessonDto>>(query);

        return Result<List<GetLessonDto>>.Success(map, "Lesson list");
    }
}