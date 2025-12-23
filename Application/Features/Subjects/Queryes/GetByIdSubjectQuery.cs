using Application.Dto.Locations;
using Application.Dto.Subjectes;
using Application.Features.Countries.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Subjects;
using MediatR;
using Shared;

namespace Application.Features.Subjects.Queryes;

public class GetByIdSubjectQuery : IRequest<Result<GetSubjectDto>>
{
    public int Id { get; set; }

    public GetByIdSubjectQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdSubjectQueryHandler : IRequestHandler<GetByIdSubjectQuery, Result<GetSubjectDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdSubjectQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetSubjectDto>> Handle(GetByIdSubjectQuery request, CancellationToken cancellationToken)
    {
        var subject = await _unitOfWork.Repository<Subject>().GetByID(request.Id);

        if (subject == null)
        {
            return Result<GetSubjectDto>.BadRequest("Subject not found.");
        }

        var mapData = _mapper.Map<GetSubjectDto>(subject);

        return Result<GetSubjectDto>.Success(mapData, "Subject");
    }
}