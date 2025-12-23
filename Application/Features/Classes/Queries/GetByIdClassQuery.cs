using Application.Dto.Classes;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Classes;
using MediatR;
using Shared;

namespace Application.Features.Classes.Queries;

public class GetByIdClassQuery : IRequest<Result<GetClassDto>>
{
    public int Id { get; set; }

    public GetByIdClassQuery(int id)
    {
        Id = id;
    }
}
internal class GetByIdClassQueryHandler : IRequestHandler<GetByIdClassQuery, Result<GetClassDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdClassQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetClassDto>> Handle(GetByIdClassQuery request, CancellationToken cancellationToken)
    {
        var Class = await _unitOfWork.Repository<Class>().GetByID(request.Id);

        if (Class == null)
        {
            return Result<GetClassDto>.BadRequest("Class not found.");
        }

        var mapData = _mapper.Map<GetClassDto>(Class);

        return Result<GetClassDto>.Success(mapData, "Class");
    }
}