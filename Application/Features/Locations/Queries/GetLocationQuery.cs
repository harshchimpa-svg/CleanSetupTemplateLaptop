using Application.Dto.Locations;
using Application.Interfaces.UnitOfWorkRepositories;
using AutoMapper;
using Domain.Locations;
using MediatR;
using Shared;

namespace Application.Features.Locations.Queries;

public class GetLocationQuery : IRequest<Result<List<GetLocationDto>>>
{
}

internal class GetLocationQueryHandler : IRequestHandler<GetLocationQuery, Result<List<GetLocationDto>>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetLocationQueryHandler(IUnitOfWork locationRepository, IMapper mapper)
    {
        _unitOfWork = locationRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetLocationDto>>> Handle(GetLocationQuery request, CancellationToken cancellationToken)
    {
        var locations = await _unitOfWork.Repository<Location>().GetAll();

        var map = _mapper.Map<List<GetLocationDto>>(locations);

        return Result<List<GetLocationDto>>.Success(map, "Location list");
    }
}
