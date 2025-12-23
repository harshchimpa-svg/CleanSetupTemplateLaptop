using Application.Dto.Houses;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Entities.Houses;
using MediatR;
using Shared;

namespace Application.Features.Houses.Queries;

public class GetHouseQuery : IRequest<Result<List<GetHouseDto>>>
{
}
internal class GetHouseQueryHandler : IRequestHandler<GetHouseQuery, Result<List<GetHouseDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetHouseQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<GetHouseDto>>> Handle(GetHouseQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<House>().GetAll();

        var map = _mapper.Map<List<GetHouseDto>>(locations);

        return Result<List<GetHouseDto>>.Success(map, "House list");
    }
}