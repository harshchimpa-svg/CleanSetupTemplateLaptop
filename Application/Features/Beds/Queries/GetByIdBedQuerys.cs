using Application.Dto.Beds;
using Application.Dto.Chairs;
using Application.Features.Chairs.Queries;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Beds;
using MediatR;
using Shared;

namespace Application.Features.Beds.Queries;

public class GetByIdBedQuerys : IRequest<Result<GetBedDto>>
{
    public int Id { get; set; }

    public GetByIdBedQuerys(int id)
    {
        Id = id;
    }
}
internal class GetByIdBedQuerysHandler : IRequestHandler<GetByIdBedQuerys, Result<GetBedDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdBedQuerysHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GetBedDto>> Handle(GetByIdBedQuerys request, CancellationToken cancellationToken)
    {
        var Chair = await _unitOfWork.Repository<Bed>().GetByID(request.Id);

        if (Chair == null)
        {
            return Result<GetBedDto>.BadRequest("Chair not found.");
        }

        var mapData = _mapper.Map<GetBedDto>(Chair);

        return Result<GetBedDto>.Success(mapData, "Chair");
    }
}