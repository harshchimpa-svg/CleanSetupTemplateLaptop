using Application.Dto.Subjectes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Subjects;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.Subjects.Queryes;

public class GetSubjectQuery : IRequest<Result<List<GetSubjectDto>>>
{
}
internal class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, Result<List<GetSubjectDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetSubjectQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetSubjectDto>>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.Repository<Subject>().Entities.Include(s => s.Class)
        .Include(s => s.Lesson)
       .ToList();

        var map = _mapper.Map<List<GetSubjectDto>>(query);

        return Result<List<GetSubjectDto>>.Success(map, "Subject list");
    }
}